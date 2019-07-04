using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UXI.Filters.Configuration;

namespace UXI.Filters
{
    public class SingleFilterHost<TOptions> : SingleFilterHost<FilterContext, TOptions>
    {
        public SingleFilterHost(IFilter filter) 
            : base(filter)
        {
        }

        public SingleFilterHost(IFilter filter, params FilterConfiguration[] configurations) 
            : base(filter, configurations)
        {
        }

        public SingleFilterHost(FilterContext context, IFilter filter, params FilterConfiguration[] configurations) 
            : base(context, filter, configurations)
        {
        }
    }



    public class SingleFilterHost<TContext, TOptions> : FilterHost<TContext>
        where TContext : FilterContext, new()
    {
        public SingleFilterHost(IFilter filter)
            : this(filter, new FilterConfiguration[0])
        {
        }


        public SingleFilterHost(IFilter filter, params FilterConfiguration[] configurations)
            : this(new TContext(), filter, configurations)
        {

        }


        public SingleFilterHost(TContext context, IFilter filter, params FilterConfiguration[] configurations)
            : base(context, configurations?.AsEnumerable() ?? Enumerable.Empty<FilterConfiguration>(), new[] { filter })
        {
            AssertOptionsTypeMatch(filter.Info.OptionsType);
        }


        private void AssertOptionsTypeMatch(Type optionsType)
        {
            if (optionsType.Equals(typeof(TOptions)) == false)
            {
                throw new ArgumentException("Filter options type mismatch.");
            }
        }


        protected override bool TryParseFilterOptions(Parser parser, string[] args, out object options)
        {
            bool parsed = false;
            TOptions parsedOptions = default(TOptions);

            parser.ParseArguments<TOptions>(args).WithParsed(o =>
            {
                parsedOptions = o;
                parsed = true;
            });

            options = parsedOptions;
            return parsed;
        }
    }
}
