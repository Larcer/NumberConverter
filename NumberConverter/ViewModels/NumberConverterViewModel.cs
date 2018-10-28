﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
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
        private ObservableCollection<Request> _requests;

        private Visibility _requestsPanelVisibility = Visibility.Collapsed;
        private Visibility _requestsListVisibility = Visibility.Collapsed;
        private Visibility _requestsEmptyMessageVisibility = Visibility.Visible;

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

        public Visibility RequestsEmptyMessageVisibility
        {
            get => _requestsEmptyMessageVisibility;
            set
            {
                _requestsEmptyMessageVisibility = value;
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
            SessionManager.Instance.EndSession();
            NavigationManager.Instance.Navigate(WindowMode.SignIn);
        }

        public void ConvertNumberExecute(object o)
        {
            RomanNumber = string.Empty;
            if (service.TryConvertToUintNumber(_arabicNumber, out uint arabicNumber))
            {
                RomanNumber = service.ExecuteConvertion(arabicNumber, out Request request);
                AddRequest(request);
            }
        }

        private void AddRequest(Request request)
        {
            if (RequestsPanelVisibility == Visibility.Visible)
            {
                Requests.Add(request);
                if (RequestsEmptyMessageVisibility == Visibility.Visible)
                {
                    RequestsEmptyMessageVisibility = Visibility.Collapsed;
                    RequestsListVisibility = Visibility.Visible;
                }
            }
        }

        public bool ConvertNumberCanExecute(object o)
        {
            return true;
        }

        public void ShowRequestsExecute(object o)
        {
            IList<Request> userRequests = service.GetCurrentUserRequests();
            Requests = new ObservableCollection<Request>(userRequests);
            if (Requests.Count > 0)
            {
                RequestsEmptyMessageVisibility = Visibility.Collapsed;
                RequestsListVisibility = Visibility.Visible;
            }
            RequestsPanelVisibility = Visibility.Visible;
        }
    }
}
