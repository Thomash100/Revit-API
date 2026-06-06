namespace RevitApi.Contracts.Bim;

public sealed record CoordinationIssue
{
    public string Id { get; init; } = string.Empty;

    public string Title { get; init; } = string.Empty;

    public string Description { get; init; } = string.Empty;

    public string Status { get; init; } = string.Empty;

    public string Severity { get; init; } = string.Empty;

    public SourceApplication SourceApplication { get; init; } = SourceApplication.Unknown;

    public List<Discipline> Disciplines { get; init; } = new();

    public List<string> RelatedUniqueIds { get; init; } = new();
}
