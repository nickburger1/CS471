using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <FileName> ExcelFileModel.cs  </FileName>
/// <PartOfProject> CS471 Senior Capstone Project / FGMS </PartOfProject>
/// <DateCreated> 03/18/2023 </DateCreated>
/// <AdditionalContributors> CS471 WI23 Development Team </AdditionalContributors>
/// <LastModified> 03/18/2023 </LastModified>
/// <LastModifiedBy> Tyler Moody </LastModifiedBy>
/// <summary>
/// The Purpose of this file is to represent an Excel file.
/// </summary>
/// <author> Tyler Moody </author>
namespace B_FGMS.BusinessLogic.Models.Excel
{
    public class ExcelFileModel
    {
        public string FileName { get; set; }
        public List<ExcelSheetModel> Sheets { get; set; }
    }
}
