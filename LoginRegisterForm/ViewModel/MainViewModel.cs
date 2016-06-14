using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginRegisterForm.ViewModel
{
    [Export(typeof(MainViewModel))]

    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
        }
    }
}
