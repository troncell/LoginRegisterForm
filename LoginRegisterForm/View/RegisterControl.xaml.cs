using LoginRegisterForm.Entity;
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
    /// Interaction logic for RegisterControl.xaml
    /// </summary>
    public partial class RegisterControl : UserControl
    {
        RegisterViewModel m_vm;
        private UserService m_userService;

        public RegisterControl()
        {
            InitializeComponent();
            this.Loaded += (s, e) => 
            {
                m_vm = this.DataContext as RegisterViewModel;
            };
            m_userService = new UserService();
        }


        private void register_Click(object sender, RoutedEventArgs e)
        {
            
            bool success = m_vm.ValidateInputs();
            Debug.Assert(success == !m_vm.HasErrors);
            if (!m_vm.HasErrors)
            {
                m_userService.Register(new User { Username = m_vm.UserName,Contact = m_vm.Contact, Password = m_vm.Password});
                m_vm.NavigationManager.NavigateTo(DefaultNavigableContexts.RegisterSuccessScreen, m_vm.UserName);
                m_vm.ClearUserInfo();
            }
        }

        private void reset_Click(object sender, RoutedEventArgs e)
        {
            m_vm.ClearUserInfo();
        }

      

        
    }
}
