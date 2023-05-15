using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B_FGMS.BusinessLogic.BusinessLogicObjects
{
    public class GeneralBusinessLogic
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

        public int GetCurrentMonthIndex()
        {
            DateTime DateToday = DateTime.Now;
            int intThisMonthIndex = DateToday.Month - 1;
            return intThisMonthIndex;
        }

        #region FiscalOrGrantYearDate
        /// <summary>
        /// This method gets the start an end date for a fiscal year when given the start year and end year of the fiscal year
        /// </summary>
        /// <param name="intStartYear">the start year of the fiscal year</param>
        /// <param name="intEndYear">the end year of the fiscal year</param>
        /// <returns>an array with both the start and end date of the desired fiscal year</returns>
        /// <author>Andrew Loesel</author>
        /// <created>2/9/2023</created>
        public DateTime[] GetFiscalYearDates(int intStartYear, int intEndYear)
        {
            //grant year of the county is october 1st through september 30th, so if start year is 2021 and end year is 2022 then the dates would be 10/1/2021 through 9/30/2022
            //start date time should be 12AM(midnight), end date time should be 11:59 PM since this will be used as a range. Time of a datetime is 12AM by default so only need to set the end time
            DateTime[] fiscalYearDates = new DateTime[] { new DateTime(intStartYear, 10, 1), new DateTime(intEndYear, 9, 30, 11, 59, 59)};
            return fiscalYearDates;
        }

        /// <summary>
        /// This method gets the start an end date for a grant year when given the start year and end year of the fiscal year
        /// </summary>
        /// <param name="intStartYear">the start year of the grant year</param>
        /// <param name="intEndYear">the end year of the grant year</param>
        /// <returns>an array with both the start and end date of the desired grant year</returns>
        /// <author>Andrew Loesel</author>
        /// <created>2/9/2023</created>
        public DateTime[] GetGrantYearDates(int intStartYear, int intEndYear)
        {
            //grant year runs from july 1 - june 30th
            DateTime[] grantYearDates = new DateTime[] { new DateTime(intStartYear, 7, 1), new DateTime(intEndYear, 6, 30, 11, 59, 59) };
            return grantYearDates;
        }
        #endregion
        /// <summary>
        /// This method will return the fiscal year quarter that a given date falls in
        /// </summary>
        /// <param name="date">The date that we want to get the quarter of</param>
        /// <returns>A string representing the quarter that the date falls in.</returns>
        /// <author>Andrew Loesel</author>
        /// <created>~2/1/23</created>
        public string GetFiscalYearQuarter(DateTime date)
        {
            string quarter = "2nd Quarter";

            //Fiscal quarters are Q1 : 10/1 - 12/31, Q2 : 1/1 - 3/31, Q3 : 4/1 - 6/30, Q4 : 7/1 - 9/30
            if(date.Month > 9)
            {
                quarter = "1st Quarter";
            }
            else if(date.Month > 6)
            {
                quarter = "4th Quarter";
            }
            else if(date.Month > 3)
            {
                quarter = "3rd Quarter";
            }

            return quarter;
        }

        /// <summary>
        /// This method will return a string with the grant year quarter that a given date falls in
        /// </summary>
        /// <param name="date">The date whose quarter we desire</param>
        /// <returns>a string containing the quarter of the date</returns>
        /// <author>Andrew Loesel</author>
        /// <created>2/25/23</created>
        public string GetGrantYearQuarter(DateTime date)
        {
            string quarter = "3rd Quarter";

            if (date.Month > 9)
            {
                return "2nd Quarter";
            }
            else if (date.Month > 6)
            {
                return "1st Quarter";
            }
            else if (date.Month > 3)
            {
                return "4th Quarter";
            }

            return quarter;
        }

        /// <summary>
        /// This method will create an array that has 8 date time elements where every pair of Datetimes is 
        /// a start date then end date for a fiscal year quarter.
        /// </summary>
        /// <param name="intStartYear">The first year of the fiscal year</param>
        /// <param name="intEndYear">the second year of the fiscal year</param>
        /// <returns>an array of Datetimes where each pair is a start then end date for a quarter</returns>
        /// <author>Andrew Loesel</author>
        /// <created>2/20/23</created>
        public DateTime[] GetFiscalYearQuarters(int intStartYear, int intEndYear)
        {
            //first item in a pair is a start date, second is an end date. End dates are given a time since we have to cover the full day
            //if no time is provided default is 12:00AM which is what we would want for the first day of a quarter
            DateTime[] fiscalYearQuarters = new DateTime[8];
            fiscalYearQuarters[0] = new DateTime(intStartYear, 10, 1);
            fiscalYearQuarters[1] = new DateTime(intStartYear, 12, 31, 23, 59, 59);
            fiscalYearQuarters[2] = new DateTime(intEndYear, 1, 1);
            fiscalYearQuarters[3] = new DateTime(intEndYear, 3, 31, 23, 59, 59);
            fiscalYearQuarters[4] = new DateTime(intEndYear, 4, 1);
            fiscalYearQuarters[5] = new DateTime(intEndYear, 6, 30, 23, 59, 59);
            fiscalYearQuarters[6] = new DateTime(intEndYear, 7, 1);
            fiscalYearQuarters[7] = new DateTime(intEndYear, 9, 30, 23, 59, 59);

            return fiscalYearQuarters;
        }

        /// <summary>
        /// This method will create an array that has 8 date time elements where every pair of Datetimes is 
        /// a start date then end date for a grant year quarter.
        /// </summary>
        /// <param name="intStartYear">The first year of the grant year</param>
        /// <param name="intEndYear">the second year of the grant year</param>
        /// <returns>an array with 8 dateTime objects in pairs of two corresponding to a start and end date of a grant year quarter</returns>
        /// <author>Andrew Loesel</author>
        /// <created>~2/21/23</created>
        public DateTime[] GetGrantYearQuarters(int intStartYear, int intEndYear)
        {
            //first item in a pair is a start date, second is an end date. End dates are given a time since we have to cover the full day
            //if no time is provided default is 12:00AM which is what we would want for the first day of a quarter
            DateTime[] grantYearQuarters = new DateTime[8];
            grantYearQuarters[0] = new DateTime(intStartYear, 7, 1);
            grantYearQuarters[1] = new DateTime(intStartYear, 9, 30, 23, 59, 59);
            grantYearQuarters[2] = new DateTime(intStartYear, 10, 1);
            grantYearQuarters[3] = new DateTime(intStartYear, 12, 31, 23, 59, 59);
            grantYearQuarters[4] = new DateTime(intEndYear, 1, 1);
            grantYearQuarters[5] = new DateTime(intEndYear, 3, 31, 23, 59, 59);
            grantYearQuarters[6] = new DateTime(intEndYear, 4, 1);
            grantYearQuarters[7] = new DateTime(intEndYear, 6, 30, 23, 59, 59);

            return grantYearQuarters;
        }
    }
}
