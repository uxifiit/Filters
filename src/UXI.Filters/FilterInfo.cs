using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UXI.Filters
{
    public class FilterInfo
    {
        public static FilterInfo Create<TInput, TOutput, TOptions>(string name)
        {
            return new FilterInfo(name, typeof(TInput), typeof(TOutput), typeof(TOptions));
        }


        public FilterInfo(string name, Type inputType, Type outputType, Type optionsType)
        {
            Name = name;
            InputType = inputType;
            OutputType = outputType;
            OptionsType = optionsType;
        }


        public string Name { get; }


        public Type InputType { get; }


        public Type OutputType { get; }


        public Type OptionsType { get; }
    }
}
