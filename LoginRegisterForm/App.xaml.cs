using LoginRegisterForm.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace LoginRegisterForm
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            //if (!this.disposed)
            //{
            //    if (disposing)
            //    {
            //        this.catalog.Dispose();
            //        this.compositionContainer.Dispose();
            //    }
            //}

            //this.disposed = true;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            this.Dispose();
        }
    }
}
