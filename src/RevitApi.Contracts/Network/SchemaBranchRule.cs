namespace RevitApi.Contracts.Network;

public sealed record SchemaBranchRule
{
    public string Id { get; init; } = string.Empty;

    public string Description { get; init; } = string.Empty;

    public string Trigger { get; init; } = string.Empty;

    public bool StartsNewStrang { get; init; }
}
