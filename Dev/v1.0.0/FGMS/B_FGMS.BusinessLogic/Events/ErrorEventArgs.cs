using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B_FGMS.BusinessLogic.Events
{
    public class ErrorEventArgs : EventArgs
    {
        public string ErrorMessage { get; private set; }
        public string ErrorCode { get; private set; }

        public ErrorEventArgs(string errorMessage, string errorCode)
        {
            ErrorMessage = errorMessage;
            ErrorCode = errorCode;
        }
    }
}
