using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B_FGMS.BusinessLogic.Models
{
    public class clsPTOStipendRatesModel
    {
       
        public int? Id { get; set; }
        public double? StipendRate { get; set; }
        
        public double? PTORate { get; set; }

        public DateTime Date { get; set; }


    }
}
