using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using Nameless.NumberConverter.Tools;
using Nameless.NumberConverter.Windows;

namespace Nameless.NumberConverter.ViewModels
{
    public class ContentWindowViewModel : SingletonBase<ContentWindowViewModel>, INotifyPropertyChanged
    {
        private readonly ContentWindow _contentWindow = new ContentWindow();
        private Visibility _loaderVisibility = Visibility.Hidden;
        private bool _isEnabled = true;

        public Visibility LoaderVisibility
        {
            get => _loaderVisibility;
            set
            {
                _loaderVisibility = value;
                OnPropertyChanged();
            }
        }

        public bool IsEnabled
        {
            get => _isEnabled;
            set
            {
                _isEnabled = value;
                OnPropertyChanged();
            }
        }

        private ContentWindowViewModel()
        {
            _contentWindow.DataContext = this;
        }

        public void Show()
        {
            _contentWindow.Show();
        }

        public void ReplaceContent(ContentControl content)
        {
            _contentWindow.ContentControl.Content = content;
        }

        public void ShowLoader()
        {
            LoaderVisibility = Visibility.Visible;
            IsEnabled = false;
        }

        public void HideLoader()
        {
            LoaderVisibility = Visibility.Hidden;
            IsEnabled = true;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

    }
}
