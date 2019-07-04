using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UXI.Filters.Configuration;
using UXI.Filters.Observers;
using UXI.Filters.Observers.Serialization;
using UXI.Filters.Options;
using UXI.Serialization;
using UXI.Serialization.Formats.Csv.Configurations;
using UXI.Serialization.Extensions;

namespace UXI.Filters.Configuration
{
    public class SamplesCounterObserverConfiguration : FilterConfiguration<ISamplesCounterOptions>
    {
        protected override void ConfigureOverride(FilterContext context, ISamplesCounterOptions options)
        {
            if (options.IsSamplesCounterEnabled)
            {
                AddConverters(context);
                AddObserver(context, options);
            }
        }


        private static void AddObserver(FilterContext context, ISamplesCounterOptions options)
        {
            var output = UXI.Filters.Common.FileHelper.DescribeOutput(
                             options.SamplesCounterOutputFilePath,
                             options.SamplesCounterOutputFileFormat,
                             options.DefaultSamplesCounterOutputFileFormat,
                             typeof(SamplesCount),
                             Console.Error
                         );

            context.Observers.Add(new SamplesCounterObserver(context.Filter.Name, output));
        }


        private void AddConverters(FilterContext context)
        {
            context.IO
                   .Formats
                   .GetOrDefault(FileFormat.CSV)?
                   .Configurations
                   .Add(new CsvConvertersSerializationConfiguration(new SamplesCountCsvConverter()));
        }
    }
}
