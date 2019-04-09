namespace Blog.UITests.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class CreateArticleContent
    {
        private string title;
        private string content;

        public CreateArticleContent(string title, string content)
        {
            this.title = title;
            this.content = content;
        }

        public string Title
        {
            get { return this.title; }
            set { this.title = value; }
        }

        public string Content
        {
            get { return this.content; }
            set { this.content = value; }
        }
    }
}

