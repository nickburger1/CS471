using System.ComponentModel.DataAnnotations;


/// <summary>
/// The Purpose of this file is to provide the schema for the PTOStipendRates table.
/// Stores the pto and stipend rates for a given time period. Used for calculating reports.                      
/// </summary>
/// <author>Tyler P. Moody</author>
/// <created>1/20/23</created>
namespace A_FGMS.DataLayer.Entities
{
    public class PTOStipendRate
    {
        [Key]
        public int Tuid { get; set; }
        [Required]
        public double PTORate { get; set; }
        [Required]
        public double StipendRate { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
}
