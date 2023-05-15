using A_FGMS.DataLayer.Entities;
using B_FGMS.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B_FGMS.BusinessLogic.Services.FinanceProviders
{
    /// <summary>
    /// This is the interface that the DatabaseExpenseTypeProvider will be based off of
    /// </summary>
    /// <author>Andrew Loesel</author>
    /// <date>2/23/23</date>
    public interface IExpenseProvider
    {
        public event EventHandler<Events.ErrorEventArgs> DatabaseError;
        public List<ExpenseTypeModel> GetAllExpenseTypes();
        public InKindExpense? GetExpenseByExpenseTypeId(int tuid);
        public bool DonationTypeUsed(int tuid);
        public IEnumerable<InKindExpenseModel> GetExpensesForDateRange(DateTime startDate, DateTime endDate, int intExpenseTypeTuid, int intVolunteerTuid);
        public List<string> GetExpenseYearRanges();
        public List<FinanceFocusGridModel> GetExpenseForYearToDateTable(DateTime? startDate, DateTime? endDate);
        public IEnumerable<SchoolCostShareModel> GetCostShares(DateTime startDate, DateTime endDate);
        public bool AddCostShare(DateTime date, string strName, double dblValue);
        public bool UpdateCostShare(DateTime date, string strName, double dblValue, int intTuid);
        public List<string> GetCostShareYears();
        public bool AddExpense(DateTime date, double dblValue, int intExpenseTypeTuid, int intVolunteerTuid);
        public bool UpdateExpense(DateTime date, double dblValue, int intExpenseTypeTuid, int intVolunteerTuid, int intExpenseTuid);
        public bool DeleteCostShare(int intCostShareTuid);
        public bool DeleteExpense(int intExpenseTuid);
        public void AddNewExpenseType(string name);
        public void AddNewDonationType(string name);
        public void DelExpenseType(ExpenseTypeModel item);
        public void DeleteDonationType(ExpenseTypeModel item);
        public IEnumerable<InKindExpenseModel> GetDonations(DateTime startDate, DateTime endDate);
        public bool UpdateDonation(int intDonationTuid, double dblValue, DateTime date, int intDonationTypeTuid, string DonorName);
        public bool DeleteDonation(int intDonationTuid);
        public bool AddDonation(double dblValue, DateTime date, string DonorName, int intDonationTypeTuid);
        public IEnumerable<ExpenseTypeModel> GetDonationTypes();
    }
}
