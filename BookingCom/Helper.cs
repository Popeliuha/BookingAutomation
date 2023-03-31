using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Framework
{
    public static class Helper
    {
        public static void Wait(int seconds)
        {
            Thread.Sleep(seconds * 1000);
        }
    }
}
