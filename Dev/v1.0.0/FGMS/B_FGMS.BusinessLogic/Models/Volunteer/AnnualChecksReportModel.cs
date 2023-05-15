using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 ************************************************************************************************************************
 *                                      File Name : AnnualChecksReportModel.cs                                          *
 *                                      Part of Project : CS471 Senior Capstone Project / FGMS                          *
 ************************************************************************************************************************
 *                                      Created By : Timothy Johnson                                                    *
 *                                      Date Created : 3/20/2023                                                        *
 *                                      Additional Contributors : CS471 WI23 Development Team                           *
 *                                      Last Modified : 3/20/2023                                                       *
 *                                      Last Modified By : Timothy Johnson                                              *
 ************************************************************************************************************************
 * File Purpose : The Purpose of this file is to create a Model for the Reports->AnnualChecks                           *
 * It will hold everything you need to populate dtgPTO in Reports->AnnualChecks                                         *
 ************************************************************************************************************************
 **/
namespace B_FGMS.BusinessLogic.Models.Volunteer
{
    public class AnnualChecksReportModel
    {
        public string? Name { get; set; }
        public DateTime? SchedulePhotoRelease { get; set; }
        public DateTime? EmergancyBeneficiaryForm { get; set; }
        public DateTime? HippaRelease { get; set; }
        public DateTime? Physical { get; set; }
        public DateTime? AnnualIncomeCarInsurance { get; set; }
        public DateTime? CovidSOU { get; set; }
        public DateTime? ConfidSOU { get; set; }
        public DateTime? ServiceStartDate { get; set; }
        public DateTime? NSOPW { get; set; }
        public DateTime? IChat { get; set; }
        public DateTime? AliasFPrint { get; set; }
        public DateTime? FieldPrintCleared { get; set; }
        public DateTime? DHS { get; set; }
        public DateTime? TBShot { get; set; }
        public DateTime? TrueScreen { get; set; }
        public DateTime? SeparatedTime{ get; set; }
        public bool? FilePhoto { get; set; }
        public bool? ServiceDescription { get; set; }
        public bool? OrientationTraining { get; set; }
        public bool? BackgroundCheck { get; set; }
        public bool? IDCopy { get; set; }
        public bool? NSCHC { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }

    }
}