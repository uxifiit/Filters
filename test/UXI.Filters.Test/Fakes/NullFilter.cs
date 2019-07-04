//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace UXI.Filters.Fakes
//{
//    public class NullFilter<TInput, TOutput, TOptions> : Filter<TInput, TOutput, TOptions>
//    {
//        protected override void Initialize(TOptions options, FilterContext context)
//        {
//        }

//        protected override IObservable<TOutput> Process(IObservable<TInput> data, TOptions options)
//        {
//            return data.Select(_ => default(TOutput));
//        }
//    }
//}
