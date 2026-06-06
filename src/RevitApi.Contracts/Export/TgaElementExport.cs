using System.Text.Json.Serialization;
using RevitApi.Contracts.Domain;

namespace RevitApi.Contracts.Export;

public sealed record TgaElementExport
{
    public string ElementId { get; init; } = string.Empty;

    public string UniqueId { get; init; } = string.Empty;

    public string Category { get; init; } = string.Empty;

    public string Family { get; init; } = string.Empty;

    [JsonPropertyName("type")]
    public string TypeName { get; init; } = string.Empty;

    public string? LevelName { get; init; }

    public string? SystemName { get; init; }

    public Trade Trade { get; init; } = Trade.Undefined;

    public PlanningStatus PlanningStatus { get; init; } = PlanningStatus.Unknown;

    public Dictionary<string, string> Parameters { get; init; } = new();
}
