using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// The Purpose of this file is to provide the schema for the GrantStipends table.
/// </summary>
/// <author>Nathan VanSnepson</author>
/// <created>1/22/23</created>
namespace A_FGMS.DataLayer.Entities
{
    public class GrantStipend
    {
        [Key]
        public int Tuid { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        [Column(TypeName = "decimal(12,2)")]
        public decimal StartValue { get; set; }
    }
}
