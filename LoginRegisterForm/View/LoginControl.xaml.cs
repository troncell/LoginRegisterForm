using LoginRegisterForm.Service;
using LoginRegisterForm.ViewModel;
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

namespace LoginRegisterForm.View
{
    /// <summary>
    /// Interaction logic for LoginControl.xaml
    /// </summary>
    public partial class LoginControl : UserControl
    {
        LoginViewModel m_vm;

        public LoginControl()
        {
            InitializeComponent();
            this.Loaded += (s, e) => 
            {
                m_vm = this.DataContext as LoginViewModel;
            };
        }


        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            bool success = m_vm.ValidateInputs();
            Debug.Assert(success == !m_vm.HasErrors);
            if(!m_vm.HasErrors)
            {
                m_vm.NavigationManager.NavigateTo(DefaultNavigableContexts.LoginSuccessScreen, m_vm.UserName);
                m_vm.ClearLoginInfo();
            }
          
        }

        private void resetBtn_Click(object sender, RoutedEventArgs e)
        {
            // m_vm.ClearLoginInfo();
            m_vm.NavigationManager.NavigateTo(DefaultNavigableContexts.RegisterScreen);
        }

    
    }
}
