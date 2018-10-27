using System.Windows.Controls;
using Nameless.NumberConverter.ViewModels.Authentication;

namespace Nameless.NumberConverter.Views
{
    /// <summary>
    /// Логика взаимодействия для SignInView.xaml
    /// </summary>
    public partial class SignInView : UserControl
    {
        public SignInView()
        {
            InitializeComponent();
            DataContext = new SignInViewModel();
        }
    }
}
