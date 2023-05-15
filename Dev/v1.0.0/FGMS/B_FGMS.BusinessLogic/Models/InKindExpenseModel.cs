using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B_FGMS.BusinessLogic.Models
{
    /// <summary>
    /// This class is build to model in kind expenses, as well as donations for the finance general page
    /// </summary>
    /// <author>Andrew Loesel</author>
    /// <created>4/1/2023</created>
    public class InKindExpenseModel
    {
        public bool IsDonation { get; set; }
        public string? VolunteerDonorName { get; set; }
        public int dbMirrorTuid { get; set; }
        public int TypeTuid { get; set; }
        public double Value { get; set; }
        public DateTime Date { get; set; }
        public int? intVolunteerTuid { get; set; }
        public string? ExpenseTypeName { get; set; }
    }
}
