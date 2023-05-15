using A_FGMS.DataLayer;
using A_FGMS.DataLayer.Entities;
using B_FGMS.BusinessLogic.BusinessLogicObjects;
using B_FGMS.BusinessLogic.Constants;
using B_FGMS.BusinessLogic.Models;
using B_FGMS.BusinessLogic.Models.Volunteer;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B_FGMS.BusinessLogic.Services.FinanceProviders
{
    /// <summary>
    /// This class implements the IInKindExpenseProvider interface to provide logic for getting and setting In Kind expenses as need between
    /// the application layer and data layer.
    /// </summary>
    /// <author>Andrew Loesel</author>
    /// <created>2/5/2023</created>
    /// <modification>
    ///     <author>Andrew Loesel</author>
    ///     <date>2/22/23</date>
    ///         <change>Finished functionality for getting all in kind items for both fiscal and grant year</change>
    /// </modification>
    public class DatabaseInKindExpenseProvider : IInKindExpenseProvider
    {
        private readonly ApplicationDbContext _dbContext; //provides the database interaction
        private readonly GeneralBusinessLogic generalBusinessLogic; //provides several methods for already implemented business logic
        public event EventHandler<Events.ErrorEventArgs> DatabaseError;

        /// <summary>
        /// Parameterized constructor for DatabaseInKindExpenseProvider
        /// </summary>
        /// <param name="dbContext">The runtime dbContext that will be provided by the service provider.</param>
        public DatabaseInKindExpenseProvider(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            generalBusinessLogic = new GeneralBusinessLogic();
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
        /// This function will use the dates of entries in the InKindExpense table to generate a list of year ranges (as strings) 
        /// </summary>
        /// <returns>An IEnumerable of strings of year ranges</returns>
        /// <author>Andrew Loesel</author>
        /// <created>2/18/23</created>
        public IEnumerable<string> GetFinancialYearRanges()
        {
            try
            {
                return _dbContext.InKindExpenses.Select(x => x.Date.Year + "-" + (x.Date.Year + 1)).Distinct().ToList();
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._0600._message, ErrorMessages._0600._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._0601._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._0601._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._0602._message + e.Message, ErrorMessages._0602._code);
            }
            
            return Enumerable.Empty<string>();
        }


        #region GetExpenseFunctions


        /// <summary>
        /// This method will create a MealTransportMileageModel whose properties reflect the Meal, Miles and Bus transport data
        /// of a given timeframe
        /// </summary>
        /// <param name="startDate">the start date of our timeframe</param>
        /// <param name="endDate">the end date of our timeframe</param>
        /// <param name="MealMilesTransport"></param>
        /// <returns>a MealTransportMileage model with statistical data for a time range</returns>
        /// <author>Andrew Loesel</author>
        /// <modified>Tyler Moody</modified>
        /// TODO: round the rates to nearest 100th
        private MealTransportMileageModel GetMealMileTransportForDateRange(DateTime startDate, DateTime endDate, IEnumerable<MealTransportMileageModel> MealMilesTransport, string quarter)
        {


            MealTransportMileageModel MealMileTransportItem = new MealTransportMileageModel();

            //Meals  
            double mealValue = MealMilesTransport.Where(x => x.strQuarter == quarter).Sum(x => x.dblMealValue());
            MealMileTransportItem.dbTotalMealValue += mealValue;
            MealMileTransportItem.strMealValue = mealValue.ToString("C", CultureInfo.CurrentCulture);

            //Miles
            double mileageValue = MealMilesTransport.Where(x => x.strQuarter == quarter).Sum(x => x.dblMileageValue());
            MealMileTransportItem.dbTotalMileageValue += mileageValue;
            MealMileTransportItem.strMileageValue = mileageValue.ToString("C", CultureInfo.CurrentCulture);

            //Bus Transportation
            double busValue = MealMilesTransport.Where(x => x.strQuarter == quarter).Sum(x => x.dblBusValue());
            MealMileTransportItem.intBusCount = MealMilesTransport.Where(x => x.strQuarter == quarter).Select(x => x.intBusCount).Sum();
            MealMileTransportItem.dbTotalBusValue += busValue;
            MealMileTransportItem.strBusValue = busValue.ToString("C", CultureInfo.CurrentCulture);

            //set date
            MealMileTransportItem.strDate = startDate.ToString("MM/dd/yyyy") + "-" + endDate.ToString("MM/dd/yyyy");

            return MealMileTransportItem;
        }

        /// <summary>
        /// This method will create a list of MealTransportMileage models, which are created by the GetMealMileTransportForDateRange
        /// method using the time frames that correspond to fiscal year quarters based off of the start and end year.
        /// </summary>
        /// <param name="intStartYear">start year for the fiscal year</param>
        /// <param name="intEndYear">end year for the fiscal year</param>
        /// <returns>A list of MealTransportMileage models</returns>
        /// <author>Andrew Loesel</author>
        /// <created>2/20/23</created>
        public List<MealTransportMileageModel> GetAllInKindForFiscalYear(int intStartYear, int intEndYear)
        {
                //DateTime objects are going to be in pairs where the first(even #) is the start date of a quarter and second is end date
                DateTime[] fiscalYearQuarters = generalBusinessLogic.GetFiscalYearQuarters(intStartYear, intEndYear);

                return GetAllInKindForQuarters(fiscalYearQuarters, true);
        }

        /// <summary>
        /// This method will create a list of MealTransportMileage models, which are created by the GetMealMileTransportForDateRange
        /// method using the time frames that correspond to grant year quarters based off of the start and end year.
        /// </summary>
        /// <param name="intStartYear">start year for the fiscal year</param>
        /// <param name="intEndYear">end year for the fiscal year</param>
        /// <returns>A list of MealTransportMileage models</returns>
        /// <author>Andrew Loesel</author>
        /// <created>2/22/23</created>
        public List<MealTransportMileageModel> GetAllInKindForGrantYear(int intStartYear, int intEndYear)
        {
            //DateTime objects are going to be in pairs where the first(even #) is the start date of a quarter and second is end date
            DateTime[] grantYearQuarters = generalBusinessLogic.GetGrantYearQuarters(intStartYear, intEndYear);

            return GetAllInKindForQuarters(grantYearQuarters, false);
        }

        /// <summary>
        /// Gets all in kind expenses depending by quarter.
        /// </summary>
        /// <param name="quarters">Quarters to get expenses for.</param>
        /// <param name="isFiscal">True if fiscal, false if grant.</param>
        /// <author>Tyler Moody</author>
        /// <created>4/1/2023</created>
        /// <modified>Tyler Moody</modified>
        /// <returns>The list of quarterly expenses.</returns>
        private List<MealTransportMileageModel> GetAllInKindForQuarters(DateTime[] quarters, bool isFiscal)
        {
            try
            {
                var MealMilesTransport = _dbContext.MealMileages
                    .Where(m => m.Date >= quarters[0] && m.Date <= quarters[7])
                    .OrderBy(m => m.Date)
                    .Select(m => new
                    {
                        MealMileage = m,
                        MealTransportRate = _dbContext.MealTransportRates
                            .Where(r => r.Date <= m.Date)
                            .OrderByDescending(r => r.Date)
                            .FirstOrDefault()
                    })
                    .Select(x => new MealTransportMileageModel
                    {
                        strQuarter = isFiscal ? generalBusinessLogic.GetFiscalYearQuarter(x.MealMileage.Date) : generalBusinessLogic.GetGrantYearQuarter(x.MealMileage.Date),
                        intMealCount = x.MealMileage.MealCount,
                        dblMealRate = x.MealTransportRate != null ? x.MealTransportRate.MealRate : 0.0,
                        strMealValue = x.MealTransportRate != null ? (x.MealMileage.MealCount * x.MealTransportRate.MealRate).ToString("C") : (0).ToString("C"),
                        intBusCount = x.MealMileage.BusRideCount,
                        dblBusRate = x.MealTransportRate != null ? x.MealTransportRate.BusMileageRate : 0.0,
                        strBusValue = x.MealTransportRate != null ? (x.MealMileage.BusRideCount * x.MealTransportRate.BusMileageRate).ToString("C") : (0).ToString("C"),
                        intMileCount = (int)x.MealMileage.Mileage,
                        dblMileRate = x.MealTransportRate != null ? x.MealTransportRate.MileageRate : 0.0,
                        strMileageValue = x.MealTransportRate != null ? ((int)x.MealMileage.Mileage * x.MealTransportRate.MileageRate).ToString("C") : (0).ToString("C"),
                    });

                MealMilesTransport.ToList();

                //loop through our date array, incrementing by two. Use our dates and Query Results to get a quarterly item with all In Kind data for that quarter
                return CreateQuarterSummary(MealMilesTransport, quarters, isFiscal);

            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._0603._message, ErrorMessages._0603._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._0604._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._0604._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._0605._message + e.Message, ErrorMessages._0605._code);
            }
            return new List<MealTransportMileageModel>();
        }

        /// <summary>
        /// Loops through the quarters and creates a summary for each.
        /// </summary>
        /// <param name="quarters">Quarter boundries.</param>
        /// <param name="isFiscal">True if fiscal, false if grant.</param>
        /// <param name="MealMilesTransport"></param>
        /// <author>Tyler Moody</author>
        /// <created>4/4/23</created>
        private List<MealTransportMileageModel> CreateQuarterSummary(IQueryable<MealTransportMileageModel> MealMilesTransport, DateTime[] quarters, bool isFiscal)
        {
            List<MealTransportMileageModel> QuarterlyExpenses = new List<MealTransportMileageModel>();

            string quarterPrefix = "Quarter ";

            for (int i = 0; i < quarters.Length; i += 2)
            {
                string currentQuarter = isFiscal ? generalBusinessLogic.GetFiscalYearQuarter(quarters[i]) : generalBusinessLogic.GetGrantYearQuarter(quarters[i]);
                MealTransportMileageModel MealMileTransportItem = GetMealMileTransportForDateRange(quarters[i], quarters[i + 1], MealMilesTransport, currentQuarter);
                MealMileTransportItem.strQuarter = quarterPrefix + (i / 2 + 1);
                QuarterlyExpenses.Add(MealMileTransportItem);
            }

            return QuarterlyExpenses;
        }

        #endregion

        #region GetExpenseByTypeForDates

        /// <summary>
        /// This method will return a List of MealTransportMileageModel items that have a date in between the given start and end date.
        /// The objects only have their Meal and generic fields populated with data
        /// </summary>
        /// <param name="startDate">the start date of the search period</param>
        /// <param name="endDate">end date of the search period</param>
        /// <param name="isFiscal">True if fiscal, false if grant.</param>
        /// <returns>a List of MealTransportMileage models</returns>
        /// <author>Andrew Loesel</author>
        /// <created>2/25/23</created>
        public List<FinanceFocusGridModel> GetMealInKindForDates(DateTime? startDate, DateTime? endDate, bool isFiscal)
        {
            try
            {
                var mealMilesTransport = from m in _dbContext.MealMileages
                                         where (!startDate.HasValue || m.Date >= startDate) && (!endDate.HasValue || m.Date <= endDate)
                                         orderby m.Date ascending
                                         let mealTransportRate = (from r in _dbContext.MealTransportRates
                                                                  where r.Date <= m.Date
                                                                  orderby r.Date descending
                                                                  select r).FirstOrDefault()
                                         select new FinanceFocusGridModel
                                         {
                                             Count = m.MealCount,
                                             Rate = mealTransportRate != null ? mealTransportRate.MealRate : 0.0,
                                             Value = (m.MealCount * (mealTransportRate != null ? mealTransportRate.MealRate : 0.0)),
                                             Date = m.Date.ToString("MM/dd/yyyy"),
                                             Quarter = isFiscal ? generalBusinessLogic.GetFiscalYearQuarter(m.Date) : generalBusinessLogic.GetGrantYearQuarter(m.Date),
                                             Name = m.Volunteer != null ? m.Volunteer.FullName : "Missing Name"
                                         };

                return mealMilesTransport.ToList();
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._0606._message, ErrorMessages._0606._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._0607._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._0607._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._0608._message + e.Message, ErrorMessages._0608._code);
            }

            return new List<FinanceFocusGridModel>();
        }

        /// <summary>
        /// This method will return a List of MealTransportMileageModel items that have a date in between the given start and end date.
        /// The objects only have their Mileage and generic fields populated with data
        /// </summary>
        /// <param name="startDate">the start date of the search period</param>
        /// <param name="endDate">end date of the search period</param>
        /// <param name="isFiscal">True if fiscal, false if grant.</param>
        /// <returns>a List of MealTransportMileage models</returns>
        /// <author>Andrew Loesel</author>
        /// <created>2/25/23</created>
        public List<FinanceFocusGridModel> GetMileageForDates(DateTime? startDate, DateTime? endDate, bool isFiscal)
        {
            try
            {
                var mealMilesTransport = from m in _dbContext.MealMileages
                                         where (!startDate.HasValue || m.Date >= startDate) && (!endDate.HasValue || m.Date <= endDate)
                                         orderby m.Date ascending
                                         let mealTransportRate = (from r in _dbContext.MealTransportRates
                                                                  where r.Date <= m.Date
                                                                  orderby r.Date descending
                                                                  select r).FirstOrDefault()
                                         select new FinanceFocusGridModel
                                         {
                                             Count = m.MealCount,
                                             Rate = mealTransportRate != null ? mealTransportRate.MileageRate : 0.0,
                                             Value = ((int)m.Mileage * (mealTransportRate != null ? mealTransportRate.MileageRate : 0.0)),
                                             Date = m.Date.ToString("MM/dd/yyyy"),
                                             Quarter = isFiscal ? generalBusinessLogic.GetFiscalYearQuarter(m.Date) : generalBusinessLogic.GetGrantYearQuarter(m.Date),
                                             Name = m.Volunteer != null ? m.Volunteer.FullName : "Missing Name"
                                         };

                return mealMilesTransport.ToList();
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._0609._message, ErrorMessages._0609._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._0610._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._0610._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._0611._message + e.Message, ErrorMessages._0611._code);
            }

            return new List<FinanceFocusGridModel>();
        }

        /// <summary>
        /// This method will return a List of MealTransportMileageModel items that have a date in between the given start and end date.
        /// The objects only have their bus transportation and generic fields populated with data
        /// </summary>
        /// <param name="startDate">the start date of the search period</param>
        /// <param name="endDate">end date of the search period</param>
        /// <param name="isFiscal">True if fiscal, false if grant.</param>
        /// <returns>a List of MealTransportMileage models</returns>
        /// <author>Andrew Loesel</author>
        /// <created>2/25/23</created>
        public List<FinanceFocusGridModel> GetBusTransportForDates(DateTime? startDate, DateTime? endDate, bool isFiscal)
        {
            try
            {
                var mealMilesTransport = from m in _dbContext.MealMileages
                                         where (!startDate.HasValue || m.Date >= startDate) && (!endDate.HasValue || m.Date <= endDate)
                                         orderby m.Date ascending
                                         let mealTransportRate = (from r in _dbContext.MealTransportRates
                                                                  where r.Date <= m.Date
                                                                  orderby r.Date descending
                                                                  select r).FirstOrDefault()
                                         select new FinanceFocusGridModel
                                         {
                                             Count = m.BusRideCount,
                                             Rate = mealTransportRate != null ? mealTransportRate.BusMileageRate : 0.0,
                                             Value = (m.BusRideCount * (mealTransportRate != null ? mealTransportRate.BusMileageRate : 0.0)),
                                             Date = m.Date.ToString("MM/dd/yyyy"),
                                             Quarter = isFiscal ? generalBusinessLogic.GetFiscalYearQuarter(m.Date) : generalBusinessLogic.GetGrantYearQuarter(m.Date),
                                             Name = m.Volunteer != null ? m.Volunteer.FullName : "Missing Name"
                                         };

                return mealMilesTransport.ToList();
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._0612._message, ErrorMessages._0612._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._0613._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._0613._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._0614._message + e.Message, ErrorMessages._0614._code);
            }

            return new List<FinanceFocusGridModel>();
        }


        #endregion
    }
}
