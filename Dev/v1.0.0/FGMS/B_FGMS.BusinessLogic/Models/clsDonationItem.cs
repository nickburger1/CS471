using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B_FGMS.BusinessLogic
{
    /**
    ************************************************************************************************************************
    *                                      File Name : clsDonationItem.cs                                                  *
    *                                      Part of Project : CS471 Senior Capstone Project / FGMS                          *
    ************************************************************************************************************************
    *                                      Created By : Andrew Loesel                                                      *
    *                                      Date Created : 1/22/23                                                          *
    ************************************************************************************************************************
    * File Purpose : This file contains the modeling of a donation item that will be displayed on the financail page,      *
    *                General tab. This model contains three fields and a single parameterized constructor.                 *
    ************************************************************************************************************************
    * Modification History:                                                                                                *
    ************************************************************************************************************************
    **/
    public class clsDonationItem
    {
        public string strDonorName;
        public DateOnly? donationDate;
        public float fltDonationValue;

       public clsDonationItem(string strDonorName, DateOnly donationDate, float fltDonationValue)
        {
            this.strDonorName = strDonorName;
            this.donationDate = donationDate;
            this.fltDonationValue = fltDonationValue;
        }
    }
}
