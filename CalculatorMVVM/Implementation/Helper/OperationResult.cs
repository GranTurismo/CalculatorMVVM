using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorMVVM.Implementation.Helper
{
    public class OperationResult
    {

        public OperationResult(OperationResultStatus status, string result)
        {
            StringResult = result;
            ResultStatus = status;
        }

        public OperationResult(OperationResultStatus status, double result)
        {
            DoubleResult = result;
            ResultStatus = status;
        }
        public OperationResultStatus ResultStatus { get; }

        public string StringResult { get; set; }
        public double DoubleResult { get; set; }
    }
}
