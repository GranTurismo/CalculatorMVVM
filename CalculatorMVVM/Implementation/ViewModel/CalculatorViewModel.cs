using CalculatorMVVM.Implementation.Helper;
using CalculatorMVVM.Implementation.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Media3D;

namespace CalculatorMVVM.Implementation.ViewModel
{
    public class CalculatorViewModel : INotifyPropertyChanged
    {
        private readonly CalculatorModel _calculatorModel;
        private OperationResult _operationResult;
        private string _inputValue = "0";

        public event PropertyChangedEventHandler PropertyChanged;

        public string Title { get; } = "Calculator MVVM";

        public string InputValue
        {
            get => _inputValue;
            set
            {
                string normalizedValue = value;

                if (normalizedValue.Length > 0 && !normalizedValue.All(o => char.IsDigit(o) || o == '.'))
                    return;

                if (!normalizedValue.Contains(".") && normalizedValue.Length > 1)
                    normalizedValue = normalizedValue.TrimStart('0');

                _inputValue = normalizedValue.Length == 0 ? "0" : normalizedValue;
                PropChanged(nameof(InputValue));
            }
        }

        private void PropChanged(string name)
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(name));
        }


        public OperationResult OperationResult
        {
            get => _operationResult;
            set
            {
                _operationResult = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(OperationResult)));
            }
        }

        public ICommand OperationCommand { get; }
        public ICommand EqualCommand { get; }
        public ICommand NumberCommand { get; }
        public ICommand ClearCommand { get; }
        public ICommand FloatPointCommand { get; }
        public ICommand BackspaceCommand { get; }

        public CalculatorViewModel()
        {
            _calculatorModel = new CalculatorModel();
            _calculatorModel.OperationResultValueChanged += _calculatorModel_OperationResultValueChanged;

            OperationCommand = new CalculatorCommand<Operation>(
                execute: (o) =>
                {
                    _calculatorModel.OnOperationButtonClicked(o, InputValue);
                    InputValue = string.Empty;
                },
                canExecute: _calculatorModel.OnCanBeOperationExecuted
                );

            EqualCommand = new CalculatorCommand(
                execute: () =>
                {
                    _calculatorModel.OnCalculateResultExecute(InputValue);
                    InputValue = OperationResult.DoubleResult.ToString();
                },
                canExecute: _calculatorModel.OnCanBeOperationExecuted
                );

            NumberCommand = new CalculatorCommand<string>(
                execute: OnNumberClickedExecute,
                canExecute: () => true
                );

            ClearCommand = new CalculatorCommand(
                execute: () =>
                {
                    _calculatorModel.Clear();
                    InputValue = OperationResult.DoubleResult.ToString();
                },
                canExecute: () => true
                );

            FloatPointCommand = new CalculatorCommand(
                execute: () =>
                {
                    InputValue += '.';
                },
                canExecute: () => !InputValue.Contains('.')
                );
            BackspaceCommand = new CalculatorCommand(
                execute: () =>
                {
                    InputValue = InputValue.Remove(InputValue.Length - 1);
                },
                canExecute: () => true);
        }

        private void _calculatorModel_OperationResultValueChanged(OperationResult obj)
        {
            OperationResult = obj;
        }

        private void OnNumberClickedExecute(string data)
        {
            InputValue += data;
        }
    }
}
