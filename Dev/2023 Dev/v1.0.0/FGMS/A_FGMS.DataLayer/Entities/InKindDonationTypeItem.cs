using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


/// <summary>
/// The Purpose of this file is to provide the schema for the InKindDonationTypeItems table.
/// Stores information of who made the donation, the amount, when it was made, and what type
/// of donation it was.
/// </summary>
/// <author>Richard Nader, Jr.</author>
/// <created>1/20/23</created>
namespace A_FGMS.DataLayer.Entities
{
#pragma warning disable CS8618
    public class InKindDonationTypeItem
    {
        [Key]
        public int Tuid { get; set; }
        [Column(TypeName = "varchar(45)")]
        public string? Name { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public double Value { get; set; }
        [Required]
        [ForeignKey("DonationTypeItem")]
        public int DonationTypeItemTuid { get; set; }
        public virtual DonationTypeItem? DonationTypeItem { get; set; }
    }
}
