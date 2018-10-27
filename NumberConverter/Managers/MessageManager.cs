using System.Windows;

namespace NumberConverter.Managers
{
    // Manages printing messages to some stream (message box, console, file, debug log...)
    public static class MessageManager
    {
        // Prints message to the MessageBox
        public static void UserMessage(string message)
        {
            MessageBox.Show(message);
        }
    }
}
