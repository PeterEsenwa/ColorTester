using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ColorTesterAPI.Domain.ValueObjects;

[JsonConverter(typeof(StringEnumConverter))]
public enum ColorType
{
    Color = 0,
    Dictionary = 1
}