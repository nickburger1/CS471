using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B_FGMS.BusinessLogic.Models.Reports
{
    public class ReportVolunteerGeneralInformationModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Status { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? AltPhone { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? TempInfo { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int Tuid { get; set; }
    }
}
