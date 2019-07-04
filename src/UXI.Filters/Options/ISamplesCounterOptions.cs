using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UXI.Serialization;

namespace UXI.Filters.Options
{
    public interface ISamplesCounterOptions
    {
        bool IsSamplesCounterEnabled { get; }

        string SamplesCounterOutputFilePath { get; set; }

        FileFormat SamplesCounterOutputFileFormat { get; set; }

        FileFormat DefaultSamplesCounterOutputFileFormat { get; }
    }
}
