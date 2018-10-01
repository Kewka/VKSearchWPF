using System.Threading.Tasks;

namespace VkSearchWPF.ViewModels
{
    interface IAsyncCommand: IDelegateCommand
    {
        Task ExecuteAsync();
    }
}
