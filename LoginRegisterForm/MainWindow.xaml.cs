using LoginRegisterForm.Entity;
using LoginRegisterForm.Repository;
using LoginRegisterForm.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
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
        private bool disposed = false;

        private AssemblyCatalog catalog;

        /// <summary>
        /// Managed Entity Framework composition container used to compose the entity graph
        /// </summary>
        private CompositionContainer compositionContainer;

        public MainWindow()
        {
            InitializeComponent();
         
            this.Loaded += (s, e) =>
            {
                this.catalog = new AssemblyCatalog(typeof(MainViewModel).Assembly);
                this.compositionContainer = new CompositionContainer(this.catalog);
                var vm = this.compositionContainer.GetExportedValue<MainViewModel>();
                vm.NavigationManager.NavigateTo(DefaultNavigableContexts.LoginScreen);
                this.DataContext = vm;

            };
        }
    }

   
}
