using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UXI.Filters.Serialization.Converters;
using UXI.Filters.Serialization.Json.Converters;
using UXI.Serialization;
using UXI.Serialization.Configurations;

namespace UXI.Filters.Serialization.Json
{
    public class JsonTimestampSerializationConfiguration : SerializationConfiguration<JsonSerializer> // TODO ADD TimeSpan, not just DateTimeOffset, when the class is for serialization of Timestamp
    {
        public JsonTimestampSerializationConfiguration(ITimestampStringConverter converter)
        {
            Converter = converter;
        }


        public ITimestampStringConverter Converter { get; set; }


        protected override JsonSerializer Configure(JsonSerializer serializer, DataAccess access, Type dataType, object settings)
        {
            SetupDateTimeOffsetSerialization(serializer, Converter);

            return serializer;
        }


        private void SetupDateTimeOffsetSerialization(JsonSerializer serializer, ITimestampStringConverter timestampConverter)
        {
            serializer.DateTimeZoneHandling = DateTimeZoneHandling.Local;

            if (timestampConverter == null)
            {
                serializer.DateFormatHandling = DateFormatHandling.IsoDateFormat;
                serializer.DateParseHandling = DateParseHandling.DateTimeOffset;
            }
            else
            {
                serializer.DateTimeZoneHandling = DateTimeZoneHandling.Local;
                serializer.DateParseHandling = DateParseHandling.None;

                serializer.Converters.Add(new DateTimeOffsetJsonConverter(timestampConverter));
            }
        }
    }
}
