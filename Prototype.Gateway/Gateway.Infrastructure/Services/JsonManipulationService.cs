using System.Text.Json.Nodes;

namespace Gateway.Infrastructure.Services;

public class JsonManipulationService
{

    internal JsonObject ConvertForObject(string json)
    {
        var jsonFixed = FixJson(json);
        return JsonNode.Parse(jsonFixed).AsObject();
    }

    internal string FixJson(string json)
    {
        var data = json.Replace("ObjectId(", null).Replace(")", "");
        return data;
    }
}