using System;
using System.Threading.Tasks;

namespace VkSearchWPF.ViewModels
{
    public class AsyncCommand : IAsyncCommand
    {
        private readonly Func<Task> execute;
        private readonly Predicate<object> canExecute;

        private bool isExecuting;

        public event EventHandler CanExecuteChanged;

        public AsyncCommand(Func<Task> execute, Predicate<object> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return !isExecuting && (canExecute?.Invoke(parameter) ?? true);
        }

        public void Execute(object parameter)
        {
            ExecuteAsync();
        }

        public async Task ExecuteAsync()
        {
            if (CanExecute(null))
            {
                try
                {
                    isExecuting = true;
                    RaiseCanExecuteChanged();
                    await execute();
                } finally
                {
                    isExecuting = false;
                    RaiseCanExecuteChanged();
                }
            }
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
