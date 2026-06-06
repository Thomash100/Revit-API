using System.Text.Json.Serialization;

namespace RevitApi.Contracts.Bim;

public sealed record ModelElement
{
    public string ElementId { get; init; } = string.Empty;

    public string UniqueId { get; init; } = string.Empty;

    public string? GlobalId { get; init; }

    public Discipline Discipline { get; init; } = Discipline.Undefined;

    public SourceApplication SourceApplication { get; init; } = SourceApplication.Unknown;

    public ElementClassification? Classification { get; init; }

    public string Category { get; init; } = string.Empty;

    public string Family { get; init; } = string.Empty;

    [JsonPropertyName("type")]
    public string TypeName { get; init; } = string.Empty;

    public string? LevelName { get; init; }

    public string? SystemName { get; init; }

    public List<ParameterSet> ParameterSets { get; init; } = new();
}
