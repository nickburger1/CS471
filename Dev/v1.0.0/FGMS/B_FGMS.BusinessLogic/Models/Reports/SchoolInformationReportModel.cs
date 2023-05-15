using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B_FGMS.BusinessLogic.Models.Reports
{
    public class SchoolInformationReportModel
    {
        public string? SchoolName { get; set; }
        public string? Status { get; set; }
        public string? Address { get; set; }
        public string? Principal { get; set; }
        public string? Secratary { get; set; }
        public string? Phone { get; set; }
        public string? Days { get; set; }
        public string? Hours { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }

}
