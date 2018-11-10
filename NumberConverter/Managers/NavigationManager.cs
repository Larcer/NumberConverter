using System;
using System.Collections.Generic;
using System.Windows.Controls;

using Nameless.NumberConverter.Tools;
using Nameless.NumberConverter.Windows;

namespace Nameless.NumberConverter.Managers
{
    // The application windows modes (basically their names)
    public enum WindowMode
    {
        SignIn,
        SignUp,
        NumberConverter
    }

    // Handles navigation between windows
    public class NavigationManager : SingletonBase<NavigationManager>
    {
        // The content window to place windows in
        private readonly ContentWindow _contentWindow;
        // The list of application views
        private readonly IDictionary<WindowMode, ContentControl> _views;

        private NavigationManager()
        {
            _contentWindow = new ContentWindow();
            _contentWindow.Show();

            _views = InitializeDictionary();
        }

        // Navigates to the specified view
        public void Navigate(WindowMode mode)
        {
            ContentControl view = _views[mode];
            if (view == null)
                _views[mode] = (view = CreateWindowMode(mode));

            _contentWindow.ContentControl.Content = view;
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
            string fullName = GetFullViewName(mode);
            Type viewType = Type.GetType(fullName);
            object viewObject = Activator.CreateInstance(viewType);

            return (ContentControl)viewObject;
        }

        // Returns full view name including namespace and suffix 'View'
        private string GetFullViewName(WindowMode mode)
        {
            return "Nameless.NumberConverter.Views." + mode.ToString() + "View";
        }
    }
}
