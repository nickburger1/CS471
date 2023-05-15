using A_FGMS.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B_FGMS.BusinessLogic.Models.Volunteer
{
    public class TemporaryInfoModel
    {
        public int Tuid { get; set; }
        public string? Name { get; set; }
        public string? Value { get; set; }
        public TempInfoTypes? Type { get; set; }

    }
}
