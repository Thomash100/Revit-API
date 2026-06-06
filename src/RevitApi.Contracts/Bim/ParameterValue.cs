namespace RevitApi.Contracts.Bim;

public sealed record ParameterValue
{
    public string Name { get; init; } = string.Empty;

    public string Value { get; init; } = string.Empty;

    public string? DataType { get; init; }

    public string? Unit { get; init; }

    public SourceApplication SourceApplication { get; init; } = SourceApplication.Unknown;
}
