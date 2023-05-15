using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B_FGMS.BusinessLogic.Models
{
    public class AddressModel
    {
        public int Tuid { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set;}
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
    }
}
