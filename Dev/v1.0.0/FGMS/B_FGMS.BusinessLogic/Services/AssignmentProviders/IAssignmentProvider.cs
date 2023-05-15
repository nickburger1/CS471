using A_FGMS.DataLayer.Entities;
using B_FGMS.BusinessLogic.Models;
using B_FGMS.BusinessLogic.Models.Volunteer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/**
 ************************************************************************************************************************
 *                                      File Name : IAssignmentProvider.cs                                              *
 *                                      Part of Project : CS471 Senior Capstone Project / FGMS                          *
 ************************************************************************************************************************
 *                                      Created By : Kiefer Thorson & Nathan VanSnepson                                 *
 *                                      Date Created : 1/31/23                                                          *
 *                                      Additional Contributors : CS471 WI23 Development Team                           *
 *                                      Last Modified : 2/16/2023                                                       *
 *                                      Last Modified By : Kiefer Thorson                                               *
 ************************************************************************************************************************
 * File Purpose : The Purpose of this file is to provide an interface for Assignments                                   *
 ************************************************************************************************************************
 * Modification Log:                                                                                                    *
 * Author: Kiefer Thorson                                                                                               *
 * Date: 2/16/2023                                                                                                      *
 * Description: Added GetActiveVolunteersBySchoolTuid                                                                   *
 ************************************************************************************************************************
 **/
namespace B_FGMS.BusinessLogic.Services.AssignmentProviders
{
	/// <summary>
	/// Interface Name: IAssignmentProvider
	/// Created By: Kiefer Thorson & Nathan VanSnepson
	/// Date Created: 2/9/2023
	/// Additional Contributors:
	/// Last Modified: 2/16/2023
	/// Last Modified By: Kiefer Thorson                    
	/// 
	/// Purpose:
	/// This interface is the contract that has method declarations for the Assignment provider 
	/// </summary>
	public interface IAssignmentProvider
	{
        event EventHandler<Events.ErrorEventArgs> DatabaseError;
        IEnumerable<Assignment> GetAllAssignments();
		int GetActiveVolunteersBySchoolAssignmentCount(int schoolTuid);
		IEnumerable<AssignmentModel> GetActiveVolunteersBySchoolTuid(int schoolTuid);
        IEnumerable<SchoolAssignmentModel> GetAssignmentBySchoolTuid(int schoolTuid);
		public int? GetTotalStudentsClassroomBySchoolTuid(int schoolTuid);
        public int? GetTotalStudentsAssignedBySchoolTuid(int schoolTuid);
        public int? GetTotal0to5BySchoolTuid(int schoolTuid);
        public int? GetTotal6to12BySchoolTuid(int schoolTuid);
        public bool DeleteChildAssignments();
		public List<SchoolAssignmentModel> GetAllAssignmentsWithIncludes();
		public int? GetClassroomTuidByStudentTuid(int intStudentTuid);
	}

}
