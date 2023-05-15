using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


/// <summary>
/// The Purpose of this file is to provide the schema for the Assignment table.
/// </summary>
/// <author>Nathan VanSnepson</author>
/// <created>1/22/23</created>
namespace A_FGMS.DataLayer.Entities
{
#pragma warning disable CS8618
    public class Assignment
    {
        [Key]
        public int Tuid { get; set; }

        [Required]
        [ForeignKey("Volunteer")]
        public int VolunteerTuid { get; set; }
        public virtual Volunteer? Volunteer { get; set; }

        [Required]
        [ForeignKey("Classroom")]
        public int ClassroomTuid { get; set; }
        public virtual Classroom? Classroom { get; set; }

        [Column(TypeName = "varchar(500)")]
        public string? Schedule { get; set; }

        public bool? IsDeleted { get; set; }
    }
}
