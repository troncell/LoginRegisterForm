using LoginRegisterForm.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginRegisterForm.Service
{
    public interface IUserService
    {
        bool ValidateRegisterUserName(string userName, out List<string> errors);
        bool ValidatePassword(string password, out List<string> errors);
        bool ValidateConfirmPassword(string password, string confirmPassword, out List<string> errors);
        void Register(User user);
        bool Login(string userName, string password, out List<string> errors);
        bool ValidateUserName(string userName, out List<string> errors);
    }
}
