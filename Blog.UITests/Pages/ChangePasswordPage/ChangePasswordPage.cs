namespace Blog.UITests.Pages.ChangePasswordPage

{
    using System;
    using Blog.UITests.Models;
    using OpenQA.Selenium;



    public partial class ChangePasswordPage : BasePage
    {

        public ChangePasswordPage(IWebDriver driver) : base(driver)
        {
        }


        public void FillForm(ChangePassword password)
        {

            CurrentPassword.SendKeys(password.CurrentPassword);
            NewPassword.SendKeys(password.NewPassword);
            ConfirmNewPassword.SendKeys(password.ConfirmNewPassword);
            ChangePasswordButton.Click();

        }

      
    }

}