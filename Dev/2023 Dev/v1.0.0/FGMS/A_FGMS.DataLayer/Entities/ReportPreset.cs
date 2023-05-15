using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


/// <summary>
/// The Purpose of this file is to provide the schema for the ReportPreset table
/// </summary>
/// <author>Nathan VanSnepson</author>
/// <created>3/5/23</created>
namespace A_FGMS.DataLayer.Entities
{
#pragma warning disable CS8618
    public class ReportPreset
	{
		[Key]
		public int Tuid { get; set; }
		[Required]
		[Column(TypeName = "varchar(100)")]
		public string Name { get; set; }
		[Required]
		[Column(TypeName = "varchar(max)")]
		public string Preset { get; set; }
		[Required]
        [Column(TypeName = "varchar(100)")]
        public string SortBy { get; set; }
		[Required]
		public bool? Current { get; set; }
		[Required]
		public bool? Former { get; set; }
        [Required]
        public bool? Active { get; set; }
        [Required]
		public bool? Inactive { get; set; }
		[Required]
		public DateTime? LastUpdated { get; set; }
	}
}
