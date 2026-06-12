using RevitApi.Contracts.Bim;
using RevitApi.Contracts.Json;
using RevitApi.Contracts.Network;

namespace RevitApi.Contracts.Tests;

public sealed class NetworkGraphContractTests
{
    [Fact]
    public void Network_graph_document_serializes_startpoint_based_schema_preview()
    {
        var document = new NetworkGraphDocument
        {
            RuleSets =
            [
                new SchemaRuleSet
                {
                    Id = "tga-basic-schema-rules",
                    Name = "TGA basic schema preview rules",
                    StartPointRules =
                    [
                        new SchemaStartPointRule
                        {
                            Id = "start-mechanical-equipment",
                            Priority = 10,
                            Roles = [NetworkNodeRole.StartPoint],
                            CategoryPatterns = ["OST_MechanicalEquipment"]
                        }
                    ],
                    BranchRules =
                    [
                        new SchemaBranchRule
                        {
                            Id = "branch-at-unconnected-terminal",
                            Trigger = "unreachableNode",
                            StartsNewStrang = false
                        }
                    ]
                }
            ],
            Graphs =
            [
                new NetworkGraph
                {
                    Id = "graph-001",
                    Name = "Zuluft EG",
                    Discipline = Discipline.Ventilation,
                    SourceApplication = SourceApplication.Revit,
                    StartNodeIds = ["node-start"],
                    Nodes =
                    [
                        new NetworkNode
                        {
                            Id = "node-start",
                            ElementId = "100",
                            UniqueId = "uid-start",
                            Discipline = Discipline.Ventilation,
                            SourceApplication = SourceApplication.Revit,
                            Role = NetworkNodeRole.StartPoint,
                            Category = "OST_MechanicalEquipment"
                        }
                    ],
                    Edges =
                    [
                        new NetworkEdge
                        {
                            Id = "edge-001",
                            FromNodeId = "node-start",
                            ToNodeId = "node-terminal",
                            Kind = NetworkEdgeKind.Connector,
                            FlowDirection = NetworkFlowDirection.Supply,
                            DirectionSource = NetworkDirectionSource.RevitConnector,
                            IsDirected = true
                        }
                    ]
                }
            ],
            NumberingPreviews =
            [
                new SchemaNumberingPreview
                {
                    Id = "preview-001",
                    NetworkGraphId = "graph-001",
                    RuleSetId = "tga-basic-schema-rules",
                    Strategy = SchemaNumberingStrategy.StartPointBased,
                    StartNodeIds = ["node-start"],
                    Assignments =
                    [
                        new SchemaStrangAssignment
                        {
                            NodeId = "node-start",
                            ElementId = "100",
                            UniqueId = "uid-start",
                            StartNodeId = "node-start",
                            StrangIndex = 1,
                            PreviewValue = "S01",
                            SourceApplication = SourceApplication.Revit
                        }
                    ],
                    WriteBackCandidates =
                    [
                        new SchemaWriteBackCandidate
                        {
                            NodeId = "node-start",
                            ElementId = "100",
                            UniqueId = "uid-start",
                            ProposedValue = "S01",
                            RequiresApproval = true,
                            SourceApplication = SourceApplication.Revit
                        }
                    ]
                }
            ]
        };

        var json = RevitApiJsonSerializer.Serialize(document);

        Assert.Contains("\"graphs\":", json);
        Assert.Contains("\"ruleSetId\": \"tga-basic-schema-rules\"", json);
        Assert.Contains("\"role\": \"startPoint\"", json);
        Assert.Contains("\"flowDirection\": \"supply\"", json);
        Assert.Contains("\"directionSource\": \"revitConnector\"", json);
        Assert.Contains("\"strategy\": \"startPointBased\"", json);
        Assert.Contains("\"parameterName\": \"TGA_SchemaStrangNr\"", json);
        Assert.Contains("\"previewValue\": \"S01\"", json);
        Assert.Contains("\"writeBackCandidates\":", json);
        Assert.Contains("\"requiresApproval\": true", json);
    }

