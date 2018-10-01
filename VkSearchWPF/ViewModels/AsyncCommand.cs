using System;
using System.Threading.Tasks;

namespace VkSearchWPF.ViewModels
{
    public class AsyncCommand : IAsyncCommand
    {
        private readonly Func<Task> execute;
        private readonly Predicate<object> canExecute;
        private bool isExecuting = false;

        public event EventHandler CanExecuteChanged;

        public AsyncCommand(Func<Task> execute, Predicate<object> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public AsyncCommand(Func<Task> execute) : this(execute, (obj) => true) { }

        public bool CanExecute(object parameter)
        {
            return !isExecuting && canExecute(parameter);
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
