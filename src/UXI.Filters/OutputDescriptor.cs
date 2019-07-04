using System;
using System.IO;
using UXI.Serialization;

namespace UXI.Filters
{
    public class OutputDescriptor
    {
        public static readonly OutputDescriptor Null = new OutputDescriptor(TextWriter.Null, FileFormat.Default, null);


        public OutputDescriptor(TextWriter writer, FileFormat format, Type dataType)
        {
            Writer = writer;
            Format = format;
            DataType = dataType;
        }


        public Type DataType { get; }


        public FileFormat Format { get; }


        public TextWriter Writer { get; }
    }
}
