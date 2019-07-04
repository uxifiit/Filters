using System;
using Newtonsoft.Json.Linq;
using UXI.Serialization.Formats.Json.Converters;
using Newtonsoft.Json;
using UXI.Filters.Serialization.Converters;

namespace UXI.Filters.Serialization.Json.Converters
{
    public class DateTimeOffsetJsonConverter : GenericJsonConverter<DateTimeOffset>
    {
        private readonly ITimestampStringConverter _timestampConverter;

        public DateTimeOffsetJsonConverter(ITimestampStringConverter timestampConverter)
        {
            _timestampConverter = timestampConverter;
        }

        public override bool CanWrite => true;

        protected override DateTimeOffset Convert(JToken token, JsonSerializer serializer)
        {
            return _timestampConverter.Convert(token.Value<string>());
        }

        protected override JToken ConvertBack(DateTimeOffset value, JsonSerializer serializer)
        {
            return _timestampConverter.ConvertBack(value);
        }
    }
}
