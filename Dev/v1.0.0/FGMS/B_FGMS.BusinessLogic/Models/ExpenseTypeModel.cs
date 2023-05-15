using A_FGMS.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B_FGMS.BusinessLogic.Models
{
    /// <summary>
    /// This class is the data model for non in-kind expenses
    /// </summary>
    /// <author>Adnrew Loesel</author>
    /// <created>2/23/2022</created>
    public class ExpenseTypeModel
    {
        public int Tuid { get; set; }
        public string? Name { get; set; }

    }
}
