namespace Blog.UITests
{
    using System;
    using System.IO;
    using System.Reflection;
	using Blog.UITests.Models;
	using Blog.UITests.Pages.ChangePasswordPage;
	using Blog.UITests.Pages.HomePage;
	using Blog.UITests.Pages.HomePageOfLoggedUser;
	using Blog.UITests.Pages.LoginPage;
	using NUnit.Framework;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Support.UI;

    [TestFixture]
    public class DeleteArticleTests
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        private HomePageOfLoggedUser homePageOfLoggedUser;
        private ChangePasswordPage changePasswordPage;
		private LoginPage loginPage;
		private HomePage homePage;

		public User createUser()
		{
			var path = Path.GetFullPath(Directory.GetCurrentDirectory() + 
				"/../../../Jsons/Login/IvanValidCredentials.json");
			var user = User.UserFromJson(File.ReadAllText(path));
			return user;
		}

	    [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Manage().Window.Maximize();
            homePageOfLoggedUser = new HomePageOfLoggedUser(driver);
            changePasswordPage = new ChangePasswordPage(driver);
			homePage = new HomePage(driver);
			loginPage = new LoginPage(driver);
			driver.Navigate().GoToUrl("http://blogservice.azurewebsites.net/");
			homePage.logInButton.Click();
			loginPage.fillLoginForm(createUser());

        }

        // TC - 05.01
        [Test]
        [Order(1)]
        [Author("Ivan Stalev")]
        public void CheckDeleteButtonRedirectionToArticlePage()
        {
            
            IWebElement ArticleTitle = wait.Until((driver) => driver.FindElement(By.PartialLinkText("How to Sleep Better Every Night")));
            ArticleTitle.Click();
            IWebElement DeleteButton = wait.Until((driver) => driver.FindElement(By.XPath(@"/html/body/div[2]/div/article/footer/a[2]")));
            DeleteButton.Click();
            IWebElement Content = wait.Until((driver) => driver.FindElement(By.XPath(@"/html/body/div[2]/div/div/h2")));
            var text = Content.Text;

            Assert.AreEqual(text, "Delete Article");
        }

        // TC - 05.02
        [Test]
        [Order(2)]
        [Author("Ivan Stalev")]
        public void TestCancelButton()
        {
            IWebElement ArticleTitle = wait.Until((driver) => driver.FindElement(By.PartialLinkText("How to Sleep Better Every Night")));
            ArticleTitle.Click();
            IWebElement DeleteButton = wait.Until((driver) => driver.FindElement(By.XPath(@"/html/body/div[2]/div/article/footer/a[2]")));
            DeleteButton.Click();
            IWebElement CancelButton = wait.Until((driver) => driver.FindElement(By.XPath(@"/html/body/div[2]/div/div/form/div[3]/div/a")));
            CancelButton.Click();
            var page = driver.Url;

            Assert.AreEqual(page, "http://blogservice.azurewebsites.net/Article/List");
        }

        // TC - 05.03
        [Test]
        [Order(4)]
        [Author("Ivan Stalev")]
        public void TestDeleteButton()
        {

            IWebElement ArticleTitle = wait.Until((driver) => driver.FindElement(By.PartialLinkText("How to Sleep Better Every Night")));
            ArticleTitle.Click();
            IWebElement DeleteButton = wait.Until((driver) => driver.FindElement(By.XPath(@"/html/body/div[2]/div/article/footer/a[2]")));
            DeleteButton.Click();
            IWebElement SecondDeleteButton = wait.Until((driver) => driver.FindElement(By.XPath(@"/html/body/div[2]/div/div/form/div[3]/div/input")));
            SecondDeleteButton.Click();
            var page = driver.Url;

            Assert.AreEqual(page, "http://blogservice.azurewebsites.net/Article/List");
        }

        // TC - 06.01
        [Test]
        [Order(5)]
        [Author("Ivan Stalev")]
        public void ArticleAddedBySomeoneElse()
        {

            IWebElement ArticleTitle = wait.Until((driver) => driver.FindElement(By.XPath(@"/html/body/div[2]/div/div/div[1]/article/header")));
            ArticleTitle.Click();
            IWebElement DeleteButton = wait.Until((driver) => driver.FindElement(By.XPath(@"/html/body/div[2]/div/article/footer/a[2]")));
            var page = driver.Url;

            Assert.AreEqual(page, "http://blogservice.azurewebsites.net/Article/Details/1");
        }

        // TC - 06.02
        [Test]
        [Order(3)]
        [Author("Ivan Stalev")]
        public void TestEditButton()
        {

            IWebElement ArticleTitle = wait.Until((driver) => driver.FindElement(By.PartialLinkText("How to Sleep Better Every Night")));
            ArticleTitle.Click();
            IWebElement EditButton = wait.Until((driver) => driver.FindElement(By.XPath(@"/html/body/div[2]/div/article/footer/a[1]")));
            EditButton.Click();
            IWebElement Content = wait.Until((driver) => driver.FindElement(By.ClassName("well")));
            var text = Content.Text;

            Assert.IsTrue(text.Contains("Edit Article"));
        }
        // TC - 06.03
        [Test]
        [Order(6)]
        [Author("Ivan Stalev")]
        public void TestDeleteButtonForArticleAddedBySomeoneElse()
        {

            IWebElement ArticleTitle = wait.Until((driver) => driver.FindElement(By.XPath(@"/html/body/div[2]/div/div/div[1]/article/header")));
            ArticleTitle.Click();
            IWebElement DeleteButton = wait.Until((driver) => driver.FindElement(By.XPath(@"/html/body/div[2]/div/article/footer/a[2]")));
            DeleteButton.Click();
            IWebElement Content = wait.Until((driver) => driver.FindElement(By.XPath(@"/html/body")));
            var text = Content.Text;

            Assert.IsTrue(text.Contains("You do not have permission to view this directory or page."));
        }

        // TC - 06.04
        [Test]
        [Order(7)]
        [Author("Ivan Stalev")]
        public void TestBackButton()
        {

            IWebElement ArticleTitle = wait.Until((driver) => driver.FindElement(By.XPath(@"/html/body/div[2]/div/div/div[1]/article/header")));
            ArticleTitle.Click();
            IWebElement BackButton = wait.Until((driver) => driver.FindElement(By.XPath(@"/html/body/div[2]/div/article/footer/a[3]")));
            BackButton.Click();
            var page = driver.Url;

            Assert.AreEqual(page, "http://blogservice.azurewebsites.net/Article/List");
        }

        [Test]
        [Order(8)]
        [Author("Ivan Stalev")]
        public void CreateArticle()
        {
            IWebElement CreateNewArticle = wait.Until((driver) => driver.FindElement(By.XPath(@"//*[@id=""logoutForm""]/ul/li[1]/a")));
            CreateNewArticle.Click();
            homePageOfLoggedUser.TitleField.SendKeys("How to Sleep Better Every Night");
            homePageOfLoggedUser.ContentField.SendKeys("If you want to learn how to sleep better");
            homePageOfLoggedUser.CreateButton.Click(); 
            IWebElement FindArticleTitle = wait.Until((driver) => driver.FindElement(By.XPath(@"/html/body/div[2]")));
            var MainPageBodyText = FindArticleTitle.Text;

            Assert.IsTrue(MainPageBodyText.Contains("How to Sleep Better Every Night"));
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}