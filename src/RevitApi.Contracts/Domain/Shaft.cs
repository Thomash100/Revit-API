namespace RevitApi.Contracts.Domain;

public sealed record Shaft
{
    public Shaft(
        string id,
        string number,
        string name,
        Trade trade,
        PlanningStatus status,
        string? levelName = null,
        string? systemName = null)
    {
        Id = Guard.Required(id, nameof(id));
        Number = Guard.Required(number, nameof(number));
        Name = Guard.Required(name, nameof(name));
        Trade = trade;
        Status = status;
        LevelName = NormalizeOptional(levelName);
        SystemName = NormalizeOptional(systemName);
    }

    public string Id { get; }

    public string Number { get; }

    public string Name { get; }

    public Trade Trade { get; }

    public PlanningStatus Status { get; }

    public string? LevelName { get; }

    public string? SystemName { get; }

    private static string? NormalizeOptional(string? value)
    {
        return string.IsNullOrWhiteSpace(value) ? null : value.Trim();
    }
}
