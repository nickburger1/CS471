using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B_FGMS.BusinessLogic.Models.Volunteer
{
    public class VolunteerChildAssignmentDataGridModel
    {
        public int StudentTuid { get; internal set; }
        public string? Identifier { get; set; }
        public string? Condition { get; set; }
        public string? ConditionDescription { get; set; }
        public string? StudentNeeds { get; set; }
        public string? StudentNeedsDescription { get; set; }
        public string? DesiredOutcome { get; set; }
        public ClassroomsModel? Classroom { get; set; }
    }
}
