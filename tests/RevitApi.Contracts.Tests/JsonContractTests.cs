using RevitApi.Contracts.Domain;
using RevitApi.Contracts.Export;
using RevitApi.Contracts.Json;
using RevitApi.Contracts.Validation;
using RevitApi.Contracts.WriteBack;

namespace RevitApi.Contracts.Tests;

public sealed class JsonContractTests
{
    [Fact]
    public void Export_document_serializes_schema_project_summary_and_unique_id()
    {
        var document = TgaExportDocument.Create(
            new TgaProjectInfo
            {
                Name = "Demo Projekt",
                RevitVersion = "2026",
                ExportDate = new DateTimeOffset(2026, 6, 6, 10, 0, 0, TimeSpan.Zero)
            },
            new TgaExportSummary
            {
                Rooms = 1,
                PlumbingFixtures = 1,
                MepElements = 1
            },
            new[]
            {
                new TgaRoomExport
                {
                    Number = "01.001",
                    Name = "Bad",
                    LevelName = "Level 01"
                }
            },
            new[]
            {
                new TgaElementExport
                {
                    ElementId = "12345",
                    UniqueId = "revit-unique-id",
                    Category = "Plumbing Fixtures",
                    Family = "WC",
                    TypeName = "Standard",
                    Trade = Trade.Sanitary,
                    PlanningStatus = PlanningStatus.Checked
                }
            });

        var json = RevitApiJsonSerializer.Serialize(document);

        Assert.Contains("\"schemaVersion\": 1", json);
        Assert.Contains("\"revitVersion\": \"2026\"", json);
        Assert.Contains("\"uniqueId\": \"revit-unique-id\"", json);
        Assert.Contains("\"trade\": \"sanitary\"", json);
        Assert.Contains("\"type\": \"Standard\"", json);
    }

    [Fact]
    public void Validation_import_preserves_writeback_map()
    {
        const string json = """
        {
          "results": [
            {
              "uniqueId": "revit-unique-id",
              "status": "warning",
              "sourceSystem": "IFC-Editor",
              "ruleCode": "HLS-001",
              "message": "Bauteil ohne eindeutige Systemzuordnung.",
              "writeBack": {
                "SM_Pruefstatus": "Warnung"
              }
            }
          ]
        }
        """;

        var import = RevitApiJsonSerializer.Deserialize<TgaValidationImport>(json);

        Assert.NotNull(import);
        var result = Assert.Single(import.Results);
        Assert.Equal("revit-unique-id", result.UniqueId);
        Assert.Equal("Warnung", result.WriteBack["SM_Pruefstatus"]);
    }

    [Fact]
    public void Writeback_request_serializes_parameter_instructions()
    {
        var request = new WriteBackRequest
        {
            CreatedAt = new DateTimeOffset(2026, 6, 6, 12, 0, 0, TimeSpan.Zero),
            Instructions =
            [
                new WriteBackInstruction
                {
                    UniqueId = "revit-unique-id",
                    SourceSystem = "IFC-Editor",
                    RuleCode = "HLS-001",
                    Parameters =
                    {
                        ["SM_Pruefstatus"] = "Warnung"
                    }
                }
            ]
        };

        var json = RevitApiJsonSerializer.Serialize(request);

        Assert.Contains("\"uniqueId\": \"revit-unique-id\"", json);
        Assert.Contains("\"parameters\":", json);
        Assert.Contains("\"SM_Pruefstatus\": \"Warnung\"", json);
    }
}
