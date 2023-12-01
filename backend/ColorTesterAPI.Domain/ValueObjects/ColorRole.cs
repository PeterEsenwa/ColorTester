using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ColorTesterAPI.Domain.ValueObjects;

[JsonConverter(typeof(StringEnumConverter))]
public enum ColorRole
{
    Primary,
    Secondary,
    Background,
    Accent,
    Error,
    Success,
    Warning,
    OnPrimary,
    OnSecondary,
    OnBackground,
    OnAccent,
    OnError,
    OnSuccess,
    OnWarning
}