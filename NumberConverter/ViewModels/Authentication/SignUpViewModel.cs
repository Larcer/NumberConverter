using System;
using System.Windows.Input;

using Nameless.NumberConverter.Managers;
using Nameless.NumberConverter.Models;
using Nameless.NumberConverter.Tools;
using Nameless.NumberConverter.ViewModels.Core;
using Nameless.NumberConverter.Properties;
using Nameless.NumberConverter.Services;

namespace Nameless.NumberConverter.ViewModels.Authentication
{
    public class SignUpViewModel : NotifiableViewModel
    {
        private readonly SignUpService service = new SignUpService();

        private string _firstName;
        private string _lastName;
        private string _login;
        private string _email;
        private string _password;

        // Navigates back to the login window
        private ICommand _backCommand;
        // Performs user registration
        private ICommand _signUpCommand;
        // Stops the application
        private ICommand _closeCommand;


        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChanged();
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged();
            }
        }

        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
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


        public ICommand BackCommand =>
            _backCommand ?? (_backCommand = new RelayCommand(CancelExecute));

        public ICommand SignUpCommand =>
            _signUpCommand ?? (_signUpCommand = new RelayCommand(SignUpExecute, SignUpCanExecute));

        public ICommand CloseCommand =>
            _closeCommand ?? (_closeCommand = new RelayCommand(CloseExecute));

        private void CancelExecute(object o)
        {
            NavigationManager.Instance.Navigate(WindowMode.SignIn);
        }

        private void SignUpExecute(object o)
        {
            User user = new User(_firstName, _lastName,
                _login, _email, _password);

            bool success = service.SignUp(user);
            if (success)
            {
                MessageManager.UserMessage(string.Format(
                    Resources.SignUp_UserSuccessfullyCreated, user.Login));

                NavigationManager.Instance.Navigate(WindowMode.SignIn);
                ClearTextBoxes();
            }
        }

        private bool SignUpCanExecute(object o)
        {
            return !(string.IsNullOrWhiteSpace(_firstName)
                || string.IsNullOrWhiteSpace(_lastName)
                || string.IsNullOrWhiteSpace(_login)
                || string.IsNullOrWhiteSpace(_email)
                || string.IsNullOrWhiteSpace(_password));
        }

        private void CloseExecute(object o)
        {
            Environment.Exit(1);
        }

        // Clears all text boxes in the current window
        private void ClearTextBoxes()
        {
            FirstName = LastName = Login = Email = Password = string.Empty;
        }

    }
}
