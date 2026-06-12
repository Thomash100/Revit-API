namespace RevitApi.Contracts.Network;

public sealed record NetworkEdge
{
    public string Id { get; init; } = string.Empty;

    public string FromNodeId { get; init; } = string.Empty;

    public string ToNodeId { get; init; } = string.Empty;

    public NetworkEdgeKind Kind { get; init; } = NetworkEdgeKind.Undefined;

    public string? FromConnectorId { get; init; }

    public string? ToConnectorId { get; init; }

    public string? SystemName { get; init; }

    public NetworkFlowDirection FlowDirection { get; init; } = NetworkFlowDirection.Undefined;

    public NetworkDirectionSource DirectionSource { get; init; } = NetworkDirectionSource.Unknown;

    public bool IsDirected { get; init; } = true;
}
