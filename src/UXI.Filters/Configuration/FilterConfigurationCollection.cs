using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UXI.Filters.Configuration
{
    public class FilterConfigurationCollection : Collection<FilterConfiguration>
    {
        public FilterConfigurationCollection()
        {
        }


        public FilterConfigurationCollection(IEnumerable<FilterConfiguration> configurations)
            : base(configurations?.ToList() ?? new List<FilterConfiguration>())
        {
        }

        
        public void AddRange(IEnumerable<FilterConfiguration> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            foreach (var item in collection)
            {
                this.Add(item);
            }
        }
    }
}
