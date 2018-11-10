using System;
using System.Windows;

using Nameless.NumberConverter.Tools;

namespace Nameless.NumberConverter.Managers
{
    // Manages printing messages to some stream (message box, console, file, debug log...)
    public static class MessageManager
    {
        // Prints message to the MessageBox
        public static void UserMessage(string message)
        {
            MessageBox.Show(message);
        }
        
        // Logs message to a file
        public static void Log(string message)
        {
            Logger.Log(message);
        }

        // Logs message and exception to a file
        public static void Log(string message, Exception ex)
        {
            Logger.Log(message, ex);
        }

        // Logs exception to a file
        public static void Log(Exception ex)
        {
            Logger.Log(ex);
        }
    }
}
