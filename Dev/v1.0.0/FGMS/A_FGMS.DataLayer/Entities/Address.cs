using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


/// <summary>
/// The Purpose of this file is to provide the schema for the Address table.
/// </summary>
/// <author>Nathan VanSnepson</author>
/// <created>1/27/23</created>
namespace A_FGMS.DataLayer.Entities
{
#pragma warning disable CS8618
    public class Address
    {
        [Key]
        public int Tuid { get; set; }

        [Required]
        [Column(TypeName = "varchar(45)")]
        public string AddressLine1 { get; set; }

        [Column(TypeName = "varchar(45)")]
        public string? AddressLine2 { get; set; }

        [Required]
        [Column(TypeName = "varchar(45)")]
        public string City { get; set; }

        [Required]
        [Column(TypeName = "varchar(45)")]
        public string State { get; set; }

        [Required]
        [Column(TypeName = "varchar(45)")]
        public string Zipcode { get; set; }
    }
}
