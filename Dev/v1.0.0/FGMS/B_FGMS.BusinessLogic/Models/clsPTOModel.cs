using A_FGMS.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B_FGMS.BusinessLogic.Models
{
    public class clsPTOModel
    {
        public int? Tuid { get; set; }
        public int? volunteerID { get; set; }
        public string? strFullName { get; set; }
        public decimal? RegularHours { get; set; }
        public decimal? PtoStart { get; set; }
        public decimal? PtoEnd { get; set; }
        public decimal? PtoUsed { get; set; }
        public decimal? PtoEarned { get; set; }
        public decimal? StipendPaid { get; set; }
        public decimal? YearToDateHour { get; set; }
        public bool? IsPTOEligible { get; set; }
        public DateTime? date { get; set; }





        public clsPTOModel()
        {
            RegularHours = 0;
            PtoStart = 0;
            PtoEnd = 0;
            PtoUsed = 0;
            PtoEarned = 0;
            StipendPaid = 0;
            YearToDateHour = 0;
            date = DateTime.MinValue;
        }











    }
}
