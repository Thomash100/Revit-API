using RevitApi.Contracts.Export;

namespace RevitApi.Contracts.Bim;

public sealed record BimExchangeDocument
{
    public const int CurrentSchemaVersion = 1;

    public int SchemaVersion { get; init; } = CurrentSchemaVersion;

    public TgaProjectInfo Project { get; init; } = new();

    public List<Discipline> Disciplines { get; init; } = new();

    public List<ModelElement> Elements { get; init; } = new();

    public List<ValidationIssue> ValidationIssues { get; init; } = new();

    public List<CoordinationIssue> CoordinationIssues { get; init; } = new();
}
