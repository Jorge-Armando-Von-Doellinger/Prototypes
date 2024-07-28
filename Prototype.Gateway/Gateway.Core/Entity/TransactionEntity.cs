
namespace Gateway.Core.Entity
{
    public class TransactionEntity : BaseTransaction
    {
        public string Origin { get; set; }
        public string DataJson { get; set; }
        public string Destination { get; set; }
    }
}

