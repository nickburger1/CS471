using A_FGMS.DataLayer;
using A_FGMS.DataLayer.Entities;
using B_FGMS.BusinessLogic.Constants;
using B_FGMS.BusinessLogic.Models;
using B_FGMS.BusinessLogic.Models.Volunteer;
using Bogus.DataSets;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

/**
 ************************************************************************************************************************
 *                                      File Name : DatabaseSchoolProvider.cs                                           *
 *                                      Part of Project : CS471 Senior Capstone Project / FGMS                          *
 ************************************************************************************************************************
 *                                      Created By : Kiefer Thorson & Nathan VanSnepson                                 *
 *                                      Date Created : 2/9/23                                                           *
 *                                      Additional Contributors : CS471 WI23 Development Team                           *
 *                                      Last Modified : 2/16/2023                                                       *
 *                                      Last Modified By : Kiefer Thorson                                               *
 ************************************************************************************************************************
 * File Purpose : The Purpose of this file is to retrieve School data                                                   *
 ************************************************************************************************************************
 * Modification Log:                                                                                                    *
 * Author: Kiefer Thorson                                                                                               *
 * Date: 2/16/2023                                                                                                      *
 * Description: added GetSchoolByName                                                                                   *
 ************************************************************************************************************************
 **/
namespace B_FGMS.BusinessLogic.Services.SchoolProviders
{
	/// <summary>
	/// Class Name: DatabaseSchoolProvider
	/// Created By: Kiefer Thorson & Nathan VanSnepson
	/// Date Created: 2/9/2023
	/// Additional Contributors:
	/// Last Modified: 2/16/2023
	/// Last Modified By: Kiefer Thorson & Nathan VanSnepson
	/// 
	/// Purpose:
	/// The purpose of this class is to provide access to the School entity
	/// </summary>
	public class DatabaseSchoolProvider : ISchoolProvider
	{
		private readonly ApplicationDbContext _dbContext; //This contains the information about the database
        public event EventHandler<Events.ErrorEventArgs> DatabaseError;

        /// <summary>
        /// Function Name: DatabaseSchoolProvider
        /// Created By: Kiefer Thorson & Nathan VanSnepson
        /// Date Created: 2/9/2023
        /// Additional Contributors:
        /// Last Modified: 2/9/2023
        /// Last Modified By: Kiefer Thorson & Nathan VanSnepson
        /// 
        /// Purpose:
        /// The Purpose of this Function is to:
        ///     - Initialize the database context (dbContext)
        /// </summary>
        /// <param name="dbContext"></param>
        public DatabaseSchoolProvider(ApplicationDbContext dbContext)
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
        /// Function Name: GetAllSchools
        /// Created By: Kiefer Thorson & Nathan VanSnepson
        /// Date Created: 2/9/2023
        /// Additional Contributors:
        /// Last Modified: 2/9/2023
        /// Last Modified By: Kiefer Thorson & Nathan VanSnepson
        /// 
        /// Purpose:
        /// The Purpose of this Function is to:
        ///     - Returns collection of Schools
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SchoolModel> GetAllSchools()
		{
            try 
            { 
            return _dbContext.Schools.Select(x =>
             new SchoolModel
             {
                 Tuid = x.Tuid,
                 AddressTuid = x.AddressTuid,
                 Name = x.Name,
                 Hours = x.Hours,
                 Principal = x.Principal,
                 Secretary = x.Secretary,
                 ContactNumber = x.ContactNumber,
                 StartTime = x.StartTime,
                 EndTime = x.EndTime,
                 IsActive = x.IsActive,
                 Days = x.Days,
                 IsDeleted = x.IsDeleted,
             }).OrderBy(x => x.Name).Where(x => x.IsDeleted == false).ToList();
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1200._message, ErrorMessages._1200._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1201._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1201._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1202._message + e.Message, ErrorMessages._1202._code);
            }

            return Enumerable.Empty<SchoolModel>();
        }

