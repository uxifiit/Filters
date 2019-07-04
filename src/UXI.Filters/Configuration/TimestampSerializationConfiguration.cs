using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UXI.Filters.Options;
using UXI.Filters.Serialization.Converters;
using UXI.Filters.Serialization.Csv;
using UXI.Filters.Serialization.Json;
using UXI.Serialization;
using UXI.Serialization.Extensions;

namespace UXI.Filters.Configuration
{
    public class TimestampSerializationConfiguration : FilterConfiguration<ITimestampSerializationOptions>
    {
        protected override void ConfigureOverride(FilterContext context, ITimestampSerializationOptions options)
        {
            ITimestampStringConverter timestampConverter = TimestampStringConverterResolver.Default.Resolve(options.TimestampFormat);

            context.IO
                   .Formats
                   .GetOrDefault(FileFormat.JSON)?
                   .Configurations
                   .Add
                   (
                       new JsonTimestampSerializationConfiguration(timestampConverter)
                   );

            context.IO
                   .Formats
                   .GetOrDefault(FileFormat.CSV)?
                   .Configurations
                   .Add
                   (
                       new CsvTimestampSerializationConfiguration(timestampConverter)
                   );
        }
    }
}
