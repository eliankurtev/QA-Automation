namespace Blog.UITests.Pages.CreateArticlePage
{
    using OpenQA.Selenium;

    public partial class CreateArticlePage
    {
        public IWebElement Title => Wait.Until(d => { return d.FindElement(By.Id("Title")); });

        public IWebElement Content => Wait.Until(d => { return d.FindElement(By.Id("Content")); });

        public IWebElement CreateNewArticle => Wait.Until((driver) => driver.FindElement(By.XPath(@"//*[@id=""logoutForm""]/ul/li[1]/a")));

        public IWebElement CreateArticleButton => Driver.FindElement(By.XPath(@"/html/body/div[2]/div/div/form/div[4]/div/input"));

        public IWebElement CancelArticleButton => Driver.FindElement(By.XPath(@"/html/body/div[2]/div/div/form/div[4]/div/a"));

        public IWebElement Create_ArticleText => Driver.FindElement(By.XPath(@"/html/body/div[2]/div/div/h2"));

        public IWebElement Create_Article_ContentFieldRequired_Error => Driver.FindElement(By.XPath(@"/html/body/div[2]/div/div/form/div[1]/ul/li"));

        public IWebElement Create_Article_TitleFieldRequired_Position1 => Driver.FindElement(By.XPath(@"/html/body/div[2]/div/div/form/div[1]/ul/li"));

        public IWebElement Create_Article_ContentFieldRequired_Position2 => Driver.FindElement(By.XPath(@"/html/body/div[2]/div/div/form/div[1]/ul/li[2]"));
    }
}
