//In this test class are executed all test cases from 01.01 to 01.09 
namespace Blog.UITests
{
	using System;
	using System.IO;
	using System.Reflection;
	using Blog.UITests.Models;
	using Blog.UITests.Pages.HomePage;
	using Blog.UITests.Pages.HomePageOfLoggedUser;
	using Blog.UITests.Pages.LoginPage;
	using FluentAssertions;
	using NUnit.Framework;
	using OpenQA.Selenium;
	using OpenQA.Selenium.Chrome;
	using OpenQA.Selenium.Support.UI;
	[TestFixture]
	[Author("Elian Dimov Kurtev, eliankurtev@gmail.com")]
	class LoginTests
	{
		private IWebDriver driver;
		private WebDriverWait wait;
		private HomePage homePage;
		private LoginPage loginPage;
		private HomePageOfLoggedUser homePageOfLoggedUser;

		[SetUp]
		public void SetUp()
		{
			driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
			driver.Manage().Window.Maximize();
			wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
			homePage = new HomePage(driver);
			loginPage = new LoginPage(driver);
			homePageOfLoggedUser = new HomePageOfLoggedUser(driver);
			homePage.Driver.Navigate().GoToUrl("https://blogservice.azurewebsites.net");
		}

		[TearDown]
		public void TearDown()
		{
			driver.Quit();
		}

		[Test]
		[Description("TC 01.01")]
		public void LoadLoginPage()
		{
			homePage.logInButton.Click();
			string tabTitle = driver.Title;
			tabTitle.Should().Be("Log in - My ASP.NET Application");
		}
		[Test]
		[Description("TC 01.02")]
		public void ValidDataLogin()
		{
			var path = Path.GetFullPath(Directory
				.GetCurrentDirectory() +
				"/../../../Jsons/Login/ElianValidCredentials.json");
			var user = User.UserFromJson(File.ReadAllText(path));
			homePage.logInButton.Click();
			loginPage.fillLoginForm(user);
		}
		[Test]
		[Description("TC 01.03")]
		public void EmptyEmailFieldSubmit()
		{
			var path = Path.GetFullPath(Directory
				.GetCurrentDirectory() +
				"/../../../Jsons/Login/InvalidCredentialsMissedEmail.json");
			var user = User.UserFromJson(File.ReadAllText(path));
			homePage.logInButton.Click();
			loginPage.fillLoginForm(user);
			loginPage.EmailReqiuredMessage.Displayed.Should().BeTrue();
		}
		[Test]
		[Description("TC 01.04")]
		public void WrongEmailFormat()
		{
			var path = Path.GetFullPath(Directory
				.GetCurrentDirectory() +
				"/../../../Jsons/Login/InvalidCredentialsWrongFormatOfEmail.json");
			var user = User.UserFromJson(File.ReadAllText(path));
			homePage.logInButton.Click();
			loginPage.fillLoginFormWithoutClickedSubmit(user);
			loginPage.WrongFormatOfEmailMessage.Displayed.Should().BeTrue();
		}
		[Test]
		[Description("TC 01.05")]
		public void EmptyPasswordFieldSubmit()
		{
			var path = Path.GetFullPath(Directory
				.GetCurrentDirectory() +
				"/../../../Jsons/Login/InvalidCredentialsMissedPassword.json");
			var user = User.UserFromJson(File.ReadAllText(path));
			homePage.logInButton.Click();
			loginPage.fillLoginForm(user);
			loginPage.PasswordReqiuredMessage.Displayed.Should().BeTrue();
		}
		[Test]
		[Description("TC 01.06")]
		public void WrongPasswordSubmit()
		{
			var path = Path.GetFullPath(Directory
				.GetCurrentDirectory() +
				"/../../../Jsons/Login/InvalidCredentialsWrongPassword.json");
			var user = User.UserFromJson(File.ReadAllText(path));
			homePage.logInButton.Click();
			loginPage.fillLoginForm(user);
			loginPage.FailedLoginMessage.Displayed.Should().BeTrue();
		}
		[Test]
		[Description("TC 01.07")]
		public void EmptyFormSubmit()
		{
			homePage.logInButton.Click();
			loginPage.LoginButton.Click();
			loginPage.EmailReqiuredMessage.Displayed.Should().BeTrue();
			loginPage.PasswordReqiuredMessage.Displayed.Should().BeTrue();
		}
		[Test]
		[Description("TC 01.08")]
		public void IncompleteFormSubmit()
		{
			var path = Path.GetFullPath(Directory
				.GetCurrentDirectory() +
				"/../../../Jsons/Login/InvalidCredentialsIncomplete.json");
			var user = User.UserFromJson(File.ReadAllText(path));
			homePage.logInButton.Click();
			loginPage.fillLoginForm(user);
			loginPage.FailedLoginMessage.Displayed.Should().BeTrue();
		}
		[Test]
		[Description("TC 01.09")]
		public void CheckedRememberMe()
		{
			var path = Path.GetFullPath(Directory
				.GetCurrentDirectory() +
				"/../../../Jsons/Login/ElianValidCredentials.json");
			var user = User.UserFromJson(File.ReadAllText(path));
			homePage.logInButton.Click();
			loginPage.fillLoginFormWithCheckedRememberMe(user);
			driver.Navigate().GoToUrl("https://blogservice.azurewebsites.net");
			homePageOfLoggedUser.LogOffButton.Displayed.Should().BeTrue();
		}
	}
}
