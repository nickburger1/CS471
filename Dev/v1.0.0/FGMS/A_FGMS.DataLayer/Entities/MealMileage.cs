using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


/// <summary>
/// The Purpose of this file is to provide the schema for the MealMileage table.
/// </summary>
/// <author>Richard Nader, Jr.</author>
/// <created>1/22/23</created>
namespace A_FGMS.DataLayer.Entities
{
#pragma warning disable CS8618
    public class MealMileage
    {
        [Key]
        public int Tuid { get; set; }

        [Required]
        [ForeignKey("Volunteer")]
        public int VolunteerTuid { get; set; }
        public virtual Volunteer? Volunteer { get; set; }

        [Required]
        public int MealCount { get; set; }

        [Required]
        public int BusRideCount { get; set; }

        [Required]
        [Column(TypeName = "decimal(16,2)")]
        public decimal Mileage { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
