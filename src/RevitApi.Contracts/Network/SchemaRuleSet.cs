namespace RevitApi.Contracts.Network;

public sealed record SchemaRuleSet
{
    public string Id { get; init; } = string.Empty;

    public string Name { get; init; } = string.Empty;

    public List<SchemaStartPointRule> StartPointRules { get; init; } = new();

    public List<SchemaBranchRule> BranchRules { get; init; } = new();
}
