using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginRegisterForm.ViewModel
{
    [ExportNavigable(NavigableContextName = DefaultNavigableContexts.RegisterSuccessScreen)]

    public class RegisterSuccessViewModel : ViewModelBase
    {
        private string m_userName;
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
