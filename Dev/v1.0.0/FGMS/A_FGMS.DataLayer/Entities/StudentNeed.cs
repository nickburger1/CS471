using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// The Purpose of this file is to provide the schema for the StudentNeed table.
/// </summary>
/// <author>Nathan VanSnepson</author>
/// <created>1/22/23</created>
namespace A_FGMS.DataLayer.Entities
{
#pragma warning disable CS8618
    public class StudentNeed
    {
        [Key]
        public int Tuid { get; set; }

        [Required]
        [ForeignKey("Student")]        
        public int StudentTuid { get; set; }
        public virtual Student? Student { get; set; }

        [Required]
        [ForeignKey("StudentNeedItem")]
        public int StudentNeedItemTuid { get; set; }
        public virtual StudentNeedItem? StudentNeedItem { get; set; }
    }
}
