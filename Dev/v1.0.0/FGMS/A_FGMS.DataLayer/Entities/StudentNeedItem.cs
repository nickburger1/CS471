using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


/// <summary>
/// The Purpose of this file is to provide the schema for the StudentNeedItems table. A student
/// Can have multiple needs.  
/// </summary>
/// <author>Tyler P. Moody</author>
/// <created>1/20/23</created>
namespace A_FGMS.DataLayer.Entities
{
#pragma warning disable CS8618
    public class StudentNeedItem
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
