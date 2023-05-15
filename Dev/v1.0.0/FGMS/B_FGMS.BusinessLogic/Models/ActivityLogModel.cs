using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <FileName> ActivityLogModel.cs  </FileName>
/// <PartOfProject> CS471 Senior Capstone Project / FGMS </PartOfProject>
/// <DateCreated> 2/14/2023 </DateCreated>
/// <AdditionalContributors> CS471 WI23 Development Team </AdditionalContributors>
/// <LastModified> 2/14/2023 </LastModified>
/// <LastModifiedBy> Tyler Moody </LastModifiedBy>
/// <summary>
/// The Purpose of this file is to provide the model for an activity log object.
/// </summary>
/// <author> Tyler Moody </author>

namespace B_FGMS.BusinessLogic.Models
{
    public class ActivityLogModel
    {
        public int? Tuid { get; set; }
        public VolunteerModel Volunteer { get; set; }
        public int VolunteerTuid { get; set; }
        public DateTime Date { get; set; }
        public string Initial { get; set; }
        public string Incident { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <author>Nathan VanSnepson</author>
        /// <created>04/05/2023</created>
        public ActivityLogModel() { }

        /// <summary>
        /// Parameterized constructor.
        /// </summary>
        /// <param name="volunteer">Volunteer that log belongs to.</param>
        /// <param name="date">Date of the incident.</param>
        /// <param name="initial">Whether or not initals were obtained. Y/N</param>
        /// <param name="incident">Description of incident.</param>
        /// <author>Tyler Moody</author>
        /// <created>02/14/2023</created>
        public ActivityLogModel(VolunteerModel volunteer, DateTime date, string initial, string incident)
        {
            Volunteer = volunteer;
            VolunteerTuid = volunteer.Tuid;
            Date = date;
            Initial = initial;
            Incident = incident;
        }

        /// <summary>
        /// Parameterized constructor with tuid.
        /// </summary>
        /// <param name="volunteer">Volunteer that log belongs to.</param>
        /// <param name="date">Date of the incident.</param>
        /// <param name="initial">Whether or not initals were obtained. Y/N</param>
        /// <param name="incident">Description of incident.</param>
        /// <author>Tyler Moody</author>
        /// <created>02/14/2023</created>
        public ActivityLogModel(int? tuid, VolunteerModel volunteer, DateTime date, string initial, string incident)
        {
            Tuid = tuid;
            Volunteer = volunteer;
            VolunteerTuid = volunteer.Tuid;
            Date = date;
            Initial = initial;
            Incident = incident;
        }
    }
}
