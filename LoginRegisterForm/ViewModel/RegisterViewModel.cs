using LoginRegisterForm.Service;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LoginRegisterForm.ViewModel
{
    public class RegisterViewModel: INotifyDataErrorInfo, INotifyPropertyChanged
    {
        private readonly Dictionary<string, ICollection<string>> _validationErrors = new Dictionary<string, ICollection<string>>();
        private UserService m_userService;

        public RegisterViewModel()
        {
            m_userService = new UserService();
        }


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
            bool isValid = m_userService.ValidateRegisterUserName(m_userName, out errors);
            if (!isValid)
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

        private bool ValidatePassword([CallerMemberName]string propertyKey = "")
        {
            List<string> errors = null;
            bool isValid = m_userService.ValidatePassword(m_password, out errors);
            if (!isValid)
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

        private string m_passwordConfirm;
        public string PasswordConfirm
        {
            get { return m_passwordConfirm; }
            set
            {
                m_passwordConfirm = value;
            }
        }

        private bool ValidatePasswordConfirm([CallerMemberName]string propertyKey = "")
        {
            List<string> errors = null;
            bool isValid = m_userService.ValidateConfirmPassword(m_password, m_passwordConfirm, out errors);
            if (!isValid)
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
            get { return _validationErrors.Count > 0; }
        }

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        private void RaiseErrorsChanged(string propertyName)
        {
            if (ErrorsChanged != null)
                ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
        }

        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName) || !_validationErrors.ContainsKey(propertyName))

                return null;
            return _validationErrors[propertyName];
        }

        public bool ValidateInputs()
        {
           return ValidateUserName(nameof(UserName)) &&
                  ValidatePassword(nameof(Password)) &&
                  ValidatePasswordConfirm(nameof(PasswordConfirm));
        }

        public void ClearUserInfo()
        {
            UserName = "";
            Password = "";
            PasswordConfirm = "";
            RaisePropertyChanged(nameof(UserName));
            RaisePropertyChanged(nameof(Password));
            RaisePropertyChanged(nameof(PasswordConfirm));
            _validationErrors.Clear();
            RaiseErrorsChanged(nameof(UserName));
            RaiseErrorsChanged(nameof(Password));
            RaiseErrorsChanged(nameof(PasswordConfirm));
        }
    }
}
