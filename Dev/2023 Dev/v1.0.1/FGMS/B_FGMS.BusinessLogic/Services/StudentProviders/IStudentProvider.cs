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
namespace B_FGMS.BusinessLogic.Services.StudentProviders
{
	
	public interface IStudentProvider
	{

        public event EventHandler<Events.ErrorEventArgs> DatabaseError;
        // Start with this tomorrow morning
        int GetAllStudentsCount();

        int GetAllAssignedStudentsCount();
		int GetStudentCount6to12();
        bool CheckConditionInUse(int conditionTuid);
        bool CheckNeedInUse(int needTuid);
        void DeleteNeedItem(int needTuid);
        bool UpdateNeedItem(StudentNeedItemModel studentNeedItem);
        void DeleteConditionItem(int conditionTuid);
        bool UpdateCondtionItem(ConditionItemModel conditionItemModel);
    }
}
