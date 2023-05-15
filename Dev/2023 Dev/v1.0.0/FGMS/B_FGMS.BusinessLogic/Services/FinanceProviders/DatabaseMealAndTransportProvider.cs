using A_FGMS.DataLayer;
using A_FGMS.DataLayer.Entities;
using B_FGMS.BusinessLogic.BusinessLogicObjects;
using B_FGMS.BusinessLogic.Constants;
using B_FGMS.BusinessLogic.Models;
using DocumentFormat.OpenXml.Bibliography;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B_FGMS.BusinessLogic.Services.FinanceProviders
{
    /// <summary>
    /// This class implements the IMealAndTransportProvider interface to provide logic for getting and setting Meal and Transport values as need between
    /// the application layer and data layer.
    /// </summary>
    /// <author>Brendan Breuss</author>
    /// <created>2/15/2023</created>
    /// <modification>
    ///     <author>Brendan Breuss</author>
    ///     <date>3/21/23</date>
    ///         <change>Added function to allow database to be updated</change>
    /// </modification>
    public class DatabaseMealAndTransportProvider : IMealAndTransportProvider
    {
        private readonly ApplicationDbContext _dbContext;
        public event EventHandler<Events.ErrorEventArgs> DatabaseError;

        /// <summary>
        /// Parameterized constructor for DatabaseInKindExpenseProvider
        /// </summary>
        /// <param name="dbContext">The runtime dbContext that will be provided by the service provider.</param>
        public DatabaseMealAndTransportProvider(ApplicationDbContext dbContext)
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
        /// Function Name: getMealAndTransportYears
        /// This function will use the dates of entries in MealMileages table and return them as list of years
        /// </summary>
        /// <returns>An IEnumerable of ints of all years</returns>
        /// <author>Brendan Breuss</author>
        /// <created>2/15/2023</created>
        public IEnumerable<int> getMealAndTransportYears()
        {
            try
            {
                var item = _dbContext.MealMileages.Select(x => x.Date.Year).Distinct().ToList();
                return item;
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._0700._message, ErrorMessages._0700._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._0701._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._0701._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._0702._message + e.Message, ErrorMessages._0702._code);
            }

            return Enumerable.Empty<int>();
        }

        #region Gets Volunteer Info

        /// <summary>
        /// Function Name: getAllMealAndTransportDataForSelectedTime
        /// Method will look for all volunteers with data in the given month and year range
        /// Will return an IEnumerable the volunteers coresponding model
        /// </summary>
        /// <param name="inputMonth">The month desired to be searched for</param>
        /// <param name="inputYear">The year desired to be searched for</param>
        /// <returns>An IEnumerable of all MealAndTransportModels for the selected year and month </returns>
        /// <author>Brendan Breuss</author>
        /// <created>02/16/2023</created>
        public IEnumerable<MealAndTransportModel> getAllMealAndTransportDataForSelectedTime(int inputYear, int inputMonth)
        {
            try
            {
                var item = _dbContext.MealMileages
                .Where(y => (y.Date.Year == inputYear && y.Date.Month == inputMonth))
                .Select(x => new MealAndTransportModel
                {
                    volunteerID = x.VolunteerTuid,
                    strVolunteerName = x.Volunteer == null ? "N/A" : x.Volunteer.LastName + ", " + x.Volunteer.FirstName,
                    numMeals = x.MealCount,
                    numBusRides = x.BusRideCount,
                    Mileage = x.Mileage,
                    date = x.Date

                });
                return item;
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._0703._message, ErrorMessages._0703._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._0704._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._0704._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._0705._message + e.Message, ErrorMessages._0705._code);
            }

            return Enumerable.Empty<MealAndTransportModel>();
        }

        /// <summary>
        /// Function Name: getAllMealAndTransportVolunteersForSelectedTime
        /// Method will look for all volunteers with data in the given month and year range
        /// Will return an IEnumerable of the volunteers name and tuid to populate comboboxes
        /// </summary>
        /// <param name="inputMonth">The month desired to be searched for</param>
        /// <param name="inputYear">The year desired to be searched for</param>
        /// <returns>An IEnumerable of all MealAndTransportModels for the selected year and month </returns>
        /// <author>Brendan Breuss</author>
        /// <created>02/16/2023</created>
        public IEnumerable<MealAndTransportModel> getAllMealAndTransportVolunteersForSelectedTime(int inputYear, int inputMonth)
        {
            try
            {
                var item = _dbContext.MealMileages
                .Where(y => (y.Date.Year == inputYear && y.Date.Month == inputMonth))
                .Select(x => new MealAndTransportModel
                {
                    strVolunteerName = x.Volunteer == null ? "N/A" : x.Volunteer.LastName + ", " + x.Volunteer.FirstName,
                    volunteerID = x.VolunteerTuid
                });
                return item;
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._0706._message, ErrorMessages._0706._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._0707._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._0707._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._0708._message + e.Message, ErrorMessages._0708._code);
            }

            return Enumerable.Empty<MealAndTransportModel>();
        }

        /// <summary>
        /// Function Name: getAllMealAndTransportDataForSelectedVolunteer
        /// Method will look for a single volunteer with data in the given month and year range and having given name
        /// Will return an IEnumerable of the single volunteer
        /// </summary>
        /// <param name="inputMonth">The month desired to be searched for</param>
        /// <param name="inputYear">The year desired to be searched for</param>
        /// <param name="volunteerName">The specific volunteer desired to be searched</param>
        /// <returns>An IEnumerable of the single MealAndTransportModel for the selected year, month and name</returns>
        /// <author>Brendan Breuss</author>
        /// <created>02/18/2023</created>
        public IEnumerable<MealAndTransportModel> getAllMealAndTransportDataForSelectedVolunteer(int inputYear, int inputMonth, String volunteerName)
        {
            try
            {
                var item = _dbContext.MealMileages
                .Where(y => (y.Date.Year == inputYear && y.Date.Month == inputMonth && (y.Volunteer == null ? false : y.Volunteer.LastName + ", " + y.Volunteer.FirstName == volunteerName)))
                .Select(x => new MealAndTransportModel
                {
                    volunteerID = x.VolunteerTuid,
                    strVolunteerName = x.Volunteer == null ? "N/A" : x.Volunteer.LastName + ", " + x.Volunteer.FirstName,
                    numMeals = x.MealCount,
                    numBusRides = x.BusRideCount,
                    Mileage = x.Mileage
                });
                return item;
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._0709._message, ErrorMessages._0709._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._0710._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._0710._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._0711._message + e.Message, ErrorMessages._0711._code);
            }

            return Enumerable.Empty<MealAndTransportModel>();
        }

        #endregion



        #region YearlyValues

        /// <summary>
        /// Function Name: getYearNumMeals
        /// Returns all of the NumMeals in a given year
        /// <author>Brendan Breuss</author>
        /// <Created>03/13/2023</Created>
        /// </summary>
        /// <param name="year"> The desired year to search</param>
        public int getYearNumMeals(int year)
        {
            try
            {
                var item = _dbContext.MealMileages.Where(y => y.Date.Year == year).Sum(x => x.MealCount);
                return item;
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._0712._message, ErrorMessages._0712._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._0713._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._0713._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._0714._message + e.Message, ErrorMessages._0714._code);
            }

            return -1;
        }

        /// <summary>
        /// Function Name: getYearNumBusRides
        /// Returns all of the num bus rides in a given year
        /// <author>Brendan Breuss</author>
        /// <Created>03/13/2023</Created>
        /// </summary>
        /// <param name="year"> The desired year to search</param>
        public int getYearNumBusRides(int year)
        {
            try
            {
                var item = _dbContext.MealMileages.Where(y => y.Date.Year == year).Sum(x => x.BusRideCount);
                return item;
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._0715._message, ErrorMessages._0715._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._0716._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._0716._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._0717._message + e.Message, ErrorMessages._0717._code);
            }

            return -1;
        }

        /// <summary>
        /// Function Name: getYearNumMileage
        /// Returns all of the mileages in a given year
        /// <author>Brendan Breuss</author>
        /// <Created>03/13/2023</Created>
        /// </summary>
        /// <param name="year"> The desired year to search</param>
        public decimal getYearNumMileage(int year)
        {

            try
            {
                var item = _dbContext.MealMileages.Where(y => y.Date.Year == year).Sum(x => x.Mileage);
                return item;
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._0718._message, ErrorMessages._0718._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._0719._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._0719._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._0720._message + e.Message, ErrorMessages._0720._code);
            }

            return -1;
        }

        #endregion



        #region UpdateVolunteers

        /// <summary>
        /// Function Name: updateMealAndTransportDatabase
        /// This will check if there is an instance of a volunteer in the database that matches the current volunteer
        /// we are on in the list of volunteers for the current month and year passed to the function. If the volunteer
        /// exists in the database then update the volunteer in the database. If there is no instance of the volunteer
        /// Then create a new volunteer Meal transport model for the volunteer and add it to the database. 
        /// <author>Brendan Breuss</author>
        /// <created>03/29/2023</created>
        /// </summary>
        /// <param name="listMealAndTransportModels">The list of volunteer models to loop through to check 
        /// if they exist in the database</param>
        /// <param name="year">The year to check the database for</param>
        /// <param name="monthIndex">the month to check the database for</param>
        public void updateMealAndTransportDatabase(List<MealAndTransportModel> listMealAndTransportModels, int year, int monthIndex)
        {
            try
            {
                foreach (MealAndTransportModel volunteer in listMealAndTransportModels)
                {
                    var checkDataBaseForVolunteer = _dbContext.MealMileages.Where(y => (y.VolunteerTuid == volunteer.volunteerID) &&
                    (y.Date.Year == year && y.Date.Month == monthIndex));
                    if (checkDataBaseForVolunteer.Any() == true)
                    {
                        var volunteerToUpdate = checkDataBaseForVolunteer.First();
                        if (volunteer.numMeals != null && volunteer.numBusRides != null && volunteer.Mileage != null)
                        {
                            volunteerToUpdate.MealCount = (int)volunteer.numMeals;
                            volunteerToUpdate.BusRideCount = (int)volunteer.numBusRides;
                            volunteerToUpdate.Mileage = (decimal)volunteer.Mileage;
                        }
                        _dbContext.SaveChanges();
                    }
                    else
                    {
                        MealMileage newMealMileage = new MealMileage();
                        if (volunteer.numMeals != null && volunteer.numBusRides != null && volunteer.Mileage != null && volunteer.volunteerID != null)
                        {
                            newMealMileage.VolunteerTuid = (int)volunteer.volunteerID;
                            newMealMileage.MealCount = (int)volunteer.numMeals;
                            newMealMileage.BusRideCount = (int)volunteer.numBusRides;
                            newMealMileage.Mileage = (decimal)volunteer.Mileage;
                            newMealMileage.Date = volunteer.date;
                            _dbContext.Add(newMealMileage);
                            _dbContext.SaveChanges();
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._0721._message, ErrorMessages._0721._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._0722._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._0722._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._0723._message + e.Message, ErrorMessages._0723._code);
            }
        }

        #endregion
    }
}
