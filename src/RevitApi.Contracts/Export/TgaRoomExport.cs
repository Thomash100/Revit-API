namespace RevitApi.Contracts.Export;

public sealed record TgaRoomExport
{
    public string Number { get; init; } = string.Empty;

    public string Name { get; init; } = string.Empty;

    public string? LevelName { get; init; }

    public decimal? AreaSquareMeters { get; init; }

    public decimal? HeightMeters { get; init; }

    public decimal? VolumeCubicMeters { get; init; }

    public Dictionary<string, string> Parameters { get; init; } = new();
}
