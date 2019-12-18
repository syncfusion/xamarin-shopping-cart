using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ShoppingCart.Commands
{
    /// <summary>
    /// This class extends from ICommand.
    /// To avoid pressing button multiple times.
    /// </summary>
    public class DelegateCommand : ICommand
    {
        private readonly Action<object> _execute;
        private bool _canExecute = true;

        public DelegateCommand(Action<object> execute)
        {
            if (execute == null) throw new ArgumentNullException(nameof(execute));

            _execute = execute;
        }

        public int Delay { get; set; } = 100;

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return _canExecute;
        }

        public async void Execute(object parameter)
        {
            try
            {
                _canExecute = false;
                RaiseCanExecuteChanged();

                _execute(parameter);
                await Task.Delay(Delay);
            }
            finally
            {
                _canExecute = true;
                RaiseCanExecuteChanged();
            }
        }

        public void RaiseCanExecuteChanged()
        {
            var handler = CanExecuteChanged;
            if (handler != null)
                try
                {
                    handler(this, EventArgs.Empty);
                }
                catch (Exception)
                {
                }
        }
    }
}