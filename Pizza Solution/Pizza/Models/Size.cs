using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Pizza.Models
{
    public enum Size
    {
        [EnumMember(Value = "Value1")]
        Small,
        Medium,
        Large
    }
}
