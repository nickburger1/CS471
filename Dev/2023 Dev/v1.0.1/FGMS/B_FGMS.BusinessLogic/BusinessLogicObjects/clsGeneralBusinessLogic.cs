using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B_FGMS.BusinessLogic.BusinessLogicObjects
{
    public class clsGeneralBusinessLogic
    {
        public List<string> GetMonths()
        {
            List<string> lstMonths = new List<string>();
            lstMonths.Add("January");
            lstMonths.Add("February");
            lstMonths.Add("March");
            lstMonths.Add("April");
            lstMonths.Add("May");
            lstMonths.Add("June");
            lstMonths.Add("July");
            lstMonths.Add("August");
            lstMonths.Add("September");
            lstMonths.Add("October");
            lstMonths.Add("November");
            lstMonths.Add("December");
            return lstMonths;
        }

        public List<string> GetShortMonths()
        {
            List<string> lstMonths = new List<string>();
            lstMonths.Add("Jan");
            lstMonths.Add("Feb");
            lstMonths.Add("Mar");
            lstMonths.Add("Apr");
            lstMonths.Add("May");
            lstMonths.Add("Jun");
            lstMonths.Add("Jul");
            lstMonths.Add("Aug");
            lstMonths.Add("Sep");
            lstMonths.Add("Oct");
            lstMonths.Add("Nov");
            lstMonths.Add("Dec");
            return lstMonths;

        }

        public int GetCurrentMonthIndex()
        {
            DateTime DateToday = DateTime.Now;
            int intThisMonthIndex = DateToday.Month - 1;
            return intThisMonthIndex;
        }

        public int GetCurrentYearIndex()
        {
            DateTime DateToday = DateTime.Now;
            int intThisYearIndex = DateToday.Year;
            return intThisYearIndex;
        }

        

    }
}
