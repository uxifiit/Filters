using CommandLine;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading;
using UXI.Filters.Common.Extensions;
using UXI.Filters.Configuration;

namespace UXI.Filters
{
    public abstract class FilterHost<TContext>
        where TContext : FilterContext
    {
        private readonly StringWriter _help;
        private readonly Parser _commandLineParser;


        public static readonly IEnumerable<FilterConfiguration> DefaultConfigurations = new FilterConfiguration[]
        {
            new InputConfiguration(),
            new OutputConfiguration(),
            new SamplesCounterObserverConfiguration(),
            new TimestampSerializationConfiguration()
        };


        protected FilterHost(TContext context, IEnumerable<FilterConfiguration> configurations, IEnumerable<IFilter> filters)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context), "Context must not be null.");
            }

            if (filters == null || filters.Any() == false)
            {
                throw new ArgumentNullException(nameof(filters), "No filter specified.");
            }

            _help = new StringWriter();
            _commandLineParser = new Parser(s =>
            {
                s.CaseSensitive = false;
                s.CaseInsensitiveEnumValues = true;
                s.ParsingCulture = CultureInfo.GetCultureInfo("en-US");
                s.HelpWriter = _help;
            });

            Context = context;

            Configurations = new FilterConfigurationCollection(DefaultConfigurations);

            Configurations.AddRange(FilterConfigurationAttribute.GetConfigurations(Context.GetType(), false));
            Configurations.AddRange(FilterConfigurationAttribute.GetConfigurations(this.GetType(), false));

            if (configurations != null)
            {
                Configurations.AddRange(configurations);
            }

            Filters = filters.ToDictionary(f => f.Info.OptionsType);
        }


        public FilterConfigurationCollection Configurations { get; }


        public TContext Context { get; }


        public Dictionary<Type, IFilter> Filters { get; }


        public int Execute(string[] args)
        {
            object options;
            IFilter filter;

            if (TryParseFilterOptions(_commandLineParser, args, out options)
                && Filters.TryGetValue(options.GetType(), out filter))
            {
                //#if DEBUG
                //    Console.Error.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(options, Newtonsoft.Json.Formatting.Indented, new StringEnumConverter(false)));
                //#endif

                Context.Filter = filter.Info;

                Configure(options);

                ExecuteFilter(filter, options);

                return 0;
            }
            else
            {
                Console.Error.WriteLine(_help.ToString());

                return 1;
            }
        }


        private void ExecuteFilter(IFilter filter, object options)
        {
            try
            {
                using (var cts = new CancellationTokenSource())
                {
                    Console.CancelKeyPress += (_, e) => { cts.Cancel(); e.Cancel = true; };

                    var execution = FilterExecution.ExecuteAsync(filter, Context, options, cts.Token);

                    execution.Wait();
                }
            }
            catch
            {
                throw;
            }
        }


        protected abstract bool TryParseFilterOptions(Parser parser, string[] args, out object options);


        private void Configure(object options)
        {
            foreach (var configuration in Configurations)
            {
                configuration.Configure(Context, options);
            }
        }
    }
}
