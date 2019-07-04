using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UXI.Serialization;

namespace UXI.Filters.Observers
{
    public interface IFilterObserver
    {
        IObserver<object> InputObserver { get; }

        IObserver<object> OutputObserver { get; }

        IObservable<object> Results { get; }

        OutputDescriptor Output { get; }
    }
}
