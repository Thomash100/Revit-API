namespace RevitApi.Contracts.WriteBack;

public sealed record WriteBackInstruction
{
    public string UniqueId { get; init; } = string.Empty;

    public string SourceSystem { get; init; } = string.Empty;

    public string RuleCode { get; init; } = string.Empty;

    public Dictionary<string, string> Parameters { get; init; } = new();
}
