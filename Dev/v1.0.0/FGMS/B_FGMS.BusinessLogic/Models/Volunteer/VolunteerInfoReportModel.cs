using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <FileName> VolunteerInfoReportModel.cs  </FileName>
/// <PartOfProject> CS471 Senior Capstone Project / FGMS </PartOfProject>
/// <DateCreated> 3/24/2023 </DateCreated>
/// <AdditionalContributors> CS471 WI23 Development Team </AdditionalContributors>
/// <LastModified> 3/24/2023 </LastModified>
/// <LastModifiedBy> Tyler Moody </LastModifiedBy>
/// <summary>
/// The Purpose of this file represent the data in a volunteer info report object.
/// </summary>
/// <author> Tyler Moody </author>

namespace B_FGMS.BusinessLogic.Models.Volunteer
{
    public class VolunteerInfoReportModel
    {
        public VolunteerModel? Volunteer { get; set; }
        public VolunteerDemographicsModel? Demographics { get; set; }
        public GenderNameIdModel GenderNameAndId { get; set; }
        public IdentifiesAsNameIdModel IdentifiesNameAndId { get; set; }
        public EthnicityNameIdModel EthnicityNameAndId { get; set; }
        public RacialGroupNameIdModel RacialGroupNameAndId { get; set; }
        public InactiveStatusTypesNameIdModel? InactiveStatusNameAndId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? ReasonSeparatedTuid { get; set; }

        public VolunteerInfoReportModel() { }
    }
}
