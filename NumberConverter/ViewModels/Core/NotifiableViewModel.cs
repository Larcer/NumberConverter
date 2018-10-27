using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Nameless.NumberConverter.ViewModels.Core
{
    public class NotifiableViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
