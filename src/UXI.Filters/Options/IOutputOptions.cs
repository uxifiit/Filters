using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UXI.Serialization;

namespace UXI.Filters.Options
{
    public interface IOutputOptions
    {
        string OutputFilePath { get; set; }

        FileFormat OutputFileFormat { get; set; }

        FileFormat DefaultOutputFileFormat { get; }
    }
}
