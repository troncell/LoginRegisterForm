using LoginRegisterForm.Entity;
using LoginRegisterForm.Repository;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel vm;

        public MainWindow(MainViewModel viewModel)
        {
            InitializeComponent();
            vm = viewModel;
            this.DataContext = vm;
            this.Loaded += (s, e) =>
            {
                vm.NavigationManager.NavigateTo(DefaultNavigableContexts.LoginScreen);
            };
        }

        protected override void OnTouchLeave(TouchEventArgs e)
        {
            base.OnTouchLeave(e);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
          
           
        }
    }

   
}
