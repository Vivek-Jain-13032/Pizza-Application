using System.Text.Json.Serialization;

namespace Pizza.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Crust
    {   
        StuffedCrust,
        CrackerCrust,
        FlatBreadCrust,
        ThinCrust
    }
}
