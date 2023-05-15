using A_FGMS.DataLayer;
using A_FGMS.DataLayer.Entities;
using B_FGMS.BusinessLogic.Constants;
using B_FGMS.BusinessLogic.Models;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace B_FGMS.BusinessLogic.Services.FinanceProviders
{


    public class DatabasePTOStipendRate : IPTOStipendRates
    {
        public event EventHandler<Events.ErrorEventArgs> DatabaseError;

        private readonly ApplicationDbContext _dbContext;

        public DatabasePTOStipendRate(ApplicationDbContext dbContext)
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
        /// Returns a clsPTOStipendRatesModel object containing the stipend rate and PTO rate for a given month and year.
        /// It queries the PTOStipendRates table in the database context (_dbContext) to retrieve a list of PTO stipend rates for dates earlier than or equal to the given month and year.
        /// </summary>
        /// <author>
        /// Nicklas Mortensen-Seguin
        /// </author>
        /// <date>
        /// Last modified: 4/3/23
        /// </date>
        /// 
        public clsPTOStipendRatesModel getStipendAndPTORateForAGivenMonth(int year, int month)
        {
            try
            {
                //We want to check to see if there are any possible rates in the database 
                var listOfclsPtoSipendRates = _dbContext.PTOStipendRates.Where(x => x.Date.Year <= year && x.Date.Month <= month)
                        .Select(x => new clsPTOStipendRatesModel { StipendRate = x.StipendRate, Date = x.Date, PTORate = x.PTORate }).OrderByDescending(x => x.Date);

                if (listOfclsPtoSipendRates.Any())
                {
                    return listOfclsPtoSipendRates.First();
                }
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._0901._message, ErrorMessages._0901._code);
                    return new clsPTOStipendRatesModel();
                }
                else
                {
                    OnDatabaseError(ErrorMessages._0901._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._0901._code);
                    return new clsPTOStipendRatesModel();
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._0902._message + e.Message, ErrorMessages._0902._code);
                return new clsPTOStipendRatesModel();
            }





            //if there isn't any rates set the rates to 0
            clsPTOStipendRatesModel clsDummyModel = new clsPTOStipendRatesModel();

            clsDummyModel.StipendRate = 0;
            clsDummyModel.PTORate = 0;

            return clsDummyModel;
        }


        /// <summary>
        /// Retrieves the total grant stipend and the remaining balance for the given year and month.
        /// </summary>
        /// <author>
        /// Nicklas Mortensen-Seguin
        /// </author>
        /// <date>
        /// Last Modified: 2/16/23
        /// </date>
        /// <param name="year">The year for which the grant stipend balance is required.</param>
        /// <param name="month">The month for which the grant stipend balance is required.</param>
        /// <returns>
        /// An array of two decimal values, where the first value represents the grant stipend balance before the specified month,
        /// and the second value represents the grant stipend balance at the end of the specified month.
        /// Returns an array with two zero values if no grant stipend information is found for the specified year.
        /// </returns>
        public decimal[] getTotalGrantStipend(int year, int month)
        {

            if (month >= 7)
            {
                try
                {
                    var GrantStipendfromDB = _dbContext.GrantStipends
                                            .Where(x => x.Date.Year == year)
                                            .Select(x => new GrantStipend { Date = x.Date, StartValue = x.StartValue, Tuid = x.Tuid });

                    if (GrantStipendfromDB.Any())
                    {

                    }
                    else
                    {

                        decimal[] badArray = { 0, 0 };
                        return badArray;
                    }


                    var decGrantYearStipendPaid = _dbContext.PTOStipends
                                                        .Where(x => x.Date.Year == year && x.Date.Month <= month)
                                                        .Select(x => x.StipendPaid).Sum(x => x);

                    var decGrantYearStipendPaidForPreviousMonth = _dbContext.PTOStipends
                                                                     .Where(x => x.Date.Year == year && x.Date.Month <= month - 1)
                                                                     .Select(x => x.StipendPaid).Sum(x => x);

                    decimal[] decArrayOfValues = { GrantStipendfromDB.First().StartValue - decGrantYearStipendPaidForPreviousMonth, GrantStipendfromDB.First().StartValue - decGrantYearStipendPaid };
                    return decArrayOfValues;

                }
                catch (SqlException e)
                {
                    if (e.ErrorCode == -2146232060)
                    {
                        OnDatabaseError(ErrorMessages._0903._message, ErrorMessages._0903._code);
                        return new decimal[0];
                    }
                    else
                    {
                        OnDatabaseError(ErrorMessages._0904._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._0904._code);
                        return new decimal[0];
                    }
                }
                catch (Exception e)
                {
                    OnDatabaseError(ErrorMessages._0905._message + e.Message, ErrorMessages._0905._code);
                    return new decimal[0];
                }
            }
            else
            {
                try
                {

                    var GrantStipendfromDB = _dbContext.GrantStipends
                            .Where(x => x.Date.Year == year - 1)
                            .Select(x => new GrantStipend { Date = x.Date, StartValue = x.StartValue, Tuid = x.Tuid });



                    if (GrantStipendfromDB.Any())
                    {

                    }
                    else
                    {

                        decimal[] badArray = { 0, 0 };
                        return badArray;


                    }



                    var decGrantYearStipendPaid = _dbContext.PTOStipends
                                                         .Where(x => (x.Date.Year == year && x.Date.Month <= month) || (x.Date.Year == year - 1 && x.Date.Month < 7))
                                                         .Select(x => x.StipendPaid).Sum(x => x);

                    var decGrantYearStipendPaidForPreviousMonth = _dbContext.PTOStipends
                                                                    .Where(x => (x.Date.Year == year && x.Date.Month <= month - 1) || (x.Date.Year == year - 1 && x.Date.Month - 1 < 7))
                                                                     .Select(x => x.StipendPaid).Sum(x => x);



                    decimal[] decArrayOfValues = { GrantStipendfromDB.First().StartValue - decGrantYearStipendPaidForPreviousMonth, GrantStipendfromDB.First().StartValue - decGrantYearStipendPaid };
                    return decArrayOfValues;
                }
                catch (SqlException e)
                {
                    if (e.ErrorCode == -2146232060)
                    {
                        OnDatabaseError(ErrorMessages._0903._message, ErrorMessages._0903._code);
                        return new decimal[0];
                    }
                    else
                    {
                        OnDatabaseError(ErrorMessages._0904._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._0904._code);
                        return new decimal[0];
                    }
                }
                catch (Exception e)
                {
                    OnDatabaseError(ErrorMessages._0905._message + e.Message, ErrorMessages._0905._code);
                    return new decimal[0];
                }
            }
        }


        public void PushPTOStipend(PTOStipendRate newRates)
        {
            try
            {
                var oldRates = _dbContext.PTOStipendRates.Where(x => x.Date.Month == newRates.Date.Month && x.Date.Year == newRates.Date.Year).FirstOrDefault();
                //Update the old if it already exists for the month.
                if (oldRates != null)
                {
                    oldRates.StipendRate = newRates.StipendRate;
                    oldRates.PTORate = newRates.PTORate;
                    _dbContext.SaveChanges();
                }
                else
                {
                    _dbContext.PTOStipendRates.Add(newRates);
                    _dbContext.SaveChanges();
                }
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._0906._message, ErrorMessages._0906._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._0907._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._0907._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._0908._message + e.Message, ErrorMessages._0908._code);
            }
        }


        public void AddTotalGrantStipend(GrantStipend grant)
        {
            try
            {
                DateTime grantStart = DateTime.Now;
                DateTime grantEnd = DateTime.Now;
                GrantStipend? oldGrant;
                //We are in a year later than July 1st, the year is unknown.
                if (grant.Date >= new DateTime(grant.Date.Year, 7, 1))
                {
                    grantStart = new DateTime(grant.Date.Year, 7, 1);
                    grantEnd = new DateTime(grant.Date.Year + 1, 7, 1);
                }
                //We are potentially earlier
                else
                {
                    grantStart = new DateTime(grant.Date.Year - 1, 7, 1);
                    grantEnd = new DateTime(grant.Date.Year, 7, 1);
                }


                oldGrant = _dbContext.GrantStipends.Where(x => x.Date < grantEnd && x.Date >= grantStart).FirstOrDefault();
                if (oldGrant != null)
                {
                    oldGrant.StartValue = grant.StartValue;
                    oldGrant.Date = grant.Date;
                    _dbContext.SaveChanges();
                    return;
                }
                _dbContext.GrantStipends.Add(grant);
                _dbContext.SaveChanges();
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._0909._message, ErrorMessages._0909._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._0910._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._0910._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._0911._message + e.Message, ErrorMessages._0911._code);
            }
        }
    }
}