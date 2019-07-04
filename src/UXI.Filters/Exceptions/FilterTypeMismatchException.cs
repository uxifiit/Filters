using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UXI.Filters.Exceptions
{
    public class FilterTypeMismatchException : Exception
    {
        public FilterTypeMismatchException(string message) : base(message)
        {
        }
    }
}
