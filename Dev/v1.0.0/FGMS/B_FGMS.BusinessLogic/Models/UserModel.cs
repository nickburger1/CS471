using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/**
 ************************************************************************************************************************
 *                                      File Name : UserModel.cs                                                        *
 *                                      Part of Project : CS471 Senior Capstone Project / FGMS                          *
 ************************************************************************************************************************
 *                                      Created By : Kiefer Thorson & Nathan VanSnepson                                 *
 *                                      Date Created : 2/16/23                                                          *
 *                                      Additional Contributors : CS471 WI23 Development Team                           *
 *                                      Last Modified : 2/16/23                                                         *
 *                                      Last Modified By : Kiefer Thorson & Nathan VanSnepson                           *
 ************************************************************************************************************************
 * File Purpose : The Purpose of this file is to communicate with the User Entity                                       *
 ************************************************************************************************************************
 * Modification Log:                                                                                                    *
 * Author:                                                                                                              *
 * Date:                                                                                                                *
 * Description:                                                                                                         *
 ************************************************************************************************************************
 **/
namespace B_FGMS.BusinessLogic.Models
{
	/// <summary>
	/// Class Name: UserModel
	/// Created By: Kiefer Thorson & Nathan VanSnepson
	/// Date Created: 2/16/2023
	/// Additional Contributors: Nathan VanSnepson
	/// Last Modified: 2/16/2023
	/// Last Modified By: Kiefer Thorson & Nathan VanSnepson
	/// 
	/// Purpose:
	/// The purpose of this class is to contain the User model
	/// </summary>
	public class UserModel
	{
		public int Tuid { get; set; }
		public string? Name { get; set; }
		public string? Email { get; set; }
		public string? PhoneNumber { get; set; }
		public bool IsActive { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsReadOnly { get; set; }

        public UserModel()
		{
			Tuid = 0;
			Name = "";
			Email = "";
			PhoneNumber = "";
			IsActive = true;
			IsAdmin = false;
            IsReadOnly = false;
        }
    }
}
