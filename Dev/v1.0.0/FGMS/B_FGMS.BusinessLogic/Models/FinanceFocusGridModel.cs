using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B_FGMS.BusinessLogic.Models
{
    public class FinanceFocusGridModel
    {
        public string? Name { get; set; }
        public string? Quarter { get; set; }
        public string? Date { get; set; }
        public double Rate { get; set; }
        public int Count { get; set; }
        public double Value { get; set; }
    }
}
