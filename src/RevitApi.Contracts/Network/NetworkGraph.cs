using RevitApi.Contracts.Bim;

namespace RevitApi.Contracts.Network;

public sealed record NetworkGraph
{
    public string Id { get; init; } = string.Empty;

    public string Name { get; init; } = string.Empty;

    public Discipline Discipline { get; init; } = Discipline.Undefined;

    public SourceApplication SourceApplication { get; init; } = SourceApplication.Unknown;

    public string? SystemName { get; init; }

    public List<NetworkNode> Nodes { get; init; } = new();

    public List<NetworkEdge> Edges { get; init; } = new();

    public List<string> StartNodeIds { get; init; } = new();
}
