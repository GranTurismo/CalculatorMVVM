using CalculatorMVVM.Abstraction.Helper;
using CalculatorMVVM.Abstraction.Model;
using CalculatorMVVM.Implementation.Helper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorMVVM.Implementation.Model
{
    public class CalculatorModel : ICalculatorModel
    {
        private readonly Dictionary<Operation, Func<double,double,double>> _equalButtonDelegateDictionary;

        public CalculatorModel()
        {
            _equalButtonDelegateDictionary = new()
            {
                { Operation.Add, (a,b)=>a+b },
                { Operation.Substract, (a,b)=>a-b },
                { Operation.Multiply,(a,b)=>a*b },
                { Operation.Divide, (a,b)=>a/b }
            };
        }

        private Queue<IMathObject> mathObjects = new Queue<IMathObject>();

        private OperationResult _operationResult;
        private OperationResult OperationResult
        {
            get => _operationResult;
            set
            {
                _operationResult = value;
                OperationResultValueChanged?.Invoke(OperationResult);
            }
        }

        public Operation Operation { get; private set; } = Operation.Add;

        public event Action<OperationResult> OperationResultValueChanged;

        public void Clear()
        {
            mathObjects.Clear();
            OperationResult = new OperationResult(OperationResultStatus.Success, 0);
        }

        public OperationResult OnEqaulButtonClicked(double secondValue)
        {
            double result = mathObjects.First().Value;

            for(int i=0;i<mathObjects.Count-1;i++)
            {
                Func<double, double, double> del = _equalButtonDelegateDictionary
                    .First(o => o.Key == mathObjects.ElementAt(i).Operation).Value;
                result = del.Invoke(result, mathObjects.ElementAt(i+1).Value);
            }
            Func<double, double, double> del2 = _equalButtonDelegateDictionary
                    .First(o => o.Key == mathObjects.Last().Operation).Value;
            result = del2.Invoke(result, secondValue);

            mathObjects.Clear();

            return new OperationResult(OperationResultStatus.Success, result);
        }

        public void OnOperationButtonClicked(Operation operation, string firstInput)
        {

            OperationResult = ValidationInput(firstInput);
            if (OperationResult.ResultStatus == OperationResultStatus.Fail)
                return;

            double firstValue = OperationResult.DoubleResult;
            mathObjects.Enqueue(new MathObject(firstValue, operation));
        }

        public void OnCalculateResultExecute(string second)
        {
            OperationResult = ValidationInput(second);
            if (OperationResult.ResultStatus == OperationResultStatus.Fail)
                return;
            double secValue = OperationResult.DoubleResult;
            OperationResult = OnEqaulButtonClicked(secValue);
        }

        public bool OnCanBeOperationExecuted()
        {
            return true;
        }

        private OperationResult ValidationInput(string num)
        {
            double val;
            bool isValid = double.TryParse(num, out val);
            if (isValid)
                return new OperationResult(OperationResultStatus.Success, val);
            return new OperationResult(OperationResultStatus.Fail, "Number format is not correct");
        }
    }
}
