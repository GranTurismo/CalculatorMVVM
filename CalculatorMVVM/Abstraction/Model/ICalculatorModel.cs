using CalculatorMVVM.Implementation.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorMVVM.Abstraction.Model
{
    public interface ICalculatorModel
    {

        public Operation Operation { get; }

        public void OnOperationButtonClicked(Operation operation, string firstValue);

        public OperationResult OnEqaulButtonClicked(double secondValue);
    }
}
