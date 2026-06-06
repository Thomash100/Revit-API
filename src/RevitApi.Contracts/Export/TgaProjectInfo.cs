namespace RevitApi.Contracts.Export;

public sealed record TgaProjectInfo
{
    public string Name { get; init; } = string.Empty;

    public string RevitVersion { get; init; } = string.Empty;

    public DateTimeOffset ExportDate { get; init; }

    public string Source { get; init; } = "Revit-API";
}
