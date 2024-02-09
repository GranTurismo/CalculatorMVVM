using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalculatorMVVM.Abstraction.Helper;

namespace CalculatorMVVM.Implementation.Helper
{
    public class MathObject : IMathObject
    {
        public MathObject(double val, Operation op)
        {
            Value = val;
            Operation = op;
        }

        public double Value { get; }

        public Operation Operation { get; }
    }
}
