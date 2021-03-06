﻿using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Nameless.NumberConverter.Managers;
using Nameless.NumberConverter.Models;
using Nameless.NumberConverter.Tools;
using Nameless.NumberConverter.ViewModels.Core;
using Nameless.NumberConverter.Services;

namespace Nameless.NumberConverter.ViewModels
{
    
    public class NumberConverterViewModel : NotifiableViewModel
    {
        private readonly NumberConverterService _service = new NumberConverterService();

        private string _arabicNumber;
        private string _romanNumber;
        private ObservableCollection<Request> _requests;

        private Visibility _requestsPanelVisibility = Visibility.Collapsed;
        private Visibility _requestsListVisibility = Visibility.Collapsed;

        // Gets user to log out
        private ICommand _logOutCommand;
        // Converts arabic number to the roman number
        private ICommand _convertNumberCommand;
        // Shows user requests
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

        public ObservableCollection<Request> Requests
        {
            get => _requests;
            set
            {
                _requests = value;
                OnPropertyChanged();
            }
        }

        public Visibility RequestsPanelVisibility
        {
            get => _requestsPanelVisibility;
            set
            {
                _requestsPanelVisibility = value;
                OnPropertyChanged();
            }
        }

        public Visibility RequestsListVisibility
        {
            get => _requestsListVisibility;
            set
            {
                _requestsListVisibility = value;
                OnPropertyChanged();
            }
        }

        public ICommand LogOutCommand =>
            _logOutCommand ?? (_logOutCommand = new RelayCommand(LogOutExecute));

        public ICommand ConvertNumberCommand =>
            _convertNumberCommand ??
            (_convertNumberCommand = new RelayCommand(ConvertNumberExecute, ConvertNumberCanExecute));

        public ICommand ShowRequestsCommand =>
            _showRequestsCommand ?? (_showRequestsCommand = new RelayCommand(ShowRequestsExecute));
        
        public void LogOutExecute(object o)
        {
            _service.LogOut();
            NavigationManager.Instance.Navigate(WindowMode.SignIn);

            ArabicNumber = RomanNumber = string.Empty;
            RequestsPanelVisibility = Visibility.Collapsed;
            RequestsListVisibility = Visibility.Collapsed;
        }

        public async void ConvertNumberExecute(object o)
        {
            ContentWindowViewModel.Instance.ShowLoader();

            Request request = null;
            var success = await Task.Run(() =>
            {
                RomanNumber = string.Empty;
                if (_service.TryConvertToUintNumber(_arabicNumber, out int arabicNumber))
                {
                    RomanNumber = _service.ExecuteConversion(arabicNumber, out request);
                    return true;
                }

                return false;
            });

            if (success)
                AddRequest(request);

            ContentWindowViewModel.Instance.HideLoader();
        }

        private void AddRequest(Request request)
        {
            if (RequestsPanelVisibility == Visibility.Visible)
            {
                Requests.Add(request);
            }
        }

        public bool ConvertNumberCanExecute(object o)
        {
            return true;
        }

        public async void ShowRequestsExecute(object o)
        {
            ContentWindowViewModel.Instance.ShowLoader();

            await Task.Run(() =>
            {
                var userRequests = _service.GetCurrentUserRequests();
                Requests = new ObservableCollection<Request>(userRequests);
                if (Requests.Count > 0)
                    RequestsListVisibility = Visibility.Visible;

                RequestsPanelVisibility = Visibility.Visible;
            });
            
            ContentWindowViewModel.Instance.HideLoader();
        }
    }
}
