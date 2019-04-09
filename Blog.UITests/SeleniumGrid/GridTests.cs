namespace Blog.UITests
{
    using OpenQA.Selenium.Remote;
    using OpenQA.Selenium.Chrome;
    using System;
    using OpenQA.Selenium;
    using NUnit.Framework;

    [TestFixture]
    class GridTests
    {
        [Test]
        public void SeleniumStart()
        {
            IWebDriver driver = GetChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://blogservice.azurewebsites.net");
            Assert.AreEqual("List - My ASP.NET Application", driver.Title);
//            Assert.AreEqual("© 2019 - SoftUni Blog", driver.FindElement(By.TagName("footer")).Text);
            driver.Quit();
        }

        private RemoteWebDriver GetChromeDriver()
        {
            //var path = "D:\\chromedriver_win32\\"; // Add ChromeWebDriver variable to the SYSTEM environment variables!
            var path = Environment.GetEnvironmentVariable("ChromeWebDriver", EnvironmentVariableTarget.Machine);
            var options = new ChromeOptions();
            options.AddArguments("--no-sandbox");

            if (!string.IsNullOrWhiteSpace(path))
            {
                return new ChromeDriver(path, options, TimeSpan.FromSeconds(300));
            }
            else
            {
                return new ChromeDriver(options);
            }
        }
    }
}
