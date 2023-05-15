using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B_FGMS.BusinessLogic.Models
{
    public class StudentModel
    {
        public int Tuid { get; set; }
        public string? Identifier { get; set; }
        public bool? IsAge5To12 { get; set; }

        public bool? IsAgeBirthTo5 { get; set; }
    }
}
