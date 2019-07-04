using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;
using UXI.Serialization;

namespace UXI.Filters.Observers
{
    public class SamplesCounterObserver : FilterObserver
    {
        private readonly Stopwatch _stopwatch = new Stopwatch();
        private int _inputSamplesCount = 0;
        private int _outputSamplesCount = 0;

        public SamplesCounterObserver(string filterName, OutputDescriptor output)
            : base(output)
        {
            FilterName = filterName;
        }


        public string FilterName { get; }


        protected override IObserver<object> CreateInputObserver(IObserver<object> resultsObserver)
        {
            return System.Reactive.Observer.Create<object>(_ => OnNextInput());
        }


        protected override IObserver<object> CreateOutputObserver(IObserver<object> resultsObserver)
        {
            return System.Reactive.Observer.Create<object>(_ => OnNextOutput(), ex => Error(ex, resultsObserver), () => Complete(resultsObserver));
        }


        protected override IDisposable Subscribe()
        {
            return Disposable.Create(() => Stop());
        }


        private void OnNextInput()
        {
            _stopwatch.Start();
            _inputSamplesCount += 1;
        }


        private void OnNextOutput()
        {
            _outputSamplesCount += 1;
        }


        private void Stop()
        {
            _stopwatch.Stop();
        }


        private void Error(Exception ex, IObserver<object> observer)
        {
            Stop();

            observer.OnError(ex);
        }


        private void Complete(IObserver<object> observer)
        {
            Stop();

            var result = new SamplesCount(FilterName, _inputSamplesCount, _outputSamplesCount, _stopwatch.Elapsed);
            observer.OnNext(result);

            observer.OnCompleted();
        }
    }
}
