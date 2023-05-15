using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


/// <summary>
/// The Purpose of this file is to provide the schema for the IdentifiesAsTypeItems table.
/// </summary>
/// <author>Richard Nader, Jr.</author>
/// <created>1/22/23</created>
namespace A_FGMS.DataLayer.Entities
{
#pragma warning disable CS8618
    public class IdentifiesAsTypeItem
    {
        [Key]
        public int Tuid { get; set; }

        [Required]
        [Column(TypeName = "varchar(45)")]
        public string Name { get; set; }
    }
}
