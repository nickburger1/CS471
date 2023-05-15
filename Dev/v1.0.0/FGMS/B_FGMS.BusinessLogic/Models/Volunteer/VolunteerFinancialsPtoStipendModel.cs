using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B_FGMS.BusinessLogic.Models.Volunteer
{
    public class VolunteerFinancialsPtoStipendModel
    {
        public int VolunteerTuid { get; set; }
        public decimal? PtoEarned { get; set; }
        public decimal? PtoUsed { get; set; }
        public decimal? PtoStart { get; set; }
        public decimal? PtoEnded { get; set; }
        public decimal? StipendPaid { get; set; }
        public bool? IsPTOEligible { get; set; }
        public DateTime Date { get; set; }
    }
}
