using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CalculatorMVVM.Implementation.Helper
{
    public class CalculatorCommand<T> : ICommand
    {
        private readonly Action<T> _execute;
        private readonly Func<bool> _canExecute;

        public CalculatorCommand(Action<T> execute, Func<bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return _canExecute.Invoke();
        }

        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }
    }

    public class CalculatorCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public CalculatorCommand(Action execute, Func<bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return _canExecute.Invoke();
        }

        public void Execute(object parameter)
        {
            _execute();
        }
    }
}
