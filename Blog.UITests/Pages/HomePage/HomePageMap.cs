namespace Blog.UITests.Pages.HomePage
{
	using OpenQA.Selenium;
	using System.Collections.Generic;
	public partial class HomePage
	{

		public IWebElement logInButton => Driver.FindElement(By.Id("loginLink"));

		public IWebElement registrationButton => Driver.FindElement(By.Id("registerLink"));

		public IWebElement logo => Driver.FindElement(By.XPath(@"/html/body/div[1]/div/div[1]/a"));
	}
}
