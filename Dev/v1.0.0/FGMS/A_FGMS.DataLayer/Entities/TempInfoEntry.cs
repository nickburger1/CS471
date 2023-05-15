using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


/// <summary>
/// The Purpose of this file is to provide the schema for the TempInfoEntries table
/// </summary>
/// <author>Richard Nader, Jr.</author>
/// <created>1/22/23</created>
namespace A_FGMS.DataLayer.Entities
{
#pragma warning disable CS8618
    public class TempInfoEntry
    {
        [Key]
        public int Tuid { get; set; }

        [Required]
        [ForeignKey("Volunteer")]
        public int VolunteerTuid { get; set; }
        public virtual Volunteer? Volunteer { get; set; }

        [Required]
        [ForeignKey("TempInfoType")]
        public int TempInfoTypeItemTuid { get; set; }
        public virtual TempInfoTypeItem? TempInfoType { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(50)")]
        public string value { get; set; }
    }
}
