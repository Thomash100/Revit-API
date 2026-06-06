namespace RevitApi.Contracts.Validation;

public sealed record TgaValidationImport
{
    public List<TgaValidationResult> Results { get; init; } = new();
}
