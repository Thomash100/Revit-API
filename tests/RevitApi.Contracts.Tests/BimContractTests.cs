using RevitApi.Contracts.Bim;
using RevitApi.Contracts.Export;
using RevitApi.Contracts.Json;

namespace RevitApi.Contracts.Tests;

public sealed class BimContractTests
{
    [Fact]
    public void Model_element_supports_multidisciplinary_classification_and_parameters()
    {
        var element = new ModelElement
        {
            ElementId = "4711",
            UniqueId = "revit-unique-id",
            Discipline = Discipline.Architecture,
            SourceApplication = SourceApplication.Revit,
            Classification = new ElementClassification
            {
                ClassificationSystem = "DIN276",
                Code = "330",
                Name = "Aussenwaende",
                Discipline = Discipline.Architecture
            },
            Category = "Walls",
            Family = "Basic Wall",
            TypeName = "STB 200",
            ParameterSets =
            [
                new ParameterSet
                {
                    Name = "Revit",
                    Discipline = Discipline.Architecture,
                    SourceApplication = SourceApplication.Revit,
                    Values =
                    {
                        ["FireRating"] = new ParameterValue
                        {
                            Name = "FireRating",
                            Value = "F90",
                            DataType = "string",
                            SourceApplication = SourceApplication.Revit
                        }
                    }
                }
            ]
        };

        Assert.Equal(Discipline.Architecture, element.Discipline);
        Assert.Equal("DIN276", element.Classification?.ClassificationSystem);
        Assert.Equal("F90", element.ParameterSets[0].Values["FireRating"].Value);
    }

    [Fact]
    public void Bim_exchange_document_serializes_disciplines_issues_and_parameters()
    {
        var document = new BimExchangeDocument
        {
            Project = new TgaProjectInfo
            {
                Name = "Multidisziplinar",
                RevitVersion = "2026",
                ExportDate = new DateTimeOffset(2026, 6, 6, 10, 0, 0, TimeSpan.Zero)
            },
            Disciplines =
            [
                Discipline.Architecture,
                Discipline.Structural,
                Discipline.TechnicalBuildingEquipment,
                Discipline.Electrical,
                Discipline.MSR,
                Discipline.FireProtection,
                Discipline.Landscape
            ],
            Elements =
            [
                new ModelElement
                {
                    ElementId = "100",
                    UniqueId = "arch-wall-unique-id",
                    Discipline = Discipline.Architecture,
                    SourceApplication = SourceApplication.Revit,
                    Category = "Walls",
                    Family = "Basic Wall",
                    TypeName = "STB 200"
                }
            ],
            ValidationIssues =
            [
                new ValidationIssue
                {
                    UniqueId = "arch-wall-unique-id",
                    Discipline = Discipline.FireProtection,
                    SourceApplication = SourceApplication.ExternalValidation,
                    RuleCode = "BS-001",
                    Severity = "warning",
                    Status = "open",
                    Message = "Brandschutzanforderung pruefen.",
                    ProposedWriteBack =
                    {
                        ["SM_Pruefstatus"] = "Warnung"
                    }
                }
            ],
            CoordinationIssues =
            [
                new CoordinationIssue
                {
                    Id = "coord-001",
                    Title = "Kollision Trasse Wand",
                    Status = "open",
                    Severity = "warning",
                    SourceApplication = SourceApplication.Navisworks,
                    Disciplines =
                    [
                        Discipline.Architecture,
                        Discipline.TechnicalBuildingEquipment
                    ],
                    RelatedUniqueIds =
                    [
                        "arch-wall-unique-id"
                    ]
                }
            ]
        };

        var json = RevitApiJsonSerializer.Serialize(document);

        Assert.Contains("\"disciplines\":", json);
        Assert.Contains("\"architecture\"", json);
        Assert.Contains("\"technicalBuildingEquipment\"", json);
        Assert.Contains("\"msr\"", json);
        Assert.Contains("\"sourceApplication\": \"externalValidation\"", json);
        Assert.Contains("\"coordinationIssues\":", json);
        Assert.Contains("\"type\": \"STB 200\"", json);
    }
}
