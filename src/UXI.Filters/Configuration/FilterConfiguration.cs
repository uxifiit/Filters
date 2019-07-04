using System;

namespace UXI.Filters.Configuration
{
    public abstract class FilterConfiguration
    {
        public static FilterConfiguration FromMethod<TOptions>(Action<FilterContext, TOptions> configure)
        {
            return new RelayFilterConfiguration<TOptions>(configure);
        }


        public static FilterConfiguration FromMethod<TContext, TOptions>(Action<TContext, TOptions> configure)
            where TContext : FilterContext
        {
            return new RelayFilterConfiguration<TContext, TOptions>(configure);
        }


        public abstract void Configure(FilterContext context, object options);
    }



    public abstract class FilterConfiguration<TOptions> : FilterConfiguration
    {
        public virtual bool ThrowIfFailed => false;


        public sealed override void Configure(FilterContext context, object options)
        {
            if (options is TOptions)
            {
                ConfigureOverride(context, (TOptions)options);
            }
            else if (ThrowIfFailed)
            {
                throw new ArgumentException($"Failed to cast options of type {options?.GetType().FullName} to the expected type {typeof(TOptions).FullName}.", nameof(options));
            }
        }


        protected abstract void ConfigureOverride(FilterContext context, TOptions options);
    }



    public abstract class FilterConfiguration<TContext, TOptions> : FilterConfiguration<TOptions>
        where TContext : FilterContext
    {
        protected sealed override void ConfigureOverride(FilterContext context, TOptions options)
        {
            if (context is TContext)
            {
                ConfigureOverride((TContext)context, options);
            }
            else if (ThrowIfFailed)
            {
                throw new ArgumentException($"Failed to cast context of type {context?.GetType().FullName} to the expected type {typeof(TContext).FullName}.", nameof(context));
            }
        }


        protected abstract void ConfigureOverride(TContext context, TOptions options);
    }

}
