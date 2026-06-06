namespace RevitApi.Contracts.Bim;

public sealed record ValidationIssue
{
    public string UniqueId { get; init; } = string.Empty;

    public Discipline Discipline { get; init; } = Discipline.Undefined;

    public SourceApplication SourceApplication { get; init; } = SourceApplication.Unknown;

    public string RuleCode { get; init; } = string.Empty;

    public string Severity { get; init; } = string.Empty;

    public string Status { get; init; } = string.Empty;

    public string Message { get; init; } = string.Empty;

    public Dictionary<string, string> ProposedWriteBack { get; init; } = new();
}
