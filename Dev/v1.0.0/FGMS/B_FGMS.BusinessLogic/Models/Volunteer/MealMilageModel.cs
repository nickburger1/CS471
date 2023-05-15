using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B_FGMS.BusinessLogic.Models.Volunteer
{
    public class MealMilageModel
    {
        public int Tuid { get; set; }
        public int VolunteerTuid { get; set; }
        public int MealCount { get; set; }
        public double MealRate { get; set; }
        public double MealValue { get; set; }
        public int BusRideCount { get; set; }
        public double BusRideRate { get; set; }
        public double Mileage { get; set; }
        public double MilageRate { get; set; }
        public DateTime Date { get; set; }
    }
}
