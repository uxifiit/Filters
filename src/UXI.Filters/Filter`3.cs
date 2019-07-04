using System;
using System.Linq;
using System.Reactive.Linq;

namespace UXI.Filters
{
    public abstract class Filter<TInput, TOutput, TOptions>
        : Filter<TInput, TOutput, TOptions, FilterContext>
    {
        protected Filter()
            : base()
        {
        }


        protected Filter(string name)
            : base(name)
        {
        }
    }



    public abstract class Filter<TInput, TOutput, TOptions, TContext>
        : IFilter
        where TContext : FilterContext
    {
        protected Filter()
        {
            Info = FilterInfo.Create<TInput, TOutput, TOptions>(this.GetType().Name);
        }


        protected Filter(string name)
        {
            Info = FilterInfo.Create<TInput, TOutput, TOptions>(name);
        }


        public FilterInfo Info { get; }


        public void Initialize(object options, FilterContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context), "No FilterContext specified.");
            }
            else if ((context is TContext) == false)
            {
                throw new ArgumentException($"Given context is of the type [{context.GetType().FullName}], instead of the expected [{typeof(TContext).FullName}].");
            }

            if (options is TOptions)
            {
                Initialize((TOptions)options, (TContext)context);
            }
        }


        protected abstract void Initialize(TOptions options, TContext context);


        public IObservable<object> Process(IObservable<object> data, object options)
        {
            var typedData = data.OfType<TInput>();

            var result = Process(typedData, (TOptions)options);

            return result.Select(d => (object)d);
        }


        protected abstract IObservable<TOutput> Process(IObservable<TInput> data, TOptions options);
    }
}
