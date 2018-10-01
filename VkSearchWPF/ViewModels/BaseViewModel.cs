using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace VkSearchWPF.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public bool SetProperty<T>(ref T field, T value, [CallerMemberName]string property = null)
        {
            if (Equals(field, value))
            {
                return false;
            }
            field = value;
            RaisePropertyChanged(property);
            return true;
        }

        private void RaisePropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
