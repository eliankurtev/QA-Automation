using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Reflection;
using System.Linq;

namespace Blog.UITests
{
    [TestFixture]
    class EditArticle
    {
        private IWebDriver driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
        private WebDriverWait wait;
        private String localhostBlog = "http://blogservice.azurewebsites.net/";
        private String Email = "tinkiwinki@testmail.com";
        private String Password = "tinkiwinki";

        private static Random random = new Random();

        [SetUp]
        public void SetUp()
        {
            driver.Manage().Window.FullScreen();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        [TearDown]
        public void TearDown()
        {
            // driver.Quit();
        }

        [Test, Order(1), Author("Elitsa Draganova")]
        public void LogInPage()
        {
            driver.Navigate().GoToUrl(localhostBlog);

            IWebElement LoginButton = wait.Until((d) => {
                return d.FindElement(By.XPath("//*[@id='loginLink']"));
            });

            LoginButton.Click();
        }

        [Test, Order(2), Author("Elitsa Draganova")]
        public void LogIn()
        {
            IWebElement Email = wait.Until((d) => {
                return d.FindElement(By.XPath("//*[@id='Email']"));
            });

            IWebElement Password = wait.Until((d) => {
                return d.FindElement(By.XPath("//*[@id='Password']"));
            });

            IWebElement SubmitButton = wait.Until((d) => {
                return d.FindElement(By.XPath("/html/body/div[2]/div/div/form/div[4]/div/input"));
            });

            Email.Clear();
            Email.SendKeys(this.Email);

            Password.Clear();
            Password.SendKeys(this.Password);

            SubmitButton.Submit();
        }

        [Test, Order(3), Author("Elitsa Draganova")]
        public void IsUserLogIn()
        {

            IWebElement LogOutButton = wait.Until((d) => {
                return d.FindElement(By.XPath("//*[@id='logoutForm']/ul/li[3]/a"));
            });

            Assert.IsTrue(LogOutButton.Text.Equals("Log off"));
        }

        [Test, Order(4), Author("Elitsa Draganova")]
        public void NavigateToArticles()
        {
            IWebElement CurrentArticle = wait.Until((d) => {
                return d.FindElement(By.XPath("/html/body/div[2]/div/div/div[11]/article/header/h2/a"));
            });

            CurrentArticle.Click();
        }

        [Test, Order(5), Author("Elitsa Draganova")]
        public void ClickEditButton()
        {

            IWebElement EditButton = wait.Until((d) => {
                return d.FindElement(By.LinkText("Edit"));
            });

            EditButton.Click();
        }

        [Test, Order(6), Author("Elitsa Draganova")]
        public void TitleMoreThan50Chars()
        {

            IWebElement TitleField = wait.Until((d) => {
                return d.FindElement(By.XPath("//*[@id='Title']"));
            });

            IWebElement EditButton = wait.Until((d) => {
                return d.FindElement(By.XPath("/html/body/div[2]/div/div/form/div[4]/div/input"));
            });

            TitleField.Clear();
            TitleField.SendKeys(RandomString(51));

            EditButton.Submit();
        }

        [Test, Order(7), Author("Elitsa Draganova")]
        public void CheckTitleMoreThan50CharsErrorMessage()
        {

            IWebElement ErrorMessage = wait.Until((d) => {
                return d.FindElement(By.XPath("/html/body/div[2]/div/div/form/div[1]/ul/li"));
            });

            Assert.IsTrue(ErrorMessage.Text.Equals("The field Title must be a string with a maximum length of 50."));
        }

        [Test, Order(8), Author("Elitsa Draganova")]
        public void Title10Chars()
        {

            IWebElement TitleField = wait.Until((d) => {
                return d.FindElement(By.XPath("//*[@id='Title']"));
            });

            IWebElement EditButton = wait.Until((d) => {
                return d.FindElement(By.XPath("/html/body/div[2]/div/div/form/div[4]/div/input"));
            });

            TitleField.Clear();
            TitleField.SendKeys(RandomString(10));

            EditButton.Submit();
        }

        [Test, Order(9), Author("Elitsa Draganova")]
        public void BackToArticle() {
            this.NavigateToArticles();
            this.ClickEditButton();
        }

        [Test, Order(10), Author("Elitsa Draganova")]
        public void TitleEmpty()
        {

            IWebElement TitleField = wait.Until((d) => {
                return d.FindElement(By.XPath("//*[@id='Title']"));
            });

            IWebElement EditButton = wait.Until((d) => {
                return d.FindElement(By.XPath("/html/body/div[2]/div/div/form/div[4]/div/input"));
            });

            TitleField.Clear();

            EditButton.Submit();
        }

        [Test, Order(11), Author("Elitsa Draganova")]
        public void CheckEmptyTitleError()
        {

            IWebElement ErrorMessage = wait.Until((d) => {
                return d.FindElement(By.XPath("/html/body/div[2]/div/div/form/div[1]/ul/li"));
            });

            Assert.IsTrue(ErrorMessage.Text.Equals("The Title field is required."));
        }

        [Test, Order(12), Author("Elitsa Draganova")]
        public void ResetTitle()
        {

            IWebElement TitleField = wait.Until((d) => {
                return d.FindElement(By.XPath("//*[@id='Title']"));
            });

            IWebElement EditButton = wait.Until((d) => {
                return d.FindElement(By.XPath("/html/body/div[2]/div/div/form/div[4]/div/input"));
            });

            IWebElement TextBox = wait.Until((d) => {
                return d.FindElement(By.XPath("//*[@id='Content']"));
            });

            TitleField.Clear();
            TitleField.SendKeys("This is Test Article - Edit Article");

            TextBox.Clear();

            EditButton.Submit();
        }

        [Test, Order(13), Author("Elitsa Draganova")]
        public void CheckTextBoxError()
        {

            IWebElement ErrorMessage = wait.Until((d) => {
                return d.FindElement(By.XPath("/html/body/div[2]/h1"));
            });

            Assert.IsTrue(ErrorMessage.Text.Equals("Error."));
        }

        [Test, Order(14), Author("Elitsa Draganova")]
        public void ResetAll()
        {
            IWebElement Logo = wait.Until((d) => {
                return d.FindElement(By.XPath("/html/body/div[1]/div/div[1]/a"));
            });

            Logo.Click();

            this.NavigateToArticles();
            this.ClickEditButton();

            IWebElement TitleField = wait.Until((d) => {
                return d.FindElement(By.XPath("//*[@id='Title']"));
            });

            IWebElement EditButton = wait.Until((d) => {
                return d.FindElement(By.XPath("/html/body/div[2]/div/div/form/div[4]/div/input"));
            });

            IWebElement TextBox = wait.Until((d) => {
                return d.FindElement(By.XPath("//*[@id='Content']"));
            });

            TitleField.Clear();
            TitleField.SendKeys("This is Test Article - Edit Article");

            TextBox.Clear();
            TextBox.SendKeys("Some special text goes here");

            EditButton.Submit();
        }

        [Test, Order(15), Author("Elitsa Draganova")]
        public void ClickEdit()
        {
            this.NavigateToArticles();
            this.ClickEditButton();

            IWebElement EditButton = wait.Until((d) => {
                return d.FindElement(By.XPath("/html/body/div[2]/div/div/form/div[4]/div/input"));
            });

            EditButton.Submit();
        }

        [Test, Order(16), Author("Elitsa Draganova")]
        public void ClickCancel()
        {
            this.NavigateToArticles();
            this.ClickEditButton();

            IWebElement CancelButton = wait.Until((d) => {
                return d.FindElement(By.XPath("/html/body/div[2]/div/div/form/div[4]/div/a"));
            });

            CancelButton.Click();
        }
    }
}
