using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace B_FGMS.BusinessLogic.Models.Volunteer
{
    public class VolunteerFinancialsHoursModel
    {
        public int VolunteerTuid { get; set; }
        public decimal? RegHours { get; set; }
        public decimal? YtdHours { get; set; }
        public DateTime Date { get; set; }

    }
}
