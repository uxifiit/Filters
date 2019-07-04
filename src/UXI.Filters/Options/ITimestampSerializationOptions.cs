using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UXI.Filters.Options
{
    public interface ITimestampSerializationOptions
    {
        string TimestampFormat { get; set; }
    }
}
