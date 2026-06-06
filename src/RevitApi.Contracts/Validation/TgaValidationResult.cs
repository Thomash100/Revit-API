namespace RevitApi.Contracts.Validation;

public sealed record TgaValidationResult
{
    public string UniqueId { get; init; } = string.Empty;

    public string Status { get; init; } = string.Empty;

    public string SourceSystem { get; init; } = string.Empty;

    public string RuleCode { get; init; } = string.Empty;

    public string Message { get; init; } = string.Empty;

    public Dictionary<string, string> WriteBack { get; init; } = new();
}
