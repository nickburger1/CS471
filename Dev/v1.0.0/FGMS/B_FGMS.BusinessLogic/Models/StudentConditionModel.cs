using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B_FGMS.BusinessLogic.Models
{
    public class StudentConditionModel
    {
        public int Tuid { get; set; }
        public StudentModel? Student { get; set; }
        public ConditionItemModel? StudentConditionItem { get; set; }
    }
}
