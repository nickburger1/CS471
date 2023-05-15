using DocumentFormat.OpenXml.Office2010.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B_FGMS.BusinessLogic.Models.Volunteer
{
    public class VolunteerNameIdModel
    {
        public int? Tuid { get; set; }
        public string? FullName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }  
        public string? FormattedName { get { string x = LastName + ", " + FirstName;return x; }  }
 

        /// <summary>
        /// This is required so the model can be used in a combobox as an observable collection.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>03/22/2023</created>
        /// <returns>String full name of volunteer.</returns>
        public override string ToString()
        {
            return FullName ?? string.Empty;
        }
    }
}
