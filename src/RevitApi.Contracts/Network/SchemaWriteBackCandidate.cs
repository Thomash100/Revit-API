using RevitApi.Contracts.Bim;

namespace RevitApi.Contracts.Network;

public sealed record SchemaWriteBackCandidate
{
    public string NodeId { get; init; } = string.Empty;

    public string ElementId { get; init; } = string.Empty;

    public string UniqueId { get; init; } = string.Empty;

    public string ParameterName { get; init; } = "TGA_SchemaStrangNr";

    public string ProposedValue { get; init; } = string.Empty;

    public string Status { get; init; } = "candidate";

    public bool RequiresApproval { get; init; } = true;

    public SourceApplication SourceApplication { get; init; } = SourceApplication.Unknown;
}
