using System.ComponentModel.DataAnnotations;


/// <summary>
/// The Purpose of this file is to provide the schema for the MealTransportRates table. 
/// Stores the cost per meal and mileage rate for a given time period. Used for 
/// calculating reports.
/// </summary>
/// <author>Tyler P. Moody</author>
/// <created>1/20/23</created>
namespace A_FGMS.DataLayer.Entities
{
    public class MealTransportRate
    {
        [Key]
        public int Tuid { get; set; }
        [Required]
        public double MealRate { get; set; }
        [Required]
        public double MileageRate { get; set; }
        [Required]
        public double BusMileageRate { get; set; }
        [Required]
        public DateTime Date { get; set; }

	}
}
