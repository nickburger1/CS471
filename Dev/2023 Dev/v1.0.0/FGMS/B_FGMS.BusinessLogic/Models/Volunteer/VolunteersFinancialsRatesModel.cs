using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B_FGMS.BusinessLogic.Models.Volunteer
{
    public class VolunteersFinancialsRatesModel
    {
        public int VolunteerTuid { get; set; }
        public double? CurrentStipendRate { get; set; }
        public double? CurrentPtoRate { get; set; }
        public double? YearlyMealValue { get; set; }
        public double? CurrentMileageRate { get; set; }
        public double? CurrentBusRideRate { get; set; }
        public DateTime Date { get; set; }
    }
}
