using RevitApi.Contracts.Bim;
using RevitApi.Contracts.Json;

namespace RevitApi.Contracts.Tests;

public sealed class BimExchangeSampleTests
{
    private static readonly Discipline[] ExpectedDisciplines =
    [
        Discipline.Architecture,
        Discipline.Structural,
        Discipline.Heating,
        Discipline.Ventilation,
        Discipline.Sanitary,
        Discipline.Electrical,
        Discipline.MSR,
        Discipline.FireProtection,
        Discipline.Landscape
    ];

    [Fact]
    public void Multidisciplinary_sample_can_be_deserialized()
    {
        var document = LoadSample();

        Assert.Equal(BimExchangeDocument.CurrentSchemaVersion, document.SchemaVersion);
        Assert.Equal("Demo BIM Campus", document.Project.Name);
        Assert.Equal("2026", document.Project.RevitVersion);
        Assert.NotEmpty(document.Elements);
    }

    [Fact]
    public void Sample_contains_required_fields_for_every_model_element()
    {
        var document = LoadSample();

        Assert.True(document.Levels.Count >= 2);
        Assert.True(document.Elements.Select(element => element.LevelName).Distinct().Count() >= 2);

        foreach (var element in document.Elements)
        {
            Assert.False(string.IsNullOrWhiteSpace(element.UniqueId));
            Assert.False(string.IsNullOrWhiteSpace(element.ElementId));
            Assert.NotEqual(Discipline.Undefined, element.Discipline);
            Assert.NotNull(element.Classification);
            Assert.False(string.IsNullOrWhiteSpace(element.Classification.Code));
            Assert.NotEmpty(element.ParameterSets);
            Assert.All(element.ParameterSets, set => Assert.NotEmpty(set.Values));
        }
    }

    [Fact]
    public void Sample_contains_expected_disciplines_with_multiple_elements()
    {
        var document = LoadSample();

        foreach (var discipline in ExpectedDisciplines)
        {
            Assert.Contains(discipline, document.Disciplines);
            Assert.True(document.Elements.Count(element => element.Discipline == discipline) >= 2, $"Expected at least two elements for {discipline}.");
        }
    }

    [Fact]
    public void Sample_parameter_values_preserve_declared_types()
    {
        var document = LoadSample();
        var allParameters = document.Elements
            .SelectMany(element => element.ParameterSets)
            .SelectMany(set => set.Values.Values)
            .ToList();

        Assert.Contains(allParameters, value => value.DataType == "string" && value.Value == "F90");
        Assert.Contains(allParameters, value => value.DataType == "number" && value.Value == "0.24" && value.Unit == "m");
        Assert.Contains(allParameters, value => value.DataType == "integer" && value.Value == "25" && value.Unit == "mm");
        Assert.Contains(allParameters, value => value.DataType == "boolean" && value.Value == "true");
    }

    [Fact]
    public void Sample_reads_validation_and_coordination_issues()
    {
        var document = LoadSample();

        Assert.Equal(3, document.ValidationIssues.Count);
        Assert.Contains(document.ValidationIssues, issue =>
            issue.Discipline == Discipline.FireProtection &&
            issue.RuleCode == "BS-001" &&
            issue.ProposedWriteBack["SM_Pruefstatus"] == "Warnung");

        Assert.Equal(2, document.CoordinationIssues.Count);
        Assert.Contains(document.CoordinationIssues, issue =>
            issue.Id == "coord-001" &&
            issue.Disciplines.Contains(Discipline.Structural) &&
            issue.Disciplines.Contains(Discipline.Ventilation) &&
            issue.RelatedUniqueIds.Contains("uid-ventilation-duct-eg"));
    }

    private static BimExchangeDocument LoadSample()
    {
        var json = File.ReadAllText(FindSamplePath());
        return RevitApiJsonSerializer.Deserialize<BimExchangeDocument>(json)
            ?? throw new InvalidOperationException("Sample could not be deserialized.");
    }

    private static string FindSamplePath()
    {
        var directory = new DirectoryInfo(AppContext.BaseDirectory);

        while (directory is not null)
        {
            var candidate = Path.Combine(directory.FullName, "samples", "bim-exchange.multidisciplinary.sample.json");
            if (File.Exists(candidate))
            {
                return candidate;
            }

            directory = directory.Parent;
        }

        throw new FileNotFoundException("Could not find samples/bim-exchange.multidisciplinary.sample.json.");
    }
}
