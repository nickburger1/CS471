using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B_FGMS.BusinessLogic.Models.Volunteer
{
    public class OneTimeChecksModel
    {
        public bool FilePhoto { get; set; }
        public bool ServiceDescription { get; set; }
        public bool OrientTraining { get; set; }
        public DateTime? ConfidenceSOU { get; set; }
        public DateTime? ServiceStartDate { get; set; }
        public bool NSCHCCheckForm { get; set; }
        public bool BackgroundCheck { get; set; }
        public bool IDCopy { get; set; }
        public DateTime? NSOPW { get; set; }
        public DateTime? IChat { get; set; }
        public DateTime? TrueScreen { get; set; }
        public DateTime? AliasFingerPrint { get; set; }
        public DateTime? FieldPrintCleared { get; set; }
        public DateTime? DHS { get; set; }
        public DateTime? TBShot { get; set; }
        public string? VolunteerName { get; set; }
    }
}
