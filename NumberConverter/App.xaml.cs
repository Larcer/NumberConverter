using System.Windows;

using Nameless.NumberConverter.Managers;
using Nameless.NumberConverter.ViewModels;

namespace Nameless.NumberConverter
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ContentWindowViewModel.Instance.Show();
            NavigationManager.Instance.Navigate(SessionManager.Instance.CurrentUser == null
                ? WindowMode.SignIn
                : WindowMode.NumberConverter);
        }
    }
}
