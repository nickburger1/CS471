using A_FGMS.DataLayer.Entities;
using B_FGMS.BusinessLogic.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B_FGMS.BusinessLogic.Services.FinanceProviders
{
    public interface IPTOStipendRates
    {
        public event EventHandler<Events.ErrorEventArgs> DatabaseError;
        public clsPTOStipendRatesModel getStipendAndPTORateForAGivenMonth(int year, int month);
        public decimal[] getTotalGrantStipend(int year, int month);

        public void PushPTOStipend(PTOStipendRate newRates);

        public void AddTotalGrantStipend(GrantStipend grant);

    }
}
