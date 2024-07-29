using System.Text.Json.Nodes;

namespace Gateway.Application.DTOs;

public class TransactionDTO
{
        public string Origin { get; set; }
        public JsonObject DataJson { get; set; }
        public string Destination { get; set; }
}
