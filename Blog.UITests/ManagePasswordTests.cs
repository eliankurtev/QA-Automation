namespace Blog.UITests

{
    using System;
    using System.IO;
    using System.Reflection;
    using Blog.UITests.Models;
    using Blog.UITests.Pages;
    using Blog.UITests.Pages.ChangePasswordPage;
    using Blog.UITests.Pages.HomePage;
    using Blog.UITests.Pages.HomePageOfLoggedUser;
    using Blog.UITests.Pages.LoginPage;
    using FluentAssertions;
    using NUnit.Framework;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Support.UI;

    [TestFixture]
    public class ManagePasswordTests
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        private HomePage homePage;
        private LoginPage loginPage;
        private ManagePage managePage;
        private HomePageOfLoggedUser homePageOfLoggedUser;
        private ChangePasswordPage changePasswordPage;
        private String DefaultEmail = "borqnavassileva@gmail.com";
        private String Url = "http://blogservice.azurewebsites.net/";

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(this.Url);

            loginPage = new LoginPage(driver);
            homePageOfLoggedUser = new HomePageOfLoggedUser(driver);
            changePasswordPage = new ChangePasswordPage(driver);
            homePage = new HomePage(driver);
            managePage = new ManagePage(driver);
        }

        [Test]
        [Description("TC 08.01")]
        [Order(1)]
        [Author("Boryana Hristova")]
        public void SoftuniBlogButton()
        {

            IWebElement SoftuniBlogButton = wait.Until((driver) => driver.FindElement(By.XPath("/html/body/div[1]/div/div[1]/a")));
            SoftuniBlogButton.Click();
            String ArticleList = driver.Url;
            Assert.AreEqual(ArticleList, "http://blogservice.azurewebsites.net/Article/List");
        }

        [Test]
        [Description("TC 09.01")]
        [Order(2)]
        [Author("Boryana Hristova")]
        public void LogOff()
        {
            var path = Path.GetFullPath(Directory
                .GetCurrentDirectory() +
                "/../../../Jsons/Login/BoryanaValidCredentials.json");
            var user = User.UserFromJson(File.ReadAllText(path));
            homePage.logInButton.Click();
            loginPage.fillLoginForm(user);
            homePageOfLoggedUser.LogOffButton.Click();
            String HomePage = driver.Url;
            Assert.AreEqual(HomePage, "http://blogservice.azurewebsites.net/Article/List");
        }

        [Test]
        [Description("TC 10.01")]
        [Order(3)]
        [Author("Boryana Hristova")]
        public void HelloEmail()
        {
            var path = Path.GetFullPath(Directory
                .GetCurrentDirectory() +
                "/../../../Jsons/Login/BoryanaValidCredentials.json");
            var user = User.UserFromJson(File.ReadAllText(path));
            homePage.logInButton.Click();
            loginPage.fillLoginForm(user);
            homePageOfLoggedUser.HelloButton.Click();
            String ManagePage = driver.Url;
            Assert.AreEqual(ManagePage, "http://blogservice.azurewebsites.net/Manage");
        }

        [Test]
        [Description("TC 07.01")]
        [Order (4)]
        [Author("Boryana Hristova")]
        public void ChangePasswordPage()
        {
            var path = Path.GetFullPath(Directory
                .GetCurrentDirectory() +
                "/../../../Jsons/Login/BoryanaValidCredentials.json");
            var user = User.UserFromJson(File.ReadAllText(path));
            homePage.logInButton.Click();
            loginPage.fillLoginForm(user);
            homePageOfLoggedUser.HelloButton.Click();
            managePage.ChangeYourPasswordButton.Click();
            String ChangePage = driver.Url;
            Assert.AreEqual(ChangePage, "http://blogservice.azurewebsites.net/Manage/ChangePassword");
        }

        [Test]
        [Description("TC 07.02")]
        [Order(5)]
        [Author("Boryana Hristova")]
        [TestCase("/../../../Jsons/PasswordActions/ChangePasswordOneLetter.json")]
        [TestCase("/../../../Jsons/PasswordActions/ChangePasswordDigits.json")]
        [TestCase("/../../../Jsons/PasswordActions/ChangePasswordLetters.json")]
        [TestCase("/../../../Jsons/PasswordActions/ChangePasswordLettersAndDigits.json")]
        [TestCase("/../../../Jsons/PasswordActions/ChangePasswordOneSymbol.json")]
        [TestCase("/../../../Jsons/PasswordActions/ChangePasswordUppercase.json")]
        [TestCase("/../../../Jsons/PasswordActions/ChangePasswordLowercase.json")]
        [TestCase("/../../../Jsons/PasswordActions/ChangePasswordEmpty.json")]
        [TestCase("/../../../Jsons/PasswordActions/ChangePasswordSpaces.json")]
        [TestCase("/../../../Jsons/PasswordActions/ChangePasswordMoreThan50S.json")]
        [TestCase("/../../../Jsons/PasswordActions/ChangePasswordLessThan6S.json")]
        public void WrongPasswordInput(string path)
        {
            var testCasePath = Path.GetFullPath(Directory.GetCurrentDirectory() + path);
            var currentpassword = ChangePassword.ChangePasswordFromJson(File.ReadAllText(testCasePath)).CurrentPassword;
            homePage.logInButton.Click();
            loginPage.EmailField.SendKeys(this.DefaultEmail);
            loginPage.PasswordField.SendKeys(currentpassword);
            loginPage.LoginButton.Click();
            homePageOfLoggedUser.HelloButton.Click();
            managePage.ChangeYourPasswordButton.Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            var password = ChangePassword.ChangePasswordFromJson(File.ReadAllText(testCasePath));
            changePasswordPage.FillForm(password);
        }
   
        [Test]
        [Description("TC 07.03 and TC 07.04")]
        [Order(16)]
        [Author("Boryana Hristova")]
        public void ChangePasswordValid()
        {
            var path = Path.GetFullPath(
            Directory.GetCurrentDirectory() +
            "/../../../Jsons/PasswordActions/ChangePasswordValid.json");

            var currentpassword = ChangePassword.ChangePasswordFromJson(File.ReadAllText(path)).CurrentPassword;

            driver.Navigate().GoToUrl(this.Url);
            homePage.logInButton.Click();
            loginPage.EmailField.SendKeys(this.DefaultEmail);
            loginPage.PasswordField.SendKeys(currentpassword);
            loginPage.LoginButton.Click();
            homePageOfLoggedUser.HelloButton.Click();
            managePage.ChangeYourPasswordButton.Click();
            
            var password = ChangePassword.ChangePasswordFromJson(File.ReadAllText(path));
            changePasswordPage.FillForm(password);
        }

        [Test]
        [Description("TC 07.05")]
        [Order(17)]
        [Author("Boryana Hristova")]
        public void ChangePasswordDoesNoMaches()
        {
            var path = Path.GetFullPath(
            Directory.GetCurrentDirectory() +
            "/../../../Jsons/PasswordActions/ChangePasswordDoesNoMaches.json");

            var currentpassword = ChangePassword.ChangePasswordFromJson(File.ReadAllText(path)).CurrentPassword;

            driver.Navigate().GoToUrl(this.Url);
            homePage.logInButton.Click();
            loginPage.EmailField.SendKeys(this.DefaultEmail);
            loginPage.PasswordField.SendKeys(currentpassword);
            loginPage.LoginButton.Click();
            homePageOfLoggedUser.HelloButton.Click();
            managePage.ChangeYourPasswordButton.Click();


            var password = ChangePassword.ChangePasswordFromJson(File.ReadAllText(path));
            changePasswordPage.FillForm(password);
            changePasswordPage.ChangePasswordButton.Click();
            changePasswordPage.ErrorMessage.Displayed.Should().BeTrue();
        }
     
        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}
