using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UXI.Filters.Configuration
{
    [System.AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public sealed class FilterConfigurationAttribute : Attribute
    {
        public FilterConfigurationAttribute(Type configurationType)
        {
            if (configurationType == null)
            {
                throw new ArgumentNullException(nameof(configurationType), "Configuration type must not be null.");
            }
            else if (configurationType.IsClass == false 
                     || configurationType.IsSubclassOf(typeof(FilterConfiguration)) == false)
            {
                throw new ArgumentException($"Given configuration type [{configurationType.FullName}] is not subclass of the base type [{typeof(FilterConfiguration).FullName}].", nameof(configurationType));
            }

            Configuration = (FilterConfiguration)Activator.CreateInstance(configurationType);
        }


        public FilterConfigurationAttribute(FilterConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            Configuration = configuration;
        }


        public FilterConfiguration Configuration { get; }


        public static IEnumerable<FilterConfigurationAttribute> GetAttributeInstances(ICustomAttributeProvider source, bool inherit)
        {
            return source.GetCustomAttributes(typeof(FilterConfigurationAttribute), inherit)
                         .OfType<FilterConfigurationAttribute>();
        }


        public static IEnumerable<FilterConfiguration> GetConfigurations(ICustomAttributeProvider source, bool inherit)
        {
            return GetAttributeInstances(source, inherit).Select(attr => attr.Configuration);
        }
    }
}
