/*namespace Blog.UITests
{
    using NUnit.Framework;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Remote;
    using System;

    [TestFixture]
    public class Driver
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            // Create a new instance of the Chrome driver
            var options = new ChromeOptions
            {
                PlatformName = "windows"
            };
            options.AddAdditionalCapability("platform", "windows", true);
            options.AddAdditionalCapability("capabilityName", "chrome", true);
            options.AddAdditionalCapability("video", "True", true);

            driver = new RemoteWebDriver(new Uri("http://192.168.112.1:4444/wd/hub"), options.ToCapabilities(),TimeSpan.FromSeconds(600));
        }

        [TearDown]
        public void Teardown()
        {
            driver.Quit();
        }

        [Test]
        public void SeleniumGrid()
        {
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://blogservice.azurewebsites.net");
            Assert.AreEqual("List - My ASP>NET Application", driver.Title);
            Assert.AreEqual("© 2019 - SoftUni Blog", driver.FindElement(By.TagName("footer")).Text);
        }
    }
}
*/
