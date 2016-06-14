using LoginRegisterForm.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Runtime.CompilerServices;

namespace LoginRegisterForm.ViewModel
{
    [ExportNavigable(NavigableContextName = DefaultNavigableContexts.LoginScreen)]
    public class LoginViewModel : ViewModelBase,INotifyDataErrorInfo
    {
        private readonly Dictionary<string, ICollection<string>> _validationErrors = new Dictionary<string, ICollection<string>>();
        private UserService m_userService;

        private string m_userName;
        public string UserName
        {
            get { return m_userName; }
            set
            {
                m_userName = value;
            }
        }

        private bool ValidateUserName([CallerMemberName] string propertyKey = "")
        {
            List<string> errors = null;
            if (!m_userService.ValidateUserName(m_userName, out errors))
            {
                _validationErrors[propertyKey] = errors;
            }
            else
            {
                _validationErrors.Remove(propertyKey);
            }
            RaiseErrorsChanged(propertyKey);
            return errors == null || errors.Count == 0;
        }

        private string m_password;
        public string Password
        {
            get { return m_password; }
            set
            {
                m_password = value;
            }
        }

        private bool ValidatePassword([CallerMemberName] string propertyKey = "")
        {
            List<string> errors = null;
            if (!m_userService.Login(m_userName, m_password, out errors))
            {
                _validationErrors[propertyKey] = errors;
            }
            else
            {
                _validationErrors.Remove(propertyKey);
            }
            RaiseErrorsChanged(propertyKey);
            return errors == null || errors.Count == 0;
        }

        public bool HasErrors
        {
            get
            {
                return _validationErrors.Count > 0;
            }
        }


        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        private void RaiseErrorsChanged(string propertyName)
        {
            if (ErrorsChanged != null)
                ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
        }


        public IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName) || !_validationErrors.ContainsKey(propertyName))

                return null;
            return _validationErrors[propertyName];
        }


        public bool ValidateInputs()
        {
            return ValidateUserName(nameof(UserName)) && ValidatePassword(nameof(Password));
        }

        public void ClearLoginInfo()
        {
            UserName = "";
            Password = "";
            _validationErrors.Clear();
            RaiseErrorsChanged(nameof(UserName));
            RaiseErrorsChanged(nameof(Password));
            OnPropertyChanged(nameof(UserName));
            OnPropertyChanged(nameof(Password));
        }

        public LoginViewModel()
        {
            m_userService = new UserService();

        }

    }
}
