namespace RevitApi.Contracts.Export;

public sealed record TgaExportSummary
{
    public int Pipes { get; init; }

    public int PipeFittings { get; init; }

    public int PipeAccessories { get; init; }

    public int PlumbingFixtures { get; init; }

    public int MepElements { get; init; }

    public int Rooms { get; init; }
}
