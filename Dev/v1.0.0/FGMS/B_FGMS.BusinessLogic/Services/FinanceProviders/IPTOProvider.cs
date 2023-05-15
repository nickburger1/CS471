using A_FGMS.DataLayer.Entities;
using B_FGMS.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B_FGMS.BusinessLogic.Services.FinanceProviders
{
    public interface IPTOProvider
    {
        public event EventHandler<Events.ErrorEventArgs> DatabaseError;
        public IEnumerable<clsPTOModel> getASpecificMonthOfPtoInfomation(int year, int month);


        public IEnumerable<int> getTotalYears();

        public void updatePTOInfomation(List<clsPTOModel> model, int year, int month);

        public decimal getYdtPTOStipendsPaid(int year,int month);





    }        
}
