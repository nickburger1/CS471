using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

/// <FileName> BoolToBoolOppositeConverter.cs  </FileName>
/// <PartOfProject> CS471 Senior Capstone Project / FGMS UI</PartOfProject>
/// <DateCreated> 03/05/2023 </DateCreated>
/// <AdditionalContributors> CS471 WI23 Development Team </AdditionalContributors>
/// <LastModified> 03/05/2023 </LastModified>
/// <LastModifiedBy> Tyler Moody </LastModifiedBy>
/// <summary>
/// Used to reverse the boolean value in xaml files.
/// </summary>
/// <author> Tyler Moody </author>
namespace C_FGMS.UI.Converters
{
    public class BoolToOppositeBoolConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(bool))
                throw new InvalidOperationException("The target must be a boolean");

            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
