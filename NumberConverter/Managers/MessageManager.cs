using System;
using System.Windows;
using Nameless.NumberConverter.Tools;

namespace Nameless.NumberConverter.Managers
{
    // Manages printing messages to some stream (message box, console, file, debug log...)
    internal static class MessageManager
    {
        // Prints message to the MessageBox
        internal static void UserMessage(string message)
        {
            MessageBox.Show(message);
        }
        
        // Logs message to a file
        internal static void Log(string message)
        {
            Logger.Log(message);
        }

        // Logs message and exception to a file
        internal static void Log(string message, Exception ex)
        {
            Logger.Log(message, ex);
        }

        // Logs exception to a file
        internal static void Log(Exception ex)
        {
            Logger.Log(ex);
        }
    }
}
