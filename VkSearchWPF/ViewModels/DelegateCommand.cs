using System;

namespace VkSearchWPF.ViewModels
{
    public class DelegateCommand : IDelegateCommand
    {
        private readonly Action<object> execute;
        private readonly Predicate<object> canExecute;

        public event EventHandler CanExecuteChanged;

        public DelegateCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return canExecute?.Invoke(parameter) ?? true;
        }

        public void Execute(object parameter)
        {
            execute(parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
