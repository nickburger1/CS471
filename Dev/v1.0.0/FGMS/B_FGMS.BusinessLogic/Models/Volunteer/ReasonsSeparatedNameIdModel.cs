using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B_FGMS.BusinessLogic.Models.Volunteer
{
    public class ReasonsSeparatedNameIdModel
    {
        public int Tuid { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// This is required so the model can be used in a combobox as an observable collection.
        /// </summary>
        /// <returns>String full name of volunteer.</returns>
        public override string ToString()
        {
            return Name ?? string.Empty;
        }
    }
}
