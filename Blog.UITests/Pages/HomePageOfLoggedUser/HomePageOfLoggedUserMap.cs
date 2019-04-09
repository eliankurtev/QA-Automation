//Created by: Elian Dimov Kurtev
namespace Blog.UITests.Pages.HomePageOfLoggedUser
{
	using OpenQA.Selenium;
	public partial class HomePageOfLoggedUser
	{

		public IWebElement TitleField => Driver.FindElement(By.Id("Title"));

		public IWebElement ContentField => Driver.FindElement(By.Id("Content"));

		public IWebElement CreateButton => Driver.FindElement(By.XPath(@"/html/body/div[2]/div/div/form/div[4]/div/input"));

		public IWebElement LogOffButton => Wait.Until((d) => { return d.FindElement(By.XPath(@"//*[@id=""logoutForm""]/ul/li[3]/a")); });

        public IWebElement HelloButton => Driver.FindElement(By.XPath(@"//*[@id=""logoutForm""]/ul/li[2]/a"));

        public IWebElement CreateArticle => Driver.FindElement(By.XPath(@"//*[@id=""logoutForm""]/ul/li[2]/a"));

		//*[@id="logoutForm"]/ul/li[1]/a
	}
}
