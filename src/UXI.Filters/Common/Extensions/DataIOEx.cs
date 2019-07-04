using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UXI.Serialization;
using UXI.Serialization.Reactive;

namespace UXI.Filters.Common.Extensions
{
    public static class DataIOEx
    {
        public static IEnumerable<object> ReadInput(this DataIO io, InputDescriptor input, object settings = null)
        {
            return io.ReadInput(input.Reader, input.Format, input.DataType, settings);
        }


        public static IEnumerable<T> ReadInput<T>(this DataIO io, InputDescriptor input, object settings = null)
        {
            return io.ReadInput<T>(input.Reader, input.Format, input.DataType, settings);
        }


        public static void WriteOutput(this DataIO io, IEnumerable<object> data, OutputDescriptor output, object settings = null)
        {
            io.WriteOutput(data, output.Writer, output.Format, output.DataType, settings);
        }


        public static IObservable<object> ReadInputAsObservable(this DataIO io, InputDescriptor input, object settings = null)
        {
            return io.ReadInputAsObservable(input.Reader, input.Format, input.DataType, settings);
        }


        public static IObservable<object> WriteOutput(this DataIO io, IObservable<object> data, OutputDescriptor output, object settings = null)
        {
            return io.WriteOutput(data, output.Writer, output.Format, output.DataType, settings);
        }
    }
}
