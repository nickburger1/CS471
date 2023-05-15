using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B_FGMS.BusinessLogic.Models.Volunteer
{
    public class VolunteerFinancialsMealTransportModel
    {
        public int VolunteerTuid { get; set; }
        public int? SiteMeals { get; set; }
        public decimal? Mileage { get; set; }
        public int? BusRides { get; set; }
        public decimal? TotalMealValue { get; set; }
        public decimal? TotalMileageValue { get; set; }
        public decimal? TotalBusRidesValue { get; set; }
        public DateTime Date { get; set; }
    }
}
