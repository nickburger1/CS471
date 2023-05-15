using A_FGMS.DataLayer;
using A_FGMS.DataLayer.Entities;
using B_FGMS.BusinessLogic.Models;
using B_FGMS.BusinessLogic.Models.Volunteer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using B_FGMS.BusinessLogic.Models;
using B_FGMS.BusinessLogic.Models.Volunteer;

using System.Security.Cryptography;
using System.Xml.XPath;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System.Windows;
using DocumentFormat.OpenXml.Office.CustomUI;
using B_FGMS.BusinessLogic.Services.FinanceProviders;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;
using B_FGMS.BusinessLogic.ViewModels.VolunteerAddViewModels;
using Microsoft.Win32.SafeHandles;
using B_FGMS.BusinessLogic.ViewModels;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Wordprocessing;
using B_FGMS.BusinessLogic.Constants;
using Microsoft.Data.SqlClient;
using System.Reflection.Emit;
using Newtonsoft.Json;
using C_FGMS.UI.Helpers;

/**
************************************************************************************************************************
*                                      File Name : DatabaseVolunteerProvider.cs                                        *
*                                      Part of Project : CS471 Senior Capstone Project / FGMS                          *
************************************************************************************************************************
*                                      Created By :                                                                    *
*                                      Date Created :                                                                  *
*                                      Additional Contributors : CS471 WI23 Development Team                           *
*                                      Last Modified : 2/17/23                                                         *
*                                      Last Modified By : Kiefer Thorson					                            *
************************************************************************************************************************
* File Purpose : The Purpose of this file is to retrieve Volunteer data                                                *
************************************************************************************************************************
* Modification Log:                                                                                                    *
* Author: Kiefer Thorson & Nathan VanSnepson                                                                           *
* Date: 2/9/23                                                                                                         *
* Description: added getActiveVolunteersCount, getInactiveVolunteersCount, getVolunteersCount                          *
*																														*
* Author: Kiefer Thorson																								*
* Date: 2/16/2023                                                                                                      *
* Description: added GetVolunteerByTuid																				*
************************************************************************************************************************
**/
namespace B_FGMS.BusinessLogic.Services.VolunteerProviders
{
	public class DatabaseVolunteerProvider : IVolunteerProvider
	{
		private readonly ApplicationDbContext _dbContext;
        public event EventHandler<Events.ErrorEventArgs> DatabaseError;

        /// <summary>
        /// Function Name: DatabaseVolunteerProvider
        /// Created By: Kiefer Thorson & Nathan VanSnepson
        /// Date Created: 2/9/2023
        /// Additional Contributors:
        /// Last Modified: 2/9/2023
        /// Last Modified By: Kiefer Thorson & Nathan VanSnepson
        /// 
        /// Purpose:
        /// The Purpose of this Function is to:
        ///     - Constructor for the Volunteer Provider. Sets the Context.
        /// </summary>
        /// <param name="dbContext"></param>
        public DatabaseVolunteerProvider(ApplicationDbContext dbContext)
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
        /// Function Name: GetActiveVolunteersCount
        /// Created By: Kiefer Thorson & Nathan VanSnepson
        /// Date Created: 2/9/2023
        /// Additional Contributors:
        /// Last Modified: 2/9/2023
        /// Last Modified By: Kiefer Thorson & Nathan VanSnepson
        /// 
        /// Purpose:
        /// The Purpose of this Function is to:
        ///     - Retrieve count of active _volunteers from the database
        /// </summary>
        /// <returns>int - number of active _volunteers</returns>
        public int GetActiveVolunteersCount()
		{
            try
            {
             //   return _dbContext.Volunteers.Where(x => x.SeparatedDate == null).ToList().Count;
                return _dbContext.Volunteers.Where(x => x.IsDeleted == false && x.SeparatedDate == null).ToList().Count;
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1500._message, ErrorMessages._1500._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1501._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1501._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1502._message + e.Message, ErrorMessages._1502._code);
            }

            return -1;
        }

		/// <summary>
		/// This method returns the full name of each volunteer in the system
		/// </summary>
		/// <returns>an enumerable list of strings with each _volunteers full name</returns>
		/// <author>Andrew Loesel</author>
		/// <created>2/6/2023</created>
		public List<VolunteerModel> GetAllVolunteersIncludeInactive()
		{
			List<VolunteerModel> Volunteers = new List<VolunteerModel>();

            try
            {
                Volunteers.AddRange(_dbContext.Volunteers.Where(x => x.IsDeleted == false).Select(x => new VolunteerModel { FirstName = x.FirstName, LastName = x.LastName, Tuid = x.Tuid }).Distinct().ToArray());
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1503._message, ErrorMessages._1503._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1504._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1504._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1505._message + e.Message, ErrorMessages._1505._code);
            }


