using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Pizza.Models
{
    //To present enum as string.
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Size
    {
        Small,
        Medium,
        Large
    }
}
