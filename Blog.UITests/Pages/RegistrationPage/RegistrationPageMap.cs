//Created by: Elian Dimov Kurtev
using OpenQA.Selenium;

namespace Blog.UITests.Pages.RegistrationPage
{
	public partial class RegistrationPage
	{
		public IWebElement emailField => Driver.FindElement(By.Name("Email"));

		public IWebElement fullNameField => Driver.FindElement(By.Name("FullName"));

		public IWebElement passwordField => Driver.FindElement(By.Name("Password"));

		public IWebElement confirmPasswordField => Driver.FindElement(By.Name("ConfirmPassword"));

		public IWebElement registerButton => Driver.FindElement(By.XPath(@"/html/body/div[2]/div/div/form/div[6]/div/input"));
		
		//Explicit wait for those elements because each of them shows after the USER clicks on submit button. Each element is the only <li> in the <ul>

		public IWebElement emailAdressIsNotValidMessage => Wait.Until((d) => { return d.FindElement(By.XPath(@"/html/body/div[2]/div/div/form/div[1]/ul/li[1]")); });

		public IWebElement fullNameIsRequiredMessage => Wait.Until((d) => { return d.FindElement(By.XPath(@"/html/body/div[2]/div/div/form/div[1]/ul/li")); });

		public IWebElement FullNameIsMoreThan50LettersMessage => Wait.Until((d) => { return d.FindElement(By.XPath(@"/html/body/div[2]/div/div/form/div[1]/ul/li")); });
		
		public IWebElement passwordsDoNotMatchMessage => Wait.Until((d) => { return d.FindElement(By.XPath(@"/html/body/div[2]/div/div/form/div[1]/ul/li")); });

		public IWebElement EmptyDataMessages => Wait.Until((d) => { return d.FindElement(By.XPath(@"/html/body/div[2]/div/div/form/div[1]/ul")); });

		public IWebElement ErrorMessage => Wait.Until((d) => { return d.FindElement(By.XPath(@"/html/body/div[2]/h1")); });

	}
}
