using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UXI.Filters.Common.Extensions;
using UXI.Serialization;

namespace UXI.Filters.Common
{
    public static class FileHelper
    {
        public static TextReader OpenFileReader(string targetPath, bool throwIfNotExists = true)
        {
            if (String.IsNullOrWhiteSpace(targetPath) == false)
            {
                if (File.Exists(targetPath))
                {
                    var file = new FileStream(targetPath, FileMode.Open, FileAccess.Read);

                    return new StreamReader(file);
                }
                else if (throwIfNotExists)
                {
                    throw new FileNotFoundException($"Input file not found in: {targetPath}");
                }
            }

            return null;
        }


        public static TextWriter OpenFileWriter(string targetPath)
        {
            if (String.IsNullOrWhiteSpace(targetPath) == false)
            {
                var fileStream = new FileStream(targetPath, FileMode.Create, FileAccess.Write);

                return new StreamWriter(fileStream);
            }

            return null;
        }


        public static FileFormat ResolveFormatFromPath(string path)
        {
            string extension = Path.GetExtension(path);
            if (String.IsNullOrWhiteSpace(extension) == false)
            {
                switch (extension.ToLower())
                {
                    case ".json":
                        return FileFormat.JSON;
                    case ".csv":
                        return FileFormat.CSV;
                }
            }

            return FileFormat.Default;
        }


        public static FileFormat ResolveFormat(string path, FileFormat requestedFormat, FileFormat defaultFormat)
        {
            return requestedFormat.UseOrDefault(() => ResolveFormatFromPath(path))
                                  .UseOrDefault(defaultFormat);
        }


        public static OutputDescriptor DescribeOutput(string filePath, FileFormat format, FileFormat defaultFormat, Type dataType, TextWriter defaultWriter)
        {
            var outputWriter = FileHelper.OpenFileWriter(filePath)
                               ?? defaultWriter;
            var outputFormat = FileHelper.ResolveFormat(filePath, format, defaultFormat);

            return new OutputDescriptor(outputWriter, outputFormat, dataType);
        }


        public static InputDescriptor DescribeInput(string filePath, FileFormat format, FileFormat defaultFormat, Type dataType, TextReader defaultReader)
        {
            var inputReader = FileHelper.OpenFileReader(filePath)
                              ?? defaultReader;
            var inputFormat = FileHelper.ResolveFormat(filePath, format, defaultFormat);
                             
            return new InputDescriptor(inputReader, inputFormat, dataType);
        }
    }
}
