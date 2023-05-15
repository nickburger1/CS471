using A_FGMS.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B_FGMS.BusinessLogic.Models
{
    /// <summary>
    /// This is a data model for an in kind expense item
    /// </summary>
    /// <author>Adnrew Loesel</author>
    /// <created>2/5/2023</created>
    public class MealTransportMileageModel
    {
        public string? strFullName { get; set; } //The full name of the volunteer assigned this expense
        public DateTime? DateOf { get; set; } //the date that his expense occurred
        public string? strQuarter { get; set; } //the quarter that this expense is in.
        public string? strDate { get; set; }

        public int intMealCount { get; set; } //the number of meals
        public double dblMealRate { get; set; } //the rate of 1 meal
        public double dblMealValue() { return dblMealRate * intMealCount; } //the total cost of meals
        public string? strMealValue { get; set; }
        public double dbTotalMealValue { get; set; }

        public int intBusCount { get; set; } //the number of bus rides
        public double dblBusRate { get; set; } //the rate per 1 bus ride
        public double dblBusValue() { return dblBusRate * intBusCount; } //the value of bus transport
        public string strBusValue { get; set; }
        public double dbTotalBusValue { get; set; }

        public int intMileCount { get; set; } //the number of miles driven
        public double dblMileRate { get; set; }//the rate per mile driven
        public double dblMileageValue() { return dblMileRate * intMileCount; } //the value of the mileages
        public string strMileageValue { get; set; } //the string value of the mileage value
        public double dbTotalMileageValue { get; set; }
        public double dbRate { get; set; }
        
    }
}
