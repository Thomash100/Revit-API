namespace RevitApi.Contracts.Export;

public sealed record TgaExportDocument
{
    public const int CurrentSchemaVersion = 1;

    public int SchemaVersion { get; init; } = CurrentSchemaVersion;

    public TgaProjectInfo Project { get; init; } = new();

    public TgaExportSummary Summary { get; init; } = new();

    public List<TgaRoomExport> Rooms { get; init; } = new();

    public List<TgaElementExport> Elements { get; init; } = new();

    public static TgaExportDocument Create(
        TgaProjectInfo project,
        TgaExportSummary summary,
        IEnumerable<TgaRoomExport>? rooms = null,
        IEnumerable<TgaElementExport>? elements = null)
    {
        ArgumentNullException.ThrowIfNull(project);
        ArgumentNullException.ThrowIfNull(summary);

        return new TgaExportDocument
        {
            Project = project,
            Summary = summary,
            Rooms = rooms?.ToList() ?? new List<TgaRoomExport>(),
            Elements = elements?.ToList() ?? new List<TgaElementExport>()
        };
    }
}
