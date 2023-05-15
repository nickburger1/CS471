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
 *                                      Last Modified : 02/17/2023                                                      *
 *                                      Last Modified By : Brendan Breuss                                               *
 ************************************************************************************************************************
 * File Purpose : The Purpose of this file is to provide the model for the meal and transport page                      *
 *                Mostly used to populate datagrid on the page and to edit informaation in the database                 *
 ************************************************************************************************************************
 **/
namespace B_FGMS.BusinessLogic.Models
{
    public class MealAndTransportModel
    {

        public int? volunteerID { get; set; } //the volunteer identifier
        public string? strVolunteerName { get;set; } //full name of the volunteer
        public int? numMeals { get; set; } //number of meals for a given volunteer
        public int? numBusRides { get; set; } //number of bus rides for a given volunteer
        public decimal? Mileage { get; set; } //mileage for a given volunteer
        public DateTime date { get; set; } //date the volunteer meal and transport data was created
        public double? totalMealCost { get; set; } //a volunteers total meal cost calculated using the rates model for meal rate
        public double? totalMileageCost { get; set; }//a volunteers total mileage cost calculated using the rates model for meal mileage

        //default constructor for a model when creating a new instance of a meal mileage
        public MealAndTransportModel()
        {
            numMeals= 0;
            numBusRides= 0;
            Mileage = 0;
            totalMealCost= 0.0;
            totalMileageCost= 0.0;
        }

    }


   
}
