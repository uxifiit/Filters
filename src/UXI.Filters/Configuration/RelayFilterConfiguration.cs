using System;

namespace UXI.Filters.Configuration
{
    public class RelayFilterConfiguration<TOptions> : FilterConfiguration<TOptions>
    {
        private readonly Action<FilterContext, TOptions> _configureAction;

        public RelayFilterConfiguration(Action<FilterContext, TOptions> configure)
        {
            if (configure == null)
            {
                throw new ArgumentNullException(nameof(configure), "Configure action must not be null.");
            }

            _configureAction = configure;
        }

        protected override void ConfigureOverride(FilterContext context, TOptions options)
        {
            _configureAction?.Invoke(context, options);
        }
    }


    public class RelayFilterConfiguration<TContext, TOptions> : FilterConfiguration<TContext, TOptions>
        where TContext : FilterContext
    {
        private readonly Action<TContext, TOptions> _configureAction;

        public RelayFilterConfiguration(Action<TContext, TOptions> configure)
        {
            if (configure == null)
            {
                throw new ArgumentNullException(nameof(configure), "Configure action must not be null.");
            }

            _configureAction = configure;
        }

        protected override void ConfigureOverride(TContext context, TOptions options)
        {
            _configureAction.Invoke(context, options);
        }
    }
}
