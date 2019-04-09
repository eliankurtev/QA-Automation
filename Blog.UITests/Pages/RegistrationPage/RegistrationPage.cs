//Created by: Elian Dimov Kurtev
using Blog.UITests.Models;
using OpenQA.Selenium;

namespace Blog.UITests.Pages.RegistrationPage
{
	public partial class RegistrationPage:BasePage
	{
		public RegistrationPage(IWebDriver driver) : base(driver){}

		public void fillRegistrtionForm(User registrationUser)
		{
			emailField.SendKeys(registrationUser.Email);
			fullNameField.SendKeys(registrationUser.FullName);
			passwordField.SendKeys(registrationUser.Password);
			confirmPasswordField.SendKeys(registrationUser.ConfirmPassword);
			registerButton.Click();
		}
	}
}
