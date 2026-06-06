using System.Collections.ObjectModel;

namespace RevitApi.Contracts.Domain;

public sealed record Register
{
    public Register(
        string id,
        string shaftId,
        string number,
        string name,
        Trade trade,
        PlanningStatus status,
        IReadOnlyDictionary<string, string>? parameters = null)
    {
        Id = Guard.Required(id, nameof(id));
        ShaftId = Guard.Required(shaftId, nameof(shaftId));
        Number = Guard.Required(number, nameof(number));
        Name = Guard.Required(name, nameof(name));
        Trade = trade;
        Status = status;
        Parameters = CopyParameters(parameters);
    }

    public string Id { get; }

    public string ShaftId { get; }

    public string Number { get; }

    public string Name { get; }

    public Trade Trade { get; }

    public PlanningStatus Status { get; }

    public IReadOnlyDictionary<string, string> Parameters { get; }

    private static IReadOnlyDictionary<string, string> CopyParameters(IReadOnlyDictionary<string, string>? parameters)
    {
        var copy = parameters is null
            ? new Dictionary<string, string>()
            : parameters.ToDictionary(item => item.Key, item => item.Value);

        return new ReadOnlyDictionary<string, string>(copy);
    }
}
