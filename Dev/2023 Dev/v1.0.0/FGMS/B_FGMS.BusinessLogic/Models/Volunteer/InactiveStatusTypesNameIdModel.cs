using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B_FGMS.BusinessLogic.Models.Volunteer
{
    public class InactiveStatusTypesNameIdModel
    {
        public int Tuid { get; set; }
        public string Name { get; set; }
        public string TotalVolunteers { get; set; }

        /// <summary>
        /// This is required so the model can be used in a combobox as an observable collection.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>03/27/2023</created>
        /// <returns>String name of gender.</returns>
        public override string ToString()
        {
            return Name ?? string.Empty;
        }
    }
}