            return Volunteers.OrderBy(x => x.FullName).ToList();
		}

		public IEnumerable<Volunteer> GetAllVolunteers()
		{
            try
            {
                return _dbContext.Volunteers.Include(x => x.IdentifiesAs).Include(x => x.Ethnicity).ToList();
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1506._message, ErrorMessages._1506._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1507._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1507._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1508._message + e.Message, ErrorMessages._1508._code);
            }

            return Enumerable.Empty<Volunteer>();
        }

        /// <summary>
        /// Function Name: GetInactiveVolunteersCount
        /// Created By: Kiefer Thorson & Nathan VanSnepson
        /// Date Created: 2/9/2023
        /// Additional Contributors:
        /// Last Modified: 2/9/2023
        /// Last Modified By: Kiefer Thorson & Nathan VanSnepson
        /// 
        /// Purpose:
        /// The Purpose of this Function is to:
        ///     - Retrieve count of active _volunteers from the database
        ///     
        /// Todo:
        ///		- Add logic to remove separated _volunteers
        /// </summary>
        /// <returns>int - number of inactive _volunteers</returns>
        public int GetInactiveVolunteersCount()
        {
            try
            {
                return _dbContext.Volunteers.Where(x => x.SeparatedDate != null && x.IsDeleted == false).ToList().Count;
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1509._message, ErrorMessages._1509._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1510._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1510._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1511._message + e.Message, ErrorMessages._1511._code);
            }

            return -1;
        }

		/// <summary>
		/// Function Name: GetVolunteersByTuid
		/// Created By: Kiefer Thorson
		/// Date Created: 2/16/2023
		/// Additional Contributors:
		/// Last Modified:
		/// Last Modified By:
		/// 
		/// Purpose:
		/// The Purpose of this Function is to:
		///     - Retrieve a volunteer's personal information given their tuid
		/// </summary>
		/// <param name="volunteerTuid"></param>
		/// <returns>Volunteer - Selected Vol's personal info </returns>
		public Volunteer? GetVolunteerByTuid(int volunteerTuid)
		{
            try
            {
                return _dbContext.Volunteers.FirstOrDefault(x => x.Tuid == volunteerTuid);
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1512._message, ErrorMessages._1512._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1513._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1513._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1514._message + e.Message, ErrorMessages._1514._code);
            }

            return null;
        }

		/// <summary>
		/// Function Name: GetVolunteersCount
		/// Created By: Kiefer Thorson & Nathan VanSnepson
		/// Date Created: 2/9/2023
		/// Additional Contributors:
		/// Last Modified: 2/9/2023
		/// Last Modified By: Kiefer Thorson & Nathan VanSnepson
		/// 
		/// Purpose:
		/// The Purpose of this Function is to:
		///     - Retrieve count of all _volunteers from the database
		/// </summary>
		/// <returns>int - number of _volunteers</returns>
		public int GetVolunteersCount()
		{
            try
            {
                return _dbContext.Volunteers.ToList().Count;
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1515._message, ErrorMessages._1515._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1516._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1516._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1517._message + e.Message, ErrorMessages._1517._code);
            }

            return -1;
        }

        /// <summary>
        /// Function Name: GetVolunteerNameAndId
        /// Created By: Christopher Washburn
        /// Last Modified: 3/23/2023
        /// Last Modified By: Christopher Washburn 
        /// 
        /// Purpose: The purpose of this function is to get the Name and Tuid of a volunteer from the database and pass it to be
        /// used in the various volunteer comboboxes through out the program
        /// </summary>
        /// <returns></returns>
        public IEnumerable<VolunteerNameIdModel> GetVolunteerNameAndId()
		{
            try
            {
                return _dbContext.Volunteers.Where(x => !x.IsDeleted).Select(x => new VolunteerNameIdModel { Tuid = x.Tuid, FullName = x.FullName, FirstName = x.FirstName, LastName = x.LastName }).OrderBy(x => x.LastName).ThenBy(x => x.FirstName).ToList();
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1518._message, ErrorMessages._1518._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1519._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1519._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1520._message + e.Message, ErrorMessages._1520._code);
            }

            return Enumerable.Empty<VolunteerNameIdModel>();
        }

        public bool IsVolunteerInActive(int volunteerTUID)
        {
            try
            {
                return _dbContext.Volunteers.Any(x => x.Tuid == volunteerTUID && x.SeparatedDate != null);
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1524._message, ErrorMessages._1524._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1525._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1525._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1526._message + e.Message, ErrorMessages._1526._code);
            }

            return false;
        }

        /// <summary>
        /// This method will return the list of all volunteers who are currently assigned at a school
        /// </summary>
        /// <returns></returns>
        /// <author>Andrew Loesel</author>
        /// <created>4/6/2023</created>
        public IEnumerable<VolunteerNameIdModel> GetVolunteersWithSchools()
        {
            try
            {
                return _dbContext.Assignments.Include(x => x.Volunteer).Select(x => new VolunteerNameIdModel
                {
                    Tuid = x.Volunteer.Tuid,
                    LastName = x.Volunteer == null ? "N/A" : x.Volunteer.LastName,
                    FirstName = x.Volunteer == null ? "N/A" : x.Volunteer.FirstName,
                    FullName = x.Volunteer == null ? "N/A" : x.Volunteer.FullName
                }).Distinct().ToList();
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1674._message, ErrorMessages._1674._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1675._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1675._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1676._message + e.Message, ErrorMessages._1676._code);
            }

            return Enumerable.Empty<VolunteerNameIdModel>();
        }

        #region Volunteer General Tab
        /// <summary>
        /// Function Name: GetVolunteerGeneralInfo
        /// Created By: Christopher Washburn & Isabelle Johns
        /// Date Created 2/25/2023
        /// Additional Contributors:
        /// Last Modified: 3/26/2023
        /// Last Modified By: Isabelle Johns
        /// 
        /// Purpose: The purpose of this function is to get the
        /// general assignment data of a volunteer, put it in its model, and return it
        /// to the front end for data binding.
        /// </summary>
        /// <param name="volunteerTUID"></param>
        /// <returns> 
        ///		VolunteerGeneralModel - The model used to hold all the general information of
        ///     a volunteer.
        ///	</returns>
        public VolunteerGeneralModel? GetVolunteerGeneralInfo(int volunteerTUID)
		{
            try
            {
                var volunteer = _dbContext.Volunteers.FirstOrDefault(x => x.Tuid.Equals(volunteerTUID));
                if (volunteer != null)
                {
                    var assignment = _dbContext.Assignments.Where(x => !(x.IsDeleted ?? false)).Include(x => x.Classroom).FirstOrDefault(x => x.VolunteerTuid.Equals(volunteerTUID));
                    var address = _dbContext.Addresses.FirstOrDefault(x => x.Tuid.Equals(volunteer.AddressTuid));
       

                    if (address != null)
                    {
                        var volunteerInfo = new VolunteerGeneralModel
                        {
                            Phone = volunteer.Phone,
                            Address1 = address.AddressLine1 ?? string.Empty,
                            Address2 = address.AddressLine2 ?? string.Empty,
                            State = address.State,
                            City = address.City,
                            ZipCode = address.Zipcode,
                            Email = volunteer.Email,
                            AltPhone = volunteer.AltPhone,
                            IsActive = volunteer.IsActive,
                            StartDate = volunteer.StartDate,
                            EndDate = volunteer.SeparatedDate
                        };

                        if (assignment != null)
                        {
                            volunteerInfo.SchoolTuid = assignment.Classroom!.SchoolTuid;
                        }

                        return volunteerInfo;
                    }
                }
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1527._message, ErrorMessages._1527._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1528._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1528._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1529._message + e.Message, ErrorMessages._1529._code);
            }
            return null;
        }

		/// <summary>
		/// Function Name: GetVolunteerOneTimeChecks
		/// Created By: Christopher Washburn & Isabelle Johns
		/// Date Created 2/25/2023
		/// Additional Contributors:
		/// Last Modified: 2/25/2023
		/// Last Modified By: Christopher Washburn
		/// 
		/// Purpose: The purpose of this function is to get the
		/// one time checks data of a volunteer, put it in its model, and return it
		/// to the front end for data binding.
		/// </summary>
		/// <param name="volunteerTUID"></param>
		/// <returns>
		///		OneTimeChecksModel - the model used to hold all the one time checks data
		///		for a volunteer
		/// </returns>
		public OneTimeChecksModel? GetVolunteerOneTimeChecks(int volunteerTUID)
		{
            try
            {
                var oneTimeChecks = _dbContext.OneTimeChecks.FirstOrDefault(x => x.VolunteerTuid.Equals(volunteerTUID));

                if (oneTimeChecks != null)
                {
                    return new OneTimeChecksModel
                    {
                        FilePhoto = oneTimeChecks.HasFilePhoto,
                        ServiceDescription = oneTimeChecks.HasServiceDescription,
                        OrientTraining = oneTimeChecks.HasTrainingSheet,
                        ConfidenceSOU = oneTimeChecks.ConfidenceSouDate,
                        ServiceStartDate = oneTimeChecks.ServiceStartDate,
                        NSCHCCheckForm = oneTimeChecks.HasNschc,
                        BackgroundCheck = oneTimeChecks.HasBackgroundCheck,
                        IDCopy = oneTimeChecks.HasIdCopy,
                        NSOPW = oneTimeChecks.NsopwDate,
                        IChat = oneTimeChecks.IChatDate,
                        TrueScreen = oneTimeChecks.TrueScreenDate,
                        AliasFingerPrint = oneTimeChecks.AliasFingerprintDate,
                        FieldPrintCleared = oneTimeChecks.FieldPrintDate,
                        DHS = oneTimeChecks.DhsDate,
                        TBShot = oneTimeChecks.TbShotDate
                    };
                }
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1530._message, ErrorMessages._1530._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1531._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1531._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1532._message + e.Message, ErrorMessages._1532._code);
            }

            return null;

        }

		/// <summary>
		/// Function Name: GetVolunteerAnnualChecks
		/// Created By: Christopher Washburn & Isabelle Johns
		/// Date Created 2/25/2023
		/// Additional Contributors:
		/// Last Modified: 2/25/2023
		/// Last Modified By: Christopher Washburn
		/// 
		/// Purpose: The purpose of this function is to get the
		/// annual checks data of a volunteer, put it in its model, and return it
		/// to the front end for data binding.
		/// </summary>
		/// <param name="volunteerTUID"></param>
		/// <returns>
		///		AnnualChecksModel - the model that holds all the annual checks data 
		///		for a volunteer.
		/// </returns>
		public AnnualChecksModel? GetVolunteerAnnualChecks(int volunteerTUID, int year)
		{
            try
            {
                var annualChecks = _dbContext.AnnualChecks.Where(x => x.Year.Equals(year)).FirstOrDefault(x => x.VolunteerTuid.Equals(volunteerTUID));
                if (annualChecks != null)
                {
                    return new AnnualChecksModel
                    {
                        SchedulePhotoRelease = annualChecks.PhotoReleaseDate,
                        EmergancyBeneficiaryForm = annualChecks.EmergencyBeneficiaryDate,
                        HippaRelease = annualChecks.HippaReleaseDate,
                        Physical = annualChecks.PhysicalDate,
                        AnnualIncomeCarInsurance = annualChecks.CarInsuranceDate
                    };
                }
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1533._message, ErrorMessages._1533._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1534._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1534._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1535._message + e.Message, ErrorMessages._1535._code);
            }

            return null;
        }

        /// <summary>
        /// Function Name: GetVolunteerAnnualCheck
        /// Created By: Timothy Johnson
        /// Date Created 3/20/2023
        /// Additional Contributors:
        /// Last Modified: 2/25/2023
		/// Purpose: Slight modification to Function above.
		/// Returns AnnualChecksReportModel and Includes CovidSOU
        public IEnumerable<AnnualChecksReportModel> GetVolunteerAnnualCheck()
        {
            try
            {
                return _dbContext.Volunteers.Where(x => x.IsDeleted == false).Join(_dbContext.AnnualChecks, volunteers => volunteers.Tuid, annualchecks => annualchecks.VolunteerTuid,
                    (volunteers, annualchecks) => new { Volunteer = volunteers, AnnualCheck = annualchecks }).Join(_dbContext.OneTimeChecks, x => x.Volunteer.Tuid, onetimecheck => onetimecheck.VolunteerTuid,
                    (x, onetimecheck) => new { x.Volunteer, x.AnnualCheck, OneTimeCheck = onetimecheck })
                    .Select(x => new AnnualChecksReportModel
                    {
                        Name = x.Volunteer.LastName + ", " + x.Volunteer.FirstName,
                        SchedulePhotoRelease = x.AnnualCheck.PhotoReleaseDate,
                        EmergancyBeneficiaryForm = x.AnnualCheck.EmergencyBeneficiaryDate,
                        HippaRelease = x.AnnualCheck.HippaReleaseDate,
                        Physical = x.AnnualCheck.PhysicalDate,
                        AnnualIncomeCarInsurance = x.AnnualCheck.CarInsuranceDate,
                        CovidSOU = x.AnnualCheck.CovidSouDate,
                        FilePhoto = x.OneTimeCheck.HasFilePhoto,
                        ServiceDescription = x.OneTimeCheck.HasServiceDescription,
                        OrientationTraining = x.OneTimeCheck.HasTrainingSheet,
                        ConfidSOU = x.OneTimeCheck.ConfidenceSouDate,
                        ServiceStartDate = x.OneTimeCheck.ServiceStartDate,
                        NSCHC = x.OneTimeCheck.HasNschc,
                        BackgroundCheck = x.OneTimeCheck.HasBackgroundCheck,
                        IDCopy = x.OneTimeCheck.HasIdCopy,
                        NSOPW = x.OneTimeCheck.NsopwDate,
                        IChat = x.OneTimeCheck.IChatDate,
                        TrueScreen = x.OneTimeCheck.TrueScreenDate,
                        AliasFPrint = x.OneTimeCheck.AliasFingerprintDate,
                        FieldPrintCleared = x.OneTimeCheck.FieldPrintDate,
                        DHS = x.OneTimeCheck.DhsDate,
                        TBShot = x.OneTimeCheck.TbShotDate,
                        IsActive = x.Volunteer.IsActive,
                        IsDeleted = x.Volunteer.IsDeleted,
                        SeparatedTime = x.Volunteer.SeparatedDate,

                    }).ToList();
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1536._message, ErrorMessages._1536._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1537._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1537._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1538._message + e.Message, ErrorMessages._1538._code);
            }

            return Enumerable.Empty<AnnualChecksReportModel>();

        }

        public IEnumerable<int> GetAnnualCheckYears()
        {
            try
            {
                return _dbContext.AnnualChecks.Select(x => x.Year).Distinct().ToList();
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1539._message, ErrorMessages._1539._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1540._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1540._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1541._message + e.Message, ErrorMessages._1541._code);
            }

            return Enumerable.Empty<int>();
        }

        /// <summary>
        /// Returns the global temporary info fields to populate the form
        /// </summary>
        /// <returns> 
        ///     IEnumerable<TemporaryInfoModel> - a list of TemporaryInfoModels that contains
        ///     the label and value type (boolean or date) of each current Temporary Info field
        /// </returns>
        /// <author> Isabelle Johns </author>
        /// <created> 3/27/23 </created>
        public IEnumerable<TemporaryInfoModel> GetTemporaryInfo()
		{
            try
            {
                return _dbContext.TempInfoTypeItems.Select(
                        x => new TemporaryInfoModel
                        {
                            Tuid = x.Tuid,
                            Name = x.Name,
                            Type = x.TempInfoType
                        }
                    ).ToList();
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1542._message, ErrorMessages._1542._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1543._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1543._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1544._message + e.Message, ErrorMessages._1544._code);
            }

            return Enumerable.Empty<TemporaryInfoModel>();
        }

        /// <summary>
        /// Function Name: GetVolunteerTemporaryInfo
        /// Created By: Christopher Washburn & Isabelle Johns
        /// Date Created 2/25/2023
        /// Additional Contributors:
        /// Last Modified: 3/27/2023
        /// Last Modified By: Isabelle Johns
        /// 
        /// Purpose: The purpose of this function is to get the
        /// temporary information data of a volunteer, put it in its model, and return it
        /// to the front end for data binding.
        /// </summary>
        /// <param name="volunteerTUID"></param>
        /// <returns>
        ///		TemporaryInfoModel - a model that holds all the temporary information
        ///		for a volunteer.
        /// </returns>
        public IEnumerable<TemporaryInfoModel> GetVolunteerTemporaryInfo(int volunteerTUID)
		{
            try
            {
                return _dbContext.TempInfoEntries.Include(x => x.TempInfoType).Where(x => x.VolunteerTuid.Equals(volunteerTUID)).OrderBy(x => x.TempInfoTypeItemTuid).Select(
                    x => new TemporaryInfoModel
                    {
                        Tuid = x.Tuid,
                        Value = x.value
                    }).ToList();
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1542._message, ErrorMessages._1542._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1543._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1543._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1544._message + e.Message, ErrorMessages._1544._code);
            }

            return Enumerable.Empty<TemporaryInfoModel>();

        }

        public void UpdateVolunteer(VolunteerGeneralViewModel volunteerGeneral)
        {
                try
                {
                    var volunteerTuid = volunteerGeneral.VolunteerTuid;
                    var volunteer = _dbContext.Volunteers.FirstOrDefault(x => x.Tuid.Equals(volunteerTuid));
                    if (volunteer != null)
                    {
                        var assignment = _dbContext.Assignments.Include(x => x.Classroom).FirstOrDefault(x => x.VolunteerTuid.Equals(volunteerTuid));
                        if (assignment != null)
                        {
                            if (assignment.Classroom != null && volunteerGeneral.SchoolTuid.HasValue)
                            {
                                assignment.Classroom.SchoolTuid = volunteerGeneral.SchoolTuid.Value;

                                if (assignment.IsDeleted == true)
                                {
                                    assignment.IsDeleted = false;
                                }
                            }
                            else
                            {
                                assignment.IsDeleted = true;
                            }
                        }
                        else
                        {
                            if (volunteerGeneral.SchoolTuid.HasValue)
                            {
                                var newClassroom = new Classroom
                                {
                                    SchoolTuid = volunteerGeneral.SchoolTuid.Value
                                };

                                _dbContext.Classrooms.Add(newClassroom);
                                _dbContext.SaveChanges();

                                var newAssignment = new Assignment
                                {
                                    VolunteerTuid = volunteerGeneral.VolunteerTuid!.Value,
                                    ClassroomTuid = newClassroom.Tuid
                                };

                                _dbContext.Assignments.Add(newAssignment);
                                _dbContext.SaveChanges();

                            }
                        }

                        volunteer.Phone = volunteerGeneral.Phone ?? string.Empty;

                        var address = _dbContext.Addresses.FirstOrDefault(x => x.Tuid.Equals(volunteer.AddressTuid));
                        if (address != null)
                        {
                            address.AddressLine1 = volunteerGeneral.Address1 ?? string.Empty;
                            address.AddressLine2 = volunteerGeneral.Address2 ?? string.Empty;
                            address.State = volunteerGeneral.State ?? string.Empty;
                            address.City = volunteerGeneral.City ?? string.Empty;
                            address.Zipcode = volunteerGeneral.ZipCode ?? string.Empty;
                        }

                        volunteer.Email = volunteerGeneral.Email ?? string.Empty;
                        volunteer.AltPhone = volunteerGeneral.AltPhone;
                        volunteer.StartDate = volunteerGeneral.StartDate;
                        volunteer.SeparatedDate = volunteerGeneral.EndDate;

                        volunteer.LastUpdated = DateTime.Now;

                        var oneTimeChecks = _dbContext.OneTimeChecks.FirstOrDefault(x => x.Tuid.Equals(volunteerTuid));
                        if (oneTimeChecks != null)
                        {
                            oneTimeChecks.HasFilePhoto = volunteerGeneral.FilePhoto;
                            oneTimeChecks.HasServiceDescription = volunteerGeneral.ServiceDescription;
                            oneTimeChecks.HasTrainingSheet = volunteerGeneral.OrientTraining;
                            oneTimeChecks.ConfidenceSouDate = volunteerGeneral.ConfidenceSOU;
                            oneTimeChecks.ServiceStartDate = volunteerGeneral.ServiceStartDate;
                            oneTimeChecks.HasNschc = volunteerGeneral.NSCHCCheckForm;
                            oneTimeChecks.HasBackgroundCheck = volunteerGeneral.BackgroundCheck;
                            oneTimeChecks.HasIdCopy = volunteerGeneral.IDCopy;
                            oneTimeChecks.NsopwDate = volunteerGeneral.NSOPW;
                            oneTimeChecks.IChatDate = volunteerGeneral.IChat;
                            oneTimeChecks.TrueScreenDate = volunteerGeneral.TrueScreen;
                            oneTimeChecks.AliasFingerprintDate = volunteerGeneral.AliasFieldPrint;
                            oneTimeChecks.FieldPrintDate = volunteerGeneral.FieldPrint;
                            oneTimeChecks.DhsDate = volunteerGeneral.DHS;
                            oneTimeChecks.TbShotDate = volunteerGeneral.TBShot;
                        }

                        var annualChecks = _dbContext.AnnualChecks.Where(x => x.Year.Equals(volunteerGeneral.Year)).FirstOrDefault(x => x.VolunteerTuid.Equals(volunteerTuid));
                        if (annualChecks != null)
                        {
                            annualChecks.Year = volunteerGeneral.Year!.Value;
                            annualChecks.PhotoReleaseDate = volunteerGeneral.SchedulePhotoRelease;
                            annualChecks.EmergencyBeneficiaryDate = volunteerGeneral.EmergencyBeneficiary;
                            annualChecks.HippaReleaseDate = volunteerGeneral.HippaRelease;
                            annualChecks.PhysicalDate = volunteerGeneral.Physical;
                            annualChecks.CarInsuranceDate = volunteerGeneral.AnnualIncomeCarInsurance;
                        }
                        else
                        {
                            var newAnnualChecks = new AnnualCheck
                            {
                                VolunteerTuid = volunteerTuid!.Value,
                                Year = volunteerGeneral.Year!.Value,
                                PhotoReleaseDate = volunteerGeneral.SchedulePhotoRelease,
                                EmergencyBeneficiaryDate = volunteerGeneral.EmergencyBeneficiary,
                                HippaReleaseDate = volunteerGeneral.HippaRelease,
                                PhysicalDate = volunteerGeneral.Physical,
                                CarInsuranceDate = volunteerGeneral.AnnualIncomeCarInsurance
                            };

                            _dbContext.AnnualChecks.Add(newAnnualChecks);
                        }

                        _dbContext.SaveChanges();
                    }
                }
                catch (SqlException e)
                {
                    if (e.ErrorCode == -2146232060)
                    {
                        OnDatabaseError(ErrorMessages._1548._message, ErrorMessages._1548._code);
                    }
                    else
                    {
                        OnDatabaseError(ErrorMessages._1549._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1549._code);
                    }
                }
                catch (Exception e)
                {
                    OnDatabaseError(ErrorMessages._1550._message + e.Message, ErrorMessages._1550._code);
                }
        }

        /// <summary>
        /// Function Name: PushVolunteerGeneralInfo
        /// Created By: Christopher Washburn & Isabelle Johns
        /// Date Created 2/25/2023
        /// Additional Contributors:
        /// Last Modified: 2/25/2023
        /// Last Modified By: Isabelle Johns
        /// 
        /// Purpose: The purpose of this function is to push any changes to the
        /// general information of a volunteer to the database.
        /// </summary>
        /// <param name="volunteerGeneral"></param>
        /// <param name="volunteerTuid"></param>
        public void PushVolunteerGeneralInfo(VolunteerGeneralModel volunteerGeneral, int volunteerTuid)
        {
            try
            {
                var volunteer = _dbContext.Volunteers.FirstOrDefault(x => x.Tuid.Equals(volunteerTuid));
                if (volunteer != null)
                {
                    var assignment = _dbContext.Assignments.FirstOrDefault(x => x.VolunteerTuid.Equals(volunteerTuid));
                    if (assignment != null)
                    {
                        if (volunteerGeneral.SchoolTuid.HasValue)
                        {
                            assignment.Classroom.SchoolTuid = volunteerGeneral.SchoolTuid.Value;
                        }
                    }

                    volunteer.Phone = volunteerGeneral.Phone ?? string.Empty;

                    var address = _dbContext.Addresses.FirstOrDefault(x => x.Tuid.Equals(volunteer.AddressTuid));
                    if (address != null)
                    {
                        address.AddressLine1 = volunteerGeneral.Address1 ?? string.Empty;
                        address.AddressLine2 = volunteerGeneral.Address2 ?? string.Empty;
                        address.State = volunteerGeneral.State ?? string.Empty;
                        address.City = volunteerGeneral.City ?? string.Empty;
                        address.Zipcode = volunteerGeneral.ZipCode ?? string.Empty;
                    }

                    volunteer.Email = volunteerGeneral.Email ?? string.Empty;
                    volunteer.AltPhone = volunteerGeneral.AltPhone;
                    volunteer.StartDate = volunteerGeneral.StartDate ?? DateTime.Now.Date;
                    volunteer.SeparatedDate = volunteerGeneral.EndDate;

                    volunteer.LastUpdated = DateTime.Now;

                    _dbContext.SaveChanges();
                }
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1551._message, ErrorMessages._1551._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1552._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1552._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1553._message + e.Message, ErrorMessages._1553._code);
            }
        }
          
		/// <summary>
		/// Function Name: PushOneTimeCheck
		/// Created By: Christopher Washburn & Isabelle Johns
		/// Date Created 2/25/2023
		/// Additional Contributors:
		/// Last Modified: 2/25/2023
		/// Last Modified By: Christopher Washburn
		/// 
		/// Purpose: The purpose of this function is to push any changes to the
		/// one time checks of a volunteer to the database.
		/// </summary>
		/// <param name="oneTimeChecks"></param>
		/// <param name="volunteerTuid"></param>
		public void PushOneTimeCheck(OneTimeChecksModel oneTimeChecks, int volunteerTuid)
		{
            try
            {
                var result = _dbContext.OneTimeChecks.Where(x => x.Tuid.Equals(volunteerTuid)).FirstOrDefault();
                if (result != null)
                {
                    result.HasFilePhoto = oneTimeChecks.FilePhoto;
                    result.HasServiceDescription = oneTimeChecks.ServiceDescription;
                    result.HasTrainingSheet = oneTimeChecks.OrientTraining;
                    result.ConfidenceSouDate = oneTimeChecks.ConfidenceSOU;
                    result.ServiceStartDate = oneTimeChecks.ServiceStartDate;
                    result.HasNschc = oneTimeChecks.NSCHCCheckForm;
                    result.HasBackgroundCheck = oneTimeChecks.BackgroundCheck;
                    result.HasIdCopy = oneTimeChecks.IDCopy;
                    result.NsopwDate = oneTimeChecks.NSOPW;
                    result.IChatDate = oneTimeChecks.IChat;
                    result.TrueScreenDate = oneTimeChecks.TrueScreen;
                    result.AliasFingerprintDate = oneTimeChecks.AliasFingerPrint;
                    result.FieldPrintDate = oneTimeChecks.FieldPrintCleared;
                    result.DhsDate = oneTimeChecks.DHS;
                    result.TbShotDate = oneTimeChecks.TBShot;

                    _dbContext.SaveChanges();
                }
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1554._message, ErrorMessages._1554._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1555._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1555._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1556._message + e.Message, ErrorMessages._1556._code);
            }
        }

        /// <summary>
        /// Function Name: PushAnnualChecks
        /// Created By: Christopher Washburn & Isabelle Johns
        /// Date Created 2/25/2023
        /// Additional Contributors:
        /// Last Modified: 2/25/2023
        /// Last Modified By: Christopher Washburn
        /// 
        /// Purpose: The purpose of this function is to push any changes to the
        /// annual checks of a volunteer to the database.
        /// </summary>
        /// <param name="annualChecks"></param>
        /// <param name="volunteerTuid"></param>
        public void PushAnnualChecks(AnnualChecksModel annualChecks, int volunteerTuid)
		{
            try
            {
                var result = _dbContext.AnnualChecks.FirstOrDefault(x => x.Tuid.Equals(volunteerTuid));
                if (result != null)
                {
                    result.PhotoReleaseDate = annualChecks.SchedulePhotoRelease;
                    result.EmergencyBeneficiaryDate = annualChecks.EmergancyBeneficiaryForm;
                    result.HippaReleaseDate = annualChecks.HippaRelease;
                    result.PhysicalDate = annualChecks.Physical;
                    result.CarInsuranceDate = annualChecks.AnnualIncomeCarInsurance;

                    _dbContext.SaveChanges();
                }
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1557._message, ErrorMessages._1557._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1558._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1558._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1559._message + e.Message, ErrorMessages._1559._code);
            }
        }

        /// <summary>
        /// Function Name: PushTemporaryInfo
        /// Created By: Christopher Washburn & Isabelle Johns
        /// Date Created 2/25/2023
        /// Additional Contributors:
        /// Last Modified: 2/25/2023
        /// Last Modified By: Christopher Washburn
        /// 
        /// Purpose: The purpose of this function is to push any changes to the
        /// temporary information of a volunteer to the database.
        /// </summary>
        /// <param name="temporaryInfoModel"></param>
        /// <param name="volunteerTuid"></param>
        public void PushTemporaryInfo(IEnumerable<TemporaryInfoModel> temporaryInfoModels, int volunteerTuid)
        {
            try
            {
                var volunteerTempInfo = _dbContext.TempInfoEntries.Where(x => x.VolunteerTuid.Equals(volunteerTuid));
                foreach (var model in temporaryInfoModels)
                {
                    if (model.Value != null)
                    {
                        var currentTempInfo = volunteerTempInfo.FirstOrDefault(x => x.TempInfoTypeItemTuid.Equals(model.Tuid));
                        if (currentTempInfo != null)
                        {
                            currentTempInfo.value = model.Value;
                        }
                        else
                        {
                            var newTempInfo = new TempInfoEntry
                            {
                                VolunteerTuid = volunteerTuid,
                                TempInfoTypeItemTuid = model.Tuid,
                                value = model.Value
                            };

                            _dbContext.TempInfoEntries.Add(newTempInfo);
                        }
                    }
                }

                _dbContext.SaveChanges();
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1560._message, ErrorMessages._1560._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1561._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1561._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1562._message + e.Message, ErrorMessages._1562._code);
            }
        }

        /// <summary>
        /// Adds a new instance of a volunteer to the database
        /// </summary>
        /// <param name="volunteerInfo"></param>
        /// <author> Isabelle Johns </author>
        /// <created> 3/31/23 </created>
        public void PushNewVolunteer(VolunteerAddViewModel volunteerInfo)
        {
            var volunteer = new Volunteer();

            volunteer.FirstName = volunteerInfo.FirstName;
            volunteer.LastName = volunteerInfo.LastName;
            volunteer.Email = string.Empty;
            volunteer.Phone = string.Empty;
            volunteer.StartDate = volunteerInfo.StartDate;
            volunteer.LastUpdated = DateTime.Now;

            volunteer.Birthday = volunteerInfo.DateOfBirth;
            volunteer.GenderTuid = volunteerInfo.GenderTuid!.Value;
            volunteer.EthnicityTuid = volunteerInfo.EthnicityTuid!.Value;
            volunteer.RacialGroupTuid = volunteerInfo.RacialGroupTuid!.Value;
            volunteer.IdentifiesAsTuid = volunteerInfo.IdentifiesAsTuid!.Value;
            volunteer.IsFamilyOfMilitary = volunteerInfo.FamilyOfMilitary;
            volunteer.IsVeteran = volunteerInfo.Veteran;


            var address = new Address
            {
                AddressLine1 = volunteerInfo.Address1 ?? string.Empty,
                AddressLine2 = volunteerInfo.Address2 ?? string.Empty,
                State = volunteerInfo.State ?? string.Empty,
                City = volunteerInfo.City ?? string.Empty,
                Zipcode = volunteerInfo.ZipCode ?? string.Empty
            };


            try
            {
                    _dbContext.Addresses.Add(address);
                    _dbContext.SaveChanges();

                    volunteer.AddressTuid = address.Tuid;

                    _dbContext.Volunteers.Add(volunteer);
                    _dbContext.SaveChanges();

                    var oneTimeChecks = new OneTimeCheck
                    {
                        VolunteerTuid = volunteer.Tuid,

                        HasFilePhoto = volunteerInfo.FilePhoto,
                        HasServiceDescription = volunteerInfo.ServiceDescription,
                        HasTrainingSheet = volunteerInfo.OrientTraining,
                        ConfidenceSouDate = volunteerInfo.ConfidenceSOU,
                        ServiceStartDate = volunteerInfo.ServiceStartDate,
                        HasBackgroundCheck = volunteerInfo.BackgroundCheck,
                        HasIdCopy = volunteerInfo.IDCopy
                    };

                    _dbContext.OneTimeChecks.Add(oneTimeChecks);
                    _dbContext.SaveChanges();

                    var annualChecks = new AnnualCheck
                    {
                        VolunteerTuid = volunteer.Tuid,
                        Year = DateTime.Now.Year,
                        CarInsuranceDate = volunteerInfo.AnnualIncomeCarInsurance
                    };

                    _dbContext.AnnualChecks.Add(annualChecks);
                    _dbContext.SaveChanges();


                }
                catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1563._message, ErrorMessages._1563._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1564._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1564._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1565._message + e.Message, ErrorMessages._1565._code);
            }
        }

        public void DeleteVolunteer(int volunteerTuid)
        {
            try
            {
                var volunteer = _dbContext.Volunteers.FirstOrDefault(x => x.Tuid == volunteerTuid);
                if (volunteer != null)
                {
                    volunteer.IsDeleted = true;

                    _dbContext.SaveChanges();
                }
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1566._message, ErrorMessages._1566._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1567._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1567._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1568._message + e.Message, ErrorMessages._1568._code);
            }
        }

        #endregion

        #region Classrooms Tab

        /// <summary>
        /// Function Name: GetAllClassrooms 
        /// Created By: Christopher Washburn
        /// Last Modified: 4/11/2023
        /// Last Modified By: Christopher Washburn 
        /// Purpose: Gets all the classrooms in the database and returns them in a Model of all the
        /// Classroom data.
        /// </summary>
        /// <returns> A list of all classrooms in the database </returns>
        public List<ClassroomsModel> GetAllClassrooms()
        {
            try
            {
                List<ClassroomsModel> classrooms = _dbContext.Assignments.Include(x => x.Volunteer).Include(x => x.Classroom).Select(
                    x => new ClassroomsModel
                    {
                        AssignmentTuid = x.Tuid,
                        ClassroomTuid = x.ClassroomTuid,
                        Volunteer = new VolunteerNameIdModel { Tuid = x.Volunteer.Tuid, FirstName = x.Volunteer.FirstName, LastName = x.Volunteer.LastName },
                        School = new SchoolModel { Tuid = x.Classroom.School.Tuid, Name = x.Classroom.School.Name },
                        ClassroomNumber = x.Classroom.ClassroomNumber ?? "",
                        ClassroomSize = x.Classroom.ClassroomSize ?? 0,
                        GradeLevel = x.Classroom.GradeLevel ?? "",
                        TeacherName = x.Classroom.TeacherName ?? "",
                        JsonSchedule = x.Schedule,
                        IsDeleted = x.Classroom.IsDeleted ?? false
                    }).OrderBy(x => x.School.Name).ToList();

                //Deserialize stored JSON object and format days and times to display on data grid.
                foreach (var classroom in classrooms)
                {
                    classroom.Schedule = new VolunteerClassroomSchedule();

                    if (classroom.JsonSchedule != null && classroom.JsonSchedule != "")
                    {
                        classroom.Schedule = JsonConvert.DeserializeObject<VolunteerClassroomSchedule>(classroom.JsonSchedule);
                    }
                }

                return classrooms;
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1677._message, ErrorMessages._1677._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1678._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1678._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1679._message + e.Message, ErrorMessages._1679._code);
            }

            return new List<ClassroomsModel>();
        }

        /// <summary>
        /// Function Name: GetVolunteersClassroomsByVolunteer 
        /// Created By: Christopher Washburn
        /// Last Modified: 4/11/2023
        /// Last Modified By: Christopher Washburn 
        /// Purpose: Gets all the Classrooms in the database based on the passed volunteer Tuid.
        /// </summary>
        /// <param name="volunteerTuid"></param>
        /// <returns> IEnumerable of the Classrooms Model that holds all the classroom data </returns>
        public IEnumerable<ClassroomsModel> GetVolunteersClassroomsByVolunteer(int volunteerTuid)
        {
            try
            {
                List<ClassroomsModel> classrooms = _dbContext.Assignments.Include(x => x.Volunteer).Include(x => x.Classroom).Where(x => x.VolunteerTuid.Equals(volunteerTuid)).Select(
                    x => new ClassroomsModel
                    {
                        AssignmentTuid = x.Tuid,
                        ClassroomTuid = x.ClassroomTuid,
                        Volunteer = new VolunteerNameIdModel { Tuid = x.Volunteer.Tuid, FirstName = x.Volunteer.FirstName, LastName = x.Volunteer.LastName },
                        School = new SchoolModel { Tuid = x.Classroom.School.Tuid, Name = x.Classroom.School.Name },
                        ClassroomNumber = x.Classroom.ClassroomNumber ?? "",
                        ClassroomSize = x.Classroom.ClassroomSize ?? 0,
                        GradeLevel = x.Classroom.GradeLevel ?? "",
                        TeacherName = x.Classroom.TeacherName ?? "",
                        JsonSchedule = x.Schedule,
                        IsDeleted = x.Classroom.IsDeleted ?? false
                    }).OrderBy(x => x.School.Name).ToList();

                //Deserialize stored JSON object and format days and times to display on data grid.
                foreach (var classroom in classrooms)
                {
                    classroom.Schedule = new VolunteerClassroomSchedule();

                    if (classroom.JsonSchedule != null && classroom.JsonSchedule != "")
                    {
                        classroom.Schedule = JsonConvert.DeserializeObject<VolunteerClassroomSchedule>(classroom.JsonSchedule);
                    }
                }

                return classrooms;
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1680._message, ErrorMessages._1680._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1681._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1681._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1682._message + e.Message, ErrorMessages._1682._code);
            }

            return Enumerable.Empty<ClassroomsModel>();
        }


        /// <summary>
        /// Function Name: GetVolunteersClassroomsBySchool 
        /// Created By: Christopher Washburn
        /// Last Modified: 4/11/2023
        /// Last Modified By: Christopher Washburn 
        /// Purpose: Gets all the classrooms in the db based on the passed school Tuid
        /// </summary>
        /// <param name="schoolTuid"></param>
        /// <returns> IEnumerable of the Classrooms Model that holds all the classroom data </returns>
        public IEnumerable<ClassroomsModel> GetVolunteersClassroomsBySchool(int schoolTuid)
        {
            try
            {
                List<ClassroomsModel> classrooms = _dbContext.Assignments.Include(x => x.Volunteer).Include(x => x.Classroom).Where(x => x.Classroom.SchoolTuid == schoolTuid).Select(
                    x => new ClassroomsModel
                    {
                        AssignmentTuid = x.Tuid,
                        ClassroomTuid = x.ClassroomTuid,
                        Volunteer = new VolunteerNameIdModel { Tuid = x.Volunteer.Tuid, FirstName = x.Volunteer.FirstName, LastName = x.Volunteer.LastName },
                        School = new SchoolModel { Tuid = x.Classroom.School.Tuid, Name = x.Classroom.School.Name },
                        ClassroomNumber = x.Classroom.ClassroomNumber ?? "",
                        ClassroomSize = x.Classroom.ClassroomSize ?? 0,
                        GradeLevel = x.Classroom.GradeLevel ?? "",
                        TeacherName = x.Classroom.TeacherName ?? "",
                        JsonSchedule = x.Schedule,
                        IsDeleted = x.Classroom.IsDeleted ?? false
                    }).OrderBy(x => x.School.Name).ToList();

                //Deserialize stored JSON object and format days and times to display on data grid.
                foreach (var classroom in classrooms)
                {
                    classroom.Schedule = new VolunteerClassroomSchedule();

                    if (classroom.JsonSchedule != null && classroom.JsonSchedule != "")
                    {
                        classroom.Schedule = JsonConvert.DeserializeObject<VolunteerClassroomSchedule>(classroom.JsonSchedule);
                    }
                }

                return classrooms;
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1683._message, ErrorMessages._1683._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1684._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1684._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1685._message + e.Message, ErrorMessages._1685._code);
            }

            return Enumerable.Empty<ClassroomsModel>();
        }


        /// <summary>
        /// Function Name: GetVolunteersClassroomsBySchool 
        /// Created By: Christopher Washburn
        /// Last Modified: 4/11/2023
        /// Last Modified By: Christopher Washburn 
        /// Purpose: Gets all the classrooms in the db based on the passed school Tuid and volunteer tuid
        /// </summary>
        /// <param name="schoolTuid"></param>
        /// <returns> IEnumerable of the Classrooms Model that holds all the classroom data </returns>
        public IEnumerable<ClassroomsModel> GetVolunteersClassroomsBySchoolVolunteer(int schoolTuid, int volunteerTuid)
        {
            try
            {
                List<ClassroomsModel> classrooms = _dbContext.Assignments.Include(x => x.Volunteer).Include(x => x.Classroom).Where(x => x.VolunteerTuid.Equals(volunteerTuid) && x.Classroom.SchoolTuid == schoolTuid).Select(
                    x => new ClassroomsModel
                    {
                        AssignmentTuid = x.Tuid,
                        ClassroomTuid = x.ClassroomTuid,
                        Volunteer = new VolunteerNameIdModel { Tuid = x.Volunteer.Tuid, FirstName = x.Volunteer.FirstName, LastName = x.Volunteer.LastName },
                        School = new SchoolModel { Tuid = x.Classroom.School.Tuid, Name = x.Classroom.School.Name },
                        ClassroomNumber = x.Classroom.ClassroomNumber ?? "",
                        ClassroomSize = x.Classroom.ClassroomSize ?? 0,
                        GradeLevel = x.Classroom.GradeLevel ?? "",
                        TeacherName = x.Classroom.TeacherName ?? "",
                        JsonSchedule = x.Schedule,
                        IsDeleted = x.Classroom.IsDeleted ?? false
                    }).OrderBy(x => x.School.Name).ToList();

                //Deserialize stored JSON object and format days and times to display on data grid.
                foreach (var classroom in classrooms)
                {
                    classroom.Schedule = new VolunteerClassroomSchedule();

                    if (classroom.JsonSchedule != null && classroom.JsonSchedule != "")
                    {
                        classroom.Schedule = JsonConvert.DeserializeObject<VolunteerClassroomSchedule>(classroom.JsonSchedule);
                    }
                }
                return classrooms;
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1686._message, ErrorMessages._1686._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1687._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1687._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1688._message + e.Message, ErrorMessages._1688._code);
            }

            return Enumerable.Empty<ClassroomsModel>();
        }

        /// <summary>
        /// Function Name: GetVolunteersClassrooms 
        /// Created By: Christopher Washburn
        /// Last Modified: 4/11/2023
        /// Last Modified By: Christopher Washburn 
        /// Purpose: Gets the classrooms that are assigned to a volunteer
        /// </summary>
        /// <param name="volunteerTuid"></param>
        /// <returns> IEnumerable of the Classrooms Model that holds all the classroom data </returns>
        public IEnumerable<ClassroomsModel> GetVolunteersClassrooms(int volunteerTuid)
        {
            try
            {
                return _dbContext.Assignments.Include(x => x.Volunteer).Include(x => x.Classroom).Where(
                    x => x.VolunteerTuid.Equals(volunteerTuid) && x.Classroom.ClassroomNumber != null && x.Classroom.ClassroomNumber != "").Select(
                    x => new ClassroomsModel
                    {
                        AssignmentTuid = x.Tuid,
                        ClassroomTuid = x.ClassroomTuid,
                        Volunteer = new VolunteerNameIdModel { Tuid = x.Volunteer.Tuid, FirstName = x.Volunteer.FirstName, LastName = x.Volunteer.LastName },
                        School = new SchoolModel { Tuid = x.Classroom.School.Tuid, Name = x.Classroom.School.Name },
                        ClassroomNumber = x.Classroom.ClassroomNumber ?? "",
                        ClassroomSize = x.Classroom.ClassroomSize ?? 0,
                        GradeLevel = x.Classroom.GradeLevel ?? "",
                        TeacherName = x.Classroom.TeacherName ?? "",
                        JsonSchedule = x.Schedule,
                        IsDeleted = x.Classroom.IsDeleted ?? false
                    }).OrderBy(x => x.School.Name).ToList();
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1689._message, ErrorMessages._1689._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1690._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1690._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1691._message + e.Message, ErrorMessages._1691._code);
            }

            return Enumerable.Empty<ClassroomsModel>();
        }

        /// <summary>
        /// Function Name: InsertNewClassroom
        /// Created By: Christopher Washburn
        /// Last Modified: 4/4/2023
        /// Last Modified By: Christopher Washburn 
        /// 
        /// Purpose: Inserts a new classroom row into the database.
        /// </summary>
        /// <param name="newClassroom"></param>
        public int InsertNewClassroom(ClassroomsModel newClassroom)
        {
            //Get volunteers school
            var school = _dbContext.Assignments.Include(x => x.Volunteer).Include(x => x.Classroom).Where(x => x.VolunteerTuid.Equals(newClassroom.Volunteer.Tuid)).Select(x => x.Classroom.SchoolTuid).FirstOrDefault();
            if(school != 0)
            {
                Classroom classroom = new Classroom();
                classroom.SchoolTuid = school;
                classroom.ClassroomNumber = newClassroom.ClassroomNumber ?? "";
                classroom.ClassroomSize = newClassroom.ClassroomSize ?? 0;
                classroom.GradeLevel = newClassroom.GradeLevel ?? "";
                classroom.TeacherName = newClassroom.TeacherName ?? "";
                classroom.IsDeleted = false;

                _dbContext.Classrooms.Add(classroom);
                _dbContext.SaveChanges();


                Assignment newAssignment = new Assignment();
                newAssignment.VolunteerTuid = (int)newClassroom.Volunteer.Tuid;
                newAssignment.ClassroomTuid = classroom.Tuid;
                newAssignment.Schedule = newClassroom.JsonSchedule;
                newAssignment.IsDeleted = false;
                _dbContext.Assignments.Add(newAssignment);
                _dbContext.SaveChanges();

                return 0;
            }
            return 1;

            
        }

        /// <summary>
        /// Function Name: UpdateClassroom 
        /// Created By: Christopher Washburn
        /// Last Modified: 4/11/2023
        /// Last Modified By: Christopher Washburn 
        /// Purpose: Updates an existing classroom in the database with the new passed info
        /// </summary>
        /// <param name="updateClassroom"></param>
        /// <returns> an int that is used as codes to tell the front end if the update happend or if there were errors </returns>
        public int UpdateClassroom(ClassroomsModel updateClassroom)
        {
            var school = _dbContext.Assignments.Include(x => x.Volunteer).Include(x => x.Classroom).Where(x => x.VolunteerTuid.Equals(updateClassroom.Volunteer.Tuid)).Select(x => x.Classroom.SchoolTuid).FirstOrDefault();
            if (school != null)
            {
                //make sure assignment exists
                var assignment = _dbContext.Assignments.Where(x => x.Tuid.Equals(updateClassroom.AssignmentTuid)).FirstOrDefault();
                if (assignment != null)
                {
                    var classroom = _dbContext.Classrooms.Where(x => x.Tuid.Equals(updateClassroom.ClassroomTuid)).FirstOrDefault();
                    if (classroom != null)
                    {
                        //update existing classroom record
                        classroom.SchoolTuid = school;
                        classroom.ClassroomNumber = updateClassroom.ClassroomNumber ?? "";
                        classroom.ClassroomSize = updateClassroom.ClassroomSize ?? 0;
                        classroom.GradeLevel = updateClassroom.GradeLevel ?? "";
                        classroom.TeacherName = updateClassroom.TeacherName ?? "";

                        _dbContext.SaveChanges();
                    }

                    //update assignment in case volunteer or schedule was changed.
                    assignment.VolunteerTuid = (int)updateClassroom.Volunteer.Tuid;
                    assignment.Schedule = updateClassroom.JsonSchedule;
                    _dbContext.SaveChanges();

                    return 0;
                }
                else
                {
                    return 1;
                }

            }
            else
            {
                return 2;
            }                        
        }

        /// <summary>
        /// Function Name: DeleteClassroom 
        /// Created By: Christopher Washburn
        /// Last Modified: 4/11/2023
        /// Last Modified By: Christopher Washburn 
        /// Purpose: Deletes a classroom from the db
        /// </summary>
        /// <param name="deleteClassroom"></param>
        /// <returns></returns>
        public int DeleteClassroom(ClassroomsModel deleteClassroom)
        {
            //first make sure there are no active child assignments
            //if not then delete assignment
            //then delete classroom

            var childAssignment = _dbContext.AssignmentStudents.Include(x => x.Student).Include(x => x.Assignment).Where(x => x.AssignmentTuid.Equals(deleteClassroom.AssignmentTuid)).FirstOrDefault();
            if (childAssignment == null)
            {
                var classroom = _dbContext.Classrooms.Where(x => x.Tuid.Equals(deleteClassroom.ClassroomTuid)).FirstOrDefault();
                if (classroom != null)
                {
                    _dbContext.Classrooms.Remove(classroom);
                    _dbContext.SaveChanges();
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            else
            {
                return 2;
            }
        }
        #endregion

        #region Child Assignments Tab

        /// <summary>
        /// Function Name: GetVolunteerChildAssignments
        /// Created By: Christopher Washburn
        /// Last Modified: 4/1/2023
        /// Last Modified By: Christopher Washburn 
        /// 
        /// Purpose: The purpose of this function is to Get all valunteer child assignments and return them
        ///  to VolunteerChildAssignments.xaml.cs to add to the front end
        /// </summary>
        /// <param name="volunteerId"></param>
        /// <returns>
        ///     VolunteerChildAssignmentsModel - a model that holds all the Child assignment info for the side bar
        /// </returns>
        public VolunteerChildAssignmentsModel GetAllVolunteerChildAssignments(int volunteerId)
        {
            List<int> assignmentTuids = GetVolunteerAssignments(volunteerId);
            int Ages0To5 = GetAllAssignedStudentsAge0To5(assignmentTuids);
            int Ages6To12 = GetAllAssignedStudentsAge5To12(assignmentTuids);
            SchoolModel assignedSchool = new SchoolModel();
            VolunteerChildAssignmentsModel childAssignments = new VolunteerChildAssignmentsModel();
            SchoolModel volunteerSchool = GetVolunteerSchool(volunteerId);

            //assign volunteers school
            childAssignments.School = volunteerSchool;
            childAssignments.SchoolTuid = volunteerSchool == null ? 0 : volunteerSchool.Tuid;

            //assign age categories
            childAssignments.Age0To5 = Ages0To5;
            childAssignments.Age6To12 = Ages6To12;

            //Nathan V, set the number of students that are assigned
            childAssignments.KidsAssigned = Ages0To5 + Ages6To12;


            return childAssignments;
        }

        /// <summary>
        /// Function Name: GetVolunteerChildAssignments
        /// Created By: Christopher Washburn
        /// Last Modified: 4/1/2023
        /// Last Modified By: Christopher Washburn 
        /// 
        /// Purpose: The purpose of this function is to Get all valunteer child assignments and return them
        ///  to VolunteerChildAssignments.xaml.cs to add to the front end
        /// </summary>
        /// <param name="volunteerId"></param>
        /// <returns>
        ///     VolunteerChildAssignmentsModel - a model that holds all the Child assignment info for the side bar
        /// </returns>
        public VolunteerChildAssignmentsModel GetVolunteerChildAssignments(int volunteerId, ClassroomsModel classroom)
        {
            try
            {
                List<int> assignmentTuids = GetVolunteerAssignmentsInClassroom(volunteerId, (int)classroom.ClassroomTuid);
                int Ages0To5 = GetAssignedStudentsInClassroomAge0To5(assignmentTuids, (int)classroom.ClassroomTuid);
                int Ages6To12 = GetAssignedStudentsInClassroomAge5To12(assignmentTuids, (int)classroom.ClassroomTuid);
                SchoolModel assignedSchool = new SchoolModel();
                VolunteerChildAssignmentsModel childAssignments = new VolunteerChildAssignmentsModel();

                //Assign School
                SchoolModel volunteerSchool = GetVolunteerSchool(volunteerId);
                childAssignments.School = volunteerSchool;
                childAssignments.SchoolTuid = volunteerSchool.Tuid;

                //Get classroom info
                var classroomInfo = _dbContext.Classrooms.Where(x => x.Tuid.Equals(classroom.ClassroomTuid)).FirstOrDefault();
                if (classroomInfo != null)
                {
                    childAssignments.ClassroomSize = classroom.ClassroomSize;
                    childAssignments.GradeLevel = classroom.GradeLevel;
                }

                //assign age categories
                childAssignments.Age0To5 = Ages0To5;
                childAssignments.Age6To12 = Ages6To12;

                //Nathan V, set the number of students that are assigned
                childAssignments.KidsAssigned = Ages0To5 + Ages6To12;

                return childAssignments;
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1692._message, ErrorMessages._1692._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1693._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1693._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1694._message + e.Message, ErrorMessages._1694._code);
            }

            return new VolunteerChildAssignmentsModel();

        }

        /// <summary>
        /// Function Name: GetVolunteerChildAssignmentDataGrid
        /// Created By: Christopher Washburn
        /// Last Modified: 4/1/2023
        /// Last Modified By: Christopher Washburn 
        /// 
        /// Purpose: Get the Identifier, Condition, student needs, and Desired outcome for the Child Assignments datagrid
        /// and return it to VolunteerChildAssignments.xaml.cs to add to the front end.
        /// </summary>
        /// <param name="volunteerId"></param>
        /// <returns>
        ///     IEnumerable<VolunteerChildAssignmentDataGridModel> - IEnumeral list of a model that holds all the data needed for
        ///     the data grid
        /// </returns>
        public IEnumerable<VolunteerChildAssignmentDataGridModel> GetVolunteerChildAssignmentDataGrid(int volunteerId)
        {
            List<VolunteerChildAssignmentDataGridModel> childAssignments = new List<VolunteerChildAssignmentDataGridModel>();

            try
            {
                //get the assignment id to find all students assigned to that assignment
                List<int> listAssignmentIds = GetVolunteerAssignments(volunteerId);
                var studentIds = _dbContext.AssignmentStudents.Where(x => listAssignmentIds.Contains(x.AssignmentTuid)).ToList(); //.Select(x => x.StudentTuid)

                foreach (var student in studentIds)
                {
                    VolunteerChildAssignmentDataGridModel childAssignment = new VolunteerChildAssignmentDataGridModel();
                    string studentConditionAcronyms = "";
                    string studentConditionDescriptions = "";
                    string studentNeedAcronyms = "";
                    string studentNeedDescriptions = "";
                    string studentIdentifier = "";
                    string studentDesiredOutcome = "";

                    //get student classroom
                    var studentClassroom = _dbContext.AssignmentStudents.Include(x => x.Assignment).Where(x => x.AssignmentTuid.Equals(student.AssignmentTuid) && x.StudentTuid.Equals(student)).Select(x => new ClassroomsModel { ClassroomTuid = x.Assignment.ClassroomTuid, ClassroomNumber = x.Assignment.Classroom.ClassroomNumber }).FirstOrDefault();

                    //get a students identifier if it exists
                    studentIdentifier = _dbContext.Students.Where(x => x.Tuid.Equals(student.StudentTuid)).Select(x => x.Identifier).FirstOrDefault() ?? "";

                    //get desired outcome
                    studentDesiredOutcome = _dbContext.AssignmentStudents.Where(x => x.StudentTuid.Equals(student.StudentTuid)).Select(x => x.DesiredOutcome).FirstOrDefault() ?? "";

                    //query db to get conditon acronyms and descriptions
                    var conditionAcronymnsStudentTuid = _dbContext.StudentConditions.Include(x => x.ConditionItem).ToList()
                    .GroupBy(x => x.StudentTuid, x => x.ConditionItem == null ? "N/A" : x.ConditionItem.Acronym, (studentTuid, conditions) => new { StudentTuid = studentTuid, Conditions = conditions })
                    .ToDictionary(x => x.StudentTuid, x => x.Conditions);

                    var conditionDescriptionsStudentTuid = _dbContext.StudentConditions.Include(x => x.ConditionItem).ToList()
                    .GroupBy(x => x.StudentTuid, x => x.ConditionItem == null ? "N/A" : x.ConditionItem.Description, (studentTuid, conditions) => new { StudentTuid = studentTuid, Conditions = conditions })
                    .ToDictionary(x => x.StudentTuid, x => x.Conditions);

                    //query db to get needs acronyms and descriptions
                    var needAcronymsByStudentTuid = _dbContext.StudentNeeds.Include(x => x.StudentNeedItem).ToList()
                    .GroupBy(x => x.StudentTuid, x => x.StudentNeedItem == null ? "N/A" : x.StudentNeedItem.Acronym, (studentTuid, studentNeedItems) => new { StudentTuid = studentTuid, StudentNeedItems = studentNeedItems })
                    .ToDictionary(x => x.StudentTuid, x => x.StudentNeedItems);

                    var needDescriptionsByStudentTuid = _dbContext.StudentNeeds.Include(x => x.StudentNeedItem).ToList()
                    .GroupBy(x => x.StudentTuid, x => x.StudentNeedItem == null ? "N/A" : x.StudentNeedItem.Description, (studentTuid, studentNeedItems) => new { StudentTuid = studentTuid, StudentNeedItems = studentNeedItems })
                    .ToDictionary(x => x.StudentTuid, x => x.StudentNeedItems);

                    //RJ -- get needs and conditions as a dictionary for each student 
                    if (needAcronymsByStudentTuid.ContainsKey(student.StudentTuid))
                    {
                        studentNeedAcronyms = string.Join(", ", needAcronymsByStudentTuid[student.StudentTuid]);
                        studentNeedDescriptions = string.Join(", ", needDescriptionsByStudentTuid[student.StudentTuid]);
                    }
                    else
                    {
                        studentNeedAcronyms = string.Empty;
                        studentNeedDescriptions = string.Empty;
                    }

                    if (conditionAcronymnsStudentTuid.ContainsKey(student.StudentTuid))
                    {
                        studentConditionAcronyms = string.Join(", ", conditionAcronymnsStudentTuid[student.StudentTuid]);
                        studentConditionDescriptions = string.Join(", ", conditionDescriptionsStudentTuid[student.StudentTuid]);
                    }
                    else
                    {
                        studentConditionAcronyms = string.Empty;
                        studentConditionDescriptions = string.Empty;
                    }

                    //add the rest of the variables to the model
                    childAssignment.StudentTuid = student.StudentTuid;
                    childAssignment.Condition = studentConditionAcronyms;
                    childAssignment.ConditionDescription = studentConditionDescriptions;
                    childAssignment.StudentNeeds = studentNeedAcronyms;
                    childAssignment.StudentNeedsDescription = studentNeedDescriptions;
                    childAssignment.Identifier = studentIdentifier;
                    childAssignment.DesiredOutcome = studentDesiredOutcome;
                    childAssignment.Classroom = studentClassroom;

                    childAssignments.Add(childAssignment);
                }

                IEnumerable<VolunteerChildAssignmentDataGridModel> childAssignmentDataGrid = childAssignments;


                return childAssignmentDataGrid;
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1695._message, ErrorMessages._1695._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1696._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1696._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1697._message + e.Message, ErrorMessages._1697._code);
            }
            return Enumerable.Empty<VolunteerChildAssignmentDataGridModel>();
        }

        /// <summary>
        /// Function Name: GetVolunteerChildAssignmentDataGrid
        /// Created By: Christopher Washburn
        /// Last Modified: 4/11/2023
        /// Last Modified By: Christopher Washburn 
        /// 
        /// Purpose: Get the Identifier, Condition, student needs, and Desired outcome for the Child Assignments datagrid
        /// for a specific classroom and return it to VolunteerChildAssignments.xaml.cs to add to the front end.
        /// </summary>
        /// <param name="volunteerId"></param>
        /// <returns>
        ///     IEnumerable<VolunteerChildAssignmentDataGridModel> - IEnumerable list of a model that holds all the data needed for
        ///     the data grid
        /// </returns>
        public IEnumerable<VolunteerChildAssignmentDataGridModel> GetVolunteerChildAssignmentInRoom(int volunteerId, int schoolTuid, ClassroomsModel classroom)
        {
            List<int> listAssignmentIds = GetVolunteerAssignments(volunteerId);
            var studentIds = _dbContext.AssignmentStudents.Where(x => listAssignmentIds.Contains(x.AssignmentTuid) && x.Assignment.ClassroomTuid.Equals(classroom.ClassroomTuid)).ToList();

            List<VolunteerChildAssignmentDataGridModel> childAssignments = new List<VolunteerChildAssignmentDataGridModel>();

            //get the assignment id to find all students assigned to that assignment
            //List<int> lstAssignmentId = _dbContext.Assignments.Where(x => x.VolunteerTuid == volunteerId && x.Classroom.ClassroomNumber.Equals(roomNumber)).Select(x => x.Tuid).ToList();
            
            //var studentIds = _dbContext.AssignmentStudents.Include(x => x.Assignment).Where(x => lstAssignmentId.Contains(x.AssignmentTuid)).
               // Where(x => x.Assignment.Classroom.SchoolTuid.Equals(schoolTuid)).
                //Where(x => x.Assignment.Classroom.ClassroomNumber.Equals(roomNumber)).Select(x => x.StudentTuid).ToList();

            foreach (var student in studentIds)
            {
                VolunteerChildAssignmentDataGridModel childAssignment = new VolunteerChildAssignmentDataGridModel();
                string studentConditionAcronyms = "";
                string studentConditionDescriptions = "";
                string studentNeedAcronyms = "";
                string studentNeedDescriptions = "";
                string studentIdentifier = "";
                string studentDesiredOutcome = "";

                //get student classroom
                var studentClassroom = _dbContext.AssignmentStudents.Include(x => x.Assignment).Where(x => x.AssignmentTuid.Equals(student.AssignmentTuid) && x.StudentTuid.Equals(student.StudentTuid)).Select(x => new ClassroomsModel { ClassroomTuid = x.Assignment.ClassroomTuid, ClassroomNumber = x.Assignment.Classroom.ClassroomNumber }).FirstOrDefault();

                //get a students identifier if it exists
                studentIdentifier = _dbContext.Students.Where(x => x.Tuid.Equals(student.StudentTuid)).Select(x => x.Identifier).FirstOrDefault() ?? "";

                //get desired outcome
                studentDesiredOutcome = _dbContext.AssignmentStudents.Where(x => x.StudentTuid.Equals(student.StudentTuid)).Select(x => x.DesiredOutcome).FirstOrDefault() ?? "";

                //query db to get conditon acronyms and descriptions
                var conditionAcronymnsStudentTuid = _dbContext.StudentConditions.Include(x => x.ConditionItem).ToList()
                .GroupBy(x => x.StudentTuid, x => x.ConditionItem == null ? "N/A" : x.ConditionItem.Acronym, (studentTuid, conditions) => new { StudentTuid = studentTuid, Conditions = conditions })
                .ToDictionary(x => x.StudentTuid, x => x.Conditions);

                var conditionDescriptionsStudentTuid = _dbContext.StudentConditions.Include(x => x.ConditionItem).ToList()
                .GroupBy(x => x.StudentTuid, x => x.ConditionItem == null ? "N/A" : x.ConditionItem.Description, (studentTuid, conditions) => new { StudentTuid = studentTuid, Conditions = conditions })
                .ToDictionary(x => x.StudentTuid, x => x.Conditions);

                //query db to get needs acronyms and descriptions
                var needAcronymsByStudentTuid = _dbContext.StudentNeeds.Include(x => x.StudentNeedItem).ToList()
                .GroupBy(x => x.StudentTuid, x => x.StudentNeedItem == null ? "N/A" : x.StudentNeedItem.Acronym, (studentTuid, studentNeedItems) => new { StudentTuid = studentTuid, StudentNeedItems = studentNeedItems })
                .ToDictionary(x => x.StudentTuid, x => x.StudentNeedItems);

                var needDescriptionsByStudentTuid = _dbContext.StudentNeeds.Include(x => x.StudentNeedItem).ToList()
                .GroupBy(x => x.StudentTuid, x => x.StudentNeedItem == null ? "N/A" : x.StudentNeedItem.Description, (studentTuid, studentNeedItems) => new { StudentTuid = studentTuid, StudentNeedItems = studentNeedItems })
                .ToDictionary(x => x.StudentTuid, x => x.StudentNeedItems);

                //RJ -- get needs and conditions as a dictionary for each student 
                if (needAcronymsByStudentTuid.ContainsKey(student.StudentTuid))
                {
                    studentNeedAcronyms = string.Join(", ", needAcronymsByStudentTuid[student.StudentTuid]);
                    studentNeedDescriptions = string.Join(", ", needDescriptionsByStudentTuid[student.StudentTuid]);
                }
                else
                {
                    studentNeedAcronyms = string.Empty;
                    studentNeedDescriptions = string.Empty;
                }

                if (conditionAcronymnsStudentTuid.ContainsKey(student.StudentTuid))
                {
                    studentConditionAcronyms = string.Join(", ", conditionAcronymnsStudentTuid[student.StudentTuid]);
                    studentConditionDescriptions = string.Join(", ", conditionDescriptionsStudentTuid[student.StudentTuid]);
                }
                else
                {
                    studentConditionAcronyms = string.Empty;
                    studentConditionDescriptions = string.Empty;
                }

                //add the rest of the variables to the model
                childAssignment.StudentTuid = student.StudentTuid;
                childAssignment.Condition = studentConditionAcronyms;
                childAssignment.ConditionDescription = studentConditionDescriptions;
                childAssignment.StudentNeeds = studentNeedAcronyms;
                childAssignment.StudentNeedsDescription = studentNeedDescriptions;
                childAssignment.Identifier = studentIdentifier;
                childAssignment.DesiredOutcome = studentDesiredOutcome;
                childAssignment.Classroom = studentClassroom;

                childAssignments.Add(childAssignment);
            }

            IEnumerable<VolunteerChildAssignmentDataGridModel> childAssignmentDataGrid = childAssignments;

            return childAssignmentDataGrid;
        }

        /// <summary>
        /// Function Name: GetVolunteerSchool 
        /// Created By: Christopher Washburn
        /// Last Modified: 4/11/2023
        /// Last Modified By: Christopher Washburn 
        /// Purpose: Gets the School data for the passed volunteer
        /// </summary>
        /// <param name="volunteerTuid"></param>
        /// <returns> A school model that holds the school name and db Tuid </returns>
        public SchoolModel? GetVolunteerSchool(int volunteerTuid)
        {
            try
            {
                return _dbContext.Assignments.Include(x => x.Volunteer).Include(x => x.Classroom).Where(x => x.VolunteerTuid.Equals(volunteerTuid)).Select(
                x => new SchoolModel
                {
                    Tuid = x.Classroom == null ? 0 : x.Classroom.SchoolTuid,
                    Name = x.Classroom == null ? "" : x.Classroom == null ? "N/A" : x.Classroom.School == null ? "N/A" : x.Classroom.School.Name
                }).FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
            
        }


        /// <summary>
        /// Function Name: GetVolunteerAssignment
        /// Created By: Christopher Washburn
        /// Last Modified: 4/1/2023
        /// Last Modified By: Christopher Washburn 
        /// 
        /// Purpose: Get a volunteers assignment baised off of that volunteers Tuid 
        /// </summary>
        /// <param name="volunteerTuid"></param>
        /// <returns>
        ///     int - Tuid of assignemnt assigned to volunteer
        /// </returns>
        public List<int> GetVolunteerAssignments(int volunteerTuid)
        {
            return _dbContext.Assignments.Where(x => x.VolunteerTuid.Equals(volunteerTuid)).Select(x => x.Tuid).ToList();
        }

        /// <summary>
        /// Function Name: GetVolunteerAssignmentsInClassroom 
        /// Created By: Christopher Washburn
        /// Last Modified: 4/11/2023
        /// Last Modified By: Christopher Washburn 
        /// Purpose: Gets the Assignment Tuids for the passed Volunteer in the passed Classroom.
        /// </summary>
        /// <param name="volunteerTuid"></param>
        /// <param name="classroomTuid"></param>
        /// <returns> a list of Assignment Tuids </returns>
        public List<int> GetVolunteerAssignmentsInClassroom(int volunteerTuid, int classroomTuid)
        {
            return _dbContext.AssignmentStudents.Include(x => x.Assignment).Include(x => x.Student)
                .Where(x => x.Assignment == null ? false : x.Assignment.VolunteerTuid.Equals(volunteerTuid) && x.Assignment == null ? false : x.Assignment.ClassroomTuid.Equals(classroomTuid)).Select(x => x.AssignmentTuid).ToList();
        }

        /// <summary>
        /// Function Name: GetAllAssignedStudentAge0To5
        /// Created By: Christopher Washburn
        /// Last Modified: 4/1/2023
        /// Last Modified By: Christopher Washburn 
        /// 
        /// Purpose: Get the count students in the age category 0 to 5 baised on the assignment tuid
        /// </summary>
        /// <param name="assignmentTuids"></param>
        /// <returns>
        ///     int - count of students ages 0 to 5 assigned to volunteer 
        /// </returns>
        public int GetAllAssignedStudentsAge0To5(List<int> assignmentTuids)
        {
            return _dbContext.AssignmentStudents.Include(x => x.Assignment).Include(x => x.Student)
                .Where(x => assignmentTuids.Contains(x.AssignmentTuid) && (x.Student == null ? false : x.Student.IsAgeBirthTo5.Equals(true))).Count();
        }

        /// <summary>
        /// Function Name: GetAllAssignedStudentAge0To5
        /// Created By: Christopher Washburn
        /// Last Modified: 4/1/2023
        /// Last Modified By: Christopher Washburn 
        /// 
        /// Purpose: Get the count students in the age category 0 to 5 baised on the assignment tuid
        /// </summary>
        /// <param name="assignmentTuids"></param>
        /// <returns>
        ///     int - count of students ages 0 to 5 assigned to volunteer 
        /// </returns>
        public int GetAssignedStudentsInClassroomAge0To5(List<int> assignmentTuids, int classroomTuid)
        {
            return _dbContext.AssignmentStudents.Include(x => x.Assignment).Include(x => x.Student)
                .Where(x => assignmentTuids.Contains(x.AssignmentTuid) && (x.Student == null ? false : x.Student.IsAgeBirthTo5.Equals(true)) && (x.Assignment.ClassroomTuid.Equals(classroomTuid))).Count();
        }

        /// <summary>
        /// Function Name: GetAssignedStudentAge5To12 
        /// Created By: Christopher Washburn
        /// Last Modified: 4/1/2023
        /// Last Modified By: Christopher Washburn 
        /// 
        /// Purpose: Get the count students in the age category 6 to 12 baised on the assignment tuid
        /// </summary>
        /// <param name="assignmentTuids"></param>
        /// <returns>
        ///     int - count of students ages 6 to 12 assigned to volunteer
        /// </returns>
        public int GetAllAssignedStudentsAge5To12(List<int> assignmentTuids)
        {
            return _dbContext.AssignmentStudents.Include(x => x.Assignment).Include(x => x.Student)
                .Where(x => assignmentTuids.Contains(x.AssignmentTuid) && (x.Student == null ? false : x.Student.IsAge5To12.Equals(true))).Count();
        }


        /// <summary>
        /// Function Name: GetAssignedStudentAge5To12 
        /// Created By: Christopher Washburn
        /// Last Modified: 4/1/2023
        /// Last Modified By: Christopher Washburn 
        /// 
        /// Purpose: Get the count students in the age category 6 to 12 baised on the assignment tuid
        /// </summary>
        /// <param name="assignmentTuids"></param>
        /// <returns>
        ///     int - count of students ages 6 to 12 assigned to volunteer
        /// </returns>
        public int GetAssignedStudentsInClassroomAge5To12(List<int> assignmentTuids, int classroomTuid)
        {
            return _dbContext.AssignmentStudents.Include(x => x.Assignment).Include(x => x.Student)
                .Where(x => assignmentTuids.Contains(x.AssignmentTuid) && (x.Student == null ? false : x.Student.IsAge5To12.Equals(true)) && (x.Assignment.ClassroomTuid.Equals(classroomTuid))).Count();
        }

        /// <summary>
        /// Function Name: GetStudentConditions
        /// Created By: Christopher Washburn
        /// Last Modified: 4/1/2023
        /// Last Modified By: Christopher Washburn 
        /// 
        /// Purpose: Get the conditions assigned to a student baised on that students Tuid
        /// </summary>
        /// <param name="studentTuid"></param>
        /// <returns>
        ///     List<ConditionItemModel> - list of a model that has all a condtions information
        /// </returns>
        public List<ConditionItemModel> GetStudentConditions(int studentTuid)
        {
            try
            {
                return _dbContext.StudentConditions.Where(x => x.StudentTuid.Equals(studentTuid))
                    .Select(x => new ConditionItemModel
                    {
                        Tuid = x.ConditionItem == null ? 0 : x.ConditionItem.Tuid,
                        Acronym = x.ConditionItem == null ? "N/A" : x.ConditionItem.Acronym,
                        Description = x.ConditionItem == null ? "N/A" : x.ConditionItem.Description
                    }).OrderBy(x => x.Acronym).ToList();
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1584._message, ErrorMessages._1584._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1585._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1585._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1586._message + e.Message, ErrorMessages._1586._code);
            }

            return new List<ConditionItemModel>();
        }

        /// <summary>
        /// Function Name: GetStudentNeeds
        /// Created By: Christopher Washburn
        /// Last Modified: 4/1/2023
        /// Last Modified By: Christopher Washburn 
        /// 
        /// Purpose: Get the needs assigned to a student baised on that students Tuid
        /// </summary>
        /// <param name="studentTuid"></param>
        /// <returns>
        ///     List<StudentNeedItemModel> - list of a model that has all a needs information
        /// </returns>
        public List<StudentNeedItemModel> GetStudentNeeds(int studentTuid)
        {
            try
            {
                return _dbContext.StudentNeeds.Where(x => x.StudentTuid.Equals(studentTuid)).Select(x => new StudentNeedItemModel
                {
                    Tuid = x.StudentNeedItem == null ? 0 : x.StudentNeedItem.Tuid,
                    Acronym = x.StudentNeedItem == null ? "N/A" : x.StudentNeedItem.Acronym,
                    Description = x.StudentNeedItem == null ? "N/A" : x.StudentNeedItem.Description
                }).OrderBy(x => x.Acronym).ToList();
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1587._message, ErrorMessages._1587._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1588._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1588._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1589._message + e.Message, ErrorMessages._1589._code);
            }

            return new List<StudentNeedItemModel>();
        }

        /// <summary>
        /// Function Name: GetStudentIdentifier
        /// Created By: Christopher Washburn
        /// Last Modified: 4/1/2023
        /// Last Modified By: Christopher Washburn 
        /// 
        /// Purpose: Get the identifier assigned to a student baised on their Tuid if it exists
        /// </summary>
        /// <param name="studentTuid"></param>
        /// <returns>
        ///     string - a students identifier
        /// </returns>
        public string GetStudentIdentifier(int studentTuid) 
        {
            try
            {
                return _dbContext.Students.Where(x => x.Tuid.Equals(studentTuid)).Select(x => x.Identifier).FirstOrDefault() ?? "";
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1590._message, ErrorMessages._1590._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1591._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1591._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1592._message + e.Message, ErrorMessages._1592._code);
            }

            return string.Empty;
        }

        /// <summary>
        /// Function Name: AddOrEditClassroom 
        /// Created By: Christopher Washburn
        /// Last Modified: 4/11/2023
        /// Last Modified By: Christopher Washburn 
        /// Purpose: Checks to see if the passed identifier already exists in the database
        /// </summary>
        /// <param name="identifier"></param>
        /// <returns> If the identifier exists it returns true else false </returns>
        public bool CheckIfIdentifierExists(string identifier)
        {
            try
            {
                if (identifier != "")
                {
                    var identifierExists = _dbContext.Students.Where(x => x.Identifier.Equals(identifier))
                        .Select(x => x.Identifier).FirstOrDefault();
                    if (identifierExists != null)
                    {
                        return true;
                    }
                }
            }
            catch (SqlException e)
            {

            }
            catch (Exception e)
            {

            }
            return false;
        }

        /// <summary>
        /// Function Name: GetStudent 
        /// Created By: Christopher Washburn
        /// Last Modified: 4/1/2023
        /// Last Modified By: Christopher Washburn 
        /// 
        /// Purpose: Get a students information baised on their Tuid
        /// </summary>
        /// <param name="studentTuid"></param>
        /// <returns>
        ///     StudentModel - a model that holds all the students information
        /// </returns>
        public StudentModel? GetStudent(int studentTuid)
        {
            try
            {
                return _dbContext.Students.Where(x => x.Tuid.Equals(studentTuid)).Select(x => new StudentModel { Tuid = x.Tuid, Identifier = x.Identifier, IsAgeBirthTo5 = x.IsAgeBirthTo5, IsAge5To12 = x.IsAge5To12 }).FirstOrDefault();
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1593._message, ErrorMessages._1593._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1594._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1594._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1595._message + e.Message, ErrorMessages._1595._code);
            }

            return null;
        }

        /// <summary>
        /// Function Name: GetStudentDesiredOutCome 
        /// Created By: Christopher Washburn
        /// Last Modified: 4/1/2023
        /// Last Modified By: Christopher Washburn 
        /// 
        /// Purpose: Get the Desired Outcome of a student baised on the students Tuid
        /// </summary>
        /// <param name="studentTuid"></param>
        /// <returns>
        ///     string - a string that holds a students desired outcome
        /// </returns>
        public string GetStudentDesiredOutCome(int studentTuid)
        {
            try
            {
                return _dbContext.AssignmentStudents.Where(x => x.StudentTuid.Equals(studentTuid)).Select(x => x.DesiredOutcome).FirstOrDefault() ?? "";
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1596._message, ErrorMessages._1596._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1597._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1597._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1598._message + e.Message, ErrorMessages._1598._code);
            }

            return string.Empty;
        }

        /// <summary> GetAllConditions
        /// Function Name: 
        /// Created By: Christopher Washburn
        /// Last Modified: 4/1/2023
        /// Last Modified By: Christopher Washburn 
        /// 
        /// Purpose: Get all the conditons in the database
        /// </summary>
        /// <returns>
        ///     IEnumerable<ConditionItemModel> an IEnumerable of a model that holds all the information of a specific condition
        ///     such as its Tuid, Acronymn and Description 
        /// </returns>
        public IEnumerable<ConditionItemModel> GetAllConditions()
        {
            try
            {
                return _dbContext.ConditionItems.Select(x => new ConditionItemModel { Tuid = x.Tuid, Acronym = x.Acronym, Description = x.Description }).OrderBy(x => x.Acronym).ToList();
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1599._message, ErrorMessages._1599._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1600._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1600._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1601._message + e.Message, ErrorMessages._1601._code);
            }

            return Enumerable.Empty<ConditionItemModel>();
        }

        /// <summary> GetAllStudentNeeds
        /// Function Name: 
        /// Created By: Christopher Washburn
        /// Last Modified: 4/1/2023
        /// Last Modified By: Christopher Washburn 
        /// 
        /// Purpose: Get all needs in the database
        /// </summary>
        /// <returns>
        ///     IEnumerable<StudentNeedItemModel> an IEnumerable of a model that holds all the information of a specific need
        ///     such as its Tuid, Acronymn and Description 
        /// </returns>
        public IEnumerable<StudentNeedItemModel> GetAllStudentNeeds()
        {
            try
            {
                return _dbContext.StudentNeedItems.Select(x => new StudentNeedItemModel { Tuid = x.Tuid, Acronym = x.Acronym, Description = x.Description }).OrderBy(x => x.Acronym).ToList();
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1602._message, ErrorMessages._1602._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1603._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1603._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1604._message + e.Message, ErrorMessages._1604._code);
            }

            return Enumerable.Empty<StudentNeedItemModel>();

        }

        /// <summary>
        /// Function Name: UpdateVolunteerChildAssignmentsSchool 
        /// Created By: Christopher Washburn
        /// Last Modified: 4/1/2023
        /// Last Modified By: Christopher Washburn 
        /// 
        /// Purpose: Update the school assigned to a volunteer if there is one in the db. If no record is returned it assignes the school
        /// to that assignment and inserts the record.
        /// </summary>
        /// <param name="school"></param>
        /// <param name="volunteerId"></param>
        /// <modification>
        ///     Andrew Loesel - 4/3/2023 : added the '!' sepecification while setting the result's classroom's school tuid which asserts that assignment and classroom won't be null
        /// </modification>
        public void UpdateVolunteerChildAssignmentsSchool(SchoolModel school, int volunteerId)
		{
            try
            {
                var result = _dbContext.AssignmentStudents.Include(x => x.Assignment).Where(x => x.Assignment == null ? false : x.Assignment.VolunteerTuid.Equals(volunteerId)).FirstOrDefault();
                if (result != null)
                {
                    result.Assignment!.Classroom!.SchoolTuid = school.Tuid;

                    _dbContext.SaveChanges();
                }
                else
                {
                    AssignmentStudent newAssignment = new AssignmentStudent();
                    newAssignment.Assignment!.Classroom!.SchoolTuid = school.Tuid;

                    _dbContext.AssignmentStudents.Add(newAssignment);
                    _dbContext.SaveChanges();
                }
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1605._message, ErrorMessages._1605._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1606._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1606._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1607._message + e.Message, ErrorMessages._1607._code);
            }

        }

        /// <summary>
        /// Function Name: InsertNewChildAssignment 
        /// Created By: Christopher Washburn
        /// Last Modified: 4/1/2023
        /// Last Modified By: Christopher Washburn 
        /// 
        /// Purpose: 
        /// </summary>
        /// <param name="childAssignment"></param>
        /// <param name="volunteerId"></param>
        public int InsertNewChildAssignment(NewVolunteerChildAssignmentsModel childAssignment, int volunteerId)
        {
            //see if there is an empty assignment for this classroom, if not create assignment
            var assignmentTuid = _dbContext.Assignments.Include(x => x.Volunteer).Where(x => x.VolunteerTuid.Equals(volunteerId) && x.Classroom.ClassroomNumber.Equals(childAssignment.Classroom.ClassroomNumber)).FirstOrDefault();
            if (assignmentTuid != null)
            {
                int studentId = InsertNewStudent(childAssignment);

                AssignmentStudent newChildAssignment = new AssignmentStudent();
                newChildAssignment.AssignmentTuid = assignmentTuid.Tuid;
                newChildAssignment.StudentTuid = studentId;
                newChildAssignment.DesiredOutcome = childAssignment.DesiredOutcome ?? "";
                newChildAssignment.Date = DateTime.Now;

                _dbContext.AssignmentStudents.Add(newChildAssignment);
                _dbContext.SaveChanges();

                return 0;
            }
            else
            {
                return 1;
            }
        }

        /// <summary>
        /// Function Name: UpdateChildAssignment
        /// Created By: Christopher Washburn
        /// Last Modified: 4/1/2023
        /// Last Modified By: Christopher Washburn 
        /// 
        /// Purpose: Updates an existing child assignment in the database. It deletes and then creates new condtions/needs
        /// baised on what was selected and then updates the age range, desired outcome and identifier
        /// </summary>
        /// <param name="volunteerId"></param>
        /// <param name="studentTuid"></param>
        /// <param name="updateChildAssignment"></param>
        public void UpdateChildAssignment(int volunteerId, int studentTuid, NewVolunteerChildAssignmentsModel updateChildAssignment)
        {
            //Delete all previous conditons/needs assigned to student
            DeleteStudentConditions(studentTuid);
            DeleteStudentNeeds(studentTuid);

            //Add all new conditions/needs selected to student
            if (updateChildAssignment.StudentConditions != null)
            {
                InsertNewStudentConditons(studentTuid, updateChildAssignment.StudentConditions);
            }
            if (updateChildAssignment.StudentNeeds != null)
            {
                InsertNewStudentNeeds(studentTuid, updateChildAssignment.StudentNeeds);
            }

            //Update Identifier
            var identifier = _dbContext.Students.Where(x => x.Tuid.Equals(studentTuid)).FirstOrDefault();
            if (identifier != null)
            {
                identifier.Identifier = updateChildAssignment.Identifier;
            }

            //Update Age
            var student = _dbContext.Students.Where(x => x.Tuid.Equals(studentTuid)).FirstOrDefault();
            if (student != null)
            {
                student.Identifier = updateChildAssignment.Student == null ? "N/A" : updateChildAssignment.Student.Identifier;
                student.IsAgeBirthTo5 = updateChildAssignment.Student == null ? false : updateChildAssignment.Student.IsAgeBirthTo5;
                student.IsAge5To12 = updateChildAssignment.Student == null ? false : updateChildAssignment.Student.IsAge5To12;
            }

            //Update Desired outcome - AssignmentStudent            
            var assignment = _dbContext.AssignmentStudents.Where(x => x.StudentTuid.Equals(studentTuid)).FirstOrDefault();
            if (assignment != null)
            {
                assignment.DesiredOutcome = updateChildAssignment.DesiredOutcome ?? "";
            }

            _dbContext.SaveChanges();

        }

        /// <summary>
        /// Function Name: PushNewStudent
        /// Created By: Christopher Washburn
        /// Last Modified: 4/1/2023
        /// Last Modified By: Christopher Washburn 
        /// 
        /// Purpose: Creates a new student in the database   
        /// </summary>
        /// <param name="childAssignment"></param>
        /// <returns>
        ///     int - The Tuid of the newly created student
        /// </returns>
        public int InsertNewStudent(NewVolunteerChildAssignmentsModel childAssignment)
		{
            try
            {
                int newStudentTuid = 0;
                //Create new student
                Student newStudent = new Student();
                newStudentTuid = newStudent.Tuid;
                if (childAssignment.Student != null)
                {
                    newStudent.Identifier = childAssignment.Student.Identifier;
                    newStudent.IsAgeBirthTo5 = childAssignment.Student.IsAgeBirthTo5;
                    newStudent.IsAge5To12 = childAssignment.Student.IsAge5To12;
                }

                _dbContext.Students.Add(newStudent);
                _dbContext.SaveChanges();

                //assign newly created Student Tuid
                newStudentTuid = newStudent.Tuid;

                //split these into two functions to use for other parts 
                if (childAssignment.StudentConditions != null)
                {
                    InsertNewStudentConditons(newStudentTuid, childAssignment.StudentConditions);
                }
                if (childAssignment.StudentNeeds != null)
                {
                    InsertNewStudentNeeds(newStudentTuid, childAssignment.StudentNeeds);
                }

                return newStudentTuid;
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1614._message, ErrorMessages._1614._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1615._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1615._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1616._message + e.Message, ErrorMessages._1616._code);
            }

            return -1;
        }

        /// <summary>
        /// Function Name: InsertNewStudentConditons 
        /// Created By: Christopher Washburn
        /// Last Modified: 4/1/2023
        /// Last Modified By: Christopher Washburn 
        /// 
        /// Purpose: Inserts a new conditon into the database
        /// </summary>
        /// <param name="studentTuid"></param>
        /// <param name="childConditions"></param>
        public void InsertNewStudentConditons(int studentTuid, List<ConditionItemModel> childConditions)
        {
            try
            {
                foreach (var conditon in childConditions)
                {
                    StudentCondition newConditionAssignment = new StudentCondition();
                    newConditionAssignment.StudentTuid = studentTuid;
                    newConditionAssignment.ConditionItemTuid = conditon.Tuid;

                    _dbContext.StudentConditions.Add(newConditionAssignment);
                    _dbContext.SaveChanges();
                }
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1617._message, ErrorMessages._1617._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1618._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1618._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1619._message + e.Message, ErrorMessages._1619._code);
            }
        }

        /// <summary>
        /// Function Name: InsertNewStudentNeeds 
        /// Created By: Christopher Washburn
        /// Last Modified: 4/1/2023
        /// Last Modified By: Christopher Washburn 
        /// 
        /// Purpose:  
        /// </summary>
        /// <param name="studentTuid"></param>
        /// <param name="childNeeds"></param>
        public void InsertNewStudentNeeds(int studentTuid, List<StudentNeedItemModel> childNeeds)
        {
            try
            {
                foreach (var need in childNeeds)
                {
                    StudentNeed newStudentNeedAssignment = new StudentNeed();
                    newStudentNeedAssignment.StudentTuid = studentTuid;
                    newStudentNeedAssignment.StudentNeedItemTuid = need.Tuid;

                    _dbContext.StudentNeeds.Add(newStudentNeedAssignment);
                    _dbContext.SaveChanges();
                }
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1620._message, ErrorMessages._1620._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1621._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1621._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1622._message + e.Message, ErrorMessages._1622._code);
            }
        }

        /// <summary>
        /// Function Name: InsertNewStudentConditon
        /// Created By: Christopher Washburn
        /// Last Modified: 4/1/2023
        /// Last Modified By: Christopher Washburn 
        /// 
        /// Purpose: Insert a new condtion into the database 
        /// </summary>
        /// <param name="condition"></param>
        public void InsertNewStudentConditon(ConditionItemModel condition)
        {
            ConditionItem newConditionItem = new ConditionItem();
            newConditionItem.Acronym = condition.Acronym ?? "N/A";
            newConditionItem.Description = condition.Description ?? "N/A";

            try
            {
                _dbContext.ConditionItems.Add(newConditionItem);
                _dbContext.SaveChanges();
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1623._message, ErrorMessages._1623._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1624._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1624._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1625._message + e.Message, ErrorMessages._1625._code);
            }

        }

        /// <summary>
        /// Function Name: InsertNewStudentConditon
        /// Created By: Christopher Washburn
        /// Last Modified: 4/1/2023
        /// Last Modified By: Christopher Washburn 
        /// 
        /// Purpose: Insert a new need into the database 
        /// </summary>
        /// <param name="need"></param>
        public void InsertNewStudentNeed(StudentNeedItemModel need)
        {
            StudentNeedItem newNeedItem = new StudentNeedItem();
            newNeedItem.Acronym = need.Acronym ?? "N/A";
            newNeedItem.Description = need.Description ?? "N/A";

            try
            {
                _dbContext.StudentNeedItems.Add(newNeedItem);
                _dbContext.SaveChanges();
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1626._message, ErrorMessages._1626._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1627._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1627._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1628._message + e.Message, ErrorMessages._1628._code);
            }

        }

        /// <summary>
        /// Function Name: 
        /// Created By: Christopher Washburn
        /// Last Modified: 4/1/2023
        /// Last Modified By: Christopher Washburn 
        /// 
        /// Purpose: Gets all schools currently in the database 
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
                     Days = x.Days
                 }).ToList();
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1629._message, ErrorMessages._1629._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1630._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1630._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1631._message + e.Message, ErrorMessages._1631._code);
            }


            return Enumerable.Empty<SchoolModel>();

        }

        #endregion 

		public IEnumerable<int> GetVolunteerFinancialsYears()
		{
            try
            {
                return _dbContext.PTOStipends.Select(x => x.Date.Year).Distinct().OrderBy(x => x).ToList();
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1632._message, ErrorMessages._1632._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1633._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1633._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1634._message + e.Message, ErrorMessages._1634._code);
            }

            return Enumerable.Empty<int>();
        }

        public void DeleteChildAssignments(List<VolunteerChildAssignmentDataGridModel> childAssignments)
        {
            try
            {
                foreach (var student in childAssignments)
                {
                    //delete all associated conditions
                    DeleteStudentConditions(student.StudentTuid);

                    //detlet all associated needs
                    DeleteStudentNeeds(student.StudentTuid);

                    //delete all associated assignments
                    DeleteAssignmentStudent(student.StudentTuid);

                    //delete student record
                    DeleteStudent(student.StudentTuid);
                }
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1635._message, ErrorMessages._1635._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1636._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1636._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1637._message + e.Message, ErrorMessages._1637._code);
            }
        }

        public void DeleteStudentConditions(int studentTuid)
        {
            try
            {
                var studentConditions = _dbContext.StudentConditions.Where(x => x.StudentTuid.Equals(studentTuid)).ToList();

                if (studentConditions != null)
                {
                    foreach (var condition in studentConditions)
                    {
                        _dbContext.StudentConditions.Remove(condition);
                        _dbContext.SaveChanges();
                    }
                }
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1638._message, ErrorMessages._1638._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1639._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1639._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1640._message + e.Message, ErrorMessages._1640._code);
            }
        }

        public void DeleteStudentNeeds(int studentTuid)
        {
            try
            {
                var studentNeeds = _dbContext.StudentNeeds.Where(x => x.StudentTuid.Equals(studentTuid)).ToList();

                if (studentNeeds != null)
                {
                    foreach (var need in studentNeeds)
                    {
                        _dbContext.StudentNeeds.Remove(need);
                        _dbContext.SaveChanges();
                    }
                }
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1641._message, ErrorMessages._1641._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1642._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1642._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1643._message + e.Message, ErrorMessages._1643._code);
            }
        }

        public void DeleteAssignmentStudent(int studentTuid)
        {
            try
            {
                var studentAssignment = _dbContext.AssignmentStudents.Where(x => x.StudentTuid.Equals(studentTuid)).ToList();

                if (studentAssignment != null)
                {
                    foreach (var assignment in studentAssignment)
                    {
                        _dbContext.AssignmentStudents.Remove(assignment);
                        _dbContext.SaveChanges();
                    }
                }
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1644._message, ErrorMessages._1644._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1645._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1645._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1646._message + e.Message, ErrorMessages._1646._code);
            }
        }

        public void DeleteStudent(int studentTuid)
        {
            try
            {
                var students = _dbContext.Students.Where(x => x.Tuid.Equals(studentTuid)).ToList();

                if (students != null)
                {
                    foreach (var student in students)
                    {
                        _dbContext.Students.Remove(student);
                        _dbContext.SaveChanges();
                    }
                }
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1647._message, ErrorMessages._1647._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1648._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1648._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1649._message + e.Message, ErrorMessages._1649._code);
            }
        }


        #region Volunteer Financials Tab        
        /// <summary>
        /// Function Name: GetVolunteerFinancialsMealMileage
        /// Created By: Christopher Washburn
        /// Last Modified: 3/1/2023
        /// Last Modified By: Jon Maddocks
        /// 
        /// Purpose: The purpose of this function is to get the Volunteers meal and mileage information, put it in its model, and return it
        /// to the front end for data binding.
        /// </summary>
        /// <param name="volunteerTuid"></param>
        /// <param name="monthYear"></param>
        /// <returns>
        ///		clsMealAndTransportModel - the model that holds the meal and mileage information.
        /// </returns>
        public VolunteerFinancialsMealTransportModel? GetVolunteerFinancialsMealMileage(int volunteerTuid, DateTime monthYear)
		{
            try
            {
                //TODO: Calculate the TotalMealCost and TotalMileageCost
                VolunteerFinancialsMealTransportModel financialsMealTransportModel = new VolunteerFinancialsMealTransportModel();
                VolunteersFinancialsRatesModel? mealTransportRates = GetVolunteerFinancialsMealTransportRates(monthYear);
                var item = _dbContext.MealMileages.Where(x => x.VolunteerTuid.Equals(volunteerTuid) && x.Date.Month == monthYear.Month && x.Date.Year == monthYear.Year).Select(
                x => new VolunteerFinancialsMealTransportModel
                {

                    SiteMeals = x.MealCount,
                    Mileage = x.Mileage,
                    BusRides = x.BusRideCount
                }).FirstOrDefault();

                //if retruned values have data is it passed if not it is given 0
                var totalMeals = item?.SiteMeals ?? 0;
                var mealRate = mealTransportRates?.YearlyMealValue ?? 0;

                var totalMiles = item?.Mileage ?? 0;
                var mileageRate = mealTransportRates?.CurrentMileageRate ?? 0;

                var totalBusRides = item?.BusRides ?? 0;
                var busRate = mealTransportRates?.CurrentBusRideRate ?? 0;

                financialsMealTransportModel.SiteMeals = item?.SiteMeals ?? 0;
                financialsMealTransportModel.Mileage = item?.Mileage ?? 0;
                financialsMealTransportModel.BusRides = item?.BusRides ?? 0;
                financialsMealTransportModel.TotalMealValue = (decimal?)(totalMeals * mealRate);
                financialsMealTransportModel.TotalMileageValue = totalMiles * (decimal)mileageRate;
                financialsMealTransportModel.TotalBusRidesValue = (decimal?)(totalBusRides * busRate);

                return financialsMealTransportModel;
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1650._message, ErrorMessages._1650._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1651._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1651._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1652._message + e.Message, ErrorMessages._1652._code);
            }

            return null;
        }

		/// <summary>
		/// Function Name: GetVolunteersFinancialsPtoStipend
		/// Created By: Christopher Washburn
		/// Last Modified: 3/1/2023
		/// Last Modified By: Jon Maddocks
		/// 
		/// Purpose: The purpose of this function is to get the Volunteers PTO and Stipend information, put it in its model, and return it
		/// to the front end for data binding.
		/// </summary>
		/// <param name="volunteerTuid"></param>
		/// <param name="monthYear"></param>
		/// <returns>
		///  clsPTOModel - the model that holds the PTO and stipend information.
		/// </returns>
		public VolunteerFinancialsPtoStipendModel? GetVolunteersFinancialsPtoStipend(int volunteerTuid, DateTime monthYear)
		{
            try
            {
                //this code is just to get the info for the volunteer and is not baised on date or year. This NEEDS to be
                //changed to the commented out code one date data is put in db
                VolunteerFinancialsPtoStipendModel financialsPtoStipendModel = new VolunteerFinancialsPtoStipendModel();
                var item = _dbContext.PTOStipends.Where(x => x.VolunteerTuid.Equals(volunteerTuid) && x.Date.Month == monthYear.Month && x.Date.Year == monthYear.Year).Select(
                    x => new VolunteerFinancialsPtoStipendModel
                    {
                        PtoStart = x.PtoStart,
                        PtoEnded = x.PtoEnd,
                        PtoUsed = x.PtoUsed,
                        PtoEarned = x.PtoEarned,
                        StipendPaid = x.StipendPaid,
                        IsPTOEligible = x.IsPTOEligible
                    }).FirstOrDefault();

                //if retruned values have data is it passed if not it is given 0
                financialsPtoStipendModel.PtoStart = item?.PtoStart ?? 0;
                financialsPtoStipendModel.PtoEnded = item?.PtoEnded ?? 0;
                financialsPtoStipendModel.PtoUsed = item?.PtoUsed ?? 0;
                financialsPtoStipendModel.PtoEarned = item?.PtoEarned ?? 0;
                financialsPtoStipendModel.StipendPaid = item?.StipendPaid ?? 0;
                financialsPtoStipendModel.IsPTOEligible = item?.IsPTOEligible ?? true;

                return financialsPtoStipendModel;
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1653._message, ErrorMessages._1653._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1654._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1654._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1655._message + e.Message, ErrorMessages._1655._code);
            }

            return null;
        }

        /// <summary>
		/// Function Name: GetVolunteerFinancialsHours
		/// Created By: Christopher Washburn
		/// Last Modified: 3/1/2023
		/// Last Modified By: Jon Maddocks
		/// 
		/// Purpose: The purpose of this function is to get the Volunteers mean and transport rates information, put it in its model, and return it
		/// to the front end for data binding.
		/// </summary>
		/// <param name="monthYear"></param>
		/// <returns>
		///  VolunteersFinancialsRatesModel - the model that holds the meal and transport rates. 
		/// </returns>
		public VolunteerFinancialsHoursModel? GetVolunteerFinancialsHours(int volunteerTuid, DateTime monthYear)
		{
            try
            {
                VolunteerFinancialsHoursModel financialHoursModel = new VolunteerFinancialsHoursModel();
                var hours = _dbContext.PTOStipends.Where(x => x.VolunteerTuid.Equals(volunteerTuid) && x.Date.Month == monthYear.Month && x.Date.Year == monthYear.Year).Select(
                    x => new VolunteerFinancialsHoursModel
                    {
                        RegHours = x.RegularHours,
                        YtdHours = x.YearToDateHour
                    }).FirstOrDefault();

                //if retruned values have data is it passed if not it is given 0
                financialHoursModel.RegHours = hours?.RegHours ?? 0;
                financialHoursModel.YtdHours = hours?.YtdHours ?? 0;

                return financialHoursModel;
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1656._message, ErrorMessages._1656._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1657._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1657._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1658._message + e.Message, ErrorMessages._1658._code);
            }

            return null;
        }

		/// <summary>
		/// Function Name: GetVolunteerFinancialsMealTransportRates
		/// Created By: Christopher Washburn
		/// Last Modified: 3/1/2023
		/// Last Modified By: Jon Maddocks
		/// 
		/// Purpose: The purpose of this function is to get the Volunteers mean and transport rates information, put it in its model, and return it
		/// to the front end for data binding.
		/// </summary>
		/// <param name="monthYear"></param>
		/// <returns>
		///  VolunteersFinancialsRatesModel - the model that holds the meal and transport rates. 
		/// </returns>
		public VolunteersFinancialsRatesModel? GetVolunteerFinancialsMealTransportRates(DateTime monthYear)
		{
            try
            {
                VolunteersFinancialsRatesModel financialHoursModel = new VolunteersFinancialsRatesModel();
                var item = _dbContext.MealTransportRates.Where(x => x.Date.Month.Equals(monthYear.Month) && x.Date.Year == monthYear.Year).Select(x => new VolunteersFinancialsRatesModel
                {
                    CurrentMileageRate = x.MileageRate,
                    YearlyMealValue = x.MealRate,
                    CurrentBusRideRate = x.BusMileageRate
                }).FirstOrDefault();

                if (item == null)
                {
                    var prevFinancialHours = _dbContext.MealTransportRates.Where(x => x.Date <= monthYear.Date).OrderBy(x => x.Date).LastOrDefault();
                    financialHoursModel.CurrentMileageRate = prevFinancialHours?.MileageRate ?? 0;
                    financialHoursModel.YearlyMealValue = prevFinancialHours?.MealRate ?? 0;
                    financialHoursModel.CurrentBusRideRate = prevFinancialHours?.BusMileageRate ?? 0;
                    return financialHoursModel;
                }
                else
                {
                    //if retruned values have data is it passed if not it is given 0
                    financialHoursModel.CurrentMileageRate = item.CurrentMileageRate;
                    financialHoursModel.YearlyMealValue = item.YearlyMealValue;
                    financialHoursModel.CurrentBusRideRate = item.CurrentBusRideRate;
                    return financialHoursModel;
                }
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1659._message, ErrorMessages._1659._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1660._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1660._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1661._message + e.Message, ErrorMessages._1661._code);
            }

            return null;
        }

		/// <summary>
		/// Function Name: GetVolunteerFinancalsPtoStipendRates
		/// Created By: Christopher Washburn
		/// Last Modified: 3/1/2023
		/// Last Modified By: Jon Maddocks
		/// 
		/// Purpose: The purpose of this function is to get the Volunteers PTO and stipend rates information, put it in its model, and return it
		/// to the front end for data binding.
		/// </summary>
		/// <param name="monthYear"></param>
		/// <returns>
		///  VolunteersFinancialsRatesModel - the model that holds the PTO and stipend rates.
		/// </returns>
		public VolunteersFinancialsRatesModel? GetVolunteerFinancalsPtoStipendRates(DateTime monthYear)
		{
            try
            {
                VolunteersFinancialsRatesModel financialHoursModel = new VolunteersFinancialsRatesModel();
                var item = _dbContext.PTOStipendRates.Where(x => x.Date.Month.Equals(monthYear.Month) && x.Date.Year == monthYear.Year).Select(
                x => new VolunteersFinancialsRatesModel
                {
                    CurrentStipendRate = x.StipendRate,
                    CurrentPtoRate = x.PTORate
                }).FirstOrDefault();

                if (item == null)
                {
                    var prevPtoStipendRate = _dbContext.PTOStipendRates.Where(x => x.Date <= monthYear.Date).OrderBy(x => x.Date).LastOrDefault();
                    financialHoursModel.CurrentStipendRate = prevPtoStipendRate?.StipendRate ?? 0.00;
                    financialHoursModel.CurrentPtoRate = prevPtoStipendRate?.PTORate ?? 0.00;
                    return financialHoursModel;
                }
                else
                {
                    //if retruned values have data is it passed if not it is given 0
                    financialHoursModel.CurrentStipendRate = item.CurrentStipendRate;
                    financialHoursModel.CurrentPtoRate = item.CurrentPtoRate;
                    return financialHoursModel;
                }
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1662._message, ErrorMessages._1662._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1663._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1663._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1664._message + e.Message, ErrorMessages._1664._code);
            }

            return new VolunteersFinancialsRatesModel();
        }

        /// <summary>
        /// Function Name: GetAllVolunteerFinancialRates
        /// 
        /// Purpose: Return all the financial rates.
        /// </summary>
        /// <param name="financialPtoStipendRates">Model that holds the current Pto Stipend rates</param>
        /// <param name="financialMealTransportRates">Model that holds the current meal and transport rates</param>
        /// <returns>Model that holds all the financial rates</returns>
        /// <author>Jon Maddocks</author>
        /// <date>March 26, 2023</date>
        public VolunteersFinancialsRatesModel? GetAllVolunteerFinancialRates(VolunteersFinancialsRatesModel? financialPtoStipendRates, VolunteersFinancialsRatesModel? financialMealTransportRates)
        {
            try
            {
                VolunteersFinancialsRatesModel volunteersFinancialsRates = new VolunteersFinancialsRatesModel();

                //if the model is not null populate it with data. If it is assign it 0;            
                volunteersFinancialsRates.CurrentStipendRate = financialPtoStipendRates?.CurrentStipendRate ?? 0;
                volunteersFinancialsRates.CurrentPtoRate = financialPtoStipendRates?.CurrentPtoRate ?? 0;
                volunteersFinancialsRates.YearlyMealValue = financialMealTransportRates?.YearlyMealValue ?? 0;
                volunteersFinancialsRates.CurrentMileageRate = financialMealTransportRates?.CurrentMileageRate ?? 0;
                volunteersFinancialsRates.CurrentBusRideRate = financialMealTransportRates?.CurrentBusRideRate ?? 0;

                return volunteersFinancialsRates;
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1665._message, ErrorMessages._1665._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1666._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1666._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1667._message + e.Message, ErrorMessages._1667._code);
            }

            return null;
        }

        /// <summary>
        /// Function Name: PushVolunteerFinancialsPtoStipend
        /// Created By: Christopher Washburn
        /// Last Modified: 3/22/2023
        /// Last Modified By: Christopher Washburn
        /// 
        /// Purpose: The purpose of this function is to push any updated or new pto and stipend data for a volunteer to the database. It does this by quering 
        /// the database and if a record is returned then we update it. If there is no record we add a new one.
        /// </summary>
        /// <param name="ptoStipendModel"></param>
        /// <param name="volunteerTuid"></param>
        /// <param name="monthYear"></param>		
        public void PushVolunteerFinancialsPtoStipend(VolunteerFinancialsPtoStipendModel? ptoStipendModel, int volunteerTuid, DateTime monthYear)
		{
            try
            {
                var ptoStipend = _dbContext.PTOStipends.Where(x => x.VolunteerTuid.Equals(volunteerTuid) && x.Date.Month == monthYear.Month && x.Date.Year == monthYear.Year).FirstOrDefault();
                if (ptoStipend != null) //update record if one is found
                {
                    ptoStipend.PtoStart = ptoStipendModel?.PtoStart ?? 0;
                    ptoStipend.PtoEnd = ptoStipendModel?.PtoEnded ?? 0;
                    ptoStipend.PtoUsed = ptoStipendModel?.PtoUsed ?? 0;
                    ptoStipend.PtoEarned = ptoStipendModel?.PtoEarned ?? 0;
                    ptoStipend.StipendPaid = ptoStipendModel?.StipendPaid ?? 0;

                    var ptoEligibility = ptoStipendModel?.IsPTOEligible ?? false;
                  //  ptoStipend.IsPTOEligible = ptoStipendModel?.IsPTOEligible ?? false;

                    //there is a change
                    if(ptoStipend.IsPTOEligible != ptoEligibility)
                    {
                        ptoStipend.IsPTOEligible = ptoStipendModel?.IsPTOEligible ?? false;
                        PushPTOEligibility(ptoStipend, volunteerTuid, monthYear);
                    } else
                    {
                        ptoStipend.IsPTOEligible = ptoStipendModel?.IsPTOEligible ?? false;
                    }

                    _dbContext.SaveChanges();
                }
                CalculateVolunterFinancialsSingle(volunteerTuid, monthYear);
                CalculateVolunteerFinancialsYear(volunteerTuid, monthYear);
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1668._message, ErrorMessages._1668._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1669._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1669._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1670._message + e.Message, ErrorMessages._1670._code);
            }
        }

        /// <summary>
        /// Function Name: 
        /// Created By: Christopher Washburn
        /// Last Modified: 3/23/2023
        /// Last Modified By: Christopher Washburn
        /// 
        /// Purpose: The purpose of this function is to push any updated or new hours data for a volunteer to the database. It does this by quering 
        /// the database and if a record is returned then we update it. If there is no record we add a new one.
        /// </summary>
        /// <param name="hoursModel"></param>
        /// <param name="volunteerTuid"></param>
        /// <param name="monthYear"></param>
        public void PushVolunteerFinancialsHours(VolunteerFinancialsHoursModel? hoursModel, int volunteerTuid, DateTime monthYear)
		{
            try
            {
                var hours = _dbContext.PTOStipends.Where(x => x.VolunteerTuid.Equals(volunteerTuid) && x.Date.Month == monthYear.Month && x.Date.Year == monthYear.Year).FirstOrDefault();
                if (hours != null) //update record if one is found
                {
                    hours.RegularHours = hoursModel?.RegHours ?? 0;
                    hours.YearToDateHour = hoursModel?.YtdHours ?? 0;

                    _dbContext.SaveChanges();
                }
                else //insert new record if no record is found
                {
                    PTOStipend newHours = new PTOStipend();
                    newHours.VolunteerTuid = volunteerTuid;
                    newHours.RegularHours = hoursModel?.RegHours ?? 0;
                    newHours.YearToDateHour = hoursModel?.YtdHours ?? 0;
                    newHours.Date = monthYear;

                    _dbContext.PTOStipends.Add(newHours);
                    _dbContext.SaveChanges();
                }
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1671._message, ErrorMessages._1671._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1672._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1672._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1673._message + e.Message, ErrorMessages._1673._code);
            }
        }

        /// <summary>
        /// Function Name: 
        /// Created By: Christopher Washburn
        /// Last Modified: 3/23/2023
        /// Last Modified By: Christopher Washburn
        /// 
        /// Purpose: The purpose of this function is to push any updated or new meal and transport data for a volunteer to the database. It does this by quering 
        /// the database and if a record is returned then we update it. If there is no record we add a new one.
        /// </summary>
        /// <param name="mealTransportModel"></param>
        /// <param name="volunteerTuid"></param>
        /// <param name="monthYear"></param>
        public void PushVolunteerFinancialsMealTransport(VolunteerFinancialsMealTransportModel? mealTransportModel, int volunteerTuid, DateTime monthYear)
        {
            try
            {
                var mealTransport = _dbContext.MealMileages.Where(x => x.VolunteerTuid.Equals(volunteerTuid) && x.Date.Month == monthYear.Month && x.Date.Year == monthYear.Year).FirstOrDefault();
                if (mealTransport != null) //update record if one is found
                {
                    mealTransport.MealCount = mealTransportModel?.SiteMeals ?? 0;
                    mealTransport.Mileage = mealTransportModel?.Mileage ?? 0;
                    mealTransport.BusRideCount = mealTransportModel?.BusRides ?? 0;
                    _dbContext.SaveChanges();
                }
                else //insert new record if no record is found
                {
                    MealMileage newMealMileage = new MealMileage();
                    newMealMileage.VolunteerTuid = volunteerTuid;
                    newMealMileage.MealCount = mealTransportModel?.SiteMeals ?? 0;
                    newMealMileage.Mileage = mealTransportModel?.Mileage ?? 0;
                    newMealMileage.BusRideCount = mealTransportModel?.BusRides ?? 0;
                    newMealMileage.Date = monthYear;

                    _dbContext.MealMileages.Add(newMealMileage);
                    _dbContext.SaveChanges();
                }
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1698._message, ErrorMessages._1698._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1699._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1699._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1700._message + e.Message, ErrorMessages._1700._code);
            }
        }

        /// <summary>
        /// Function Name: CalculatePTOEarned
        /// 
        /// Purpose: The purpose of this function is to calculate the PTO Earned by a single volunteer and return the value to the database.
        /// </summary>
        /// <param name="financialsHours">Model that holds the volunteers financials hours</param>
        /// <param name="financialRates">Model that holds the financial rates</param>
        /// <param name="financialPtoStipends">Entity that holds the volunteers PTO Stipends information</param>
        /// <author>Jon Maddocks</author>
        /// <date>March 24, 2023</date>
        public void CalculatePTOEarned(VolunteersFinancialsRatesModel? financialRates, PTOStipend? financialPtoStipends)
        {
            try
            {
                //CALC: Current months regular hours * PTO rate
                if (financialPtoStipends == null)
                {
                    return;
                }
                var ptoRate = financialRates?.CurrentPtoRate ?? 0;
                var regHours = financialPtoStipends?.RegularHours ?? 0;
                var ptoEligibility = financialPtoStipends?.IsPTOEligible ?? false;

                //If volunteer is not PTO eligible, set PTO earned to 0
                if (!ptoEligibility)
                {
                    if (financialPtoStipends != null)
                    {
                        //Earned is set to 0 and Used is to 0, since PTO cannot be generated
                        financialPtoStipends.PtoEarned = 0;
                        _dbContext.SaveChanges();
                        return;
                    }
                }

                var ptoEarned = regHours * (decimal)ptoRate;
                var ptoEnded = financialPtoStipends?.PtoEnd ?? 0;
                //Volunteer should not be able to earn more PTO's than hours worked
                if (ptoEarned > regHours)
                {
                    ptoEarned = regHours;
                }
                //Volunteer is PTO Eligible
                if (financialPtoStipends != null)
                {
                    financialPtoStipends.PtoEarned = ptoEarned;
                    _dbContext.SaveChanges();
                }
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1701._message, ErrorMessages._1701._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1702._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1702._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1703._message + e.Message, ErrorMessages._1703._code);
            }

        }

        /// <summary>
        /// Function Name: CalculatePTOStart
        /// 
        /// Purpose: The purpose of this function is to calculate the PTO Start by a single volunteer and return the value to the database.
        /// </summary>
        /// <param name="ptoStipends">Entity that holds the volunteers PTO Stipends information</param>
        /// <param name="prevPtoStipends">Entity that holds the volunteers PTO Stipends information from the previous month</param>
        /// <author>Jon Maddocks</author>
        /// <date>March 24, 2023</date>
        public void CalculatePTOStart(PTOStipend? ptoStipends, PTOStipend? prevPtoStipends)
        {
            try
            {
                //CALC: PTO Start is the previous month's PTO End value
                var ptoStart = ptoStipends?.PtoStart ?? 0;
                var prevPtoEnd = prevPtoStipends?.PtoEnd ?? 0;
                if (ptoStart != prevPtoEnd)
                {
                    if (ptoStipends != null)
                    {
                        ptoStipends.PtoStart = prevPtoEnd;
                        _dbContext.SaveChanges();
                    }
                }
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1704._message, ErrorMessages._1704._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1705._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1705._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1706._message + e.Message, ErrorMessages._1706._code);
            }
        }

        /// <summary>
        /// Function Name: CalculatePTOEnded
        /// 
        /// Purpose: The purpose of this function is to calculate the PTO Ended by a single volunteer and return the value to the database.
        /// </summary>
        /// <param name="ptoStipends">Entity that holds the volunteers PTO Stipends information</param>
        /// <author>Jon Maddocks</author>
        /// <date>March 24, 2023</date>
        public void CalculatePTOEnded(PTOStipend? ptoStipends)
        {
            try
            {
                //CALC: Ended = Current month's PTO start - current month's PTO Used + current month's PTO earned
                // (round to nearest integer, 0.0-0.49 rounds down and 0.50-0.99 rounds up)
                var ptoStart = ptoStipends?.PtoStart ?? 0;
                var ptoUsed = ptoStipends?.PtoUsed ?? 0;
                var ptoEarned = ptoStipends?.PtoEarned ?? 0;
                var ptoEnded = Math.Round(ptoStart - ptoUsed + ptoEarned, 0, MidpointRounding.AwayFromZero);

                if (ptoStipends != null)
                {
                    if(ptoEnded < 0)
                    {
                        ptoStipends.PtoEnd = Math.Round(ptoStart + ptoEarned, 0, MidpointRounding.AwayFromZero);
                        ptoStipends.PtoUsed = Math.Round(ptoStart + ptoEarned, 0, MidpointRounding.AwayFromZero);
                    } else
                    {
                        ptoStipends.PtoEnd = ptoEnded;
                    }
                    _dbContext.SaveChanges();
                }
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1707._message, ErrorMessages._1707._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1708._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1708._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1709._message + e.Message, ErrorMessages._1709._code);
            }
        }

        /// <summary>
        /// Function Name: CalculateStipendPaid
        /// 
        /// Purpose: The purpose of this function is to calculate the Stipend Paid by a single volunteer and return the value to the database.
        /// </summary>
        /// <param name="financialsHoursModel">Model that holds the volunteers financials hours</param>
        /// <param name="financialsRatesModel">Model that holds the financial rates</param>
        /// <param name="ptoStipends">Entity that holds the volunteers PTO Stipends information</param>
        /// <author>Jon Maddocks</author>
        /// <date>March 24, 2023</date>
        public void CalculateStipendPaid(VolunteersFinancialsRatesModel? financialsRatesModel, PTOStipend? ptoStipends)
        {
            try
            {
                //CALC: (current month's PTO start + current month's PTO Used) * Stipend Rate
                if (ptoStipends == null)
                {
                    return;
                }
                var regHours = ptoStipends?.RegularHours ?? 0;
                var ptoUsed = ptoStipends?.PtoUsed ?? 0;
                var stipendRate = financialsRatesModel?.CurrentStipendRate ?? 0;
                var stipendPaid = (regHours + ptoUsed) * (decimal)stipendRate;

                if (ptoStipends != null)
                {
                    ptoStipends.StipendPaid = stipendPaid;
                    _dbContext.SaveChanges();
                }
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1710._message, ErrorMessages._1710._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1711._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1711._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1712._message + e.Message, ErrorMessages._1712._code);
            }
        }

        /// <summary>
        /// Function Name: CalculateYTDHours
        /// 
        /// Purpose: The purpose of this function is to calculate the Year-To-Date(YTD) Hours by a single volunteer and return the value to the database.
        /// </summary>
        /// <param name="ptoStipends">Entity that holds the volunteers PTO Stipends information</param>
        /// <param name="prevPtoStipends">Entity that holds the volunteers PTO Stipends information from the previous month</param>
        /// <author>Jon Maddocks</author>
        /// <date>March 24, 2023</date>
        public void CalculateYTDHours(PTOStipend? ptoStipends, PTOStipend? prevPtoStipends)
        {
            try
            {
                //CALC: Previous month's YTD Hours + the current month's RegHours + current month's PTO Used
                var prevYTDHours = prevPtoStipends?.YearToDateHour ?? 0;
                if (ptoStipends != null)
                {
                    ptoStipends.YearToDateHour = prevYTDHours + ptoStipends.RegularHours + ptoStipends.PtoUsed;
                    _dbContext.SaveChanges();
                }
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1713._message, ErrorMessages._1713._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1714._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1714._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1715._message + e.Message, ErrorMessages._1715._code);
            }
        }

        /// <summary>
        /// Function Name: CalculateVolunteerFinancials
        /// 
        /// Purpose: The purpose of this function is to calculate the financials for a single volunteer. ALL records for the volunteer are looped through
        /// due to all the data requiring previously existed data. Future data is updated accordingly. 
        /// 
        /// The reasoning for looping through every record, rather than only updating future records, is due largely in part of missing/null past records. A volunteer
        /// may go on leave for a couple of months and have no potential past records, and the data requires to search for the previous month's information. Looping through
        /// every valid record makes it a bit simpler when summing up past totals. 
        /// </summary>
        /// <param name="volunteerTUID">Unique ID for volunteer</param>
        /// <param name="monthYear">DateTime for the month and year values</param>
        /// <author>Jon Maddocks</author>
        /// <date>March 24, 2023</date>
        public void CalculateVolunteerFinancialsYear(int volunteerTUID, DateTime monthYear)
        {
            try
            {
            //Get PTOStipends Entity
            PTOStipend? financialPtoStipends = _dbContext.PTOStipends.Where(x => x.VolunteerTuid.Equals(volunteerTUID) && x.Date.Month == monthYear.Month && x.Date.Year == monthYear.Year).FirstOrDefault();

                if (financialPtoStipends == null)
                {
                    //No record was found, skip calculations
                    return;
                }

                IEnumerable<PTOStipend?> listAllPtoStipends = _dbContext.PTOStipends.Where(x => x.VolunteerTuid.Equals(volunteerTUID) && x.Date.Year == monthYear.Year).OrderBy(x => x.Date);
                //Calculate changes throughout the year
                for (int i = 0; i < listAllPtoStipends.Count(); i++)
                {
                    var currRecord = listAllPtoStipends.ElementAtOrDefault(i);
                    if (currRecord != null)
                    {
                        var prevRecord = listAllPtoStipends.ElementAtOrDefault(i - 1);
                        CalculatePTOStart(currRecord, prevRecord);
                        CalculatePTOEnded(currRecord);
                        CalculateYTDHours(currRecord, prevRecord);
                    }
                }
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1716._message, ErrorMessages._1716._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1717._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1717._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1718._message + e.Message, ErrorMessages._1718._code);
            }
        }

        /// <summary>
        /// Function Name: CalculateVolunterFinancialsSingle
        /// 
        /// Purpose: The purpose of this function is to calculate PTO Earned and Stipend Paid for a single volunteer during the provided month and year. 
        /// </summary>
        /// <param name="volunteerTUID"></param>
        /// <param name="monthYear"></param>
        /// <author>Jon Maddocks</author>
        /// <date>March 29, 2023</date>
        public void CalculateVolunterFinancialsSingle(int volunteerTUID, DateTime monthYear)
        {
            try
            {
                //Get PTOStipends Entity
                PTOStipend? financialPtoStipends = _dbContext.PTOStipends.Where(x => x.VolunteerTuid.Equals(volunteerTUID) && x.Date.Month == monthYear.Month && x.Date.Year == monthYear.Year).FirstOrDefault();
                //Get the current pto/stipend rates for the volunteer
                VolunteersFinancialsRatesModel? financialHours = GetVolunteerFinancalsPtoStipendRates(monthYear);
                //Get current meal/transport/bus rates for the volunteer
                VolunteersFinancialsRatesModel? financialRates = GetVolunteerFinancalsPtoStipendRates(monthYear);

                if (financialPtoStipends == null)
                {
                    return;
                }
                //Calculate single-time fields
                CalculatePTOEarned(financialHours, financialPtoStipends);
                CalculateStipendPaid(financialRates, financialPtoStipends);
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1719._message, ErrorMessages._1719._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1720._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1720._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1721._message + e.Message, ErrorMessages._1721._code);
            }
        }

        /// <summary>
        /// Function Name: VerifyPtoStipendRecord
        /// 
        /// Purpose: Verify whether or not a single volunteer has a record in the database for a given month and year. 
        /// </summary>
        /// <param name="volunteerTUID">Unique ID for volunteer</param>
        /// <param name="monthYear">DateTime for the month and year values</param>
        /// <returns>Boolean that details if the record exists</returns>
        /// <author>Jon Maddocks</author>
        /// <date>March 25, 2023</date>
        public bool VerifyPtoStipendRecord(int volunteerTUID, DateTime monthYear)
        {
            try
            {
                return _dbContext.PTOStipends.Any(x => x.VolunteerTuid.Equals(volunteerTUID) && x.Date.Month == monthYear.Month && x.Date.Year == monthYear.Year);
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1722._message, ErrorMessages._1722._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1723._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1723._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1724._message + e.Message, ErrorMessages._1724._code);
            }

            return false;
        }

        /// <summary>
        /// Function Name: PushPTOEligibility
        /// 
        /// Purpose: The purpose of this function is to specifically push whether or not a volunteer is PTO eligible.
        ///     If the volunteer is eligible, all future months will have the volunteer eligible.
        ///     If not eligible, ALL past months from the given monthYear will disable PTO eligibility and remove all previously earned PTO and PTO used.
        /// </summary>
        /// <param name="financialPtoStipends">Entity that holds a single volunteer's Pto Stipend information</param>
        /// <param name="volunteerTUID">Unique ID for volunteer</param>
        /// <param name="monthYear">DateTime for the month and year values</param>
        /// <author>Jon Maddocks</author>
        /// <date>March 25, 2023</date>
        public void PushPTOEligibility(PTOStipend? financialPtoStipends, int volunteerTUID, DateTime monthYear)
        {
            try
            {
                var ptoEligibility = financialPtoStipends?.IsPTOEligible ?? false;
                IEnumerable<PTOStipend> allFutureRecords = _dbContext.PTOStipends.Where(x => x.VolunteerTuid == volunteerTUID && x.Date >= monthYear.Date).ToList();
                if (ptoEligibility)
                {
                    //When eligibile for PTO, ensure every future record from this point is able to generate PTO
                    foreach (var x in allFutureRecords)
                    {
                        x.IsPTOEligible = true;
                    }
                }
                else
                {
                    //When not eligible for PTO, ensure every future record does not accrue any PTO and wipe out any PTO earned
                    foreach (var x in allFutureRecords)
                    {
                        x.IsPTOEligible = false;
                        x.PtoEarned = 0;
                        x.PtoUsed = 0;
                    }
                }
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1725._message, ErrorMessages._1725._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1726._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1726._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1727._message + e.Message, ErrorMessages._1727._code);
            }
        }

        /// <summary>
        /// Function Name: GetPreviousMonthPTOEligibility
        /// 
        /// Purpose: The purpose of this function is to determine if the previous month was PTO eligibile. 
        /// </summary>
        /// <param name="volunteerTUID">Unique ID for volunteer</param>
        /// <param name="monthYear">DateTime for the month and year values</param>
        /// <returns>Boolean return if the volunteer was PTO eligible last month</returns>
        /// <author>Jon Maddocks</author>
        /// <date>March 25, 2023</date>
        public bool GetPreviousMonthPTOEligibility(int volunteerTUID, DateTime monthYear)
        {
            try
            {
                var prevPtoStipend = _dbContext.PTOStipends.Where(x => x.VolunteerTuid.Equals(volunteerTUID) && x.Date <= monthYear.Date).OrderBy(x => x.Date).LastOrDefault();

                if (prevPtoStipend != null)
                {
                    return prevPtoStipend.IsPTOEligible;
                }
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1728._message, ErrorMessages._1728._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1729._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1729._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1730._message + e.Message, ErrorMessages._1730._code);
            }

            return false;
        }

        /// <summary>
        /// Function Name: VerifyMealTransportRecord
        /// 
        /// Purpose: The purpose of this function is to determine if a meal and transport record exists for the volunteer.
        /// </summary>
        /// <param name="volunteerTUID"></param>
        /// <param name="monthYear"></param>
        /// <returns></returns>
        /// <author>Jon Maddocks</author>
        /// <date>April 6, 2023</date>
        public bool VerifyMealTransportRecord(int volunteerTUID, DateTime monthYear)
        {
            try
            {
                return _dbContext.MealMileages.Any(x => x.VolunteerTuid.Equals(volunteerTUID) && x.Date.Month == monthYear.Month && x.Date.Year == monthYear.Year);
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1731._message, ErrorMessages._1731._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1732._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1732._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1733._message + e.Message, ErrorMessages._1733._code);
            }

            return false;
        }

        #endregion

        public IEnumerable<string> GetAllVolunteerNames()
        {
            return _dbContext.Volunteers.Select(x => x.FullName).Distinct().ToList();
        }

        public Volunteer? GetVolunteerByFullName(String volunteerFirstName, String volunteerLastName)
        {
            return _dbContext.Volunteers.FirstOrDefault(x => x.FirstName.Equals(volunteerFirstName) && x.LastName.Equals(volunteerLastName));
        }

        /// <summary>
        /// Gets all demographic information related to a given volunteer 
        /// </summary>
        /// <param name="volunteerTUID"> TUID of the given volunteer</param>
        /// <returns>VolunteerDemographicModel with all info</returns>
        /// <author> Isabelle Johns </author>
        /// <created> 4/11/23 </created>
        public VolunteerDemographicsModel? GetVolunteerDemographicsInfo(int volunteerTUID)
        {
            try
            {
                var volunteer = _dbContext.Volunteers.FirstOrDefault(x => x.Tuid.Equals(volunteerTUID));
                if (volunteer != null)
                {
                    var volunteerInfo = new VolunteerDemographicsModel()
                    {
                        LastUpdated = volunteer.LastUpdated,
                        DateOfBirth = volunteer.Birthday,
                        SeparationDate = volunteer.SeparatedDate,
                        StartDate = volunteer.StartDate,
                        IsActive = volunteer.IsActive,
                        IsVeteran = volunteer.IsVeteran,
                        IsFamilyOfMilitary = volunteer.IsFamilyOfMilitary,
                        GenderTuid = volunteer.GenderTuid,
                        IdentifiesAsTuid = volunteer.IdentifiesAsTuid,
                        EthnicityTuid = volunteer.EthnicityTuid,
                        RacialGroupTuid = volunteer.RacialGroupTuid,
                    };

                    volunteerInfo.Age = DateTime.Now.Year - volunteer.Birthday.Year;
                    if (DateTime.Now.DayOfYear < volunteer.Birthday.DayOfYear)
                        volunteerInfo.Age -= 1;

                    var separatedReason = _dbContext.ReasonsSeparated.FirstOrDefault(x => x.VolunteerTuid.Equals(volunteerTUID));
                    if(separatedReason != null)
                    {
                        volunteerInfo.ReasonSeparatedTuid = separatedReason.InactiveStatusTypeItemTuid;
                    }

                    return volunteerInfo;
                }
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1734._message, ErrorMessages._1734._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1735._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1735._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1736._message + e.Message, ErrorMessages._1736._code);
            }

            return null;
        }

        /// <summary>
        /// Database call to update the information from the volunteer demographics page
        /// </summary>
        /// <param name="volunteerDemographics"> View model of all Demographics Info </param>
        /// <author> Isabelle Johns </author>
        /// <created> 4/11/23 </created>
        public void UpdateVolunteerDemographics(VolunteerDemographicsViewModel volunteerDemographics)
        {
            try
            {
                var volunteerTuid = volunteerDemographics.VolunteerTuid;
                var volunteer = _dbContext.Volunteers.FirstOrDefault(x => x.Tuid.Equals(volunteerTuid));

                if(!string.IsNullOrEmpty(volunteerDemographics.FirstName))
                {
                    volunteer.FirstName = volunteerDemographics.FirstName;
                }
                if(!string.IsNullOrEmpty(volunteerDemographics.LastName))
                {
                    volunteer.LastName = volunteerDemographics.LastName;
                }

                volunteer.LastUpdated = DateTime.Now;
                volunteer.Birthday = volunteerDemographics.DateOfBirth;
                volunteer.GenderTuid = volunteerDemographics.GenderTuid!.Value;
                volunteer.EthnicityTuid = volunteerDemographics.EthnicityTuid!.Value;
                volunteer.IdentifiesAsTuid = volunteerDemographics.IdentityTuid!.Value;
                volunteer.RacialGroupTuid = volunteerDemographics.RacialTuid!.Value;
                volunteer.SeparatedDate = volunteerDemographics.SeparationDate;
                volunteer.IsVeteran = volunteerDemographics.IsVeteran;
                volunteer.IsFamilyOfMilitary = volunteerDemographics.IsFamilyOfMilitary;

                if(volunteerDemographics.SeparationTuid.HasValue)
                {
                    volunteer.LastUpdated = DateTime.Now;
                    volunteer.Birthday = volunteerDemographics.DateOfBirth;
                    volunteer.GenderTuid = volunteerDemographics.GenderTuid!.Value;
                    volunteer.EthnicityTuid = volunteerDemographics.EthnicityTuid!.Value;
                    volunteer.IdentifiesAsTuid = volunteerDemographics.IdentityTuid!.Value;
                    volunteer.RacialGroupTuid = volunteerDemographics.RacialTuid!.Value;
                    volunteer.SeparatedDate = volunteerDemographics.SeparationDate;
                    volunteer.IsVeteran = volunteerDemographics.IsVeteran;
                    volunteer.IsFamilyOfMilitary = volunteerDemographics.IsFamilyOfMilitary;

                    if (volunteerDemographics.SeparationTuid.HasValue)
                    {
                        var volunteerReasonSeparated = _dbContext.ReasonsSeparated.FirstOrDefault(x => x.VolunteerTuid.Equals(volunteerTuid));
                        if (volunteerReasonSeparated != null)
                        {
                            volunteerReasonSeparated.InactiveStatusTypeItemTuid = volunteerDemographics.SeparationTuid.Value;
                            volunteerReasonSeparated.Date = volunteerDemographics.SeparationDate!.Value;
                        }
                        else
                        {
                            var reasonSeparated = new ReasonSeparated
                            {
                                InactiveStatusTypeItemTuid = volunteerDemographics.SeparationTuid.Value,
                                Date = volunteerDemographics.SeparationDate!.Value,
                                VolunteerTuid = volunteerTuid!.Value
                            };

                            _dbContext.ReasonsSeparated.Add(reasonSeparated);
                        }

                        _dbContext.SaveChanges();
                    }
                }
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1845._message, ErrorMessages._1845._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1846._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1846._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1847._message + e.Message, ErrorMessages._1847._code);
            }
        }

        public GenderTypeItem? GetGenderByTuid(int genderTuid)
        {
            try
            {
                return _dbContext.GenderTypeItems.FirstOrDefault(x => x.Tuid == genderTuid);
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1737._message, ErrorMessages._1737._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1738._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1738._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1739._message + e.Message, ErrorMessages._1739._code);
            }

            return null;
        }

        public IdentifiesAsTypeItem? GetIdentifiesAsByTuid(int identifiesAsTuid)
        {
            try
            {
                return _dbContext.IdentifiesAsTypeItems.FirstOrDefault(x => x.Tuid == identifiesAsTuid);
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1740._message, ErrorMessages._1740._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1741._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1741._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1742._message + e.Message, ErrorMessages._1742._code);
            }

            return null;
        }

        public EthnicityTypeItem? GetEthnicityByTuid(int ethnicityTuid)
        {
            try
            {
                return _dbContext.EthnicityTypeItems.FirstOrDefault(x => x.Tuid == ethnicityTuid);
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1743._message, ErrorMessages._1743._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1744._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1744._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1745._message + e.Message, ErrorMessages._1745._code);
            }

            return null;
        }

        public RacialGroupTypeItem? GetRacialGroupByTuid(int racialGroupTuid)
        {
            try
            {
                return _dbContext.RacialGroupTypeItems.FirstOrDefault(x => x.Tuid == racialGroupTuid);
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1746._message, ErrorMessages._1746._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1747._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1747._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1748._message + e.Message, ErrorMessages._1748._code);
            }

            return null;
        }

        public ReasonSeparated? GetReasonsSeparatedByTuid(int volunteerTuid)
        {
            try
            {
                return _dbContext.ReasonsSeparated.FirstOrDefault(x => x.VolunteerTuid == volunteerTuid);
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1749._message, ErrorMessages._1749._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1750._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1750._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1751._message + e.Message, ErrorMessages._1751._code);
            }

            return null;
        }

        public InactiveStatusTypeItem? GetInactiveStatusByTuid(int inactiveStatusTuid)
        {
            try
            {
                return _dbContext.InactiveStatusTypeItems.FirstOrDefault(x => x.Tuid == inactiveStatusTuid);
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1752._message, ErrorMessages._1752._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1753._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1753._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1754._message + e.Message, ErrorMessages._1754._code);
            }

            return null;
        }

        /// <summary>
        /// Function Name: GetGenderNameAndId
        /// Created By: Ryley Taub & Jon Maddocks
        /// Date Created 2/25/2023
        /// Last Modified: 3/17/2023
        /// Last Modified By: Jon Maddocks
        /// 
        /// Purpose: Search all volunteers that contain the gender TUID and return the list of volunteers.
        ///		A condition is set in place, if false, just return the list of volunteers. If true, add a 
        ///		static item to the end of the list. This represents an option to select if they want to 
        ///		add a new item.
        /// </summary>
        /// <param name="addNew">Boolean state for categories</param>
        /// <returns>Volunteer list that has the gender item</returns>
        public IEnumerable<GenderNameIdModel> GetGenderNameAndId(bool addNew)
        {
            try
            {
                if (!addNew)
                {
                    return _dbContext.GenderTypeItems.Select(x => new GenderNameIdModel { Tuid = x.Tuid, Name = x.Name }).ToList();
                }
                else
                {
                    var item = _dbContext.GenderTypeItems.Select(x => new GenderNameIdModel { Tuid = x.Tuid, Name = x.Name }).ToList();
                    item.Add(new GenderNameIdModel { Name = "ADD NEW GENDER" });
                    return item;
                }
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1755._message, ErrorMessages._1755._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1756._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1756._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1757._message + e.Message, ErrorMessages._1757._code);
            }

            return Enumerable.Empty<GenderNameIdModel>();

        }

        /// <summary>
        /// Function Name: GetEthnityNameAndId
        /// Last Modified: 3/17/2023
        /// Last Modified By: Jon Maddocks
        /// 
        /// Purpose: Search all volunteers that contain the ethnicity TUID and return the list of volunteers.
        ///		A condition is set in place, if false, just return the list of volunteers. If true, add a 
        ///		static item to the end of the list. This represents an option to select if they want to 
        ///		add a new item.
        /// </summary>
        /// <param name="addNew">Boolean state for categories</param>
        /// <returns>Volunteer list that has the ethnicity item</returns>
		/// <author>Ryley Taub & Jon Maddocks</author>
		/// <created>2/25/2023</created>
        public IEnumerable<EthnicityNameIdModel> GetEthnityNameAndId(bool addNew)
        {
            try
            {
                if (!addNew)
                {
                    return _dbContext.EthnicityTypeItems.Select(x => new EthnicityNameIdModel { Tuid = x.Tuid, Name = x.Name }).ToList();
                }
                else
                {
                    var item = _dbContext.EthnicityTypeItems.Select(x => new EthnicityNameIdModel { Tuid = x.Tuid, Name = x.Name }).ToList();
                    item.Add(new EthnicityNameIdModel { Name = "ADD NEW ETHNICITY" });
                    return item;
                }
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1758._message, ErrorMessages._1758._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1759._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1759._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1760._message + e.Message, ErrorMessages._1760._code);
            }

            return Enumerable.Empty<EthnicityNameIdModel>();

        }

        /// <summary>
        /// Function Name: GetRacialGroupNameAndId
        /// Last Modified: 3/17/2023
        /// Last Modified By: Jon Maddocks
        /// 
        /// Purpose: Search all volunteers that contain the racial group TUID and return the list of volunteers.
        ///		A condition is set in place, if false, just return the list of volunteers. If true, add a 
        ///		static item to the end of the list. This represents an option to select if they want to 
        ///		add a new item.
        /// </summary>
        /// <param name="addNew">Boolean state for categories</param>
        /// <returns>Volunteer list that has the racial group item</returns>
		/// <author>Ryley Taub & Jon Maddocks</author>
		/// <created>2/25/2023</created>
        public IEnumerable<RacialGroupNameIdModel> GetRacialGroupNameAndId(bool addNew)
        {
            try
            {
                if (!addNew)
                {
                    return _dbContext.RacialGroupTypeItems.Select(x => new RacialGroupNameIdModel { Tuid = x.Tuid, Name = x.Name }).ToList();
                }
                else
                {
                    var item = _dbContext.RacialGroupTypeItems.Select(x => new RacialGroupNameIdModel { Tuid = x.Tuid, Name = x.Name }).ToList();
                    item.Add(new RacialGroupNameIdModel { Name = "ADD NEW RACIAL GROUP" });
                    return item;
                }
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1761._message, ErrorMessages._1761._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1762._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1762._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1763._message + e.Message, ErrorMessages._1763._code);
            }

            return Enumerable.Empty<RacialGroupNameIdModel>();
        }

        /// <summary>
        /// Function Name: GetIdentifiesAsNameAndId
        /// Last Modified: 3/17/2023
        /// Last Modified By: Jon Maddocks
        /// 
        /// Purpose: Search all volunteers that contain the identifies as TUID and return the list of volunteers.
        ///		A condition is set in place, if false, just return the list of volunteers. If true, add a 
        ///		static item to the end of the list. This represents an option to select if they want to 
        ///		add a new item.
        /// </summary>
        /// <param name="addNew">Boolean state for categories</param>
        /// <returns>Volunteer list that has the identifies as item</returns>
        /// <author>Ryley Taub & Jon Maddocks</author>
        /// <created>2/25/2023</created>
        public IEnumerable<IdentifiesAsNameIdModel> GetIdentifiesAsNameAndId(bool addNew)
        {
            try
            {
                if (!addNew)
                {
                    return _dbContext.IdentifiesAsTypeItems.Select(x => new IdentifiesAsNameIdModel { Tuid = x.Tuid, Name = x.Name }).ToList();
                }
                else
                {
                    var item = _dbContext.IdentifiesAsTypeItems.Select(x => new IdentifiesAsNameIdModel { Tuid = x.Tuid, Name = x.Name }).ToList();
                    item.Add(new IdentifiesAsNameIdModel { Name = "ADD NEW IDENTIFIES AS" });
                    return item;
                }
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1764._message, ErrorMessages._1764._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1765._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1765._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1766._message + e.Message, ErrorMessages._1766._code);
            }

            return Enumerable.Empty<IdentifiesAsNameIdModel>();
        }


        /// <summary>
        /// Function Name: GetReasonSeparatedNameAndId
        /// Last Modified: 3/17/2023
        /// Last Modified By: Jon Maddocks
        /// 
        /// Purpose: Search all volunteers that contain the identifies as TUID and return the list of volunteers.
        ///		A condition is set in place, if false, just return the list of volunteers. If true, add a 
        ///		static item to the end of the list. This represents an option to select if they want to 
        ///		add a new item.
        /// </summary>
        /// <param name="addNew">Boolean state for categories</param>
        /// <returns>Volunteer list that has the identifies as item</returns>
        /// <author>Ryley Taub & Jon Maddocks</author>
        /// <created>2/25/2023</created>
        public IEnumerable<InactiveStatusTypeItem> GetReasonSeparatedNameAndId(bool addNew)
        {
            try
            {
                if (!addNew)
                {
                    return _dbContext.InactiveStatusTypeItems.Select(x => new InactiveStatusTypeItem { Tuid = x.Tuid, Name = x.Name }).ToList();
                }
                else
                {
                    var item = _dbContext.InactiveStatusTypeItems.Select(x => new InactiveStatusTypeItem { Tuid = x.Tuid, Name = x.Name }).ToList();
                    item.Add(new InactiveStatusTypeItem { Name = "ADD NEW IDENTIFIES AS" });
                    return item;
                }
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1767._message, ErrorMessages._1767._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1768._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1768._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1769._message + e.Message, ErrorMessages._1769._code);
            }

            return Enumerable.Empty<InactiveStatusTypeItem>();
        }

        /// <summary>
        /// Function Name: GetReasonSeparatedNameAndId
        /// Last Modified: 3/17/2023
        /// Last Modified By: Jon Maddocks
        /// 
        /// Purpose: Search all volunteers that contain the identifies as TUID and return the list of volunteers.
        ///		A condition is set in place, if false, just return the list of volunteers. If true, add a 
        ///		static item to the end of the list. This represents an option to select if they want to 
        ///		add a new item.
        /// </summary>
        /// <param name="addNew">Boolean state for categories</param>
        /// <returns>Volunteer list that has the identifies as item</returns>
        /// <author>Ryley Taub & Jon Maddocks</author>
        /// <created>2/25/2023</created>
        public IEnumerable<TempInfoModel> GetTempInfoNameAndId(bool addNew)
        {
            try
            {
                if (!addNew)
                {
                    List<TempInfoModel> temps = _dbContext.TempInfoTypeItems.Select(x => new TempInfoModel { Tuid = x.Tuid, Name = x.Name, Type = (int)x.TempInfoType }).ToList();
                    foreach (var item in temps)
                    {
                        if (item.Type == 1)
                        {
                            item.Name = item.Name + "(CheckBox)";
                        }
                        else if (item.Type == 0)
                        {
                            item.Name = item.Name + "(Date)";
                        }
                    }
                    return temps;
                }
                else
                {
                    var item = _dbContext.InactiveStatusTypeItems.Select(x => new TempInfoModel { Tuid = x.Tuid, Name = x.Name }).ToList();
                    item.Add(new TempInfoModel { Name = "ADD NEW IDENTIFIES AS" });
                    return item;
                }
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1770._message, ErrorMessages._1770._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1771._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1771._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1772._message + e.Message, ErrorMessages._1772._code);
            }

            return Enumerable.Empty<TempInfoModel>();
        }




        /// <summary>
        /// Function Name: GetIdentifiesAsNameAndId
        /// Last Modified: 3/26/2023
        /// Last Modified By: Ryley Taub
        /// 
        /// Purpose: Search all volunteers that contain the identifies as TUID and return the list of volunteers.
        ///		A condition is set in place, if false, just return the list of volunteers. If true, add a 
        ///		static item to the end of the list. This represents an option to select if they want to 
        ///		add a new item.
        /// </summary>
        /// <param name="addNew">Boolean state for categories</param>
        /// <returns>Volunteer list that has the identifies as item</returns>
        /// <author>Ryley Taub & Jon Maddocks</author>
        /// <created>2/25/2023</created>
        public IEnumerable<ReasonsSeparatedNameIdModel> GetReasonsSeparatedNameAndId(bool addNew)
        {
            try
            {
                if (!addNew)
                {
                    return _dbContext.InactiveStatusTypeItems.Select(x => new ReasonsSeparatedNameIdModel { Tuid = x.Tuid, Name = x.Name }).ToList();
                }
                else
                {
                    var item = _dbContext.InactiveStatusTypeItems.Select(x => new ReasonsSeparatedNameIdModel { Tuid = x.Tuid, Name = x.Name }).ToList();
                    item.Add(new ReasonsSeparatedNameIdModel { Name = "ADD NEW REASON" });
                    return item;
                }
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1773._message, ErrorMessages._1773._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1774._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1774._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1775._message + e.Message, ErrorMessages._1775._code);
            }

            return Enumerable.Empty<ReasonsSeparatedNameIdModel>();
        }

        /// <summary>
        /// Update the database with the newly input information provided by the user. 
        /// </summary>
        /// <author>Jon Maddocks</author>
        public void UpdateVolunteer()
        {
            try
            {
                _dbContext.SaveChanges();
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1776._message, ErrorMessages._1776._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1777._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1777._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1778._message + e.Message, ErrorMessages._1778._code);
            }
        }

        // ADD CATEGORY ITEMS //
        // TODO: Refractor for efficiency

        /// <summary>
        /// Function Name: AddGenderItem
        /// 
        /// Purpose: Add a new item to the GenderTypeItems database table. 
        /// </summary>
        /// <param name="item">Gender item</param>
        /// <author>Jon Maddocks</author>
        /// <created>3/17/2023</created>
        public void AddGenderItem(string item)
		{
            try
            {
                var existingItem = _dbContext.GenderTypeItems.FirstOrDefault(x => x.Name.Equals(item));
                if (existingItem == null)
                {
                    var newItem = new GenderTypeItem()
                    {
                        Name = item
                    };

                    // Add new user and save
                    _dbContext.GenderTypeItems.Add(newItem);
                    _dbContext.SaveChanges();
                }
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1779._message, ErrorMessages._1779._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1780._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1780._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1781._message + e.Message, ErrorMessages._1781._code);
            }
        }

        /// <summary>
        /// Function Name: AddIdentifiesAsItem
        /// 
        /// Purpose: Add a new item to the IdentifiesAsTypeItems database table. 
        /// </summary>
        /// <param name="item">Identifies As item</param>
        /// <author>Jon Maddocks</author>
        /// <created>3/17/2023</created>
        public void AddIdentifiesAsItem(string item)
		{
            try
            {
                var existingItem = _dbContext.IdentifiesAsTypeItems.FirstOrDefault(x => x.Name.Equals(item));
                if (existingItem == null)
                {
                    var newItem = new IdentifiesAsTypeItem()
                    {
                        Name = item
                    };

                    // Add new user and save
                    _dbContext.IdentifiesAsTypeItems.Add(newItem);
                    _dbContext.SaveChanges();
                }
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1782._message, ErrorMessages._1782._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1783._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1783._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1784._message + e.Message, ErrorMessages._1784._code);
            }
        }

        /// <summary>
        /// Function Name: AddEthnicityItem
        /// 
        /// Purpose: Add a new item to the EthnicityTypeItems database table. 
        /// </summary>
        /// <param name="item">Ethnicity item</param>
        /// <author>Jon Maddocks</author>
        /// <created>3/17/2023</created>
        public void AddEthnicityItem(string item)
		{
            try
            {
                var existingItem = _dbContext.EthnicityTypeItems.FirstOrDefault(x => x.Name.Equals(item));
                if (existingItem == null)
                {
                    var newItem = new EthnicityTypeItem()
                    {
                        Name = item
                    };

                    // Add new user and save
                    _dbContext.EthnicityTypeItems.Add(newItem);
                    _dbContext.SaveChanges();
                }
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1785._message, ErrorMessages._1785._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1786._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1786._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1787._message + e.Message, ErrorMessages._1787._code);
            }
        }

        /// <summary>
        /// Function Name: AddRacialGroupItem
        /// 
        /// Purpose: Add a new item to the RacialGroupTypeItems database table. 
        /// </summary>
        /// <param name="item">Racial Group item</param>
        /// <author>Jon Maddocks</author>
        /// <created>3/17/2023</created>
        public void AddRacialGroupItem(string item)
		{
            try
            {
                var existingItem = _dbContext.RacialGroupTypeItems.FirstOrDefault(x => x.Name.Equals(item));
                if (existingItem == null)
                {
                    var newItem = new RacialGroupTypeItem()
                    {
                        Name = item
                    };

                    // Add new user and save
                    _dbContext.RacialGroupTypeItems.Add(newItem);
                    _dbContext.SaveChanges();
                }
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1788._message, ErrorMessages._1788._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1789._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1789._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1790._message + e.Message, ErrorMessages._1790._code);
            }
        }

        /// <summary>
        /// Function Name: AddReasonSeparatedItem
        /// 
        /// Purpose: Add a new item to the InactiveStatusTypeItems database table. 
        /// </summary>
        /// <param name="item">Reason Separated Name</param>
        /// <author>Tim Johnson</author>
        /// <created>3/29/2023</created>
        public void AddReasonSeparatedItem(string item)
		{
            try
            {
                var existingItem = _dbContext.InactiveStatusTypeItems.FirstOrDefault(x => x.Name.Equals(item));
                if (existingItem == null)
                {
                    var newItem = new InactiveStatusTypeItem()
                    {
                        Name = item
                    };

                    // Add new user and save
                    _dbContext.InactiveStatusTypeItems.Add(newItem);
                    _dbContext.SaveChanges();
                }
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1791._message, ErrorMessages._1791._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1792._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1792._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1793._message + e.Message, ErrorMessages._1793._code);
            }
        }

        /// <summary>
        /// Function Name: AddReasonSeparatedItem
        /// 
        /// Purpose: Add a new item to the RacialGroupTypeItems database table. 
        /// </summary>
        /// <param name="item">Temp info Name and Type</param>
        /// <param name="type">Temp info Checkbox or Date</param>
        /// <author>Tim Johnson</author>
        /// <created>3/29/2023</created>
        public void AddTempInfoItem(string item, int type)
        {
            try
            {
                var existingItem = _dbContext.TempInfoTypeItems.FirstOrDefault(x => x.Name.Equals(item));
                if (existingItem == null)
                {
                    var newItem = new TempInfoTypeItem()
                    {
                        Name = item,
                        TempInfoType = (TempInfoTypes)type
                    };

                    // Add new user and save
                    _dbContext.TempInfoTypeItems.Add(newItem);
                    _dbContext.SaveChanges();
                }
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1794._message, ErrorMessages._1794._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1795._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1795._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1796._message + e.Message, ErrorMessages._1796._code);
            }
        }


        // DELETE CATEGORY ITEMS //

        /// <summary>
        /// Function Name: DeleteGenderItem
        /// 
        /// Purpose: Delete a single item from the GenderTypeItems database table. 
        /// </summary>
        /// <param name="item">Gender item to be deleted</param>
        /// <author>Jon Maddocks</author>
        /// <created>3/17/2023</created>
        public void DeleteGenderItem(GenderNameIdModel item)
		{
            try
            {
                var existingItem = _dbContext.GenderTypeItems.FirstOrDefault(x => x.Name.Equals(item.Name));
                if (existingItem != null)
                {
                    //delete
                    _dbContext.GenderTypeItems.Remove(existingItem);
                    _dbContext.SaveChanges();
                }
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1797._message, ErrorMessages._1797._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1798._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1798._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1799._message + e.Message, ErrorMessages._1799._code);
            }
        }

        /// <summary>
        /// Function Name: DeleteIdentifiesAsItem
        /// 
        /// Purpose: Delete a single item from the IdentifiesAsTypeItems database table. 
        /// </summary>
        /// <param name="item">Identifies As item to be deleted</param>
        /// <author>Jon Maddocks</author>
        /// <created>3/17/2023</created>
        public void DeleteIdentifiesAsItem(IdentifiesAsNameIdModel item)
        {
            try
            {
                var existingItem = _dbContext.IdentifiesAsTypeItems.FirstOrDefault(x => x.Name.Equals(item.Name));
                if (existingItem != null)
                {
                    //delete
                    _dbContext.IdentifiesAsTypeItems.Remove(existingItem);
                    _dbContext.SaveChanges();
                }
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1800._message, ErrorMessages._1800._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1801._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1801._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1802._message + e.Message, ErrorMessages._1802._code);
            }

        }

        /// <summary>
        /// Function Name: DeleteEthnicityItem
        /// 
        /// Purpose: Delete a single item from the EthnicityTypeItems database table. 
        /// </summary>
        /// <param name="item">Ethnicity item to be deleted</param>
        /// <author>Jon Maddocks</author>
        /// <created>3/17/2023</created>
        public void DeleteEthnicityItem(EthnicityNameIdModel item)
        {
            try
            {
                var existingItem = _dbContext.EthnicityTypeItems.FirstOrDefault(x => x.Name.Equals(item.Name));
                if (existingItem != null)
                {
                    //delete
                    _dbContext.EthnicityTypeItems.Remove(existingItem);
                    _dbContext.SaveChanges();
                }
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1803._message, ErrorMessages._1803._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1804._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1804._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1805._message + e.Message, ErrorMessages._1805._code);
            }
        }

        /// <summary>
        /// Function Name: DeleteRacialGroupItem
        /// 
        /// Purpose: Delete a single item from the RacialGroupTypeItems database table. 
        /// </summary>
        /// <param name="item">Racial Group item to be deleted</param>
		/// <author>Jon Maddocks</author>
        /// <created>3/17/2023</created>
        public void DeleteRacialGroupItem(RacialGroupNameIdModel item)
        {
            try
            {
                var existingItem = _dbContext.RacialGroupTypeItems.FirstOrDefault(x => x.Name.Equals(item.Name));
                if (existingItem != null)
                {
                    //delete
                    _dbContext.RacialGroupTypeItems.Remove(existingItem);
                    _dbContext.SaveChanges();
                }
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1806._message, ErrorMessages._1806._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1807._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1807._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1808._message + e.Message, ErrorMessages._1808._code);
            }

        }


        /// <summary>
        /// Function Name: DeleteReasonSeparatedItem
        /// 
        /// Purpose: Delete a single item from the InactiveStatusTypeItem database table. 
        /// </summary>
        /// <param name="item">Inactive Status Type item to be deleted</param>
		/// <author>Tim Johnson</author>
        /// <created>3/29/2023</created>
        public void DeleteReasonSeparatedItem(InactiveStatusTypeItem item)
        {
            try
            {
                var existingItem = _dbContext.InactiveStatusTypeItems.FirstOrDefault(x => x.Name.Equals(item.Name));
                if (existingItem != null)
                {
                    //delete
                    _dbContext.InactiveStatusTypeItems.Remove(existingItem);
                    _dbContext.SaveChanges();
                }
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1809._message, ErrorMessages._1809._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1810._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1810._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1811._message + e.Message, ErrorMessages._1811._code);
            }

        }

        /// <summary>
        /// Function Name: DeleteTempInfoType
        /// 
        /// Purpose: Delete a single item from the TempInfoTypeItems database table. 
        /// </summary>
        /// <param name="item">Inactive Status Type item to be deleted</param>
		/// <author>Tim Johnson</author>
        /// <created>3/29/2023</created>
        public void DeleteTempInfoItem(TempInfoModel item)
        {
            try
            {
                var existingItem = _dbContext.TempInfoTypeItems.FirstOrDefault(x => x.Tuid.Equals(item.Tuid));
                if (existingItem != null)
                {
                    //delete
                    _dbContext.TempInfoTypeItems.Remove(existingItem);
                    _dbContext.SaveChanges();
                }
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1812._message, ErrorMessages._1812._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1813._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1813._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1814._message + e.Message, ErrorMessages._1814._code);
            }

        }

        /// <summary>
        /// Function Name: GetVolunteersWithGender
        /// 
        /// Purpose: Get a list of volunteer names that currently have the gender TUID in their demographics. 
        /// </summary>
        /// <param name="genderTUID">Gender TUID that is being searched.</param>
        /// <returns>Volunteer list with the gender TUID</returns>
        /// <author>Jon Maddocks</author>
        /// <created>3/17/2023</created>
        public IEnumerable<VolunteerNameIdModel> GetVolunteersWithGender(int genderTUID)
		{
            try
            {
                return _dbContext.Volunteers.Where(x => x.GenderTuid == genderTUID).Select(x => new VolunteerNameIdModel
                {
                    FullName = x.FullName
                }).ToList();
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1815._message, ErrorMessages._1815._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1816._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1816._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1817._message + e.Message, ErrorMessages._1817._code);
            }

            return Enumerable.Empty<VolunteerNameIdModel>();
        }

        /// <summary>
        /// Function Name: GetVolunteersWithIdentifiesAs
        /// 
        /// Purpose: Get a list of volunteer names that currently have the identifies as TUID in their demographics. 
        /// </summary>
        /// <param name="identifiesAsTUID">Identifies As TUID that is being searched.</param>
        /// <returns>Volunteer list with the identifies as TUID</returns>
        /// <author>Jon Maddocks</author>
        /// <created>3/17/2023</created>
        public IEnumerable<VolunteerNameIdModel> GetVolunteersWithIdentifiesAs(int identifiesAsTUID)
        {
            try
            {
                return _dbContext.Volunteers.Where(x => x.IdentifiesAsTuid == identifiesAsTUID).Select(x => new VolunteerNameIdModel
                {
                    FullName = x.FullName
                }).ToList();
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1818._message, ErrorMessages._1818._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1819._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1819._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1820._message + e.Message, ErrorMessages._1820._code);
            }

            return Enumerable.Empty<VolunteerNameIdModel>();
        }

        /// <summary>
        /// Function Name: GetVolunteersWithEthnicity
        /// 
        /// Purpose: Get a list of volunteer names that currently have the ethnicity TUID in their demographics. 
        /// </summary>
        /// <param name="ethnicityTUID">Ethnicity TUID that is being searched.</param>
        /// <returns>Volunteer list with the ethnicity TUID</returns>
		/// <author>Jon Maddocks</author>
        /// <created>3/17/2023</created>
        public IEnumerable<VolunteerNameIdModel> GetVolunteersWithEthnicity(int ethnicityTUID)
        {
            try
            {
                return _dbContext.Volunteers.Where(x => x.EthnicityTuid == ethnicityTUID).Select(x => new VolunteerNameIdModel
                {
                    FullName = x.FullName
                }).ToList();
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1821._message, ErrorMessages._1821._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1822._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1822._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1823._message + e.Message, ErrorMessages._1823._code);
            }

            return Enumerable.Empty<VolunteerNameIdModel>();
        }

        /// <summary>
        /// Function Name: GetVolunteersWithRacialGroup
        /// 
        /// Purpose: Get a list of volunteer names that currently have the racial group TUID in their demographics. 
        /// </summary>
        /// <param name="racialGroupTUID">Racial Group TUID that is being searched.</param>
        /// <returns>Volunteer list with the racial group TUID</returns>
        /// <author>Jon Maddocks</author>
        /// <created>3/17/2023</created>
        public IEnumerable<VolunteerNameIdModel> GetVolunteersWithRacialGroup(int racialGroupTUID)
        {
            try
            {
                return _dbContext.Volunteers.Where(x => x.RacialGroupTuid == racialGroupTUID).Select(x => new VolunteerNameIdModel
                {
                    FullName = x.FullName
                }).ToList();
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1824._message, ErrorMessages._1824._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1825._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1825._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1826._message + e.Message, ErrorMessages._1826._code);
            }

            return Enumerable.Empty<VolunteerNameIdModel>();
        }

        /// <summary>
        /// Gets inactive and active volunteer info and stores it in a list of volunteerInfoReportModels.
        /// </summary>
        /// <returns>An enumerable list of VolunteerInfoReportModels</returns>
        /// <author>Tyler Moody</author>
        /// <created>3/24/2023</created>
        public IEnumerable<VolunteerInfoReportModel> GetAllVolunteerInfoReport(int? volunteerTuid)
        {
            IQueryable<Volunteer> query = IncludeVolunteerInfoReportTables();

            query = FilterByVolunteerTuid(volunteerTuid, query);

            return SelectVolunteerInfoReportColumns(query);
        }

        /// <summary>
        /// Gets inactive and active volunteer info and stores it in a list of volunteerInfoReportModels.
        /// </summary>
        /// <returns>An enumerable list of VolunteerInfoReportModels</returns>
        /// <author>Tyler Moody</author>
        /// <created>3/25/2023</created>
        public IEnumerable<VolunteerInfoReportModel> GetAllVolunteerInfoReportByActiveStatus(int? volunteerTuid, bool activeStatus)
        {
            IQueryable<Volunteer> query = IncludeVolunteerInfoReportTables();

            query = FilterByVolunteerTuid(volunteerTuid, query);

            query = FilterByActiveStatus(activeStatus, query);

            return SelectVolunteerInfoReportColumns(query);
        }

        /// <summary>
        /// Get tables joins needed for volunteer info report.
        /// </summary
        /// <author>Tyler Moody</author>
        /// <created>3/25/2023</created>
        /// <returns>Query with table joins.</returns>
        private IQueryable<Volunteer> IncludeVolunteerInfoReportTables()
        {
            try
            {
                return _dbContext.Volunteers
                                .Include(v => v.ReasonsSeparated)
                                    .ThenInclude(rs => rs.InactiveStatusTypeItem)
                                .Include(v => v.Gender)
                                .Include(v => v.IdentifiesAs)
                                .Include(v => v.Ethnicity)
                                .Include(v => v.RacialGroup);
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1827._message, ErrorMessages._1827._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1828._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1828._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1829._message + e.Message, ErrorMessages._1829._code);
            }

            return Enumerable.Empty<Volunteer>().AsQueryable();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="volunteerTuid">Volunteer tuid for filtering</param>
        /// <param name="query">Query to update with where.</param>
        /// <author>Tyler Moody</author>
        /// <created>3/25/2023</created>
        /// <returns>Updated query if volunteer tuid is not null. Otherwise return unchanged.</returns>
        private static IQueryable<Volunteer> FilterByVolunteerTuid(int? volunteerTuid, IQueryable<Volunteer> query)
        {
            if (volunteerTuid != null && volunteerTuid != 0)
            {
                query = query.Where(a => a.Tuid == volunteerTuid);
            }

            return query;
        }

        /// <summary>
        /// Select and map columns for the volunteer info report model list.
        /// </summary>
        /// <param name="query">Query to be updated.</param>
        /// <author>Tyler Moody</author>
        /// <created>3/25/2023</created>
        /// <returns>IEnumerable of VolunteerInfoReportModels.</returns>
        private static IEnumerable<VolunteerInfoReportModel> SelectVolunteerInfoReportColumns(IQueryable<Volunteer> query)
        {
            return query.Where(v => !v.IsDeleted).Select(v => new VolunteerInfoReportModel
            {
                Volunteer = new VolunteerModel
                {
                    Tuid = v.Tuid,
                    FirstName = v.FirstName,
                    LastName = v.LastName
                },
                Demographics = new VolunteerDemographicsModel
                {
                    Status = v.IsActive ? "Active" : "Inactive",
                    DateOfBirth = v.Birthday,
                    Gender = v.Gender.Name,
                    IdentifiesAs = v.IdentifiesAs.Name,
                    Ethnicity = v.Ethnicity.Name,
                    RacialGroup = v.RacialGroup.Name,
                    Veteran = v.IsVeteran ? "Yes" : "No",
                    FamilyOfMilitary = v.IsFamilyOfMilitary ? "Yes" : "No"
                },
                GenderNameAndId = new GenderNameIdModel
                {
                    Tuid = v.GenderTuid,
                    Name = v.Gender.Name
                },
                IdentifiesNameAndId = new IdentifiesAsNameIdModel
                {
                    Tuid = v.IdentifiesAsTuid,
                    Name = v.IdentifiesAs.Name
                },
                EthnicityNameAndId = new EthnicityNameIdModel
                {
                    Tuid = v.EthnicityTuid,
                    Name = v.Ethnicity.Name
                },
                RacialGroupNameAndId = new RacialGroupNameIdModel
                {
                    Tuid = v.RacialGroupTuid,
                    Name = v.RacialGroup.Name
                },
                InactiveStatusNameAndId = v.ReasonsSeparated.FirstOrDefault(r => r.VolunteerTuid == v.Tuid) != null && !v.IsActive ? new InactiveStatusTypesNameIdModel
                {
                    Tuid = v.ReasonsSeparated.FirstOrDefault(r => r.VolunteerTuid == v.Tuid).InactiveStatusTypeItemTuid,
                    Name = v.ReasonsSeparated.FirstOrDefault(r => r.VolunteerTuid == v.Tuid).InactiveStatusTypeItem.Name,
                } : new InactiveStatusTypesNameIdModel(),
                ReasonSeparatedTuid = v.ReasonsSeparated.FirstOrDefault(r => r.VolunteerTuid == v.Tuid).Tuid,
                EndDate = v.SeparatedDate,
                StartDate = v.StartDate,
            }).ToList();
        }


        /// <summary>
        /// Function Name: GetVolunteersWithRacialGroup
        /// 
        /// Purpose: Get a list of volunteer names that currently have the racial group TUID in their demographics. 
        /// </summary>
        /// <param name="reasonSeparatedTUID">Reason Separated TUID that is being searched.</param>
        /// <returns>Volunteer list with the racial group TUID</returns>
        /// <author>Jon Maddocks</author>
        /// <created>3/17/2023</created>
        public IEnumerable<VolunteerNameIdModel> GetVolunteersWithReasonSeparated(int reasonSeparatedTUID)
        {
            try
            {
                return _dbContext.ReasonsSeparated.Where(x => x.InactiveStatusTypeItemTuid == reasonSeparatedTUID).Select(x => new VolunteerNameIdModel
                {
                }).ToList();
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1830._message, ErrorMessages._1830._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1831._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1831._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1832._message + e.Message, ErrorMessages._1832._code);
            }

            return Enumerable.Empty<VolunteerNameIdModel>();
        }

        /// <summary>
        /// Function Name: GetVolunteersWithTempInfoType
        /// 
        /// Purpose: Get a list of volunteer names that currently have the racial group TUID in their demographics. 
        /// </summary>
        /// <param name="tempInfoTypeTuidTUID">Temp Info Type TUID that is being searched.</param>
        /// <returns>Volunteer list with the temp info type TUID</returns>
        /// <author>Tim Johnson</author>
        /// <created>3/29/2023</created>
        public IEnumerable<VolunteerNameIdModel> GetVolunteerWithTempInfoType(int tempInfoTypeTuid)
        {
            try
            {
                return _dbContext.TempInfoEntries.Where(x => x.TempInfoTypeItemTuid == tempInfoTypeTuid).Select(x => new VolunteerNameIdModel
                {
                }).ToList();
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1833._message, ErrorMessages._1833._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1834._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1834._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1835._message + e.Message, ErrorMessages._1835._code);
            }

            return Enumerable.Empty<VolunteerNameIdModel>();
        }

        public void RestoreVolunteer(int tuid)
        {
            try
            {
                Volunteer? volunteer = _dbContext.Volunteers.FirstOrDefault(x => x.Tuid == tuid);
                volunteer.IsDeleted = false;
                volunteer.ReasonsSeparated = null;
                _dbContext.SaveChanges();
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1836._message, ErrorMessages._1836._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1837._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1837._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1838._message + e.Message, ErrorMessages._1838._code);
            }
        }
    
        /// <summary>
        /// Filter query by active status.
        /// </summary>
        /// <param name="activeStatus">Status of volunteer</param>
        /// <param name="query">Query to be updated.</param>
        /// <author>Tyler Moody</author>
        /// <created>3/25/2023</created>
        /// <returns>Query that has been updated with status filter.</returns>
        private static IQueryable<Volunteer> FilterByActiveStatus(bool activeStatus, IQueryable<Volunteer> query)
        {
            if (activeStatus)
            {
                query = query.Where(v => v.SeparatedDate == null);
            }
            else
            {
                query = query.Where(v => v.SeparatedDate != null);
            }

            return query;
        }

        /// <summary>
        /// Update volunteer using VolunteerInfoReportModel
        /// </summary>
        /// <param name="volunteerInfo">VolunteerInfoReportModel</param>
        /// <author>Tyler Moody</author>
        /// <created>3/26/2023</created>
        /// <returns>True if volunteer was updated.</returns>
        public bool UpdateVolunteerInfo(VolunteerInfoReportModel volunteerInfo)
        {
            Volunteer volunteerEntity = GetVolunteerByTuid(volunteerInfo.Volunteer.Tuid);

            MapVolunteerInfoToEntity(volunteerInfo, volunteerEntity);

            bool databaseWasUpdated = SaveChangesToDatabase();

            return databaseWasUpdated;
        }

        /// <summary>
        /// Map VolunteerInfoReportModel to volunteer.
        /// </summary>
        /// <param name="volunteerInfo">VolunteerInfoReportModel</param>
        /// <param name="volunteer">Volunteer Entity.</param>
        /// <author>Tyler Moody</author>
        /// <created>3/26/2023</created>
        private void MapVolunteerInfoToEntity(VolunteerInfoReportModel volunteerInfo, Volunteer volunteer)
        {
            volunteer.FirstName = volunteerInfo.Volunteer.FirstName;
            volunteer.LastName = volunteerInfo.Volunteer.LastName;
            volunteer.GenderTuid = volunteerInfo.GenderNameAndId.Tuid;
            volunteer.IdentifiesAsTuid = volunteerInfo.IdentifiesNameAndId.Tuid;
            volunteer.EthnicityTuid = volunteerInfo.EthnicityNameAndId.Tuid;
            volunteer.RacialGroupTuid = volunteerInfo.RacialGroupNameAndId.Tuid;
            volunteer.Birthday = (DateTime)volunteerInfo.Demographics.DateOfBirth;
            volunteer.StartDate = (DateTime)volunteerInfo.StartDate;

            if (volunteerInfo.InactiveStatusNameAndId != null && volunteerInfo.EndDate != null)
            {
                UpdateReasonSeparated(volunteerInfo, volunteer);
            }
            else
            {
                volunteer.SeparatedDate = null;
            }

            volunteer.IsVeteran = volunteerInfo.Demographics.Veteran == "Yes";
            volunteer.IsFamilyOfMilitary = volunteerInfo.Demographics.FamilyOfMilitary == "Yes";
        }

        /// <summary>
        /// Updates the reason separated for a volunteer.
        /// </summary>
        /// <param name="volunteerInfo">Volunteer with updated info.</param>
        /// <param name="volunteer">Entity object to be updated.</param>
        /// <author>Tyler Moody</author>
        /// <created>3/28/2023</created>
        private void UpdateReasonSeparated(VolunteerInfoReportModel volunteerInfo, Volunteer volunteer)
        {
            try
            {
                ReasonSeparated reasonSeparated = _dbContext.ReasonsSeparated.Where(r => r.VolunteerTuid == volunteer.Tuid).FirstOrDefault();

                if (reasonSeparated != null)
                {
                    reasonSeparated.InactiveStatusTypeItemTuid = volunteerInfo.InactiveStatusNameAndId.Tuid;
                }
                else
                {
                    reasonSeparated = new ReasonSeparated()
                    {
                        Volunteer = volunteer,
                        InactiveStatusTypeItemTuid = volunteerInfo.InactiveStatusNameAndId.Tuid
                    };
                    _dbContext.ReasonsSeparated.Add(reasonSeparated);
                }

                volunteer.SeparatedDate = (DateTime)volunteerInfo.EndDate;

            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1839._message, ErrorMessages._1839._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1840._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1840._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1841._message + e.Message, ErrorMessages._1841._code);
            }
        }

        /// <summary>
        /// Saves changes in the context to the database and returns true if records were changed.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>03/26/2023</created>
        /// <exception cref="Exception">Error when updating the database.</exception>
        /// <returns>True if successfully updated. False if not. </returns>
        private bool SaveChangesToDatabase()
        {
            int recordsChanged;

            try
            {
                recordsChanged = _dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Error when save to database.", e);
            }

            return recordsChanged > 0;
        }

        /// <summary>
        /// Get all inactive status types.
        /// </summary>
        /// <author>Tyler Moody</author>
        /// <created>03/27/2023</created>
        /// <returns>IEnumerable of InactiveStatusTypeNameIdModels.</returns>
        public IEnumerable<InactiveStatusTypesNameIdModel> GetInactiveStatusTypes()
        {
            try
            {
                return _dbContext.InactiveStatusTypeItems.Select(type => new InactiveStatusTypesNameIdModel { Tuid = type.Tuid, Name = type.Name }).ToList();
            }
            catch (SqlException e)
            {
                if (e.ErrorCode == -2146232060)
                {
                    OnDatabaseError(ErrorMessages._1842._message, ErrorMessages._1842._code);
                }
                else
                {
                    OnDatabaseError(ErrorMessages._1843._message + " " + e.Message + " " + e.ErrorCode.ToString(), ErrorMessages._1843._code);
                }
            }
            catch (Exception e)
            {
                OnDatabaseError(ErrorMessages._1844._message + e.Message, ErrorMessages._1844._code);
            }

            return Enumerable.Empty<InactiveStatusTypesNameIdModel>();
        }

        public int? GetPersistantSelectedVolunteer()
        {
            return (int?)Application.Current.Properties["VolunteerTuid"];
        }

        public void SetPersistantSelectedVolunteer(int selectedVolunteerIndex)
        {
            Application.Current.Properties["VolunteerTuid"] = selectedVolunteerIndex;
        }

    }
}
