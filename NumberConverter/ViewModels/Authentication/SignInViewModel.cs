using System;
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

        private ICommand _closeCommand;
        private ICommand _signInCommand;
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

        private void SignInExecute(object o)
        {
            bool success = _service.LoginUser(_login, _password);
            if (success)
                NavigationManager.Instance.Navigate(WindowMode.NumberConverter);
        }

        private bool SignInCanExecute(object o)
        {
            return !string.IsNullOrWhiteSpace(_login) && !string.IsNullOrWhiteSpace(_password);
        }

        private void SignUpExecute(object o)
        {
            NavigationManager.Instance.Navigate(WindowMode.SignUp);
        }
    }
}
