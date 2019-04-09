namespace Blog.UITests.Pages.CreateArticlePage
{
    using OpenQA.Selenium;
    using Blog.UITests.Models;
    public partial class CreateArticlePage : BasePage
    {
        public CreateArticlePage(IWebDriver driver) : base(driver)
        {
        }

        public void NavigateTo() => Driver.Navigate().GoToUrl(@"http://localhost:60634/Article/List");

        public void FillCreateArticleForm(CreateArticleContent articleContent)
        {
            TypeText(Title, articleContent.Title);
            TypeText(Content, articleContent.Content);
            CreateArticleButton.Click();
        }

        private void TypeText(IWebElement element, string text)
        {
            element.Click();
            element.SendKeys(text);
        }
    }
}