    [Fact]
    public void Network_graph_sample_can_be_deserialized()
    {
        var document = LoadSample();

        Assert.Equal(NetworkGraphDocument.CurrentSchemaVersion, document.SchemaVersion);
        Assert.Equal("Demo TGA Schema", document.Project.Name);

        var graph = Assert.Single(document.Graphs);
        Assert.Equal(Discipline.Ventilation, graph.Discipline);
        Assert.Equal(SourceApplication.Revit, graph.SourceApplication);
        Assert.Equal(3, graph.Nodes.Count);
        Assert.Equal(2, graph.Edges.Count);
        Assert.Contains("node-ahU-001", graph.StartNodeIds);

        Assert.All(graph.Edges, edge =>
        {
            Assert.Equal(NetworkFlowDirection.Supply, edge.FlowDirection);
            Assert.Equal(NetworkDirectionSource.RevitConnector, edge.DirectionSource);
            Assert.True(edge.IsDirected);
        });
    }

    [Fact]
    public void Network_graph_sample_contains_schema_rules_for_start_points_and_branching()
    {
        var document = LoadSample();

        var ruleSet = Assert.Single(document.RuleSets);
        Assert.Equal("tga-basic-schema-rules", ruleSet.Id);

        var startPointRule = Assert.Single(ruleSet.StartPointRules);
        Assert.Equal("start-mechanical-equipment", startPointRule.Id);
        Assert.Equal(10, startPointRule.Priority);
        Assert.Contains(NetworkNodeRole.StartPoint, startPointRule.Roles);
        Assert.Contains("OST_MechanicalEquipment", startPointRule.CategoryPatterns);

        var branchRule = Assert.Single(ruleSet.BranchRules);
        Assert.Equal("branch-at-unconnected-terminal", branchRule.Id);
        Assert.Equal("unreachableNode", branchRule.Trigger);
        Assert.False(branchRule.StartsNewStrang);
    }

    [Fact]
    public void Network_graph_sample_contains_tga_schema_strangnr_preview_only()
    {
        var document = LoadSample();

        var preview = Assert.Single(document.NumberingPreviews);
        Assert.Equal("TGA_SchemaStrangNr", preview.ParameterName);
        Assert.Equal("tga-basic-schema-rules", preview.RuleSetId);
        Assert.Equal(SchemaNumberingStrategy.StartPointBased, preview.Strategy);
        Assert.All(preview.Assignments, assignment =>
        {
            Assert.False(string.IsNullOrWhiteSpace(assignment.UniqueId));
            Assert.False(string.IsNullOrWhiteSpace(assignment.ElementId));
            Assert.Equal("preview", assignment.Status);
            Assert.Equal("S01", assignment.PreviewValue);
            Assert.Equal(SourceApplication.Revit, assignment.SourceApplication);
        });

        Assert.Equal(3, preview.WriteBackCandidates.Count);
        Assert.All(preview.WriteBackCandidates, candidate =>
        {
            Assert.Equal("TGA_SchemaStrangNr", candidate.ParameterName);
            Assert.Equal("S01", candidate.ProposedValue);
            Assert.Equal("candidate", candidate.Status);
            Assert.True(candidate.RequiresApproval);
            Assert.Equal(SourceApplication.Revit, candidate.SourceApplication);
        });

        Assert.Empty(document.ValidationIssues);
        Assert.Empty(document.CoordinationIssues);
    }

    private static NetworkGraphDocument LoadSample()
    {
        var json = File.ReadAllText(FindSamplePath());
        return RevitApiJsonSerializer.Deserialize<NetworkGraphDocument>(json)
            ?? throw new InvalidOperationException("Sample could not be deserialized.");
    }

    private static string FindSamplePath()
    {
        var directory = new DirectoryInfo(AppContext.BaseDirectory);

        while (directory is not null)
        {
            var candidate = Path.Combine(directory.FullName, "samples", "network-graph.schema-preview.sample.json");
            if (File.Exists(candidate))
            {
                return candidate;
            }

            directory = directory.Parent;
        }

        throw new FileNotFoundException("Could not find samples/network-graph.schema-preview.sample.json.");
    }
}
