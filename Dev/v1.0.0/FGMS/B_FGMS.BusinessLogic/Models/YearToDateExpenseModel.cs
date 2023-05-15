using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B_FGMS.BusinessLogic.Models
{
    /// <summary>
    /// This class is the model for objects shown in the Fiscal Year - Date and Grant Year - Date tables on the corresponding Finance pages
    /// </summary>
    /// <author>Andrew Loesel</author>
    /// <created>3/15/2023</created>
    public class YearToDateExpenseModel
    {
        public string Name { get; set; }
        public double Value { get; set; }
        public string strValue { get; set; }
    }
}
