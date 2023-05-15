using A_FGMS.DataLayer.Entities;
using B_FGMS.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 ************************************************************************************************************************
 *                                      File Name : IAddressProvider.cs                                                 *
 *                                      Part of Project : CS471 Senior Capstone Project / FGMS                          *
 ************************************************************************************************************************
 *                                      Created By : Kiefer Thorson                                                     *
 *                                      Date Created : 2/16/2023                                                        *
 *                                      Additional Contributors : CS471 WI23 Development Team                           *
 *                                      Last Modified :                                                                 *
 *                                      Last Modified By :                                                              *
 ************************************************************************************************************************
 * File Purpose : The Purpose of this file is to provide an interface for School addresses                              *
 ************************************************************************************************************************
 * Modification Log:                                                                                                    *
 * Author:                                                                                                              *
 * Date:                                                                                                                *
 * Description:                                                                         								*
 ************************************************************************************************************************
 **/
namespace B_FGMS.BusinessLogic.Services.AddressProviders
{
    /// <summary>
	/// Interface Name: IAddressProvider
	/// Created By: Kiefer Thorson                    
	/// Date Created: 2/16/2023
	/// Additional Contributors:
	/// Last Modified:          
	/// Last Modified By:                                    
	/// 
	/// Purpose:
	/// This interface is the contract that has method declarations for the address provider
	/// </summary>
    public interface IAddressProvider
    {
        event EventHandler<Events.ErrorEventArgs> DatabaseError;
        AddressModel GetAddressByTuid(int? tuid);
		void UpdateAddress(AddressModel address);
		void AddNewAddress(AddressModel address);
		void DeleteAddress(int? addressTuid);
    }
}
