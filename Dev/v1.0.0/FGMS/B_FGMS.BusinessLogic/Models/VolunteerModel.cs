using A_FGMS.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B_FGMS.BusinessLogic.Models
{
    /// <summary>
    /// This is a data model for the volunteer database entity
    /// </summary>
    /// <author>Andrew Loesel</author>
    /// <created>2/23/23</created>
    public class VolunteerModel
    {
        public int Tuid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get { return LastName + ", " + FirstName; }
        }
        public string Phone { get; set; }
    }
}
