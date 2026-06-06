namespace RevitApi.Contracts.Bim;

public sealed record ElementClassification
{
    public string ClassificationSystem { get; init; } = string.Empty;

    public string Code { get; init; } = string.Empty;

    public string Name { get; init; } = string.Empty;

    public Discipline Discipline { get; init; } = Discipline.Undefined;
}
