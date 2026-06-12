using RevitApi.Contracts.Bim;

namespace RevitApi.Contracts.Network;

public sealed record SchemaStrangAssignment
{
    public string NodeId { get; init; } = string.Empty;

    public string ElementId { get; init; } = string.Empty;

    public string UniqueId { get; init; } = string.Empty;

    public string? StartNodeId { get; init; }

    public int StrangIndex { get; init; }

    public string PreviewValue { get; init; } = string.Empty;

    public string Status { get; init; } = "preview";

    public SourceApplication SourceApplication { get; init; } = SourceApplication.Unknown;
}
