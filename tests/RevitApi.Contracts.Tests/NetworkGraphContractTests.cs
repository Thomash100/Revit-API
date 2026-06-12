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
                    ]
                }
            ],
            NumberingPreviews =
            [
                new SchemaNumberingPreview
                {
                    Id = "preview-001",
                    NetworkGraphId = "graph-001",
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
                    ]
                }
            ]
        };

        var json = RevitApiJsonSerializer.Serialize(document);

        Assert.Contains("\"graphs\":", json);
        Assert.Contains("\"role\": \"startPoint\"", json);
        Assert.Contains("\"strategy\": \"startPointBased\"", json);
        Assert.Contains("\"parameterName\": \"TGA_SchemaStrangNr\"", json);
        Assert.Contains("\"previewValue\": \"S01\"", json);
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
    }

    [Fact]
    public void Network_graph_sample_contains_tga_schema_strangnr_preview_only()
    {
        var document = LoadSample();

        var preview = Assert.Single(document.NumberingPreviews);
        Assert.Equal("TGA_SchemaStrangNr", preview.ParameterName);
        Assert.Equal(SchemaNumberingStrategy.StartPointBased, preview.Strategy);
        Assert.All(preview.Assignments, assignment =>
        {
            Assert.False(string.IsNullOrWhiteSpace(assignment.UniqueId));
            Assert.False(string.IsNullOrWhiteSpace(assignment.ElementId));
            Assert.Equal("preview", assignment.Status);
            Assert.Equal("S01", assignment.PreviewValue);
            Assert.Equal(SourceApplication.Revit, assignment.SourceApplication);
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
