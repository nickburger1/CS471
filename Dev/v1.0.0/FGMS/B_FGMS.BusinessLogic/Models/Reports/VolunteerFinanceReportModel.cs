using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B_FGMS.BusinessLogic.Models.Reports
{
    public class VolunteerFinanceReportModel
    {
       // [TableColumn("Volunteer Name")]
        public string? VolunteerName { get; set; }
        public double RegHours { get; set; }
        public double YTDHours { get; set; }
        public double PTOEarned { get; set; }
        public double PTOUsed { get; set; }
        public double StipendPaid { get; set; }
        public double StipendRate { get; set; }
        public DateTime? Date { get; set; }
        
    }
}
