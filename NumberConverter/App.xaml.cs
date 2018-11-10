using System.Windows;
using Nameless.NumberConverter.Managers;

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

            if (SessionManager.Instance.CurrentUser == null)
                NavigationManager.Instance.Navigate(WindowMode.SignIn);
            else
                NavigationManager.Instance.Navigate(WindowMode.NumberConverter);
        }
    }
}
