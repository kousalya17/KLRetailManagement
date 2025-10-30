using Caliburn.Micro;

namespace KRMDesktopUI.ViewModels
{
    public class LoginViewModel : Screen
    {
        private string _userName;
        private string _password;

        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                NotifyOfPropertyChange(() => UserName);
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                NotifyOfPropertyChange(() => Password);
            }
        }


        public bool CanLogIn
        {
            get
            {
                bool result = false;

                if (UserName?.Length > 0 && Password?.Length > 0)
                {
                    result = true;
                }
                return result;
            }
        }

        public void LogIn(string username, string password)
        {
        }
    }
}
