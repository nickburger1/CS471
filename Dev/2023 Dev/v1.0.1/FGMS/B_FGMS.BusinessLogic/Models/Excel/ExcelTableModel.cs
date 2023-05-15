using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <FileName> ExcelTableModel.cs  </FileName>
/// <PartOfProject> CS471 Senior Capstone Project / FGMS </PartOfProject>
/// <DateCreated> 03/18/2023 </DateCreated>
/// <AdditionalContributors> CS471 WI23 Development Team </AdditionalContributors>
/// <LastModified> 03/18/2023 </LastModified>
/// <LastModifiedBy> Tyler Moody </LastModifiedBy>
/// <summary>
/// The Purpose of this file is to represent an Excel table.
/// </summary>
/// <author> Tyler Moody </author>
namespace B_FGMS.BusinessLogic.Models.Excel
{
    public class ExcelTableModel
    {
        public string Title { get; set; }
        public List<string> Headers { get; set; }
        public List<object> Rows { get; set; }


    }
}
