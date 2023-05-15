using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 ************************************************************************************************************************
 *                                      File Name : Schedule.cs                                                         *
 *                                      Part of Project : CS471 Senior Capstone Project / FGMS                          *
 ************************************************************************************************************************
 *                                      Created By : Tyler Moody                                                        *
 *                                      Date Created : 02/09/2023                                                       *
 *                                      Additional Contributors : CS471 WI23 Development Team                           *
 *                                      Last Modified : 02/09/2023                                                      *
 *                                      Last Modified By : Tyler Moody                                                  *
 ************************************************************************************************************************
 * File Purpose : The Purpose of this file is to provide the model for a Schedule object.                               *
 ************************************************************************************************************************

 **/
namespace B_FGMS.BusinessLogic.Models
{
    public class ScheduleModel
    {
        public string Days { get; }
        public string StartTime { get; } //Format HH:MM am/pm (ex. 8:30am)
        public string EndTime { get; } //Format HH:MM am/pm (ex. 8:30pm)
        public string StringSchedule { get; }

        /// <summary>
        /// Parameterized constructor.
        /// </summary>
        /// <param name="days">String format. Ex: "Monday Wednesday"</param>
        /// <param name="startTime">Start of class</param>
        /// <param name="endTime">End of class</param>
        /// <author>Tyler Moody</author>
        /// <created>02/09/2023</created>
        public ScheduleModel(string day, string startTime, string endTime)
        {
            Days = day;
            StartTime = startTime;
            EndTime = endTime;

            if (!string.IsNullOrEmpty(Days))
            {
                StringSchedule = "Days :" + Days;
            }
            else
            {
                StringSchedule = "Days :" + "N/A";
            }
            if (!string.IsNullOrEmpty(StartTime))
            {
                StringSchedule += " Start :" + StartTime;
            }
            else
            {
                StringSchedule += " Start :" + "N/A";
            }
            if (!string.IsNullOrEmpty(EndTime))
            {
                StringSchedule += " End :" + EndTime;
            }
            else
            {
                StringSchedule += " End :" + "N/A";
            }
            
        }

        /// <summary>
        /// Converts a space delimited list of strings to a number format.
        /// </summary>
        /// <example>"Monday Wednesday Friday => 135"</example>
        /// <author>Tyler Moody</author>
        /// <created>02/09/2023</created>
        /// <param name="daysToConvert">String of days; Space delimited</param>
        /// <returns></returns>
        private string ConvertDaysToNumberFormat(string daysToConvert)
        {
            string strOriginalDays = "";
            if (string.IsNullOrEmpty(daysToConvert))
            {
                return "";
            }
            var cArray = daysToConvert.ToCharArray();
            foreach (char day in cArray)
            {
                switch (day)
                {
                    case 'M':
                        strOriginalDays+=day;
                        break;
                    case 'T':
                        strOriginalDays += day;
                        break;
                    case 'W':
                        strOriginalDays += day;
                        break;
                    case 'R':
                        strOriginalDays += day;
                        break;
                    case 'F':
                        strOriginalDays += day;
                        break;
                    default:
                        break;
                }
            }

            return strOriginalDays;
        }

        /// <summary>
        /// Takes a TimeSpan and return the number of hours as a decimal.
        /// </summary>
        /// <example>08:30 = 8.5</example>
        /// <author>Tyler Moody</author>
        /// <created>02/09/2023</created>
        /// <param name="timeSpan">HH:MM format.</param>
        /// <returns></returns>
        private double ConvertTimeSpanToHours (TimeSpan timeSpan)
        {
            double minutesInHour = 60;
            return timeSpan.Hours + (timeSpan.Minutes / minutesInHour);
        }
    }
}
