using A_FGMS.DataLayer;
using A_FGMS.DataLayer.Entities;
using B_FGMS.BusinessLogic.Constants;
using B_FGMS.BusinessLogic.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B_FGMS.BusinessLogic.Services.FinanceProviders
{
    /// <summary>
    /// This class will provider the database interaction functionality for in-kind expenses that aren't meal, mileage or bus.
    /// </summary>
    /// <author>Andrew Loesel</author>
    /// <created>2/23/2023</created>
    public class DatabaseExpenseProvider : IExpenseProvider
    {
        private readonly ApplicationDbContext _dbContext;
        public event EventHandler<Events.ErrorEventArgs> DatabaseError;

        public DatabaseExpenseProvider(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// This method will return an IEnumerable container containing all expenseTypes from the database
        /// </summary>
        /// <returns>an IEnumerable container of ExpenseTypeModel type</returns>
        /// <author>Andrew Loesel</author>
        /// <created>2/23/23</created>
        public List<ExpenseTypeModel> GetAllExpenseTypes()
        {
            try
            {
                List<ExpenseTypeModel> expenseTypes = new List<ExpenseTypeModel>();
                expenseTypes.AddRange(_dbContext.ExpenseTypeItems.OrderBy(x => x.Name).Select(x => GetExpenseTypeModel(x)).Distinct().ToList());

                return expenseTypes;
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._0500._message, ErrorMessages._0500._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._0501._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._0501._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._0502._message + e.Message, ErrorMessages._0502._code);
            }

            return new List<ExpenseTypeModel>();
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
        /// 
        /// </summary>
        /// <param name="tuid"></param>
        /// <returns></returns>
        /// <author>Tim Johnson?</author>
        public InKindExpense? GetExpenseByExpenseTypeId(int tuid)
        {
            try
            {
                return _dbContext.InKindExpenses.FirstOrDefault(x => x.ExpenseTypeTuid == tuid);
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._0512._message, ErrorMessages._0512._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._0513._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._0513._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._0514._message + e.Message, ErrorMessages._0514._code);
            }

            return null;
        }

        /// <summary>
        /// This method will add an expense with the data provided in the parameters
        /// </summary>
        /// <author>Tim Johnson</author>
        /// <created>3/29/2023</created>
        public void AddNewExpenseType(String name)
        {
            try
            {
                var existingItem = _dbContext.ExpenseTypeItems.FirstOrDefault(x => x.Name.Equals(name));
                if (existingItem == null)
                {
                    var newItem = new ExpenseTypeItem()
                    {
                        Name = name,
                        Description = ""
                    };

                    // Add new expense and save
                    _dbContext.ExpenseTypeItems.Add(newItem);
                    _dbContext.SaveChanges();
                }
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._0503._message, ErrorMessages._0503._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._0504._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._0504._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._0505._message + e.Message, ErrorMessages._0505._code);
            }
        }

        /// <summary>
        /// This method will delete an expense with the data provided in the parameters
        /// </summary>
        /// <author>Tim Johnson</author>
        /// <created>3/23/2023</created>
        public void DelExpenseType(ExpenseTypeModel item)
        {
            try
            {
                var existingItem = _dbContext.ExpenseTypeItems.FirstOrDefault(x => x.Tuid.Equals(item.Tuid));
                if (existingItem != null)
                {
                    //delete
                    _dbContext.ExpenseTypeItems.Remove(existingItem);
                    _dbContext.SaveChanges();
                }
                else
                {
                    //Validation?
                }
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._0506._message, ErrorMessages._0506._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._0507._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._0507._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._0508._message + e.Message, ErrorMessages._0508._code);
            }
        }

        #region CostShareCRUD
            /// <summary>
            /// This method will return an enumerable of SchoolCostShareModels for the provided time range.
            /// </summary>
            /// <param name="startDate">the start date of the desired range</param>
            /// <param name="endDate">the end date of the desired range</param>
            /// <returns>An IEnumerable collection of the cost shares in this time range</returns>
            /// <author>Andrew Loesel</author>
            /// <Created>3/19/2023</Created>
        public IEnumerable<SchoolCostShareModel> GetCostShares(DateTime startDate, DateTime endDate)
        {
            try
            {
                return _dbContext.SchoolCostShares.Where(x => x.Date >= startDate && x.Date <= endDate)
                    .Select(x => new SchoolCostShareModel { Name = x.Name, Date = x.Date, Tuid = x.Tuid, Value = x.Value })
                    .ToList();
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._0509._message, ErrorMessages._0509._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._0510._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._0510._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._0511._message + e.Message, ErrorMessages._0511._code);
            }

            return Enumerable.Empty<SchoolCostShareModel>();
        }

        /// <summary>
        /// This method will update a school cost share item that has the TUID intTuid with the provided paramenters.
        /// </summary>
        /// <param name="date">the new date</param>
        /// <param name="strName">the new name</param>
        /// <param name="dblValue">the new value</param>
        /// <param name="intTuid">the tuid of the item we want to update</param>
        /// <returns> true if the cost share was successfully updated, otherwise false</returns>
        /// <author>Andrew Loesel</author>
        /// <created>3/21/2023</created>
        public bool UpdateCostShare(DateTime date, string strName, double dblValue, int intTuid)
        {
            try
            {
                if (!string.IsNullOrEmpty(strName))
                {
                    //try to get the entry using our passed in tuid
                    var desiredEntry = _dbContext.SchoolCostShares.SingleOrDefault(x => x.Tuid == intTuid);
                    if (desiredEntry != null)
                    {
                        desiredEntry.Value = dblValue;
                        desiredEntry.Name = strName;
                        desiredEntry.Date = date;
                        _dbContext.SaveChanges();
                        return true;
                    }
                }
                return false;
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._0518._message, ErrorMessages._0518._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._0519._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._0519._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._0520._message + e.Message, ErrorMessages._0520._code);
            }

            return false;
        }


        /// <summary>
        /// This method will add a school cost share item into the database
        /// </summary>
        /// <param name="date">the date of the school cost share billing</param>
        /// <param name="strName">the name of the school cost share</param>
        /// <param name="dblValue">the value of the school cost share</param>
        /// <returns>true if the cost share was added, which it always should be...?</returns>
        /// <author>Andrew Loesel</author>
        /// <created>3/20/2023</created>
        public bool AddCostShare(DateTime date, string strName, double dblValue)
        {
            try
            {
                SchoolCostShare newCostShare = new SchoolCostShare
                {
                    Name = strName,
                    Date = date,
                    Value = dblValue
                };
                _dbContext.SchoolCostShares.Add(newCostShare);
                _dbContext.SaveChanges();
                //this always returns true
                return true;
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._0521._message, ErrorMessages._0521._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._0522._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._0522._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._0523._message + e.Message, ErrorMessages._0523._code);
            }

            return false;
        }

        /// <summary>
        /// This method will delete a cost share if a cost share with the provided tuid is found
        /// </summary>
        /// <param name="intCostShareTuid"></param>
        /// <returns>true if the cost share was deleted, otherwise false</returns>
        /// <author>Andrew Loesel</author>
        /// <created>4/2/2023</created>
        public bool DeleteCostShare(int intCostShareTuid)
        {
            try
            {
                //get the cost share
                SchoolCostShare? deleteCostShare = _dbContext.SchoolCostShares.SingleOrDefault(x => x.Tuid == intCostShareTuid);
                //if it was found then we can remove it
                if (deleteCostShare != null)
                {
                    _dbContext.SchoolCostShares.Remove(deleteCostShare);
                    _dbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._0524._message, ErrorMessages._0524._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._0525._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._0525._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._0526._message + e.Message, ErrorMessages._0526._code);
            }

            return false;
        }

        #endregion

        #region InKindExpensesCRUD
        
        /// <summary>
        /// This method will add an expense with the data provided in the parameters
        /// </summary>
        /// <param name="date">the date of the expense</param>
        /// <param name="dblValue">the value of the expense</param>
        /// <param name="intExpenseTypeTuid">the tuid of the selected expense type</param>
        /// <param name="intVolunteerTuid">the tuid of the selected volunteer</param>
        /// <returns>true if the expense was added, false otherwise</returns>
        /// <author>Andrew Loesel</author>
        /// <created>3/23/2023</created>
        public bool AddExpense(DateTime date, double dblValue, int intExpenseTypeTuid, int intVolunteerTuid)
        {
            try
            {
                InKindExpenseTypeItem? type = _dbContext.InKindExpenseTypeItems.Where(x => x.Tuid == intExpenseTypeTuid).FirstOrDefault();
                if (type != null)
                {
                    _dbContext.InKindExpenses.Add(new InKindExpense { Date = date, Value = (decimal)dblValue, ExpenseTypeTuid = intExpenseTypeTuid, VolunteerTuid = intVolunteerTuid });
                    _dbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._0527._message, ErrorMessages._0527._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._0528._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._0528._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._0529._message + e.Message, ErrorMessages._0529._code);
            }

            return false;
        }


        /// <summary>
        /// This method gets all expense types from the databse and returns their names
        /// </summary>
        /// <returns>a list containing all expenses for the year that was input</returns>
        /// <author>Andrew Loesel</author>
        /// <created>2/23/23</created>
        public IEnumerable<InKindExpenseModel> GetExpensesForDateRange(DateTime startDate, DateTime endDate, int intExpenseTypeTuid, int intVolunteerTuid)
        {
            try
            {
                //get a query that includes both the expenseTypeItem and volunteer for the expenses. The first where clause here is just to get ExpenseQuery typed properly so that it doesn't have to be explicitly casted.
                IEnumerable<InKindExpense> ExpenseQuery = _dbContext.InKindExpenses.Include(y => y.ExpenseTypeItem).Include(y => y.Volunteer);
                //add needed where clauses if we need to filter by volunteer or expense type (-1 value means select all)
                if (intExpenseTypeTuid != -1)
                {
                    ExpenseQuery = ExpenseQuery.Where(x => x.ExpenseTypeTuid == intExpenseTypeTuid);
                }
                if (intVolunteerTuid != -1)
                {
                    ExpenseQuery = ExpenseQuery.Where(x => x.VolunteerTuid == intVolunteerTuid);
                }
                IEnumerable<InKindExpenseModel> lstExpenses = ExpenseQuery.Where(x => x.Date >= startDate && x.Date <= endDate).Select(x => new InKindExpenseModel
                {
                    TypeTuid = x.ExpenseTypeTuid,
                    ExpenseTypeName = x.ExpenseTypeItem == null ? "N/A" : x.ExpenseTypeItem.Name,
                    Date = x.Date,
                    dbMirrorTuid = x.Tuid,
                    intVolunteerTuid = x.VolunteerTuid,
                    Value = (double)x.Value,
                    VolunteerDonorName = x.Volunteer == null ? "N/A" : x.Volunteer.FullName,
                    IsDonation = false
                });

                return lstExpenses.OrderBy(x => x.ExpenseTypeName).OrderBy(x => x.VolunteerDonorName).ToList();

            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._0530._message, ErrorMessages._0530._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._0531._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._0531._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._0532._message + e.Message, ErrorMessages._0532._code);
            }

            return Enumerable.Empty<InKindExpenseModel>();

        }


        /// <summary>
        /// This method will attempt to update an expense where all dependent data can be found in the database. We update the expense values as provided by the parameters
        /// </summary>
        /// <param name="date">the date of the expense</param>
        /// <param name="dblValue">the value of the expense</param>
        /// <param name="intExpenseTypeTuid">the tuid of the expense type selected</param>
        /// <param name="intVolunteerTuid">tuid of the selected volunteer</param>
        /// <param name="intExpenseTuid">the tuid of the expense we are updating</param>
        /// <returns>true if the expense was updated, false otherwise</returns>
        /// <author>Andrew Loesel</author>
        /// <created>3/23/2023</created>
        public bool UpdateExpense(DateTime date, double dblValue, int intExpenseTypeTuid, int intVolunteerTuid, int intExpenseTuid)
        {
            try
            {
                //first we need to null check date, and check that an entry exists for the passed in volunteer and expense type tuid.
                var volunteer = _dbContext.Volunteers.SingleOrDefault(x => x.Tuid == intVolunteerTuid);
                var expenseType = _dbContext.ExpenseTypeItems.SingleOrDefault(x => x.Tuid == intExpenseTypeTuid);
                if (volunteer == null || expenseType == null)
                {
                    return false;
                }
                //now try to get the expense tied to intExpenseTuid
                var expense = _dbContext.InKindExpenses.SingleOrDefault(x => x.Tuid == intExpenseTuid);
                if (expense != null)
                {
                    expense.VolunteerTuid = intVolunteerTuid;
                    expense.ExpenseTypeTuid = intExpenseTypeTuid;
                    expense.Value = (decimal)dblValue;
                    expense.Date = date;
                    _dbContext.SaveChanges();
                    return true;
                }
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._0533._message, ErrorMessages._0533._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._0534._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._0534._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._0535._message + e.Message, ErrorMessages._0535._code);
            }

            return false;
        }

        /// <summary>
        /// This method deletes the inKindExpense with the provided tuid
        /// </summary>
        /// <param name="intExpenseTuid">the tuid of the in-kind expense we want to delete</param>
        /// <returns>true if the in-kind expense was deleted, false otherwise</returns>
        /// <author>Andrew Loesel</author>
        /// <created>4/1/2023</created>
        public bool DeleteExpense(int intExpenseTuid)
        {
            try
            {
                //get the inKindExpense
                InKindExpense? deleteExpense = _dbContext.InKindExpenses.SingleOrDefault(x => x.Tuid == intExpenseTuid);
                //if it was found then we can remove it
                if (deleteExpense != null)
                {
                    _dbContext.InKindExpenses.Remove(deleteExpense);
                    _dbContext.SaveChanges();
                    return true;
                }
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._0536._message, ErrorMessages._0536._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._0537._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._0537._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._0538._message + e.Message, ErrorMessages._0538._code);
            }


            return false;
        }

        #endregion

        #region InKindDonationsCRUD
        /// <summary>
        /// This method gets all donations within the provided time range
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns>a list of InKindExpenseModels</returns>
        /// <author>Andrew Loesel</author>
        /// <created>4/1/2023</created>
        public IEnumerable<InKindExpenseModel> GetDonations(DateTime startDate, DateTime endDate)
        {
            try
            {
                return _dbContext.InKindDonationTypeItems.Where(x => x.Date >= startDate && x.Date <= endDate && x.DonationTypeItem != null)
                    .Select(x => new InKindExpenseModel
                    {
                        VolunteerDonorName = x.Name,
                        Date = x.Date,
                        IsDonation = true,
                        dbMirrorTuid = x.Tuid,
                        ExpenseTypeName = x.DonationTypeItem == null ? "N/A" : x.DonationTypeItem.Name,
                        TypeTuid = x.DonationTypeItemTuid,
                        Value = x.Value,
                    })
                    .ToList();
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._0539._message, ErrorMessages._0539._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._0540._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._0540._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._0541._message + e.Message, ErrorMessages._0541._code);
            }

            return Enumerable.Empty<InKindExpenseModel>();
        }

        /// <summary>
        /// This method updates the donation corresponding to the provided tuid(if found) with the provided values
        /// </summary>
        /// <param name="intDonationTuid"></param>
        /// <param name="dblValue"></param>
        /// <param name="date"></param>
        /// <param name="DonorName"></param>
        /// <returns>true if the update succeeded, false otherwise</returns>
        /// <author>Andrew Loesel</author>
        /// <created>4/1/2023</created>
        public bool UpdateDonation(int intDonationTuid, double dblValue, DateTime date, int intDonationTypeTuid, string DonorName)
        {
            try
            {
                //get the donation from the database
                InKindDonationTypeItem? donation = _dbContext.InKindDonationTypeItems.Where(x => x.Tuid == intDonationTuid).FirstOrDefault();
                DonationTypeItem? donationTypeItem = _dbContext.DonationTypeItems.Where(x => x.Tuid == intDonationTypeTuid).FirstOrDefault();
                if (donation != null && donationTypeItem != null)
                {
                    donation.Name = DonorName;
                    donation.Value = dblValue;
                    donation.Date = date;
                    donation.DonationTypeItemTuid = intDonationTypeTuid;
                    _dbContext.SaveChanges();
                    return true;
                }
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._0542._message, ErrorMessages._0542._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._0543._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._0543._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._0544._message + e.Message, ErrorMessages._0544._code);
            }
            return false;
        }

        /// <summary>
        /// This method will delete a donation if one is found with the provided tuid
        /// </summary>
        /// <param name="intDonationTuid"></param>
        /// <returns>true if the donation was deleted, false otherwise</returns>
        /// <author>Andrew Loesel</author>
        /// <created>4/1/2023</created>
        public bool DeleteDonation(int intDonationTuid)
        {
            try
            {
                //get the donation from the database
                InKindDonationTypeItem? donation = _dbContext.InKindDonationTypeItems.Where(x => x.Tuid == intDonationTuid).FirstOrDefault();
                if (donation != null)
                {
                    _dbContext.InKindDonationTypeItems.Remove(donation);
                    _dbContext.SaveChanges();
                    return true;
                }
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._0545._message, ErrorMessages._0545._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._0546._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._0546._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._0547._message + e.Message, ErrorMessages._0547._code);
            }
            return false;
        }

        /// <summary>
        /// This method adds a donation using the provided parameters
        /// </summary>
        /// <param name="dblValue"></param>
        /// <param name="date"></param>
        /// <param name="DonorName"></param>
        /// <param name="intTypeTuid"></param>
        /// <returns>true if the add succeeded, false otherwise</returns>
        /// <author>Andrew Loesel</author>
        /// <created>4/1/2023</created>
        public bool AddDonation(double dblValue, DateTime date, string DonorName, int intDonationTypeTuid)
        {
            try
            {
                DonationTypeItem? type = _dbContext.DonationTypeItems.FirstOrDefault(x => x.Tuid == intDonationTypeTuid);
                if (type != null)
                {
                    _dbContext.InKindDonationTypeItems.Add(new InKindDonationTypeItem
                    {
                        Name = DonorName,
                        Value = dblValue,
                        Date = date,
                        DonationTypeItemTuid = type.Tuid
                    });

                    _dbContext.SaveChanges();
                    return true;
                }
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._0548._message, ErrorMessages._0548._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._0549._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._0549._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._0550._message + e.Message, ErrorMessages._0550._code);
            }
            return false;
        }
        #endregion

        /// <summary>
        /// This method gets the donation types
        /// </summary>
        /// <returns>an enumerable object of expensetype models</returns>
        /// <author>Andrew Loesel</author>
        /// <created>4/2/2023</created>
        public IEnumerable<ExpenseTypeModel> GetDonationTypes()
        {
            try
            {
                return _dbContext.DonationTypeItems.Select(x => new ExpenseTypeModel
                {
                    Name = x.Name,
                    Tuid = x.Tuid,
                }).ToList();
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._0551._message, ErrorMessages._0551._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._0552._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._0552._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._0553._message + e.Message, ErrorMessages._0553._code);
            }

            return Enumerable.Empty<ExpenseTypeModel>();
        }


        /// <summary>
        /// This method will return a list of all the expenses that are not covered by the in-kind tables already on the grant and fiscal year pages. They are returned 
        /// as YearToDate model items that contain the name of the expense type and the value accrued for that type over the provided time range.
        /// </summary>
        /// <param name="startDate">the start date of the time range</param>
        /// <param name="endDate">the end date of the time range</param>
        /// <returns>a List of YearToDateExpenseModel items</returns>
        /// <author>Andrew Loesel</author>
        /// <created>3/15/2023</created>
        public List<FinanceFocusGridModel> GetExpenseForYearToDateTable(DateTime? startDate, DateTime? endDate)
        {
            try
            {
                var expensesToDate = _dbContext.InKindExpenses
                    .Where(x => (!startDate.HasValue || x.Date >= startDate) && (!endDate.HasValue || x.Date < endDate))
                    .GroupBy(x => x.ExpenseTypeTuid)
                    .Select(g => new FinanceFocusGridModel
                    {
                        Name = g.FirstOrDefault().ExpenseTypeItem.Name,
                        Value = g.Sum(x => (double)x.Value)
                    })
                    .OrderBy(x => x.Name)
                    .ToList();

                List<FinanceFocusGridModel> donationsToDate = GetDonationsForYearToDateTable(startDate, endDate);

                var result = expensesToDate.Concat(donationsToDate).OrderBy(x => x.Name).ToList();

                return result;
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._0554._message, ErrorMessages._0554._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._0555._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._0555._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._0556._message + e.Message, ErrorMessages._0556._code);
            }

            return new List<FinanceFocusGridModel>();
        }

        /// <summary>
        /// Get the donations for the Year To Date Table.
        /// </summary>
        /// <param name="startDate">Start date filter.</param>
        /// <param name="endDate">End date filter.</param>
        /// <author>Tyler Moody</author>
        /// <created>4/5/2023</created>
        /// <returns>List of FinaceFocusGridModels</returns>
        private List<FinanceFocusGridModel> GetDonationsForYearToDateTable(DateTime? startDate, DateTime? endDate)
        {
            try
            {
                return _dbContext.InKindDonationTypeItems
                                .Where(x => (!startDate.HasValue || x.Date >= startDate) && (!endDate.HasValue || x.Date < endDate))
                                .GroupBy(x => x.Name)
                                .Select(g => new FinanceFocusGridModel
                                {
                                    Name = g.Key,
                                    Value = g.Sum(x => (double)x.Value)
                                })
                                .OrderBy(x => x.Name)
                                .ToList();
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._0557._message, ErrorMessages._0557._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._0558._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._0558._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._0559._message + e.Message, ErrorMessages._0559._code);
            }

            return new List<FinanceFocusGridModel> ();
        }

        #region ExpenseAndCostShareYears
        /// <summary>
        /// This function will use the dates of entries in the InKindExpense table to generate a list of year ranges (as strings) 
        /// </summary>
        /// <returns>An IEnumerable of strings of year ranges</returns>
        /// <author>Andrew Loesel</author>
        /// <created>2/18/23</created>
        /// <modification>
        ///     <change>Date : 2/23/23, author : Andrew Loesel, change : copied this method in from DatabaseInKindExpenseProvider</change>
        /// </modification>
        public List<string> GetExpenseYearRanges()
        {
            try
            {
                IQueryable<int> afterSeptemberDonation = _dbContext.InKindDonationTypeItems.Where(x => x.Date.Month > 9).Select(x => x.Date.Year).Distinct();
                IQueryable<int> afterSeptemberExpense = _dbContext.InKindExpenses.Where(x => x.Date.Month > 9).Select(x => x.Date.Year).Distinct();
                List<int> afterSeptember = afterSeptemberDonation.Union(afterSeptemberExpense).ToList();
                IQueryable<int> beforeSeptemberDonation = _dbContext.InKindDonationTypeItems.Where(x => x.Date.Month < 10).Select(x => x.Date.Year).Distinct();
                IQueryable<int> beforeSeptemberExpense = _dbContext.InKindExpenses.Where(x => x.Date.Month < 10).Select(x => x.Date.Year).Distinct();
                List<int> beforeOctober = beforeSeptemberDonation.Union(beforeSeptemberExpense).ToList();
                var ranges = new List<string>();
                foreach (var year in afterSeptember)
                {
                    ranges.Add(year + "-" + (year + 1));
                }
                foreach (var year in beforeOctober)
                {
                    if (!ranges.Contains((year - 1) + "-" + year))
                    {
                        ranges.Add((year - 1) + "-" + year);
                    }
                }
                //I am going to order ranges outside of the db calls because doing it inside the call can lead to incorrect ordering because of the method used to generate the list
                return ranges.OrderBy(x => x).ToList();
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._0560._message, ErrorMessages._0560._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._0561._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._0561._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._0562._message + e.Message, ErrorMessages._0562._code);
            }

            return new List<string>();
        }

        /// <summary>
        /// This method gets a list of years that contain cost share entities.
        /// </summary>
        /// <returns>a list of strings representing year ranges</returns>
        /// <author>Andrew Loesel</author>
        /// <created>3/22/2023</created>
        public List<string> GetCostShareYears()
        {
            try
            {
                List<int> afterSeptember = _dbContext.SchoolCostShares.Where(x => x.Date.Month > 9).Select(x => x.Date.Year).Distinct().ToList();
                List<int> beforeOctober = _dbContext.SchoolCostShares.Where(x => x.Date.Month < 10).Select(x => x.Date.Year).Distinct().ToList();
                //Cost share is dependent on the Fiscal year, so we can show year ranges that have entries falling in their fiscal years
                var ranges = new List<string>();
                //for cost shares with dates after september they fall in the fiscal year of their year - their year + 1, 10/1/2003 falls in the 2003 - 2004 fiscal year
                foreach (var year in afterSeptember)
                {
                    ranges.Add(year + "-" + (year + 1));
                }
                //now we need to add any ranges that are missing using our before october list, these will be in the fiscal year of their year - 1 - their year
                foreach (var year in beforeOctober)
                {
                    if (!ranges.Contains((year - 1) + "-" + year))
                    {
                        ranges.Add((year - 1) + "-" + year);
                    }
                }
                return ranges.OrderBy(x => x).ToList();
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._0563._message, ErrorMessages._0563._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._0564._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._0564._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._0565._message + e.Message, ErrorMessages._0565._code);
            }
            return new List<string>();

        }
        #endregion

        #region GetModelsForEntities

        /// <summary>
        /// This method will return an expenseTypeModel when given an expenseTypeItem database entity
        /// </summary>
        /// <param name="expenseTypeItem">the databse entity of the expenseTypeItem</param>
        /// <returns>an ExpenseTypeModel</returns>
        /// <author>Andrew Loesel</author>
        /// <created>2/23/23</created>
        private static ExpenseTypeModel GetExpenseTypeModel(ExpenseTypeItem expenseTypeItem)
        {
            ExpenseTypeModel expenseTypeModel = new ExpenseTypeModel();
            expenseTypeModel.Tuid = expenseTypeItem.Tuid;
            expenseTypeModel.Name = expenseTypeItem.Name;

            return expenseTypeModel;
        }

        /// <summary>
        /// This method returns a volunteer model when given a volunteer database entity
        /// </summary>
        /// <param name="volunteer">the database entity item for the volunteer</param>
        /// <returns>A volunteerModel object</returns>
        /// <author>Andrew Loesel</author>
        /// <created>2/23/23</created>
        private static VolunteerModel GetVolunteerModel(Volunteer volunteer)
        {
            VolunteerModel volunteerModel = new VolunteerModel();
            volunteerModel.Tuid = volunteer.Tuid;
            volunteerModel.FirstName = volunteer.FirstName;
            volunteerModel.LastName = volunteer.LastName;

            return volunteerModel;
        }

        #endregion
    }
}
