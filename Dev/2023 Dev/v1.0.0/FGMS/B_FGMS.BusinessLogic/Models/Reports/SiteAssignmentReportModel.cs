using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B_FGMS.BusinessLogic.Models.Reports
{
    public class SiteAssignmentReportModel
    {
        public string? VolunteerName { get; set; }
        public string? StudentIdentifier { get; set; }
        public string? SchoolName { get; set; }
        public string? Teacher { get; set; }
        public string? GradeLevel { get; set; }
        public bool? Age0To5 { get; set; }
        public bool? Age5To12 { get; set; }
        public string? Condition { get; set; }
        public string? Needs { get; set; }
        public string? DesiredOutcome { get; set; }
        public string? HoursPerWeek { get; set; }
        public string? Classroom { get; set; }
        public string? Days { get; set; }
        public int ClassroomSize { get; set; }
        public int VolunteerTuid { get; set; }
        public int AssignmentTuid { get; set; }
        public int StudentTuid { get; set; }
        public DateTime? Date { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? SeperatedDate { get; set; }
    }
}
