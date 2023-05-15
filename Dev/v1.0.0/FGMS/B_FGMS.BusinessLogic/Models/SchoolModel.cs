using A_FGMS.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/**
 ************************************************************************************************************************
 *                                      File Name : SchoolModel.cs                                                      *
 *                                      Part of Project : CS471 Senior Capstone Project / FGMS                          *
 ************************************************************************************************************************
 *                                      Created By : Timothy Johnson                                                    *
 *                                      Date Created : 2/21/2023                                                        *
 *                                      Additional Contributors : CS471 WI23 Development Team                           *
 *                                      Last Modified : 1/22/23                                                         *
 *                                      Last Modified By : Nathan VanSnepson                                            *
 ************************************************************************************************************************
 * File Purpose : The Purpose of this file is to create a Model for the School entity                                   *
 ************************************************************************************************************************
 * Modification Log:                                                                                                    *
 * Author: Kiefer Thorson                                                                                               *
 * Date: 2/26/2023                                                                                                      *
 * Description: Added all relevant fields to what was here - renamed a few to fit Model naming for consistency          *
 ************************************************************************************************************************
 **/
namespace B_FGMS.BusinessLogic.Models
{
    public class SchoolModel 
    {
        public int Tuid { get; set; }
        public int? AddressTuid { get; set; }
        public virtual Address? Address { get; set; }
        public string? Name { get; set; }
        public string? Hours { get; set; }
        public string? Principal { get; set; }
        public string? Secretary { get; set; }
        public string? ContactNumber { get; set; }
        public string? StartTime { get; set; }
        public string? EndTime { get; set; }
        public bool IsActive { get; set; }
        public string? Days { get; set; }

        public bool IsDeleted { get; set; }
    }
}
