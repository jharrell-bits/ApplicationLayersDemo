using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Gadget
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        [Display(Name = "Key")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Type of Gadget")]
        [StringLength(60)]
        public string GadgetType { get; set; } = string.Empty;

        [Display(Name = "Usage Instructions")]
        [StringLength(250)]
        public string UsageInstructions { get; set; } = string.Empty;

        public int UserDefinedSequenceNumber { get; set; } = 0;
    }
}
