using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UXI.Filters.Options;
using UXI.Filters.Common;

namespace UXI.Filters.Configuration
{
    public class InputConfiguration : FilterConfiguration<IInputOptions>
    {
        protected override void ConfigureOverride(FilterContext context, IInputOptions options)
        {
            context.Input = FileHelper.DescribeInput(
                                options.InputFilePath, 
                                options.InputFileFormat, 
                                options.DefaultInputFileFormat, 
                                context.Filter.InputType, 
                                Console.In
                            );
        }
    }
}
