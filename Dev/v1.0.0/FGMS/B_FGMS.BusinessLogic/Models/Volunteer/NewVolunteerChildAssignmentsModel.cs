using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B_FGMS.BusinessLogic.Models.Volunteer
{
    public class NewVolunteerChildAssignmentsModel
    {        
        public string? Identifier { get; set; }        
        public ClassroomsModel? Classroom { get; set; }
        public StudentModel? Student { get; set; }
        public List<ConditionItemModel>? StudentConditions { get; set; }
        public List<StudentNeedItemModel>? StudentNeeds { get; set; }        
        public string? DesiredOutcome { get; set; }
    }
}
