using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Pizza.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Size
    {
        Small,
        Medium,
        Large
    }
}
