namespace RevitApi.Contracts.Network;

public sealed record SchemaStartPointRule
{
    public string Id { get; init; } = string.Empty;

    public string Description { get; init; } = string.Empty;

    public int Priority { get; init; }

    public List<NetworkNodeRole> Roles { get; init; } = new();

    public List<string> CategoryPatterns { get; init; } = new();
}
