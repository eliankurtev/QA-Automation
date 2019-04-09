namespace Blog.IntegrationTests
{
    using FluentAssertions;
    using NUnit.Framework;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    [TestFixture]
    public class IntegrationTests: BaseTest
    {
        string htmlWithoutToken;
        string tokenTag;

        [SetUp]
        [Author("Angela Teneva")]
        public void Init()
        {
            var response = Client.GetAsync("/Account/Login").Result;
            response.EnsureSuccessStatusCode();

            var resAsString = response.Content.ReadAsStringAsync().Result.
                Replace(System.Environment.NewLine, string.Empty);

            int splitPos1= resAsString.IndexOf("input");
            htmlWithoutToken = resAsString.Substring(0, splitPos1);
            string restOfString = resAsString.Substring(splitPos1);
            int splitPos2 = restOfString.IndexOf("div");
            tokenTag = resAsString.Substring(splitPos1, splitPos2).
                Trim().Replace(" ", string.Empty);
            htmlWithoutToken = (htmlWithoutToken + restOfString.Substring(splitPos2).Trim()).Replace(" ", string.Empty);
        }
 
        [Test]
        [Author("Angela Teneva")]
        public void LoginPageHTMLTest()
        {
            var path = Path.GetFullPath(Directory.GetCurrentDirectory() + @"\..\..\..\samples\LoginPagePlain.html");
            var expected = File.ReadAllText(path).Trim().Replace(" ", string.Empty)
                                    .Replace(System.Environment.NewLine, string.Empty);

            Assert.AreEqual(htmlWithoutToken, expected);
        }

        [Test]
        [Author("Angela Teneva")]
        public void RequestVerificationTokenTest()
        {
            var path = Path.GetFullPath(Directory.GetCurrentDirectory() + @"\..\..\..\samples\TokenTag.html");
            var expected = File.ReadAllText(path).Trim().Replace(" ", string.Empty)
                                    .Replace(System.Environment.NewLine, string.Empty);

           Assert.AreNotEqual(tokenTag, expected);
        }

        [Test]
        [Author("Borqna Hristova")]
        public async Task GetArticleList()
        {
            var response = await Client.GetAsync("/Article/List");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        [Author("Borqna Hristova")]
        public async Task GetArticle1Title()
        {
            //Arrange
            var response = await Client.GetAsync("/Article/Details/1");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var content = await response.Content.ReadAsStringAsync();
            string expectedTitle = "Я кажи ми облаче ле бяло";

            //Act
            int posArticle = content.IndexOf("<article>");
            int splitPos1 = content.IndexOf("<h2>", posArticle) + 4;
            int splitPos2 = content.IndexOf("</h2>", splitPos1);
            int length = splitPos2 - splitPos1;

            string articleTitle = content.Substring(splitPos1, length);
            articleTitle = articleTitle.Trim();

            //Assert
            expectedTitle.Should().BeEquivalentTo(articleTitle);
        }

        [Test]
        [Author("Borqna Hristova")]
        public async Task GetArticle0()
        {
            var response = await Client.GetAsync("/Article/Details/0");
            response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }

        [Author("Ivan Stalev")]
        public async Task AccountPageTestAsync()
        {
            var client = new HttpClient();
            client.BaseAddress = new System.Uri("http://blogservice.azurewebsites.net/");

            var response = await client.GetAsync("Account");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Test]
        [Author("Ivan Stalev")]
        public async Task LoginPageShouldReturnPageNotFound()
        {
            var client = new HttpClient();
            client.BaseAddress = new System.Uri("http://blogservice.azurewebsites.net/");

            var response = await client.GetAsync("Account/Login/netrqbvadarabotish");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Test]
        [Author("Eli Draganova")]
        public async Task CheckArticleContentTypeHTML()
        {
            var client = new HttpClient();
            client.BaseAddress = new System.Uri("http://blogservice.azurewebsites.net/");
            var response = await client.GetAsync("/Article/Details/12");
            string responseStr = response.Content.Headers.ContentType.ToString();
            Assert.IsTrue(string.Equals("text/html; charset=utf-8", responseStr));
        }

        [Test]
        [Author("Eli Draganova")]
        public async Task isHomeRedirect()
        {
            var client = new HttpClient();
            client.BaseAddress = new System.Uri("http://blogservice.azurewebsites.net/");
            var response = await client.GetAsync("/");
            string localPath = response.RequestMessage.RequestUri.LocalPath.ToString();
            Assert.IsFalse(localPath == "/");
        }

		[Test]
		[Author("Elian Dimov Kurtev, eliankurtev@gmail.com")]
		public async Task GetArticle2Details()
		{
			var request = new HttpRequestMessage(HttpMethod.Get, "/Article/Details/2");
			var response = await Client.SendAsync(request);
			response.StatusCode.Should().Be(HttpStatusCode.OK);
		}
		[Test]
		[Author("Elian Dimov Kurtev, eliankurtev@gmail.com")]
		public async Task GetArticle5Details()
		{
			var request = new HttpRequestMessage(HttpMethod.Get, $"/Article/Details/5");
			var response = await Client.SendAsync(request);
			response.EnsureSuccessStatusCode();
		}
	}
}