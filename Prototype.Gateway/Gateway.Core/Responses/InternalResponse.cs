using System.Text.Json.Nodes;

namespace Gateway.Core.Responses;

public class InternalResponse
{
    public string? Message { get; set; }
    public JsonObject? DataJson { get; set; }
    public List<JsonObject>? ListDataJson { get; set; }
    public bool IsSuccess { get; set; } = true;
}
