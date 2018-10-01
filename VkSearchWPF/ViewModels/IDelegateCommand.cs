using System.Windows.Input;

namespace VkSearchWPF.ViewModels
{
    public interface IDelegateCommand: ICommand
    {
        void RaiseCanExecuteChanged();
    }
}
