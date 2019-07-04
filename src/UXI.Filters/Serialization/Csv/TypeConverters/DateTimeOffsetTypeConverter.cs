using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using UXI.Filters.Serialization.Converters;

namespace UXI.Filters.Serialization.Csv.TypeConverters
{
    public class DateTimeOffsetTypeConverter : ITypeConverter
    {
        private readonly ITimestampStringConverter _timestampConverter;

        public DateTimeOffsetTypeConverter(ITimestampStringConverter timestampConverter)
        {
            _timestampConverter = timestampConverter;
        }


        public object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            return _timestampConverter.Convert(text);
        }


        public string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            return _timestampConverter.ConvertBack((DateTimeOffset)value);
        }
    }
}
