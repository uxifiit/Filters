using System;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Reactive.Threading.Tasks;
using System.Reactive.Disposables;
using System.Reactive.Concurrency;
using UXI.Filters.Common.Extensions;
using UXI.Serialization.Reactive;

namespace UXI.Filters
{
    public class FilterExecution
    {
        public static Task ExecuteAsync<TContext>(IFilter filter, TContext context, object options, CancellationToken cancellationToken)
            where TContext : FilterContext
        {
            var observers = context.Observers.ToArray();

            filter.Initialize(options, context);

            var io = context.IO;

            var subscriptions = new CompositeDisposable
                                (
                                    observers.Where(o => o.Results != null && o.Output != null)
                                             .Select(o => io.WriteOutput(o.Results, o.Output.Writer, o.Output.Format, o.Output.DataType, null)
                                                            .Subscribe())
                                );

            // TODO input and output null check, check if formats did not remain set to default, etc.
            var pipeline = io.ReadInputAsObservable(context.Input.Reader, context.Input.Format, context.Input.DataType, null)
                             .SubscribeOn(NewThreadScheduler.Default)
                             .Attach(observers.Select(o => o.InputObserver).Where(o => o != null))
                             .Publish().RefCount()
                             .Process(filter, options)
                             .Attach(observers.Select(o => o.OutputObserver).Where(o => o != null));

            return pipeline.ToTask(cancellationToken)
                           .ContinueWith(_ => subscriptions.Dispose(), TaskContinuationOptions.ExecuteSynchronously);
        }
    }
}
