using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B_FGMS.BusinessLogic.Models
{
    public class ReportPresetModel
    {
        public int Tuid { get; set; }
        public string? Name { get; set; }
        public List<TreeNode>? Preset { get; set; }
        public string? SortBy { get; set; }
        public bool? Current { get; set; }
        public bool? Former { get; set; }
        public bool? Active { get; set; }
        public bool? Inactive { get; set; }
        public DateTime? LastUpdated { get; set; }
    }
}
