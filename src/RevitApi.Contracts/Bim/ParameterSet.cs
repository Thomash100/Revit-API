namespace RevitApi.Contracts.Bim;

public sealed record ParameterSet
{
    public string Name { get; init; } = string.Empty;

    public Discipline Discipline { get; init; } = Discipline.Undefined;

    public SourceApplication SourceApplication { get; init; } = SourceApplication.Unknown;

    public Dictionary<string, ParameterValue> Values { get; init; } = new();
}
