using PropertyChanged;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace XenScheduler.Models
{
    [AddINotifyPropertyChangedInterface]
    public abstract class BaseObservableModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
