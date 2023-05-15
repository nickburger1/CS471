using B_FGMS.BusinessLogic.Models.Volunteer;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B_FGMS.BusinessLogic.Models
{
    /// <summary>
    ///     Model used for the Classrooms tab in the Volunteers section
    /// </summary>
    public class ClassroomsModel
    {
        public int? AssignmentTuid { get; set; }
        public int? ClassroomTuid { get; set; }
        public VolunteerNameIdModel? Volunteer { get; set; }
        public SchoolModel? School { get; set; }
        public string? ClassroomNumber { get; set; }
        public int? ClassroomSize { get; set; }
        public string? GradeLevel { get; set; }
        public string? TeacherName { get; set; }
        public string? JsonSchedule { get; set; }
        public VolunteerClassroomSchedule? Schedule { get; set; }         
        public bool? IsDeleted { get; set; }
        public string? DisplayName { get { string x = ClassroomNumber + " - " + TeacherName; return x; } }

    }
}
