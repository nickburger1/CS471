using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B_FGMS.BusinessLogic.Models
{
    public class FinancialQuarterModel
    {
        public double dbTotalMealValue { get; set; }
        public double dbTotalMileageValue { get; set; }
        public double dbTotalBusValue { get; set; }
        public string? strQuarter { get; set; }
    }
}
