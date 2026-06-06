namespace RevitApi.Contracts.WriteBack;

public sealed record WriteBackOutcome
{
    public string UniqueId { get; init; } = string.Empty;

    public string ParameterName { get; init; } = string.Empty;

    public WriteBackOutcomeStatus Status { get; init; } = WriteBackOutcomeStatus.Unknown;

    public string? Message { get; init; }
}
