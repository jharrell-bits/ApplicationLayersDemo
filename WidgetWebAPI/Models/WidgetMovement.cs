using Newtonsoft.Json.Converters;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WidgetWebAPI.Models
{
    public class WidgetMovement
    {
        public enum TypeOfMove
        {
            [Description("None")]
            None = 0,
            [Description("Up")]
            Up = 1,
            [Description("Down")]
            Down = 2
        }

        [Required]
        [EnumDataType(typeof(TypeOfMove))]
        [JsonConverter(typeof(StringEnumConverter))]
        public TypeOfMove MovementType { get; set; }
    }
}
