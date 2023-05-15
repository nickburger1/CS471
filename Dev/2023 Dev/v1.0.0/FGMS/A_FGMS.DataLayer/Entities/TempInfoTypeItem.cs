using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


/// <summary>
/// The Purpose of this file is to provide the schema for the TempInfoTypeItems table.
/// Temp info is used to store extra information related to volunteer annual checks.
/// </summary>
/// <author>Tyler P. Moody</author>
/// <created>1/20/23</created>
namespace A_FGMS.DataLayer.Entities
{
#pragma warning disable CS8618
    public enum TempInfoTypes
    {
        Date = 0,
        Boolean = 1
    }

    public class TempInfoTypeItem
    {
        [Key]
        public int Tuid { get; set; }

        [Required]
        [Column(TypeName = "varchar(45)")]
        public string Name { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string? Description { get; set; }

        // This will define what the temp info type is, either a date or a boolean
        [Required]
        public TempInfoTypes TempInfoType { get; set; }
    }
}
