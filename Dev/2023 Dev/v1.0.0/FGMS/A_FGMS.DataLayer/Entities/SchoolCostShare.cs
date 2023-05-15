using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


/// <summary>
/// The Purpose of this file is to provide the schema for the SchoolCostShares table.
/// Stores the school cost shares a given time period. Used for calculating reports.
/// </summary>
/// <author>Tyler P. Moody</author>
/// <created>1/20/23</created>
namespace A_FGMS.DataLayer.Entities
{
#pragma warning disable CS8618
    public class SchoolCostShare
    {
        [Key]
        public int Tuid { get; set; }
        [Required]
        [Column(TypeName = "varchar(45)")]
        public string Name { get; set; } // This could be used for billing number
        [Required]
        public double Value { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
}
