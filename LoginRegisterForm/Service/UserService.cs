using LoginRegisterForm.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoginRegisterForm.Entity;

namespace LoginRegisterForm.Service
{
    public class UserService : IUserService
    {
        public UserService()
        {
        }

        public bool Login(string userName, string password, out List<string> errors)
        {
            errors = new List<string>();
            var user = XMLDB.Instance.Users.SingleOrDefault(u => u.Username == userName && u.Password == password);
            if(user == null)
            {
                errors.Add("用户名与密码不一至");
            }
            return user != null;
        }

        public void Register(User user)
        {
            XMLDB.Instance.Users.Add(user);
            XMLDB.Instance.SaveChanged();
        }

        public bool ValidateConfirmPassword(string password, string confirmPassword, out List<string> errors)
        {
            errors = new List<string>();
            if(confirmPassword != password)
                errors.Add("密码不一至");
            return errors.Count == 0;
        }

        public bool ValidatePassword(string password, out List<string> errors)
        {
            errors = new List<string>();
            if (string.IsNullOrEmpty(password))
            {
                errors.Add("密码不能为空");
                return false;
            }
            if (password.Length < 6)
                errors.Add("到少6位字符");
            return errors.Count == 0;
        }

        public bool ValidateRegisterUserName(string userName, out List<string> errors)
        {
            errors = new List<string>();
            if (string.IsNullOrEmpty(userName))
                errors.Add("用户名不能为空");
            if (XMLDB.Instance.Users.Any(u => u.Username == userName))
                errors.Add("用户名已存在");
            return errors.Count == 0;
        }

        public bool ValidateUserName(string userName, out List<string> errors)
        {
            errors = new List<string>();
            if (!XMLDB.Instance.Users.Any(u => u.Username == userName))
                errors.Add("用户名不存在");
            return errors.Count == 0;
        }

    }
}
