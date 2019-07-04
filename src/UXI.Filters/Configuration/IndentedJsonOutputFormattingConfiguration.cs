using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UXI.Serialization;
using UXI.Serialization.Configurations;
using UXI.Serialization.Extensions;

namespace UXI.Filters.Configuration
{
    public class IndentedJsonOutputFormattingConfiguration : FilterConfiguration
    {
        public override void Configure(FilterContext context, object options)
        {
            context.IO
                   .Formats
                   .GetOrDefault(FileFormat.JSON)?
                   .Configurations
                   .Add
                   (
                       new RelaySerializationConfiguration<Newtonsoft.Json.JsonSerializer>((serializer, access, _) =>
                       {
                           if (access == DataAccess.Write)
                           {
                               serializer.Formatting = Newtonsoft.Json.Formatting.Indented;
                           }

                           return serializer;
                       })
                   );
        }
    }
}
