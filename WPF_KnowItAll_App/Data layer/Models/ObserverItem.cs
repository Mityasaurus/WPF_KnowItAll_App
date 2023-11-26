using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WPF_KnowItAll_App.Data_layer.Models
{
    public class ObserverItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
