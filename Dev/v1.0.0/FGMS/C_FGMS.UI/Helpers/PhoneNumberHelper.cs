using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace C_FGMS.UI.Helpers
{
    /// <summary>
    /// Phone Number Helper for commonly used methods
    /// </summary>
    /// <author>Nathan VanSnepson</author>
    /// <created>3/28/23</created>
    public static class PhoneNumberHelper
    {
        /// <summary>
        /// Checks validation of phone number
        /// </summary>
        /// <author>Chippi</author>
        /// <created>3/28/23</created>
        public static bool IsValidPhoneNumber(string text)
        {
            // Allow digits, spaces, hyphens, and parentheses
            return Regex.IsMatch(text, @"^[\d\s\-\(\)]*$");
        }
    }
}
