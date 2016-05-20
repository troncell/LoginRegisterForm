using LoginRegisterForm.Service;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
    /// Interaction logic for LoginControl.xaml
    /// </summary>
    public partial class LoginControl : UserControl, INotifyDataErrorInfo
    {
        private readonly Dictionary<string, ICollection<string>> _validationErrors = new Dictionary<string, ICollection<string>>();
        private UserService m_userService;
        public LoginControl()
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
                m_userName = value;            }
        }

        private bool ValidateUserName([CallerMemberName] string propertyKey = "")
        {
            List<string> errors = null;
            if(!m_userService.ValidateUserName(m_userName, out errors))
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
            if (!m_userService.Login(m_userName,m_password, out errors))
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



        private bool ValidateInputs()
        {
            return ValidateUserName(nameof(UserName)) && ValidatePassword(nameof(Password));
        }

        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            bool success = ValidateInputs();
            Debug.Assert(success == !HasErrors);
            if(!HasErrors)
            {
                MessageBox.Show("登陆成功");
                ClearLoginInfo();
            }
          
        }

        private void resetBtn_Click(object sender, RoutedEventArgs e)
        {
            ClearLoginInfo();
        }

        private void ClearLoginInfo()
        {
            userNameBox.Text = "";
            passwordBox.Text = "";
        }
    }
}
