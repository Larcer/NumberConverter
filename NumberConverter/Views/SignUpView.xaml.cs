using System.Windows.Controls;
using Nameless.NumberConverter.ViewModels.Authentication;

namespace Nameless.NumberConverter.Views
{
    /// <summary>
    /// Логика взаимодействия для SignUpView.xaml
    /// </summary>
    public partial class SignUpView : UserControl
    {
        public SignUpView()
        {
            InitializeComponent();
            DataContext = new SignUpViewModel();
        }
    }
}
