using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UXI.Filters.Observers
{
    public class FilterObserver : IFilterObserver
    {
        public FilterObserver(OutputDescriptor output)
        {
            Output = output;

            Results = Observable.Create<object>((observer) =>
            {
                InputObserver = CreateInputObserver(observer);
                OutputObserver = CreateOutputObserver(observer);

                return Subscribe();
            }).Publish().RefCount();
        }


        public IObserver<object> InputObserver { get; private set; }


        public IObserver<object> OutputObserver { get; private set; }


        public IObservable<object> Results { get; private set; }


        public OutputDescriptor Output { get; }


        protected virtual IObserver<object> CreateInputObserver(IObserver<object> resultsObserver)
        {
            return null;
        }


        protected virtual IObserver<object> CreateOutputObserver(IObserver<object> resultsObserver)
        {
            return resultsObserver;
        }


        protected virtual IDisposable Subscribe()
        {
            return Disposable.Empty;
        }
    }
}
