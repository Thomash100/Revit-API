namespace RevitApi.Contracts.Bim;

public sealed record BimLevel
{
    public string Id { get; init; } = string.Empty;

    public string Name { get; init; } = string.Empty;

    public decimal ElevationMeters { get; init; }
}
