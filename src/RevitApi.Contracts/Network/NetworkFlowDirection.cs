namespace RevitApi.Contracts.Network;

public enum NetworkFlowDirection
{
    Undefined = 0,
    Supply,
    Return,
    Exhaust,
    Bidirectional,
    Unknown
}
