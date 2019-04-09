//Created by: Elian Dimov Kurtev
using Blog.UITests.Models;
using OpenQA.Selenium;
namespace Blog.UITests.Pages.LoginPage
{
	public partial class LoginPage:BasePage
	{
		//Constructor
		public LoginPage(IWebDriver driver) : base(driver){}

		public void fillLoginForm(User user)
		{
			EmailField.SendKeys(user.Email);
			PasswordField.SendKeys(user.Password);
			LoginButton.Click();
		}
		public void fillLoginFormWithCheckedRememberMe(User user)
		{
			EmailField.SendKeys(user.Email);
			PasswordField.SendKeys(user.Password);
			RememberMeCheckBox.Click();
			LoginButton.Click();
		}
		public void fillLoginFormWithoutClickedSubmit(User user)
		{
			EmailField.SendKeys(user.Email);
			PasswordField.SendKeys(user.Password);
			RememberMeCheckBox.Click();
		}
	}
}
