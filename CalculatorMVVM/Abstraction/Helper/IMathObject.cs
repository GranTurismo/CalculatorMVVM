using CalculatorMVVM.Implementation.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorMVVM.Abstraction.Helper
{
    public interface IMathObject
    {
        double Value { get; }
        Operation Operation { get; }
    }
}
