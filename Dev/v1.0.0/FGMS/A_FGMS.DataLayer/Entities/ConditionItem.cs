using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


/// <summary>
/// The Purpose of this file is to provide the schema for the ConditionItem table. These conditions will 
/// be linked to students from the student table. A student can have zero to many conditions.
/// </summary>
/// <author>Tyler P Moody</author>
/// <created>1/20/23</created>
namespace A_FGMS.DataLayer.Entities
{
#pragma warning disable CS8618
    public class ConditionItem
    {
        [Key]
        public int Tuid { get; set; }
        [Required]
        [Column(TypeName = "varchar(45)")]
        public string Acronym { get; set; }
        [Required]
        [Column(TypeName = "varchar(100)")]
        public string Description { get; set; }
    }
}
