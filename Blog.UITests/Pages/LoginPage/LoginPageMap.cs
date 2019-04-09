//Created by: Elian Dimov Kurtev
namespace Blog.UITests.Pages.LoginPage
{
	using OpenQA.Selenium;

    public partial class LoginPage
    {

        public IWebElement EmailField => Driver.FindElement(By.Id("Email"));

        public IWebElement PasswordField => Driver.FindElement(By.Id("Password"));

		public IWebElement RememberMeCheckBox => Wait.Until((d) => { return d.FindElement(By.XPath(@"/html/body/div[2]/div/div/form/div[3]/div/div/div/label")); });

		public IWebElement LoginButton => Driver.FindElement(By.XPath(@"/html/body/div[2]/div/div/form/div[4]/div/input"));

		//Explicit wait for this element until the message shows after the USER has entered wrong credentials.
		public IWebElement FailedLoginMessage => Wait.Until((d) => { return d.FindElement(By.XPath(@"/html/body/div[2]/div/div/form/div[1]")); });
		//Explicit wait for this element until the message shows after the USER has missed to type email.
		public IWebElement EmailReqiuredMessage => Wait.Until((d) => { return d.FindElement(By.XPath(@"/html/body/div[2]/div/div/form/div[1]/div/span/span")); });
		//Explicit wait for this element until the message shows after the USER has missed to type password.
		public IWebElement PasswordReqiuredMessage => Wait.Until((d) => { return d.FindElement(By.XPath(@"/html/body/div[2]/div/div/form/div[2]/div/span/span")); });
		//Explicit wait for this element until the message shows after the USER has to typed wrong format of email.
		public IWebElement WrongFormatOfEmailMessage => Wait.Until((d) => { return d.FindElement(By.XPath(@"/html/body/div[2]/div/div/form/div[1]/div/span/span")); });

	}
}
