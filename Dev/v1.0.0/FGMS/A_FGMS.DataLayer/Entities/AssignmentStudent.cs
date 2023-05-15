using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// The Purpose of this file is to provide the schema for the AssignmentStudent table.
/// </summary>
/// <author>Nathan VanSnepson</author>
/// <created>1/22/23</created>
namespace A_FGMS.DataLayer.Entities
{
#pragma warning disable CS8618
    public class AssignmentStudent
    {
        [Key]
        public int Tuid { get; set; }

        [Required]
        [ForeignKey("Assignment")]
        public int AssignmentTuid { get; set; }
        public virtual Assignment? Assignment { get; set; }

        [Required]
        [ForeignKey("Student")]
        public int StudentTuid { get; set; }
        public virtual Student? Student { get; set; }

        [Required]
        [Column(TypeName = "varchar(45)")]
        public string DesiredOutcome { get; set; }

        [Required]
        [Column(TypeName = "varchar(45)")]
        public DateTime Date { get; set; }
    }
}
