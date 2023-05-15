using A_FGMS.DataLayer.Entities;
using A_FGMS.DataLayer.Seeders;
using B_FGMS.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/**
 ************************************************************************************************************************
 *                                      File Name : ISchoolProvider.cs                                                  *
 *                                      Part of Project : CS471 Senior Capstone Project / FGMS                          *
 ************************************************************************************************************************
 *                                      Created By : Kiefer Thorson & Nathan VanSnepson                                 *
 *                                      Date Created : 1/31/23                                                          *
 *                                      Additional Contributors : CS471 WI23 Development Team                           *
 *                                      Last Modified : 2/26/2023                                                       *
 *                                      Last Modified By : Kiefer Thorson                                               *
 ************************************************************************************************************************
 * File Purpose : The Purpose of this file is to provide an interface for Schools                                       *
 ************************************************************************************************************************
 * Modification Log:                                                                                                    *
 * Author: Kiefer Thorson                                                                                               *
 * Date: 2/16/2023                                                                                                      *
 * Description: added GetSchoolByName																					*
 *																														*
 * Author: Kiefer Thorson																								*
 * Date: 2/26/2023																										*
 * Description: added UpdateSchool																						*
 ************************************************************************************************************************
 **/
namespace B_FGMS.BusinessLogic.Services.SchoolProviders
{
	/// <summary>
	/// Interface Name: ISchoolProvider
	/// Created By: Kiefer Thorson & Nathan VanSnepson
	/// Date Created: 2/9/2023
	/// Additional Contributors:
	/// Last Modified: 2/26/2023
	/// Last Modified By: Kiefer Thorson                      
	/// 
	/// Purpose:
	/// This interface is the contract that has method declarations for the school provider 
	/// </summary>
	public interface ISchoolProvider
	{
        event EventHandler<Events.ErrorEventArgs> DatabaseError;
        IEnumerable<SchoolModel> GetAllSchools();
		SchoolModel? GetSchoolByTuid(int schoolTuid);
		SchoolModel? GetSchoolByName(string name);
		void UpdateSchool(SchoolModel school);
		void AddNewSchool(SchoolModel newSchool);
		void DeleteSchool(int? schoolId);
		void DeleteFlagSchool(SchoolModel school);
		bool? GetSchoolPhoneNum(string number);
		bool? CheckSchoolNameExists(string name);
        bool? GetSchoolPhoneNum(string number, string selectedNum);
		bool? CheckSchoolNameExists(string name, string selectedName);
        IEnumerable<SchoolNameIdModel> GetSchoolNameAndId();


    }
}
