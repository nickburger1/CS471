using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B_FGMS.BusinessLogic.Models
{
    public class SchoolCostShareModel
    {
        public int Tuid { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public double Value { get; set; }
    }
}
