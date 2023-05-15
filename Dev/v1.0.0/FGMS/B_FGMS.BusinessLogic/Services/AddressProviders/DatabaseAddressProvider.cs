using A_FGMS.DataLayer;
using A_FGMS.DataLayer.Entities;
using B_FGMS.BusinessLogic.Constants;
using B_FGMS.BusinessLogic.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace B_FGMS.BusinessLogic.Services.AddressProviders
{
 /**
 ************************************************************************************************************************
 *                                      File Name : DatabaseAddressProvider.cs                                          *
 *                                      Part of Project : CS471 Senior Capstone Project / FGMS                          *
 ************************************************************************************************************************
 *                                      Created By : Kiefer Thorson                                                     *
 *                                      Date Created : 2/16/2023                                                        *
 *                                      Additional Contributors :                                                       *
 *                                      Last Modified :                                                                 *
 *                                      Last Modified By :               					                            *
 ************************************************************************************************************************
 * File Purpose : The Purpose of this file is to retrieve Volunteer data                                                *
 ************************************************************************************************************************
 * Modification Log:                                                                                                    *
 * Author:                                                                                                              *
 * Date:                                                                                                                *
 * Description:                                                                                                         *
 ************************************************************************************************************************
 **/
    public class DatabaseAddressProvider : IAddressProvider
    {
        private readonly ApplicationDbContext _dbContext; //This contains the information about the database

        public event EventHandler<Events.ErrorEventArgs> DatabaseError;

        /// <summary>
        /// Function Name: DatabaseAddressProvider
        /// Created By: Kiefer Thorson
        /// Date Created: 2/16/2023
        /// Additional Contributors:
        /// Last Modified: 2/16/2023
        /// Last Modified By: Kiefer Thorson
        /// 
        /// Purpose:
        /// The Purpose of this Function is to:
        ///     - Initialize the database context (dbContext)
        /// </summary>
        /// <param name="dbContext"></param>
        public DatabaseAddressProvider(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// When called with invoke an event to inform user of an error
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <param name="errorCode"></param>
        /// <author>Nathan VanSnepson</author>
        /// <created>4/4/23</created>
        private void OnDatabaseError(string errorMessage, string errorCode)
        {
            DatabaseError?.Invoke(this, new Events.ErrorEventArgs(errorMessage, errorCode));
        }

        /// <summary>
        /// Function Name: DatabaseAddressProvider
        /// Created By: Kiefer Thorson
        /// Date Created: 3/15/2023
        /// Additional Contributors:
        /// Last Modified: 
        /// Last Modified By: 
        /// 
        /// Purpose:
        /// The Purpose of this Function is to:
        ///     - Connect an address to the new school and save address entry in db
        /// </summary>
        /// <param name="address"></param>
        public void AddNewAddress(AddressModel address)
        {
            try
            {
                Address newAddress= new Address()
                {
                    //Tuid = address.Tuid,
                    AddressLine1 = address.AddressLine1,
                    AddressLine2 = address.AddressLine1,
                    City = address.City,
                    State = address.State,
                    Zipcode= address.Zipcode,
                };
                _dbContext.Addresses.Add(newAddress);
                _dbContext.SaveChanges();
                address.Tuid = newAddress.Tuid;
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._0200._message, ErrorMessages._0200._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._0201._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._0201._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._0202._message + e.Message, ErrorMessages._0202._code);
            }
        }

        /// <summary>
        /// Function Name: GetAddressByTuid
        /// Created By: Kiefer Thorson
        /// Date Created: 2/16/2023
        /// Additional Contributors:
        /// Last Modified: 3/3/2023
        /// Last Modified By: Kiefer Thorson
        /// 
        /// Purpose:
        /// The Purpose of this Function is to:
        ///     - Get a School's address given its associated tuid
        /// </summary>
        /// <param name="tuid"></param>
        /// <returns>Address - School address info</returns>
        public AddressModel GetAddressByTuid(int? tuid)
        {
            try
            {
                return _dbContext.Addresses.Select(x =>
                new AddressModel
                {
                    Tuid = x.Tuid,
                    AddressLine1 = x.AddressLine1,
                    City = x.City,
                    State = x.State,
                    Zipcode = x.Zipcode
                }).FirstOrDefault(x => x.Tuid == tuid);
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._0203._message, ErrorMessages._0203._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._0204._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._0204._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._0205._message + e.Message, ErrorMessages._0205._code);
            }
            return null;
        }
        /// <summary>
        /// Function Name: GetAddressByTuid
        /// Created By: Kiefer Thorson
        /// Date Created: 3/3/2023
        /// Additional Contributors:
        /// Last Modified: 
        /// Last Modified By:
        /// 
        /// Purpose:
        /// The Purpose of this Function is to:
        ///     - Update a School's Address when passed the AddressModel
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public void UpdateAddress(AddressModel address)
        {
            try
            {
                var existingAddress = _dbContext.Addresses.FirstOrDefault(x => x.Tuid==address.Tuid);
                if (existingAddress!=null)
                {
                    existingAddress.AddressLine1 = address.AddressLine1;
                    existingAddress.City = address.City;
                    existingAddress.State = address.State;
                    existingAddress.Zipcode = address.Zipcode;
                    _dbContext.SaveChanges();
                }
                else
                {
                    // should never hit else -> should always be passed a valid school
                }
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._0206._message, ErrorMessages._0206._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._0207._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._0207._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._0208._message + e.Message, ErrorMessages._0208._code);
            }
        }


        public void DeleteAddress(int? addressTuid)
        {
            try 
            { 
                var removeAddress = _dbContext.Addresses.FirstOrDefault(x => x.Tuid == addressTuid);
                if (removeAddress!=null)
                {
                    _dbContext.Addresses.Remove(removeAddress);
                    _dbContext.SaveChanges();
                }
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._0209._message, ErrorMessages._0209._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._0210._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._0210._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._0211._message + e.Message, ErrorMessages._0211._code);
            }
        }
    }
}
