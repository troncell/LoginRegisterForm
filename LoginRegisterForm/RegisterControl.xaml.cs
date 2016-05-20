using LoginRegisterForm.Entity;
using LoginRegisterForm.Service;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LoginRegisterForm
{
    /// <summary>
    /// Interaction logic for RegisterControl.xaml
    /// </summary>
    public partial class RegisterControl : UserControl, INotifyDataErrorInfo
    {
        private readonly Dictionary<string, ICollection<string>> _validationErrors = new Dictionary<string, ICollection<string>>();
        private UserService m_userService;
        public RegisterControl()
        {
            InitializeComponent();
            m_userService = new UserService();
            this.DataContext = this;
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

        private void ValidateUserName([CallerMemberName] string propertyKey = "")
        {
            List<string> errors = null;
            bool isValid = m_userService.ValidateRegisterUserName(m_userName,out errors);
            if(!isValid)
            {
                _validationErrors[propertyKey] = errors;
            }
            else
            {
                _validationErrors.Remove(propertyKey);
            }
            RaiseErrorsChanged(propertyKey);
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

        private void ValidatePassword([CallerMemberName]string propertyKey = "")
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

        private void ValidatePasswordConfirm([CallerMemberName]string propertyKey = "")
        {
            List<string> errors = null;
            bool isValid = m_userService.ValidateConfirmPassword(m_password , m_passwordConfirm, out errors);
            if (!isValid)
            {
                _validationErrors[propertyKey] = errors;
            }
            else
            {
                _validationErrors.Remove(propertyKey);
            }
            RaiseErrorsChanged(propertyKey);
        }

        private void RaiseErrorsChanged(string propertyName)
        {
            if (ErrorsChanged != null)
                ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
        }

        public bool HasErrors
        {
            get { return _validationErrors.Count > 0; }
        }

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName)|| !_validationErrors.ContainsKey(propertyName))

                return null;
            return _validationErrors[propertyName];
        }

        private void ValidateInputs()
        {
            ValidateUserName(nameof(UserName));
            ValidatePassword(nameof(Password));
            ValidatePasswordConfirm(nameof(PasswordConfirm));
        }

        private void register_Click(object sender, RoutedEventArgs e)
        {
            ValidateInputs();
            if (!HasErrors)
            {
                User user = new User();
                user.Username = m_userName;
                user.Password = m_password;
                m_userService.Register(user);
                ClearUserInfo();
                MessageBox.Show("注册成功");
            }
        }

        private void reset_Click(object sender, RoutedEventArgs e)
        {
            ClearUserInfo();
        }

        private void ClearUserInfo()
        {
            userNameBox.Text = "";
            passwordBox.Text = "";
            passwordConfirmBox.Text = "";
        }

        
    }
}
