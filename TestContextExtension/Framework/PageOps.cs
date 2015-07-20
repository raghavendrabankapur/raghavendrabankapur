using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework
{
    public class PageOps
    {
        private readonly Pages _page;

        public PageOps(TestContextExtension extension)
        {
            _page = Pages.Create(extension);
        }

        public void Signin(string username, string password)
        {
            _page.UserNameTextBox.SendKeys(username);
            _page.PasswordTextBox.SendKeys(password);
            _page.SignInButton.Click();
        }
    }
}
