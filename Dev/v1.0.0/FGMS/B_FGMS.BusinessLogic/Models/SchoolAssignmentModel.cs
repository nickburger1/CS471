using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B_FGMS.BusinessLogic.Models
{
    public class SchoolAssignmentModel
    {
        public string? SchoolName { get; set; }
        public int SchoolTuid { get; set; }
        public string? ClassroomNumber { get; set; }
        public int ClassroomTuid { get; set; }
        public string?  TeacherName { get; set; }
        public string? PrincipalName { get; set; }
        public string? VolunteerName { get; set; }
        public string? ClassroomGradeLevel { get; set; }

    }
}
