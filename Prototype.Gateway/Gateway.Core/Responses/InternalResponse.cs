using Gateway.Core.Entity;

namespace Gateway.Core.Responses;

public class InternalResponse
{
    public string? Message { get; set; }
    public TransactionEntity? DataJson { get; set; }
    public bool IsSuccess { get; set; } = true;
}
