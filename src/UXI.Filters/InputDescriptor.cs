using System;
using System.IO;
using UXI.Serialization;

namespace UXI.Filters
{
    public class InputDescriptor
    {
        public static readonly InputDescriptor Null = new InputDescriptor(TextReader.Null, FileFormat.Default, null);


        public InputDescriptor(TextReader reader, FileFormat format, Type dataType)
        {
            Reader = reader;
            Format = format;
            DataType = dataType;
        }


        public Type DataType { get; }


        public FileFormat Format { get; }


        public TextReader Reader { get; }
    }
}
