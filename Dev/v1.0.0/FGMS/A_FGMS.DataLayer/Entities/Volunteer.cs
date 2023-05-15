using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


/// <summary>
/// The Purpose of this file is to provide the schema for the Volunteers table.
/// </summary>
/// <author>Richard Nader, Jr.</author>
/// <created>1/22/23</created>
namespace A_FGMS.DataLayer.Entities
{
#pragma warning disable CS8618
    public class Volunteer
    {
        [Key]
        public int Tuid { get; set; }

        [ForeignKey("Address")]
        public int? AddressTuid { get; set; }
        public virtual Address? Address { get; set; }

        [Required]
        [Column(TypeName = "varchar(45)")]
        public string FirstName { get; set; }

        [Required]
        [Column(TypeName = "varchar(45)")]
        public string LastName { get; set; }

        [NotMapped]
        public string FullName
        {
            get
            {
                return LastName + ", " + FirstName;
            }
        }

        [Required]
        [Column(TypeName = "varchar(45)")]
        public string Phone { get; set; }

        [Column(TypeName = "varchar(45)")]
        public string? AltPhone { get; set; }

        [Required]
        public DateTime Birthday { get; set; }
        public DateTime? SeparatedDate { get; set; }

        // Reference to all of this volunteer's ReasonsSeparated (must be lazy loaded)
        public virtual ICollection<ReasonSeparated>? ReasonsSeparated { get; set; }

        [Required]
        [ForeignKey("Gender")]
        public int GenderTuid { get; set; }
        public virtual GenderTypeItem? Gender { get; set; }

        [Required]
        [ForeignKey("Ethnicity")]
        public int EthnicityTuid { get; set; }
        public virtual EthnicityTypeItem? Ethnicity { get; set; }

        [Required]
        [ForeignKey("RacialGroup")]
        public int RacialGroupTuid { get; set; }
        public virtual RacialGroupTypeItem? RacialGroup { get; set; }

        [Required]
        [ForeignKey("IdentifiesAsTypeItem")]
        public int IdentifiesAsTuid { get; set; }
        public virtual IdentifiesAsTypeItem? IdentifiesAs { get; set; }

        [Required]
        public bool IsFamilyOfMilitary { get; set; }

        [Required]
        public bool IsVeteran { get; set; }

        [Required]
        [Column(TypeName = "varchar(45)")]
        public string Email { get; set; }

        [NotMapped]
        public bool IsActive
        { 
            get
            {
                // We assume the volunteer is not active if they have a separation date
                return !SeparatedDate.HasValue;
            }
        }

		[Required]
		public DateTime LastUpdated { get; set; }

		[Required]
		public bool IsDeleted { get; set; }

		[Required]
		public DateTime StartDate { get; set; }

	}

}
