using A_FGMS.DataLayer.Entities;
using B_FGMS.BusinessLogic.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B_FGMS.BusinessLogic.Services.ReportProviders
{
    public interface IReportPresetProvider
    {
        public event EventHandler<Events.ErrorEventArgs> DatabaseError;
        IEnumerable<ReportPresetModel> GetAllReportPresets();  
        ReportPresetModel? GetReportPreset(int Tuid);
        void UpdateReportPreset(ReportPresetModel reportPresetModel);
        void CreateReportPreset(ReportPresetModel reportPresetModel);
        void DeleteReportPreset(int Tuid);
        public string? MatchPresetOnPreset(List<TreeNode> preset);
        public bool MatchPresetOnName(string strName);
    }
}
