using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B_FGMS.BusinessLogic.Models.Reports
{
    public class DemographicReportModel
    {
        public string? VolunteerName { get; set; }
        public DateTime? LastUpdated { get; set; }
        public DateTime DOB { get; set; }
        public string? Gender { get; set; }
        public int? Age { get; set; }
        public string? IdentifiesAs { get; set; }
        public string? Ethnicity { get; set; }
        public string? RacialGroup { get; set; }
        public bool IsVeteran { get; set; }
        public bool FamilyOfMilitary { get; set; }
        public bool IsActive { get; set; }
        public DateTime? SeparatedDate { get; set; }
    }
}
