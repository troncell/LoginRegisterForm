using LoginRegisterForm.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for RegisterSuccess.xaml
    /// </summary>
    public partial class RegisterSuccess : UserControl
    {
        public RegisterSuccess()
        {
            InitializeComponent();
            this.Loaded += (s, e) => 
            {
                this.DataContext = new RegisterSuccessViewModel();
            };
        }
    }
}
