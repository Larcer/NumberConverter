using System.Windows;
using System.Windows.Controls;
using Nameless.NumberConverter.Tools;
using Nameless.NumberConverter.ViewModels.Core;
using Nameless.NumberConverter.Windows;

namespace Nameless.NumberConverter.ViewModels
{
    internal class ContentWindowViewModel : NotifiableViewModel
    {
        private readonly ContentWindow _contentWindow = new ContentWindow();
        private Visibility _loaderVisibility = Visibility.Hidden;
        private bool _isEnabled = true;

        internal static ContentWindowViewModel Instance => ContentWindowViewModelHolder.Instance;

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
        
        // Shows content window
        internal void Show()
        {
            _contentWindow.Show();
        }

        // Replaces the content of content window with specified one
        public void ReplaceContent(ContentControl content)
        {
            _contentWindow.ContentControl.Content = content;
        }

        // Shows loader on the content window
        public void ShowLoader()
        {
            LoaderVisibility = Visibility.Visible;
            IsEnabled = false;
        }

        // Hides loader on the content window
        public void HideLoader()
        {
            LoaderVisibility = Visibility.Hidden;
            IsEnabled = true;
        }

        // Holds an instance of ContentWindowViewModel.
        // Used as private class because ContentWindowViewModel should be inherited from NotifiableViewModel
        private class ContentWindowViewModelHolder : SingletonBase<ContentWindowViewModel>
        {

        }

    }
}
