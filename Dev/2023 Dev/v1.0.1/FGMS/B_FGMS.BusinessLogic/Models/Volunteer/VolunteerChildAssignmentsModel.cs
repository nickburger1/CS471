using A_FGMS.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B_FGMS.BusinessLogic.Models.Volunteer
{
    public class VolunteerChildAssignmentsModel
    {
        public int SchoolTuid { get; internal set; }
        public SchoolModel? School { get; set; }
        public int? ClassroomSize { get; set; }
        public int? KidsAssigned { get; set; }
        public string? GradeLevel { get; set; }
        public int? Age0To5 { get; set; }
        public int? Age6To12 { get; set; }   
    }
}
