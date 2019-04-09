//In this test class are executed all test cases from 02.01 to 02.11
namespace Blog.UITests
{
	using System;
	using System.IO;
	using System.Reflection;
	using Blog.UITests.Models;
	using Blog.UITests.Pages.HomePage;
	using Blog.UITests.Pages.HomePageOfLoggedUser;
	using Blog.UITests.Pages.RegistrationPage;
	using FluentAssertions;
	using NUnit.Framework;
	using OpenQA.Selenium;
	using OpenQA.Selenium.Chrome;
	using OpenQA.Selenium.Support.UI;
	[TestFixture]
	[Author("Elian Dimov Kurtev, eliankurtev@gmail.com")]
	public class RegistrationTests
	{
		private IWebDriver driver;
		private WebDriverWait wait;
		private HomePage homePage;
		private RegistrationPage registrationPage;
		private HomePageOfLoggedUser homePageOfLoggedUser;

		[SetUp]
		public void SetUp()
		{
			driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
			driver.Manage().Window.Maximize();
			wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
			homePage = new HomePage(driver);
			registrationPage = new RegistrationPage(driver);
			homePageOfLoggedUser = new HomePageOfLoggedUser(driver);
			homePage.Driver.Navigate().GoToUrl("https://blogservice.azurewebsites.net");
		}

		[TearDown]
		public void TearDown()
		{
			driver.Quit();
		}

		[Test]
		[Description("TC 02.01")]
		public void LoadRegistrationPage()
		{
			homePage.registrationButton.Click();
			string tabTitle = driver.Title;
			tabTitle.Should().Be("Register - My ASP.NET Application");
		}
		[Test]
		[Description("TC 02.02-02.05")]
		[TestCase("/../../../Jsons/Registration/EmailJsons/InvalidEmailOnlyNumbers.json")]
		[TestCase("/../../../Jsons/Registration/EmailJsons/InvalidEmailOnlyOneLetter.json")]
		[TestCase("/../../../Jsons/Registration/EmailJsons/InvalidEmailWithSpaces.json")]
		[TestCase("/../../../Jsons/Registration/EmailJsons/InvalidEmailWithout@.json")]
		public void WrongEmailInput(string path)
		{
			homePage.registrationButton.Click();
			var testCasePath = Path.GetFullPath(Directory.GetCurrentDirectory() + path);
			var user = User.UserFromJson(File.ReadAllText(testCasePath));
			registrationPage.fillRegistrtionForm(user);
			registrationPage.emailAdressIsNotValidMessage.Displayed.Should().BeTrue();
		}
		[Test]
		[Description("TC 02.06")]
		public void FullNameFieldIsReqiured()
		{
			homePage.registrationButton.Click();
			var path = Path.GetFullPath(Directory.GetCurrentDirectory() + "/../../../Jsons/Registration/FullNameJsons/InvalidFullName_WithOnlyZeroLetters.json");
			var user = User.UserFromJson(File.ReadAllText(path));
			registrationPage.fillRegistrtionForm(user);
			registrationPage.fullNameIsRequiredMessage.Displayed.Should().BeTrue();
		}
		[Test]
		[Description("TC 02.07")]
		public void FullNameFieldWithMoreThan50Letters()
		{
			homePage.registrationButton.Click();
			var path = Path.GetFullPath(Directory.GetCurrentDirectory() + "/../../../Jsons/Registration/FullNameJsons/InvalidFullName_WithMoreThanFifityLetters.json");
			var user = User.UserFromJson(File.ReadAllText(path));
			registrationPage.fillRegistrtionForm(user);
			registrationPage.FullNameIsMoreThan50LettersMessage.Displayed.Should().BeTrue();
		}
		[Test]
		[Description("TC 02.08")]
		public void UnmatchedPasswords()
		{
			homePage.registrationButton.Click();
			var path = Path.GetFullPath(Directory.GetCurrentDirectory() + "/../../../Jsons/Registration/PasswordJsons/UnmatchedPasswords.json");
			var user = User.UserFromJson(File.ReadAllText(path));
			registrationPage.fillRegistrtionForm(user);
			registrationPage.passwordsDoNotMatchMessage.Displayed.Should().BeTrue();
		}
		[Test]
		[Description("TC 02.09")]
		public void EmptyDataRegistrion()
		{
			homePage.registrationButton.Click();
			var path = Path.GetFullPath(Directory.GetCurrentDirectory() + "/../../../Jsons/Registration/EmptyDataRegistration.json");
			var user = User.UserFromJson(File.ReadAllText(path));
			registrationPage.fillRegistrtionForm(user);
			registrationPage.EmptyDataMessages.Displayed.Should().BeTrue();
		}
		//Before running this file check the data in the JSON file. 
		[Test]
		[Order(1)]
		[Description("TC 02.10")]
		public void RegistrationWithValidData()
		{
			homePage.registrationButton.Click();
			var path = Path.GetFullPath(Directory.GetCurrentDirectory() + "/../../../Jsons/Registration/ValidDataRegistration.json");
			var user = User.UserFromJson(File.ReadAllText(path));
			registrationPage.fillRegistrtionForm(user);
			homePageOfLoggedUser.LogOffButton.Displayed.Should().BeTrue();
		}
		[Test]
		[Order(2)]
		[Description("TC 02.11")]
		public void RegistrationWithAlreadyRegisteredCredentials()
		{
			homePage.registrationButton.Click();
			var path = Path.GetFullPath(Directory.GetCurrentDirectory() + "/../../../Jsons/Registration/ValidDataRegistration.json");
			var user = User.UserFromJson(File.ReadAllText(path));
			registrationPage.fillRegistrtionForm(user);
			
			//Should return some verification message, not Internal Error!
			registrationPage.ErrorMessage.Displayed.Should().BeFalse();
			
		}
	}
}
