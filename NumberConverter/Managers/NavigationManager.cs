﻿using System;
using System.Collections.Generic;
using System.Windows.Controls;
using Nameless.NumberConverter.Tools;
using Nameless.NumberConverter.ViewModels;

namespace Nameless.NumberConverter.Managers
{
    // The application windows' modes (basically their names)
    internal enum WindowMode
    {
        SignIn,
        SignUp,
        NumberConverter
    }

    // Handles navigation between windows
    internal class NavigationManager : SingletonBase<NavigationManager>
    {
        // The list of application views
        private readonly IDictionary<WindowMode, ContentControl> _views;

        private NavigationManager()
        {
            _views = InitializeDictionary();
        }

        // Navigates to the specified view
        internal void Navigate(WindowMode mode)
        {
            var view = _views[mode];
            if (view == null)
                _views[mode] = (view = CreateWindowMode(mode));
            
            ContentWindowViewModel.Instance.ReplaceContent(view);
        }

        // Returns dictionary with enum items as keys and nulls as values
        private IDictionary<WindowMode, ContentControl> InitializeDictionary()
        {
            IDictionary<WindowMode, ContentControl> views = new Dictionary<WindowMode, ContentControl>();
            foreach (WindowMode windowMode in Enum.GetValues(typeof(WindowMode)))
                views.Add(windowMode, null);

            return views;
        }

        // Returns created view that refers to the specified window mode.
        // Uses reflection to create the content control
        private ContentControl CreateWindowMode(WindowMode mode)
        {
            var fullName = GetFullViewName(mode);
            var viewType = Type.GetType(fullName);
            var viewObject = Activator.CreateInstance(viewType);

            return (ContentControl) viewObject;
        }

        // Returns full view name including namespace and suffix 'View'
        private string GetFullViewName(WindowMode mode)
        {
            return "Nameless.NumberConverter.Views." + mode.ToString() + "View";
        }
    }
}
