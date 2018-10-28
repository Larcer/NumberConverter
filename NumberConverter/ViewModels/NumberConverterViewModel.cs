using System.Collections.Generic;
using System.Windows.Input;
using Nameless.NumberConverter.Managers;
using Nameless.NumberConverter.Models;
using Nameless.NumberConverter.Tools;
using Nameless.NumberConverter.ViewModels.Core;
using NumberConverter.Managers;
using NumberConverter.Services;

namespace Nameless.NumberConverter.ViewModels
{
    public class NumberConverterViewModel : NotifiableViewModel
    {
        private readonly NumberConverterService service = new NumberConverterService();

        private string _arabicNumber;
        private string _romanNumber;

        private ICommand _logOutCommand;
        private ICommand _convertNumberCommand;
        private ICommand _showRequestsCommand;
        

        public string ArabicNumber
        {
            get => _arabicNumber;
            set
            {
                _arabicNumber = value;
                OnPropertyChanged();
            }
        }

        public string RomanNumber
        {
            get => _romanNumber;
            set
            {
                _romanNumber = value;
                OnPropertyChanged();
            }
        }

        public bool ValidationError { get; set; }

        public ICommand LogOutCommand =>
            _logOutCommand ?? (_logOutCommand = new RelayCommand(LogOutExecute));

        public ICommand ConvertNumberCommand =>
            _convertNumberCommand ??
            (_convertNumberCommand = new RelayCommand(ConvertNumberExecute, ConvertNumberCanExecute));

        public ICommand ShowRequestsCommand =>
            _showRequestsCommand ?? (_showRequestsCommand = new RelayCommand(ShowRequestsExecute));


        public void LogOutExecute(object o)
        {
            SessionManager.Instance.EndSession();
            NavigationManager.Instance.Navigate(WindowMode.SignIn);
        }

        public void ConvertNumberExecute(object o)
        {
            RomanNumber = string.Empty;
            if (service.TryConvertToUintNumber(_arabicNumber, out uint arabicNumber))
                RomanNumber = service.ExecuteConvertion(arabicNumber);
        }

        public bool ConvertNumberCanExecute(object o)
        {
            return true;
        }

        public void ShowRequestsExecute(object o)
        {
            IList<Request> userRequests = service.GetCurrentUserRequests();

            // TODO. Show the list of user requests
        }
    }
}
