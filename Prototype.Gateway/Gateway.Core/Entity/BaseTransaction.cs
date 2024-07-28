namespace Gateway.Core.Entity;

public abstract class BaseTransaction
{
    public long TransactionId { get; set;}
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

}
