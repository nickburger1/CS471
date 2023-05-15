using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


/// <summary>
/// The Purpose of this file is to provide the schema for the User table.
/// </summary>
/// <author>Kiefer Thorson, Nathan VanSnepson, & Richard Nader, Jr.</author>
/// <created>2/16/23</created>
namespace A_FGMS.DataLayer.Entities
{
#pragma warning disable CS8618
    public class User
	{
		[Key]
		public int Tuid { get; set; }

		[Required]
		[Column(TypeName = "varchar(100)")]
		public string Name { get; set; }

		[Required]
		[Column(TypeName = "varchar(100)")]
		public string Email { get; set; }

		[Required]
		[Column(TypeName = "varchar(20)")]
		public string PhoneNumber { get; set; }

		[Required]
        [Column(TypeName = "varchar(256)")]
        public string HashedPassword { get; set; }

		[Required]
		public bool IsActive { get; set; }

		[Required]
		public bool IsAdmin { get; set; }
        [Required]
        public bool IsDeleted { get; set; }

		[Required]
		public bool IsReadOnly { get; set; } = false;
    }
}
