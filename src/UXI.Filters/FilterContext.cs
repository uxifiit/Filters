using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UXI.Filters.Observers;
using UXI.Filters.Observers.Serialization;
using UXI.Serialization;
using UXI.Serialization.Configurations;
using UXI.Serialization.Formats.Csv;
using UXI.Serialization.Formats.Csv.Configurations;
using UXI.Serialization.Formats.Json;

namespace UXI.Filters
{
    public class FilterContext
    {
        public DataIO IO { get; } = new DataIO
        (
            new JsonSerializationFactory(),
            new CsvSerializationFactory()
        );

        public FilterInfo Filter { get; set; }


        public Collection<IFilterObserver> Observers { get; } = new Collection<IFilterObserver>();


        public InputDescriptor Input { get; set; } = InputDescriptor.Null;
    }
}
