using A_FGMS.DataLayer;
using A_FGMS.DataLayer.Entities;
using B_FGMS.BusinessLogic.Constants;
using B_FGMS.BusinessLogic.Models;
using B_FGMS.BusinessLogic.Models.Volunteer;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace B_FGMS.BusinessLogic.Services.FinanceProviders
{
    /// <summary>
    /// This class implements the IMealAndTransportRatesProvider interface to provide logic for getting 
    /// and setting Meal and Transport Rates as need between the application layer and data layer.
    /// </summary>
    /// <author>Brendan Breuss</author>
    /// <created>2/15/2023</created>
    /// <modification>
    /// <author>Tim Johnson</author>
    /// <modified>3/29/2023</modified>
    ///         <change>PushMealTransportRates Adds or Changes the Meal Transport Rates for the Month</change>
    /// </modification>
    public class DatabaseMealAndTransportRates : IMealAndTransportRatesProvider
    {
        private readonly ApplicationDbContext _dbContext;
        public event EventHandler<Events.ErrorEventArgs> DatabaseError;

        public DatabaseMealAndTransportRates(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// When called with invoke an event to inform user of an error
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <param name="errorCode"></param>
        /// <author>Nathan VanSnepson</author>
        /// <created>4/4/23</created>
        private void OnDatabaseError(string errorMessage, string errorCode)
        {
            DatabaseError?.Invoke(this, new Events.ErrorEventArgs(errorMessage, errorCode));
        }

        /// <summary>
        /// Function Name: getMealAndTransportRatesDataForSelectedTime
        /// This will return an Ienubeable MealAndTransportRatesModel that will contain both a meal rate
        /// ans a mileage rate for the given input month and year used to calculate meal amount
        /// and mileage amount
        /// </summary>
        /// <param name="inputYear">The desired year to search for the current rate</param>
        /// <param name="inputMonth">The desired month to search for the current rate</param>
        /// <author>Brendan Breuss</author>
        /// <created>2/15/2023</created>
        public IEnumerable<MealAndTransportRatesModel> getMealAndTransportRatesDataForSelectedTime(int inputYear, int inputMonth)
        {
            try
            {
                var item = _dbContext.MealTransportRates
                    .Where(y => (y.Date.Year == inputYear && y.Date.Month == inputMonth))
                    .Select(x => new MealAndTransportRatesModel
                    {
                        mealRate = x.MealRate,
                        mileageRate = x.MileageRate,
                        MealRatesDate = x.Date
                    }).ToList();
                return item;
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._5500._message, ErrorMessages._5500._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._5501._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._5501._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._5502._message + e.Message, ErrorMessages._5502._code);
            }

            return Enumerable.Empty<MealAndTransportRatesModel>();
        }


        #region DatabaseUpdates
        /// <summary>
        /// Function Name: updateMealAndTransportRates
        /// This will update both the meal and mileage rates for the given month and year to the database
        /// </summary>
        /// <param name="year"> the desired year to update</param>
        /// <param name="month">The desired month to update the rate in </param>
        /// <param name="updatedMealRate">The newly desired meal rate for given year and month</param>
        /// <param name="updatedMileageRate">The newly desire mileage rate </param>
        /// <author>Brendan Breuss</author>
        /// <created>2/15/2023</created>
        public void updateMealAndTransportRates(int year, int month, double updatedMealRate, double updatedMileageRate)
        {
            try
            {
                var updatedDatabase = _dbContext.MealTransportRates
                    .Where(y => (y.Date.Year == year && y.Date.Month == month)).First();

                updatedDatabase.MealRate = updatedMealRate;
                updatedDatabase.MileageRate = updatedMileageRate;

                _dbContext.SaveChanges();
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._5503._message, ErrorMessages._5503._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._5504._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._5504._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._5505._message + e.Message, ErrorMessages._5505._code);
            }
        }

        /// <summary>
        /// Function Name: PushMealTransportRates
        /// Purpose: Adds or Changes the Meal Transport Rates for the Month. 
        /// </summary>
        /// <param name="newRates">Item to be added or changed to</param>
        /// <author>Tim Johnson</author>
        /// <created>3/29/2023</created>
        public void PushMealTransportRates(MealTransportRate newRates)
        {
            try
            {
                var oldRates = _dbContext.MealTransportRates.Where(x => x.Date.Month == newRates.Date.Month && x.Date.Year == newRates.Date.Year).FirstOrDefault();
                //Update the old if it already exists for the month.
                if (oldRates != null)
                {
                    oldRates.MealRate = newRates.MealRate;
                    oldRates.BusMileageRate = newRates.BusMileageRate;
                    oldRates.MileageRate = newRates.MileageRate;
                    _dbContext.SaveChanges();
                }
                else
                {
                    _dbContext.MealTransportRates.Add(newRates);
                    _dbContext.SaveChanges();
                }
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._5506._message, ErrorMessages._5506._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._5507._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._5507._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._5508._message + e.Message, ErrorMessages._5508._code);
            }
        }
        #endregion


    }
}
