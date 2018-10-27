using System.Diagnostics;
using Nameless.NumberConverter.Models;
using Nameless.NumberConverter.Tools;

namespace NumberConverter.Managers
{
    // Manages user sessions
    public class SessionManager : SingletonBase<SessionManager>
    {
        public User CurrentUser { get; private set; }

        private SessionManager()
        {

        }

        // Starts user session
        public void StartSession(User user)
        {
            Debug.Assert(user != null);
            CurrentUser = user;
        }

        public void EndSession()
        {
            CurrentUser = null;
        }
    }
}
