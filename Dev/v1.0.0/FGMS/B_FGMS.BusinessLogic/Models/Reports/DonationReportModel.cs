using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B_FGMS.BusinessLogic.Models.Reports
{
    /// <summary>
    /// this class models a donation item that will be used in the report.
    /// </summary>
    public class DonationReportModel
    {
        public string? Name { get; set; }
        public string? Type { get; set; }
        public double Value { get; set; }
        public DateTime Date { get; set; }
    }
}
