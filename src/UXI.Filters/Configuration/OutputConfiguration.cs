using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UXI.Filters.Common;
using UXI.Filters.Observers;
using UXI.Filters.Options;

namespace UXI.Filters.Configuration
{
    public class OutputConfiguration : FilterConfiguration<IOutputOptions>
    {
        protected override void ConfigureOverride(FilterContext context, IOutputOptions options)
        {
            var output = FileHelper.DescribeOutput(
                                 options.OutputFilePath,
                                 options.OutputFileFormat,
                                 options.DefaultOutputFileFormat,
                                 context.Filter.OutputType,
                                 Console.Out
                             );

            var observer = new FilterObserver(output);

            context.Observers.Add(observer);
        }
    }
}
