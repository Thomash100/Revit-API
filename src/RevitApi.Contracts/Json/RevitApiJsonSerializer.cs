using System.Text.Json;
using System.Text.Json.Serialization;

namespace RevitApi.Contracts.Json;

public static class RevitApiJsonSerializer
{
    public static JsonSerializerOptions CreateDefaultOptions()
    {
        var options = new JsonSerializerOptions(JsonSerializerDefaults.Web)
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            WriteIndented = true
        };

        options.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
        return options;
    }

    public static string Serialize<T>(T value)
    {
        ArgumentNullException.ThrowIfNull(value);
        return JsonSerializer.Serialize(value, CreateDefaultOptions());
    }

    public static T? Deserialize<T>(string json)
    {
        if (string.IsNullOrWhiteSpace(json))
        {
            throw new ArgumentException("JSON content must not be empty.", nameof(json));
        }

        return JsonSerializer.Deserialize<T>(json, CreateDefaultOptions());
    }
}
