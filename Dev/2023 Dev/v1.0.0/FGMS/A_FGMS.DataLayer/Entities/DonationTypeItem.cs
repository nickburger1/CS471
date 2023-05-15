using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


/// <summary>
/// The Purpose of this file is to provide the schema for the DonationTypeItem table. DonationTypeItems 
/// are used to classify types of donations. DonationTypeItems will be linked to many InKindDonationTypeItems. 
/// </summary>
/// <author>Tyler P. Moody</author>
/// <created>1/20/23</created>
namespace A_FGMS.DataLayer.Entities
{
#pragma warning disable CS8618
    public class DonationTypeItem
    {
        [Key]
        public int Tuid { get; set; }

        [Required]
        [Column(TypeName = "varchar(45)")]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        public string Description { get; set; }
    }
}
