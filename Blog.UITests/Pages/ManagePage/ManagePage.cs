

namespace Blog.UITests.Pages
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using OpenQA.Selenium;

    public class ManagePage : BasePage
    {
        public ManagePage(IWebDriver driver) : base(driver)
        {
        }

        public IWebElement ChangeYourPasswordButton => Driver.FindElement(By.XPath("/html/body/div[2]/div/dl/dd/a"));
       
    }
}
