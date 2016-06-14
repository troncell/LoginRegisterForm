using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginRegisterForm.ViewModel
{
    [ExportNavigable(NavigableContextName = DefaultNavigableContexts.LoginSuccessScreen)]
    public class LoginSuccessViewModel :ViewModelBase
    {
        private string m_userName = "Hello";
        public string UserName
        {
            get { return m_userName; }
            set
            {
                m_userName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }

        public override void Initialize(object parameter)
        {
            base.Initialize(parameter);
            UserName = parameter as string;
        }

    }
}
