using A_FGMS.DataLayer;
using A_FGMS.DataLayer.Entities;
using B_FGMS.BusinessLogic.Constants;
using B_FGMS.BusinessLogic.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace B_FGMS.BusinessLogic.Services.FinanceProviders
{
    public class DatabasePTOProvider : IPTOProvider
    {
        private readonly ApplicationDbContext _dbContext;
        public event EventHandler<Events.ErrorEventArgs> DatabaseError;

        public DatabasePTOProvider(ApplicationDbContext dbContext)
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
        /// Retrieves a list of PTO information for a specific month and year.
        /// </summary>
        /// <author>
        /// Nicklas Mortensen-Seguin
        /// </author>
        /// <date>
        /// Last Modified: 2/16/23
        /// </date>
        /// <param name="year">The year for which the PTO information is required.</param>
        /// <param name="month">The month for which the PTO information is required.</param>
        /// <returns>
        /// An IEnumerable of clsPTOModel objects containing PTO information for the specified month and year.
        /// If the month is 0, it retrieves the PTO information for December of the previous year.
        /// </returns>

        public IEnumerable<clsPTOModel> getASpecificMonthOfPtoInfomation(int year, int month)
        {
            try
            {
                if (month == 0)
                {
                    year = year - 1;
                    month = 12;
                    var item = _dbContext.PTOStipends
                    .Where(y => (y.Date.Year == year && y.Date.Month == month))
                    .Select(selector: x => new clsPTOModel
                    {
                        Tuid = x.Tuid,
                        volunteerID = x.VolunteerTuid,
                        strFullName = x.Volunteer == null ? "N/A" : x.Volunteer.FullName,
                        RegularHours = x.RegularHours,
                        PtoEarned = x.PtoEarned,
                        PtoEnd = x.PtoEnd,
                        PtoStart = x.PtoStart,
                        PtoUsed = x.PtoUsed,
                        StipendPaid = x.StipendPaid,
                        YearToDateHour = x.YearToDateHour,
                        date = x.Date,
                        IsPTOEligible = x.IsPTOEligible
                    }).ToList();
                    return item;
                }
                else
                {
                    var item = _dbContext.PTOStipends
                    .Where(y => (y.Date.Year == year && y.Date.Month == month))
                    .Select(x => new clsPTOModel
                    {
                        Tuid = x.Tuid,
                        volunteerID = x.VolunteerTuid,
                        strFullName = x.Volunteer == null ? "N/A" : x.Volunteer.FullName,
                        RegularHours = x.RegularHours,
                        PtoEarned = x.PtoEarned,
                        PtoEnd = x.PtoEnd,
                        PtoStart = x.PtoStart,
                        PtoUsed = x.PtoUsed,
                        StipendPaid = x.StipendPaid,
                        YearToDateHour = x.YearToDateHour,
                        date = x.Date,
                        IsPTOEligible = x.IsPTOEligible
                    }).ToList();
                    return item;

                }
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._0800._message, ErrorMessages._0800._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._0801._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._0801._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._0802._message + e.Message, ErrorMessages._0802._code);
            }

            return Enumerable.Empty<clsPTOModel>();
        }


        /// <summary>
        /// Retrieves a list of distinct years present in the PTOStipends data.
        /// </summary>
        /// <author>
        /// Nicklas Mortensen-Seguin
        /// </author>
        /// <date>
        /// Last Modified: 2/16/23
        /// </date>
        /// <returns>
        /// An IEnumerable of integers representing the distinct years found in the PTOStipends data.
        /// </returns>
        public IEnumerable<int> getTotalYears()
        {
            try
            {
                var item = _dbContext.PTOStipends.Select(x => x.Date.Year).Distinct().ToList();
                return item;
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._0803._message, ErrorMessages._0803._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._0804._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._0804._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._0805._message + e.Message, ErrorMessages._0805._code);
            }

            return Enumerable.Empty<int>();
        }

        /// <summary>
        /// Updates the PTO information for a given list of PTO models, year, and month.
        /// The method iterates through the list of PTO models and updates the existing PTO information in the database.
        /// If a PTO record for a volunteer is not found in the database, a new record is added.
        /// </summary>
        /// <author>
        /// Nicklas Mortensen-Seguin
        /// </author>
        /// <date>
        /// Last Modified: 2/16/23
        /// </date>
        /// <param name="listOfPTOModels">A list of clsPTOModel objects containing the PTO information to be updated.</param>
        /// <param name="year">The year for which the PTO information needs to be updated.</param>
        /// <param name="month">The month for which the PTO information needs to be updated.</param>
        public void updatePTOInfomation(List<clsPTOModel> listOfPTOModels, int year, int month)
        {
            try
            {
                foreach (clsPTOModel model in listOfPTOModels)
                {
                    var PTOItemInDataBase = _dbContext.PTOStipends.Where(y => (y.VolunteerTuid == model.volunteerID) && (y.Date.Year == year && y.Date.Month == month));
                    if (PTOItemInDataBase.Any() == true)
                    {
                        var updatedItem = PTOItemInDataBase.First();

                        if (model.volunteerID != null && model.RegularHours != null && model.PtoStart != null && model.PtoUsed != null && model.PtoEnd != null && model.PtoEarned != null && model.StipendPaid != null && model.YearToDateHour != null)
                        {
                            updatedItem.VolunteerTuid = (int)model.volunteerID;
                            updatedItem.RegularHours = (decimal)model.RegularHours;
                            updatedItem.PtoStart = (decimal)model.PtoStart;
                            updatedItem.PtoUsed = (decimal)model.PtoUsed;
                            updatedItem.PtoEnd = (decimal)model.PtoEnd;
                            updatedItem.PtoEarned = (decimal)model.PtoEarned;
                            updatedItem.StipendPaid = (decimal)model.StipendPaid;
                            updatedItem.YearToDateHour = (decimal)model.YearToDateHour;

                        }
                        _dbContext.SaveChanges();
                    }
                    else
                    {
                        PTOStipend newPtoModel = new PTOStipend();


                        if (model.volunteerID != null && model.RegularHours != null && model.PtoStart != null && model.PtoUsed != null && model.PtoEnd != null && model.PtoEarned != null && model.StipendPaid != null && model.YearToDateHour != null && model.date != null)
                        {
                            newPtoModel.VolunteerTuid = (int)model.volunteerID;
                            newPtoModel.RegularHours = (decimal)model.RegularHours;
                            newPtoModel.PtoStart = (decimal)model.PtoStart;
                            newPtoModel.PtoUsed = (decimal)model.PtoUsed;
                            newPtoModel.PtoEnd = (decimal)model.PtoEnd;
                            newPtoModel.PtoEarned = (decimal)model.PtoEarned;
                            newPtoModel.StipendPaid = (decimal)model.StipendPaid;
                            newPtoModel.YearToDateHour = (decimal)model.YearToDateHour;
                            newPtoModel.Date = (DateTime)model.date;
                        }

                        _dbContext.Add(newPtoModel);
                        _dbContext.SaveChanges();
                    }
                }
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._0806._message, ErrorMessages._0806._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._0807._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._0807._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._0808._message + e.Message, ErrorMessages._0808._code);
            }
        }

        /// <summary>
        /// Retrieves the sum of year-to-date PTO stipends paid for a given year and month.
        /// </summary>
        /// <author>
        /// Nicklas Mortensen-Seguin
        /// </author>
        /// <date>
        /// Last Modified: 2/16/23
        /// </date>
        /// <param name="year">The year for which the year-to-date PTO stipends paid are required.</param>
        /// <param name="month">The month up to which the year-to-date PTO stipends paid are required.</param>
        /// <returns>
        /// A decimal value representing the sum of year-to-date PTO stipends paid for the specified year and month.
        /// </returns>

        public decimal getYdtPTOStipendsPaid(int year,int month)
        {
            try
            {
                var item = _dbContext.PTOStipends.Where(y => y.Date.Year == year && y.Date.Month <= month).Sum(x => x.StipendPaid);
                return item;
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._0809._message, ErrorMessages._0809._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._0810._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._0810._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._0811._message + e.Message, ErrorMessages._0811._code);
            }

            return -1;
        }




    }
}



   