using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UXI.Filters.Common;

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


        public bool IsOverriden { get; private set; }


        public FilterInfo OverrideInputOutput(Type newInputType, Type newOutputType)
        {
            bool isInputOverriden  = TryOverrideDataType(InputType,  newInputType,  out newInputType);
            bool isOutputOverriden = TryOverrideDataType(OutputType, newOutputType, out newOutputType);
            
            return new FilterInfo(Name, newInputType, newOutputType, OptionsType)
            {
                IsOverriden = (isInputOverriden || isOutputOverriden)
            };
        }


        private bool TryOverrideDataType(Type baseType, Type specificType, out Type result)
        {
            if (specificType == null)
            {
                result = baseType;
                return false;
            }

            if (baseType == null || TypeCompatibilityHelper.CanAssign(specificType, baseType))
            {
                result = specificType;
                return true;
            }

            throw new ArgumentException($"Specific type [{specificType.FullName}] cannot override the base type [{baseType.FullName}].");
        }
    }
}
