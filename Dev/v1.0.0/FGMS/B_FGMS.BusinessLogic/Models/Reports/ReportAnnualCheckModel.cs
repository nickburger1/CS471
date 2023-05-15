using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B_FGMS.BusinessLogic.Models
{
    public class ReportAnnualCheckModel
    {
        public DateTime? SchedulePhotoRelease { get; set; }
        public DateTime? EmergancyBeneficiaryForm { get; set; }
        public DateTime? HippaRelease { get; set; }
        public DateTime? Physical { get; set; }
        public DateTime? AnnualIncomeCarInsurance { get; set; }
        public string? VolunteerFullName { get; set; }

    }
}
