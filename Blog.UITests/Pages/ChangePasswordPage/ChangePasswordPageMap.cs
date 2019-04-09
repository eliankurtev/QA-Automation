

namespace Blog.UITests.Pages.ChangePasswordPage
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using OpenQA.Selenium;

    public partial class ChangePasswordPage


    {

        public IWebElement CurrentPassword => Driver.FindElement(By.Id("OldPassword"));
        public IWebElement NewPassword => Driver.FindElement(By.Id("NewPassword"));
        public IWebElement ConfirmNewPassword => Driver.FindElement(By.Id("ConfirmPassword"));
        public IWebElement ChangePasswordButton => Driver.FindElement(By.XPath("/html/body/div[2]/div/div/form/div[5]/div/input"));

        public IWebElement ErrorMessage => Driver.FindElement(By.XPath(@"/html/body/div[2]/div/div/form/div[1]/ul/li"));
        public IWebElement FieldIsRequired => Driver.FindElement(By.XPath(@"html/body/div[2]/div/div/form/div[1]/ul/li"));
        
    }
}
