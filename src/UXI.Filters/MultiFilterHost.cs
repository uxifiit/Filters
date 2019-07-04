using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UXI.Filters.Configuration;

namespace UXI.Filters
{
    public class MultiFilterHost : MultiFilterHost<FilterContext>
    {
        public MultiFilterHost(params IFilter[] filters) 
            : base(filters)
        {
        }

        public MultiFilterHost(IEnumerable<FilterConfiguration> configurations, params IFilter[] filters) 
            : base(configurations, filters)
        {
        }

        public MultiFilterHost(FilterContext context, IEnumerable<FilterConfiguration> configurations, params IFilter[] filters) 
            : base(context, configurations, filters)
        {
        }
    }



    public class MultiFilterHost<TContext> : FilterHost<TContext>
        where TContext : FilterContext, new()
    {
        public MultiFilterHost(params IFilter[] filters)
            : this(Enumerable.Empty<FilterConfiguration>(), filters)
        {
        }


        public MultiFilterHost(IEnumerable<FilterConfiguration> configurations, params IFilter[] filters)
            : this(new TContext(), configurations, filters)
        {
        }


        public MultiFilterHost(TContext context, IEnumerable<FilterConfiguration> configurations, params IFilter[] filters)
            : base(context, configurations, filters.AsEnumerable())
        {
        }


        protected override bool TryParseFilterOptions(Parser parser, string[] args, out object options)
        {
            bool parsed = false;
            object parsedOptions = null;

            parser.ParseArguments(args, Filters.Keys.ToArray()).WithParsed(o =>
            {
                parsedOptions = o;
                parsed = true;
            });

            options = parsedOptions;
            return parsed;
        }
    }
}
