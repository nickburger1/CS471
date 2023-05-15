using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


/// <summary>
/// The Purpose of this file is to provide the schema for the Student table.
/// </summary>
/// <author>Nathan VanSnepson</author>
/// <created>1/22/23</created>
namespace A_FGMS.DataLayer.Entities
{
    public class Student
    {
        [Key]
        public int Tuid { get; set; }

        [Column(TypeName = "varchar(45)")]
        public string? Identifier { get; set; }

        public bool? IsAge5To12 { get; set; }

        public bool? IsAgeBirthTo5 { get; set; }
    }
}
