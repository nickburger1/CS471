using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


/// <summary>
/// The Purpose of this file is to provide the schema for the PTOStipends table.
/// </summary>
/// <author>Richard Nader, Jr.</author>
/// <created>1/22/23</created>
namespace A_FGMS.DataLayer.Entities
{
#pragma warning disable CS8618
    public class PTOStipend
    {
        [Key]
        public int Tuid { get; set; }

        [Required]
        [ForeignKey("Volunteer")]
        public int VolunteerTuid { get; set; }
        public virtual Volunteer? Volunteer { get; set; }

        [Required]
        [Column(TypeName = "decimal(12,2)")]
        public decimal RegularHours { get; set; }

        [Required]
        [Column(TypeName = "decimal(12,2)")]
        public decimal PtoStart { get; set; }

        [Required]
        [Column(TypeName = "decimal(12,2)")]
        public decimal PtoEnd { get; set; }

        [Required]
        [Column(TypeName = "decimal(12,2)")]
        public decimal PtoUsed { get; set; }

        [Required]
        [Column(TypeName = "decimal(12,2)")]
        public decimal PtoEarned { get; set; }

        [Required]
        [Column(TypeName = "decimal(12,2)")]
        public decimal StipendPaid { get; set; }

        [Required]
        [Column(TypeName = "decimal(12,2)")]
        public decimal YearToDateHour { get; set; }

        [Required]
        public bool IsPTOEligible { get; set; }

        [Required]
        public DateTime Date { get; set; }

    }
}
