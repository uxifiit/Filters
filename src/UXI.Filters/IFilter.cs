using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UXI.Filters
{
    public interface IFilter
    {
        FilterInfo Info { get; }

        void Initialize(object options, FilterContext context);

        IObservable<object> Process(IObservable<object> data, object options);
    }



    public static class IFilterEx
    {
        public static IObservable<object> Process(this IObservable<object> data, IFilter filter, object options)
        {
            return filter.Process(data, options);
        }
    }
}
