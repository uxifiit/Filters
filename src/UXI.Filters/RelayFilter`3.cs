using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UXI.Filters
{
    public class RelayFilter<TSource, TResult, TOptions> 
        : RelayFilter<TSource, TResult, TOptions, FilterContext>
    {
        public RelayFilter(Func<IObservable<TSource>, TOptions, FilterContext, IObservable<TResult>> filter)
            : base(filter)
        {
        }


        public RelayFilter(string name, Func<IObservable<TSource>, TOptions, FilterContext, IObservable<TResult>> filter)
            : base(name, filter)
        {
        }
    }



    public class RelayFilter<TSource, TResult, TOptions, TContext>
       : Filter<TSource, TResult, TOptions, TContext>
        where TContext : FilterContext
    {
        private readonly Func<IObservable<TSource>, TOptions, TContext, IObservable<TResult>> _filter;
        private TContext _context;

        protected RelayFilter()
            : base()
        {
            _filter = (_, __, ___) => Observable.Empty<TResult>();
        }


        public RelayFilter(Func<IObservable<TSource>, TOptions, TContext, IObservable<TResult>> filter)
            : base()
        {
            _filter = filter;
        }


        public RelayFilter(string name, Func<IObservable<TSource>, TOptions, TContext, IObservable<TResult>> filter)
            : base(name)
        {
            _filter = filter;
        }


        protected override void Initialize(TOptions options, TContext context)
        {
            _context = context;
        }


        protected override IObservable<TResult> Process(IObservable<TSource> data, TOptions options)
        {
            return _filter.Invoke(data, options, _context);
        }
    }
}
