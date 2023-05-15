using A_FGMS.DataLayer.Entities;
using B_FGMS.BusinessLogic.Models;
using B_FGMS.BusinessLogic.Models.Volunteer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B_FGMS.BusinessLogic.Services.FinanceProviders
{
    /// <summary>
    /// This interface provides the required structure for our MealTransport Rates Database interactions
    /// </summary>
    /// <author>Brendan Breuss</author>
    /// <created>2/15/2023</created>
    /// <modification>
    ///     <author>Tim Johnson</author>
    ///     <Date>3/29/2023</Date>
    ///         <change>PushMealTransportRates</change>
    /// </modification>
    public interface IMealAndTransportRatesProvider
    {
        public event EventHandler<Events.ErrorEventArgs> DatabaseError;
        public IEnumerable<MealAndTransportRatesModel> getMealAndTransportRatesDataForSelectedTime(int inputYear, int inputMonth);

        public void updateMealAndTransportRates(int year, int month, double updatedMealRate, double updatedMileageRate);
        public void PushMealTransportRates(MealTransportRate newRates);
    }
}
