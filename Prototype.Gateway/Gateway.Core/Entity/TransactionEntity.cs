using System.Text.Json.Nodes;

namespace Gateway.Core.Entity
{
    public class TransactionEntity : BaseTransaction
    {
        public string Origin { get; set; }
        public JsonObject DataJson { get; set; }
        public string Destination { get; set; }
    }
}

