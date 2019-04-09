namespace Blog.UITests
{
    using Blog.UITests.Models;
    using Blog.UITests.Pages.CreateArticlePage;
    using Blog.UITests.Pages.HomePage;
    using Blog.UITests.Pages.HomePageOfLoggedUser;
    using Blog.UITests.Pages.LoginPage;
    using NUnit.Framework;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Interactions;
    using OpenQA.Selenium.Support.UI;
    using System;
    using System.IO;
    using System.Reflection;
    using System.Threading;

    [TestFixture]
    public class CreateArticleTests
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        private CreateArticlePage articlePage;
        private LoginPage loginPage;
        private HomePage homePage;

        public User CreateUser()
        {
            var path = Path.GetFullPath(Directory.GetCurrentDirectory() +
                "/../../../Jsons/Login/AngelaValidCredentials.json");
            var user = User.UserFromJson(File.ReadAllText(path));
            return user;
        }

       [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            driver.Manage().Window.Maximize();
            articlePage = new CreateArticlePage(driver);
            homePage = new HomePage(driver);
            loginPage = new LoginPage(driver);
            driver.Navigate().GoToUrl("http://localhost:60634/");
//          driver.Navigate().GoToUrl("https://blogservice.azurewebsites.net/");

            homePage.logInButton.Click();
            loginPage.fillLoginForm(CreateUser());
        }

        [TearDown]
        public void CleanUp()
        {
           driver.Quit();
        }

        [Test]
        [Description("TC 03.01")]
        [Author("Angela Teneva")]
        public void Create_ArticlePageShouldDisplayCorrect()
        {
            articlePage.CreateNewArticle.Click();
            Assert.AreEqual(articlePage.Create_ArticleText.Text, "Create Article");
            Assert.IsTrue(articlePage.CreateArticleButton.Displayed);
        }

        [Test]
        [Description("TC 03.02")]
        [Author("Angela Teneva")]
        [TestCase("", "The Title field is required.")]
        [TestCase("these are fifty one characters - upper limit plus 1", "The field Title must be a string with a maximum length of 50.")]
        public void Create_ArticleTitleNotCorrectShouldDisplayErrorMessage(string title, string errorMessage)
        {
            articlePage.CreateNewArticle.Click();
            articlePage.Title.SendKeys(title);
            articlePage.Content.SendKeys("ne6to si");
            articlePage.CreateArticleButton.Click();

            Assert.AreEqual(articlePage.Create_Article_TitleFieldRequired_Position1.Text, errorMessage);
        }

        [Test]
        [Description("TC 03.03")]
        [Author("Angela Teneva")]
        [TestCase("u")]
        [TestCase("these fifty characters are equal to the upper limit")]
        public void Create_ArticleTitleCorrectShouldSuccess(string title)
        {
            articlePage.CreateNewArticle.Click();
            articlePage.Title.SendKeys(title);
            articlePage.Content.SendKeys("ne6to si");
            articlePage.CreateArticleButton.Click();

            IWebElement FindNewArticleTitle = wait.Until((driver) => driver.FindElement(By.LinkText(title)));
            var newArticleTitleText = FindNewArticleTitle.Text;

            Assert.IsTrue(articlePage.Title.Displayed);
            Assert.IsTrue(newArticleTitleText.Contains(title));
        }

        [Test]
        [Description("TC 03.04")]
        [Author("Angela Teneva")]
        public void Create_ArticleWithoutContentShouldRefuse_Article()
        {
            articlePage.CreateNewArticle.Click();
            articlePage.Title.SendKeys("Empty Contents");
            articlePage.Content.SendKeys(String.Empty);
            articlePage.CreateArticleButton.Click();

            Assert.AreEqual(articlePage.Create_Article_ContentFieldRequired_Error.Text, "The Content field is required.");
        }

        [Test]
        [Description("TC 03.05")]
        [Author("Angela Teneva")]
        public void Create_ArticleWithValidDataShouldSuccess()
        {
            string title = "Computer Science";
            string content = "Computer Science enables the use of algorithms....";

            articlePage.CreateNewArticle.Click();
            articlePage.Title.SendKeys(title);
            articlePage.Content.SendKeys(content);
            articlePage.CreateArticleButton.Click();

            IWebElement FindNewArticleContent = wait.Until((driver) => driver.FindElement(By.LinkText("Computer Science enables the use of algorithms....")));
            var newArticleContentText = FindNewArticleContent.Text;

            Assert.IsTrue(articlePage.Content.Displayed);
            Assert.IsTrue(newArticleContentText.Contains("algorithms"));
        }

        [Test]
        [Description("TC 03.06")]
        [Author("Angela Teneva")]
        public void CancelButton_ShouldRefuse_Article()
        {
            articlePage.CreateNewArticle.Click();
            articlePage.CancelArticleButton.Click();
            var pageUrl = driver.Url;

//            Assert.AreEqual(pageUrl, "https://blogservice.azurewebsites.net/Article/List");
            Assert.AreEqual(pageUrl, "http://localhost:60634/Article/List");
        }
    }
}
