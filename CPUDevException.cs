using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPUFramework
{
    public class CPUDevException : Exception
    {
        //why are you typing "base"?
        public CPUDevException(string? message, Exception? innerexception): base (message, innerexception)
        {

        }
    }
}