		/// <summary>
		/// Function Name: GetSchoolByTuid
		/// Created By: Kiefer Thorson & Nathan VanSnepson
		/// Date Created: 2/9/2023
		/// Additional Contributors:
		/// Last Modified: 2/9/2023
		/// Last Modified By: Kiefer Thorson & Nathan VanSnepson
		/// 
		/// Purpose:
		/// The Purpose of this Function is to:
		///     - Returns a school given the schools Tuid
		/// </summary>
		/// <param name="schoolTuid"></param>
		/// <returns>School</returns>
		public SchoolModel? GetSchoolByTuid(int schoolTuid)
		{
            try { 
			    //return _dbContext.Schools.FirstOrDefault(x => x.Tuid == schoolTuid);
			    return _dbContext.Schools.Select(x =>
			    new SchoolModel
			    {
				    Tuid = x.Tuid,
				    AddressTuid = x.AddressTuid,
				    Name = x.Name,
				    Hours= x.Hours,
				    Principal = x.Principal,
				    Secretary = x.Secretary,
				    ContactNumber= x.ContactNumber,
				    StartTime= x.StartTime,
				    EndTime= x.EndTime,
				    IsActive= x.IsActive,
				    Days= x.Days
			    }).FirstOrDefault(x => x.Tuid == schoolTuid);
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1203._message, ErrorMessages._1203._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1204._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1204._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1205._message + e.Message, ErrorMessages._1205._code);
            }

