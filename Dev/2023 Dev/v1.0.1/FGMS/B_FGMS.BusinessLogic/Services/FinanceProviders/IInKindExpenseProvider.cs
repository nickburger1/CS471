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
    /// This interface provides the required structure for our Expense Type database interactions
    /// </summary>
    /// <author>Andrew Loesel</author>
    /// <created>2/5/2023</created>
    public interface IInKindExpenseProvider
    {
        public event EventHandler<Events.ErrorEventArgs> DatabaseError;
        public IEnumerable<string> GetFinancialYearRanges();
        public List<MealTransportMileageModel> GetAllInKindForFiscalYear(int intStartYear, int intEndYear);
        public List<MealTransportMileageModel> GetAllInKindForGrantYear(int intStartYear, int intEndYear); 
        public List<FinanceFocusGridModel> GetMealInKindForDates(DateTime? startDate, DateTime? endDate, bool isFiscal);
        public List<FinanceFocusGridModel> GetMileageForDates(DateTime? startDate, DateTime? endDate, bool isFiscal);
        public List<FinanceFocusGridModel> GetBusTransportForDates(DateTime? startDate, DateTime? endDate, bool isFiscal);
        
    }
}
