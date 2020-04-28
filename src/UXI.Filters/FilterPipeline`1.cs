using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using UXI.Filters.Common;
using UXI.Filters.Exceptions;

namespace UXI.Filters
{
    public class FilterPipeline<TOptions> : IFilter
    {
        private readonly List<IFilter> _filters;

        public FilterPipeline(string pipelineName, IEnumerable<IFilter> filters)
        {
            _filters = filters?.ToList() ?? new List<IFilter>();

            if (_filters.Any() == false)
            {
                throw new ArgumentException("No filter specified.", nameof(filters));
            }

            Info = new FilterInfo(pipelineName, _filters.FirstOrDefault()?.Info.InputType, _filters.LastOrDefault()?.Info.OutputType, typeof(TOptions));
        }


        public FilterPipeline(string pipelineName, params IFilter[] filters)
            : this(pipelineName, filters?.AsEnumerable())
        {
        }


        public FilterPipeline(IEnumerable<IFilter> filters)
            : this(CreateName(filters), filters)
        {
        }


        public FilterPipeline(params IFilter[] filters) 
            : this(filters?.AsEnumerable())
        {
        }


        private static string CreateName(IEnumerable<IFilter> filters)
        {
            string name = "Pipeline";

            IEnumerable<string> filterNames = filters?.Where(f => f != null).Select(f => f.Info.Name);

            if (filterNames != null && filterNames.Any())
            {
                return name + "_" + filterNames.Aggregate((a, b) => a + "-" + b);
            }
            else
            {
                return name;
            }
        }


        public FilterInfo Info { get; }


        public void Initialize(object options, FilterContext context)
        {
            ValidateFilters();

            foreach (var filter in _filters)
            {
                filter.Initialize(options, context);
            }
        }


        public IObservable<object> Process(IObservable<object> data, object options)
        {
            IObservable<object> pipeline = data;

            foreach (var filter in _filters)
            {
                pipeline = filter.Process(pipeline, options);
            }

            return pipeline;
        }


        private void ValidateFilters()
        {
            // then check pairs of output and input types in the pipeline:
            // first create pairs of successive filters
            var filterPairs = _filters.Zip(_filters.Skip(1), (a, b) => new Tuple<IFilter, IFilter>(a, b));

            // and check if the types in each pair match
            foreach (var filterPair in filterPairs)
            {
                EnsureCanConnect(filterPair.Item1.GetType(), filterPair.Item1.Info.OutputType, filterPair.Item2.GetType(), filterPair.Item2.Info.InputType);
            }
        }


        private static void EnsureCanConnect(Type sourceFilter, Type sourceOutputDataType, Type targetFilter, Type targetInputDataType)
        {
            bool canAssign = TypeCompatibilityHelper.CanAssign(sourceOutputDataType, targetInputDataType);

            if (canAssign == false)
            {
                throw new FilterTypeMismatchException($"Mismatch in data types between filters [{sourceFilter.FullName}] and [{targetFilter.FullName}]: Source type [{sourceOutputDataType.FullName}] does not match the expected type [{targetInputDataType.FullName}].");
            }
        }
    }
}
