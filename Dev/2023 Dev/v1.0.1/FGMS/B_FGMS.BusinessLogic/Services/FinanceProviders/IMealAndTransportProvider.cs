using B_FGMS.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B_FGMS.BusinessLogic.Services.FinanceProviders
{
    /// <summary>
    /// This interface provides the required structure for our MealMileage Database interactions
    /// </summary>
    /// <author>Brendan Breuss</author>
    /// <created>2/15/2023</created>
    /// <modification>
    ///     <author>Brendan Breuss</author>
    ///     <date>3/21/23</date>
    ///         <change>Added function to allow database to be updated</change>
    /// </modification>
    public interface IMealAndTransportProvider
    {
        public event EventHandler<Events.ErrorEventArgs> DatabaseError;
        public IEnumerable<int> getMealAndTransportYears();
        public IEnumerable<MealAndTransportModel> getAllMealAndTransportDataForSelectedTime(int inputYear, int inputMonth);

        public IEnumerable<MealAndTransportModel> getAllMealAndTransportDataForSelectedVolunteer(int inputYear, int inputMonth, string volunteerName);

        public int getYearNumMeals(int year);

        public int getYearNumBusRides(int year);

        public decimal getYearNumMileage(int year);

        public IEnumerable<MealAndTransportModel> getAllMealAndTransportVolunteersForSelectedTime(int inputYear, int inputMonth);

        public void updateMealAndTransportDatabase(List<MealAndTransportModel> listMealAndTransportModels, int year, int monthIndex);
    }
}
