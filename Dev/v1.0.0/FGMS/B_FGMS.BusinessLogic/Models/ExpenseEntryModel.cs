using A_FGMS.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B_FGMS.BusinessLogic.Models
{
    /// <summary>
    /// This is the data model for the InKindExpense databsae entity
    /// </summary>
    /// <author>Andrew Loesel</author>
    /// <created>2/23/23</created>
    public class ExpenseEntryModel
    {
        public int Tuid { get; set; }
        public decimal Value { get; set; }
        public DateTime DateOf { get; set; }
        public ExpenseTypeModel? ExpenseType { get; set; }
        public VolunteerModel? Volunteer { get; set; }

    }
}
