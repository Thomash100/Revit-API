namespace RevitApi.Contracts.Network;

public sealed record SchemaNumberingPreview
{
    public string Id { get; init; } = string.Empty;

    public string NetworkGraphId { get; init; } = string.Empty;

    public string ParameterName { get; init; } = "TGA_SchemaStrangNr";

    public string? RuleSetId { get; init; }

    public SchemaNumberingStrategy Strategy { get; init; } = SchemaNumberingStrategy.Undefined;

    public List<string> StartNodeIds { get; init; } = new();

    public List<SchemaStrangAssignment> Assignments { get; init; } = new();

    public List<SchemaWriteBackCandidate> WriteBackCandidates { get; init; } = new();
}
