using ReactiveUI;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace dllCallback.ViewModels
{
    public class ViewModelBase : ReactiveObject, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}