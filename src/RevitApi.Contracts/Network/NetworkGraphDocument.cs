using RevitApi.Contracts.Bim;
using RevitApi.Contracts.Export;

namespace RevitApi.Contracts.Network;

public sealed record NetworkGraphDocument
{
    public const int CurrentSchemaVersion = 1;

    public int SchemaVersion { get; init; } = CurrentSchemaVersion;

    public TgaProjectInfo Project { get; init; } = new();

    public List<NetworkGraph> Graphs { get; init; } = new();

    public List<SchemaNumberingPreview> NumberingPreviews { get; init; } = new();

    public List<ValidationIssue> ValidationIssues { get; init; } = new();

    public List<CoordinationIssue> CoordinationIssues { get; init; } = new();
}
