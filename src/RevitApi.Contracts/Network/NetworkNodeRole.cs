namespace RevitApi.Contracts.Network;

public enum NetworkNodeRole
{
    Undefined = 0,
    StartPoint,
    Equipment,
    DistributionSegment,
    Fitting,
    Accessory,
    Fixture,
    Terminal,
    RoomReference
}
