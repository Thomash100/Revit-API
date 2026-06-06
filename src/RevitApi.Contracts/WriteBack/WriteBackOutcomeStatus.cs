namespace RevitApi.Contracts.WriteBack;

public enum WriteBackOutcomeStatus
{
    Unknown = 0,
    Succeeded,
    ElementNotFound,
    ParameterNotFound,
    ParameterReadOnly,
    Failed
}
