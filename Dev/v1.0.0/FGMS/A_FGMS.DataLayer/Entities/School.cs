using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


/// <summary>
/// The Purpose of this file is to provide the schema for the School table.
/// </summary>
/// <author>Nathan VanSnepson</author>
/// <created>2/18/2023</created>
namespace A_FGMS.DataLayer.Entities
{
#pragma warning disable CS8618
    public class School
    {
        [Key]
        public int Tuid { get; set; }

        [Required]
        [ForeignKey("Address")]
        public int? AddressTuid { get; set; }
        public virtual Address? Address { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        public string Name { get; set; }

        [NotMapped]
        public string Hours 
        {
            get
            {
                return StartTime + " - " + EndTime;
            }
        }

        [Required]
        [Column(TypeName = "varchar(100)")]
        public string Principal { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        public string Secretary { get; set; }

        [Required]
        [Column(TypeName = "varchar(20)")]
        public string ContactNumber { get; set; }

        [Required]
        [Column(TypeName = "varchar(12)")]
        public string StartTime { get; set; }

        [Required]
        [Column(TypeName = "varchar(12)")]
        public string EndTime { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
		[Column(TypeName = "varchar(5)")]
		public string Days { get; set; }
        [Required]
        public bool IsDeleted { get; set; }
    }
}
