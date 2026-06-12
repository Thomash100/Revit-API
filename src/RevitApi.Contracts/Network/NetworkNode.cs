using RevitApi.Contracts.Bim;

namespace RevitApi.Contracts.Network;

public sealed record NetworkNode
{
    public string Id { get; init; } = string.Empty;

    public string ElementId { get; init; } = string.Empty;

    public string UniqueId { get; init; } = string.Empty;

    public Discipline Discipline { get; init; } = Discipline.Undefined;

    public SourceApplication SourceApplication { get; init; } = SourceApplication.Unknown;

    public NetworkNodeRole Role { get; init; } = NetworkNodeRole.Undefined;

    public ElementClassification? Classification { get; init; }

    public string Category { get; init; } = string.Empty;

    public string Family { get; init; } = string.Empty;

    public string TypeName { get; init; } = string.Empty;

    public string? LevelName { get; init; }

    public string? SystemName { get; init; }

    public List<ParameterSet> ParameterSets { get; init; } = new();
}
