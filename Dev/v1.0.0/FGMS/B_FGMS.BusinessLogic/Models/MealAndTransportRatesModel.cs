using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 ************************************************************************************************************************
 *                                      File Name : Classroom.cs                                                        *
 *                                      Part of Project : CS471 Senior Capstone Project / FGMS                          *
 ************************************************************************************************************************
 *                                      Created By : Brendan Breuss                                                     *
 *                                      Date Created : 02/12/2023                                                       *
 *                                      Additional Contributors : CS471 WI23 Development Team                           *
 *                                      Last Modified : 02/12/2023                                                      *
 *                                      Last Modified By : Brendan Breuss                                               *
 ************************************************************************************************************************
 * File Purpose : The purpose of this file is to create a rates model to hold the rates for a given month for           *
 *                the meal and transport page                                                                           *
 ************************************************************************************************************************
 **/
namespace B_FGMS.BusinessLogic.Models
{
    public class MealAndTransportRatesModel
    {
        public double mealRate { get; set; } //meal rate for given month
        public double mileageRate { get; set; } //mileage rate for given month
        public DateTime MealRatesDate { get; set; } //Date for the above rates
    }
}