            return null;
        }

        /// <summary>
        /// Function Name: GetSchoolByName
        /// Created By: Kiefer Thorson                     
        /// Date Created: 2/16/2023
        /// Additional Contributors:
        /// Last Modified: 3/2/23        
        /// Last Modified By: Kiefer Thorson                                  
        /// 
        /// Purpose:
        /// The Purpose of this Function is to:
        ///     - Returns a school given the schools name
        /// </summary>
        /// <param name="Name"></param>
        /// <returns>School of matching name</returns>
        public SchoolModel? GetSchoolByName(String Name)
		{
            try { 
                return _dbContext.Schools.Select(x =>
                new SchoolModel
                {
                    Tuid = x.Tuid,
                    AddressTuid = x.AddressTuid,
                    Name = x.Name,
                    Hours = x.Hours,
                    Principal = x.Principal,
                    Secretary = x.Secretary,
                    ContactNumber = x.ContactNumber,
                    StartTime = x.StartTime,
                    EndTime = x.EndTime,
                    IsActive = x.IsActive,
                    Days = x.Days
                }).FirstOrDefault(x => x.Name == Name);
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1206._message, ErrorMessages._1206._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1207._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1207._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1208._message + e.Message, ErrorMessages._1208._code);
            }

            return null;
        }


        /// <summary>
        /// Function Name: UpdateSchool
        /// Created By: Kiefer Thorson                     
        /// Date Created: 2/26/2023
        /// Additional Contributors:
        /// Last Modified:         
        /// Last Modified By:                                   
        /// 
        /// Purpose:
        /// The Purpose of this Function is to:
        ///     - Update the info for the passed school - will be invoked when user presses save
        /// </summary>
        /// <param name="school"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void UpdateSchool(SchoolModel school)
        {
            try { 
			    var SelectedSchool = _dbContext.Schools.FirstOrDefault(x => x.Tuid == school.Tuid);

                SelectedSchool.Name = school.Name;
                SelectedSchool.ContactNumber = school.ContactNumber;
                SelectedSchool.Principal = school.Principal;
                SelectedSchool.Secretary = school.Secretary;
			    SelectedSchool.Days = school.Days;
                // NEED TO ADD START AND END TIME ONCE DATABASE UPDATED
                SelectedSchool.IsActive = school.IsActive;
                SelectedSchool.StartTime = school.StartTime;
                SelectedSchool.EndTime = school.EndTime;

			    _dbContext.SaveChanges();
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1209._message, ErrorMessages._1209._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1210._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1210._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1211._message + e.Message, ErrorMessages._1211._code);
            }
        }

        public void AddNewSchool(SchoolModel newSchool)
        {
			// MUST CHECK TO SEE IF NAME, ADDRESS, PHONE NUMBER UNIQUE
            try{
			    var addSchool = new School()
			    {
				    AddressTuid = newSchool.AddressTuid,
				    Name = newSchool.Name,
				    //Hours = newSchool.Hours,
				    Principal = newSchool.Principal,
				    Secretary = newSchool.Secretary,
				    ContactNumber = newSchool.ContactNumber,
				    StartTime = newSchool.StartTime,
				    EndTime = newSchool.EndTime,
				    IsActive = newSchool.IsActive,
				    Days = newSchool.Days
			    };
			    _dbContext.Schools.Add(addSchool);
			    _dbContext.SaveChanges();
			    newSchool.Tuid= addSchool.Tuid;
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1212._message, ErrorMessages._1212._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1213._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1213._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1214._message + e.Message, ErrorMessages._1214._code);
            }
        }

        public void DeleteSchool(int? schoolId)
        {
            try { 
			    var removeSchool = _dbContext.Schools.FirstOrDefault(x => x.Tuid== schoolId);
			    if(removeSchool != null)
			    {
				    _dbContext.Schools.Remove(removeSchool);
				    _dbContext.SaveChanges();
			    }
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1215._message, ErrorMessages._1215._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1216._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1216._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1217._message + e.Message, ErrorMessages._1217._code);
            }
        }

		public bool? GetSchoolPhoneNum(string enteredNumber)
        {
            try { 
			    // exists is false until instance of phone number appears, then changed to true
			    bool exists = false;

                // make name lowercase and remove all spacing and formatting characters

                enteredNumber = CleanStringOfNonDigits(enteredNumber);

                var schoolList = _dbContext.Schools.Select(x =>
                 new SchoolModel
                 {
                     Tuid = x.Tuid,
                     AddressTuid = x.AddressTuid,
                     Name = x.Name,
                     Hours = x.Hours,
                     Principal = x.Principal,
                     Secretary = x.Secretary,
                     ContactNumber = x.ContactNumber,
                     StartTime = x.StartTime,
                     EndTime = x.EndTime,
                     IsActive = x.IsActive,
                     Days = x.Days
                 }).ToList();

			    foreach(var school in schoolList)
			    {
                    string number = CleanStringOfNonDigits(school.ContactNumber);
                    if (enteredNumber == number)
				    {
					    exists = true;
				    }
			    }

			    return exists;
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1218._message, ErrorMessages._1218._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1219._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1219._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1220._message + e.Message, ErrorMessages._1220._code);
            }

            return null;
        }

        private static readonly Regex rxNonDigits = new Regex(@"[^\d]+");
        // simply replace the offending substrings with an empty string
        private string CleanStringOfNonDigits(string s)
        {
            if (string.IsNullOrEmpty(s)) return s;
            string cleaned = rxNonDigits.Replace(s, "");
            return cleaned;
        }

        public bool? CheckSchoolNameExists(string name)
        {
            try
            {
                // exists is false until instance of phone number appears, then changed to true
                bool exists = false;
            
                // make name lowercase and remove all spacing
			    name = Regex.Replace(name.ToLower(), @"\s", "");

                var schoolList = _dbContext.Schools.Select(x =>
                 new SchoolModel
                 {
                     Tuid = x.Tuid,
                     AddressTuid = x.AddressTuid,
                     Name = x.Name,
                     Hours = x.Hours,
                     Principal = x.Principal,
                     Secretary = x.Secretary,
                     ContactNumber = x.ContactNumber,
                     StartTime = x.StartTime,
                     EndTime = x.EndTime,
                     IsActive = x.IsActive,
                     Days = x.Days
                 }).ToList();

                foreach (var school in schoolList)
                {
				    // take the entry in db: make lowercase and remove spacing to compare to entered value
				    string rawTextName = Regex.Replace(school.Name.ToLower(), @"\s", "");

                    if (rawTextName == name)
                    {
                        exists = true;
                    }
                }

                return exists;
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1221._message, ErrorMessages._1221._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1222._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1222._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1223._message + e.Message, ErrorMessages._1223._code);
            }

            return null;
        }

        public bool? GetSchoolPhoneNum(string number, string selectedNum)
        {
            try 
            { 
                // exists is false until instance of phone number appears, then changed to true
                bool exists = false;

                // make name lowercase and remove all spacing and formatting characters
                number = CleanStringOfNonDigits(number);
                selectedNum = CleanStringOfNonDigits(selectedNum);

                if (selectedNum != number)
                {

                    var schoolList = _dbContext.Schools.Select(x =>
                     new SchoolModel
                     {
                         Tuid = x.Tuid,
                         AddressTuid = x.AddressTuid,
                         Name = x.Name,
                         Hours = x.Hours,
                         Principal = x.Principal,
                         Secretary = x.Secretary,
                         ContactNumber = x.ContactNumber,
                         StartTime = x.StartTime,
                         EndTime = x.EndTime,
                         IsActive = x.IsActive,
                         Days = x.Days
                     }).ToList();

                    foreach (var school in schoolList)
                    {
                        string num = CleanStringOfNonDigits(school.ContactNumber);
                        if (num == number)
                        {
                            exists = true;
                        }
                    }
                }
                return exists;
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1224._message, ErrorMessages._1224._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1225._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1225._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1226._message + e.Message, ErrorMessages._1226._code);
            }

            return null;
        }

        public bool? CheckSchoolNameExists(string name, string selectedName)
        {
            try 
            { 
                // exists is false until instance of phone number appears, then changed to true
                bool exists = false;

                // make name lowercase and remove all spacing
                name = Regex.Replace(name.ToLower(), @"\s", "");

                selectedName = Regex.Replace(selectedName.ToLower(), @"\s", "");

                if (name != selectedName)
                {
                    var schoolList = _dbContext.Schools.Select(x =>
                     new SchoolModel
                     {
                         Tuid = x.Tuid,
                         AddressTuid = x.AddressTuid,
                         Name = x.Name,
                         Hours = x.Hours,
                         Principal = x.Principal,
                         Secretary = x.Secretary,
                         ContactNumber = x.ContactNumber,
                         StartTime = x.StartTime,
                         EndTime = x.EndTime,
                         IsActive = x.IsActive,
                         Days = x.Days
                     }).ToList();

                    foreach (var school in schoolList)
                    {
                        // take the entry in db: make lowercase and remove spacing to compare to entered value
                        string rawTextName = Regex.Replace(school.Name.ToLower(), @"\s", "");

                        if (rawTextName == name)
                        {
                            exists = true;
                        }
                    }
                }
                return exists;
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1227._message, ErrorMessages._1227._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1228._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1228._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1229._message + e.Message, ErrorMessages._1229._code);
            }

            return null;
        }

        /// <summary>
		/// Returns a list of all school tuids and names
		/// </summary>
		/// <returns></returns>
		/// <author> Isabelle Johns </author>
		/// <created> 3/26/23 </created>
        public IEnumerable<SchoolNameIdModel> GetSchoolNameAndId()
        {
            try
            {
                return _dbContext.Schools.Select(x => new SchoolNameIdModel { Tuid = x.Tuid, Name = x.Name }).ToList();
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1230._message, ErrorMessages._1230._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1231._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1231._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1232._message + e.Message, ErrorMessages._1232._code);
            }

            return Enumerable.Empty<SchoolNameIdModel>();
        }



        // delete flag school takes the value of isdeleted and flips it
        public void DeleteFlagSchool(SchoolModel school)
        {
            try { 
                var SelectedSchool = _dbContext.Schools.FirstOrDefault(x => x.Tuid == school.Tuid);

                if(SelectedSchool.IsDeleted == false)
                {
                    SelectedSchool.IsDeleted = true;

                }
                else
                {
                    SelectedSchool.IsDeleted = false;
                }

                _dbContext.SaveChanges();

            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1233._message, ErrorMessages._1233._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1234._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1234._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1235._message + e.Message, ErrorMessages._1235._code);
            }
        }
    }
}