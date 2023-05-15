using HandyControl.Controls;
using HandyControl.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_FGMS.UI.Helpers
{
    public static class GrowlHelpers
    {
        public static void Success(string message)
        {
            Growl.Success(new GrowlInfo
            {
                Message = message,
                ShowDateTime = false,
                StaysOpen = false,
                WaitTime = 3
            });
        }
        public static void Info(string message)
        {
            Growl.Info(new GrowlInfo
            {
                Message = message,
                ShowDateTime = false,
                StaysOpen = false,
                WaitTime = 3
            });
        }

        public static void Warning(string message)
        {
            Growl.Warning(new GrowlInfo
            {
                Message = message,
                ShowDateTime = false,
                StaysOpen = false,
                WaitTime = 3
            });
        }

        public static void Error(string message)
        {
            Growl.Error(new GrowlInfo
            {
                Message = message,
                ShowDateTime = false,
                StaysOpen = false,
                WaitTime = 3
            });
        }
    }
}
