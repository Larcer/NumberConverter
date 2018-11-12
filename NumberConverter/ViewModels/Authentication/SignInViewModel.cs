using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Nameless.NumberConverter.Managers;
using Nameless.NumberConverter.Services;
using Nameless.NumberConverter.Tools;
using Nameless.NumberConverter.ViewModels.Core;

namespace Nameless.NumberConverter.ViewModels.Authentication
{
    public class SignInViewModel : NotifiableViewModel
    {
        private readonly SignInService _service = new SignInService();

        private string _login;
        private string _password;

        // Stops the application
        private ICommand _closeCommand;
        // Performs user authentication
        private ICommand _signInCommand;
        // Navigates to sign up window
        private ICommand _signUpCommand;
        
        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }
        
        public ICommand CloseCommand =>
            _closeCommand ?? (_closeCommand = new RelayCommand(CloseExecute));

        public ICommand SignInCommand =>
            _signInCommand ?? (_signInCommand = new RelayCommand(SignInExecute, SignInCanExecute));

        public ICommand SignUpCommand =>
            _signUpCommand ?? (_signUpCommand = new RelayCommand(SignUpExecute));
        
        private void CloseExecute(object o)
        {
            Environment.Exit(1);
        }

        private async void SignInExecute(object o)
        {
            ContentWindowViewModel.Instance.ShowLoader();
            var success = await Task.Run(() => _service.LoginUser(_login, _password));
            ContentWindowViewModel.Instance.HideLoader();

            if (success)
            {
                NavigationManager.Instance.Navigate(WindowMode.NumberConverter);
                ClearTextBoxes();
            }
        }

        private bool SignInCanExecute(object o)
        {
            return !string.IsNullOrWhiteSpace(_login) && !string.IsNullOrWhiteSpace(_password);
        }

        private void SignUpExecute(object o)
        {
            NavigationManager.Instance.Navigate(WindowMode.SignUp);
        }

        // Clears all text boxes in the current window
        private void ClearTextBoxes()
        {
            Login = Password = string.Empty;
        }
    }
}
