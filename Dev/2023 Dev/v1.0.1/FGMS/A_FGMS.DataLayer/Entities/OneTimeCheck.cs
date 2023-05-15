using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


/// <summary>
/// The Purpose of this file is to provide the schema for the OneTimeChecks table.
/// </summary>
/// <author>Richard Nader, Jr.</author>
/// <created>1/22/23</created>
namespace A_FGMS.DataLayer.Entities
{
#pragma warning disable CS8618
    public class OneTimeCheck
    {
        [Key]
        public int Tuid { get; set; }

        [Required]
        [ForeignKey("Volunteer")]
        public int VolunteerTuid { get; set; }
        public virtual Volunteer? Volunteer { get; set; }

        [Required]
        public bool HasFilePhoto { get; set; }

        [Required]
        public bool HasServiceDescription { get; set; }

        [Required]
        public bool HasTrainingSheet { get; set; }

        public DateTime? ConfidenceSouDate { get; set; }

        public DateTime? ServiceStartDate { get; set; }

        [Required]
        public bool HasNschc { get; set; }

        [Required]
        public bool HasBackgroundCheck { get; set; }

        [Required]
        public bool HasIdCopy { get; set; }

        public DateTime? NsopwDate { get; set; }

        public DateTime? IChatDate { get; set; }

        public DateTime? TrueScreenDate { get; set; }

        public DateTime? AliasFingerprintDate { get; set; }

        public DateTime? FieldPrintDate { get; set; }

        public DateTime? DhsDate { get; set; }

        public DateTime? TbShotDate { get; set; }


    }
}
