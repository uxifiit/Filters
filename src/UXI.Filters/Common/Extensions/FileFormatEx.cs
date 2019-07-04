using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UXI.Serialization;

namespace UXI.Filters.Common.Extensions
{
    public static class FileFormatEx
    {
        public static FileFormat UseOrDefault(this FileFormat format, FileFormat defaultFormat)
        {
            return format != FileFormat.Default
                 ? format
                 : defaultFormat;
        }


        public static FileFormat UseOrDefault(this FileFormat format, Func<FileFormat> defaultFormatFunc)
        {
            return format != FileFormat.Default
                 ? format
                 : defaultFormatFunc.Invoke();
        }
    }
}
