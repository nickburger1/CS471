using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B_FGMS.BusinessLogic.Models
{
    public class StudentNeedModel
    {
        public int Tuid { get; set; }
        public StudentModel? Student { get; set; }
        public StudentNeedItemModel? StudentNeedItem { get; set; }
    }
}
