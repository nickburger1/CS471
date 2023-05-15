using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B_FGMS.BusinessLogic.Models.Reports
{
    public class MealInKindReportModel
    {
        public string? strVolunteerName { get; set; }
        public int numMeals { get; set; }
        public int numBusRides { get; set; }
        public decimal Mileage { get; set; }
        public DateTime date { get; set; }
        public double mealRate { get; set; }
        public double busRate { get; set; }
        public double mileRate { get; set; }
    }
}
