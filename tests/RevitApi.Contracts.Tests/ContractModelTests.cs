using RevitApi.Contracts.Domain;

namespace RevitApi.Contracts.Tests;

public sealed class ContractModelTests
{
    [Fact]
    public void Shaft_requires_identity_and_keeps_status()
    {
        var shaft = new Shaft(
            " shaft-001 ",
            "S-001",
            "Steigeschacht Nord",
            Trade.Sanitary,
            PlanningStatus.InCoordination,
            "Level 01",
            "TW");

        Assert.Equal("shaft-001", shaft.Id);
        Assert.Equal(Trade.Sanitary, shaft.Trade);
        Assert.Equal(PlanningStatus.InCoordination, shaft.Status);
        Assert.Equal("Level 01", shaft.LevelName);
    }

    [Fact]
    public void Register_copies_parameters()
    {
        var parameters = new Dictionary<string, string>
        {
            ["DN"] = "80"
        };

        var register = new Register(
            "reg-001",
            "shaft-001",
            "R-001",
            "Register Nord",
            Trade.Sanitary,
            PlanningStatus.Draft,
            parameters);

        parameters["DN"] = "100";

        Assert.Equal("shaft-001", register.ShaftId);
        Assert.Equal("80", register.Parameters["DN"]);
    }
}
