using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// The Purpose of this file is to provide the schema for the ActivityLog table.
/// </summary>
/// <author>Nathan VanSnepson</author>
/// <created>1/22/23</created>
namespace A_FGMS.DataLayer.Entities
{
#pragma warning disable CS8618
    public class ActivityLog
    {
        [Key]
        public int Tuid { get; set; }

        [Required]
        [ForeignKey("Volunteer")]
        public int VolunteerTuid { get; set; }
        public virtual Volunteer? Volunteer { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [Column(TypeName = "varchar(10)")]
        public string Initial { get; set; }

        [Required]
        [Column(TypeName = "varchar(150)")]
        public string Incident { get; set; }
        [Required]
        public bool IsDeleted { get; set; }
    }
}
