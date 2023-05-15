using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Data;

namespace C_FGMS.UI.Converters
{
    /// <FileName> StringToPhoneConverter.cs  </FileName>
    /// <PartOfProject> CS471 Senior Capstone Project / FGMS UI</PartOfProject>
    /// <DateCreated> 03/31/2023 </DateCreated>
    /// <AdditionalContributors> CS471 WI23 Development Team </AdditionalContributors>
    /// <LastModified> 03/31/2023 </LastModified>
    /// <LastModifiedBy> Isabelle Johns </LastModifiedBy>
    /// <summary>
    /// Used to format string values to proper phone format in xaml files.
    /// </summary>
    /// <author> Isabelle Johns </author>
    public class StringToPhoneConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return string.Empty;

            // Strips the string to only digits
            string phoneNo = value.ToString().Replace("(", string.Empty).Replace(")", string.Empty).Replace(" ", string.Empty).Replace("-", string.Empty);

            // Formats the number depending on the length
            switch (phoneNo.Length)
            {
                case 3:
                    return Regex.Replace(phoneNo, @"(\d{3})", "$1");
                case 4:
                    return Regex.Replace(phoneNo, @"(\d{3})(\d{1})", "$1-$2");
                case 5:
                    return Regex.Replace(phoneNo, @"(\d{3})(\d{2})", "$1-$2");
                case 6:
                    return Regex.Replace(phoneNo, @"(\d{3})(\d{3})", "$1-$2");
                case 7:
                    return Regex.Replace(phoneNo, @"(\d{3})(\d{4})", "$1-$2");
                case 8:
                    return Regex.Replace(phoneNo, @"(\d{3})(\d{3})(\d{2})", "($1) $2-$3");
                case 9:
                    return Regex.Replace(phoneNo, @"(\d{3})(\d{3})(\d{3})", "($1) $2-$3");
                case 10:
                    return Regex.Replace(phoneNo, @"(\d{3})(\d{3})(\d{4})", "($1) $2-$3");
                default:
                    return phoneNo;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
