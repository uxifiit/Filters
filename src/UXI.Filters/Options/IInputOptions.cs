using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UXI.Serialization;

namespace UXI.Filters.Options
{
    public interface IInputOptions
    {
        string InputFilePath { get; set; }

        FileFormat InputFileFormat { get; set; }

        FileFormat DefaultInputFileFormat { get; }
    }
}
