namespace RevitApi.Contracts.WriteBack;

public sealed record WriteBackRequest
{
    public int SchemaVersion { get; init; } = 1;

    public DateTimeOffset CreatedAt { get; init; }

    public List<WriteBackInstruction> Instructions { get; init; } = new();
}
