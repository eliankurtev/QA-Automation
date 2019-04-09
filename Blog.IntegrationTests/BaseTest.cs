namespace Blog.IntegrationTests
{
	using System;
	using NUnit.Framework;
	using System.Net.Http;
	using System.Collections.Generic;
	using FluentAssertions;
	using System.Threading.Tasks;

	public class BaseTest
	{
		//Arrange
		public HttpClient Client { get; set; }

		[OneTimeSetUp]
		public void SetUp()
		{
			Client = new HttpClient
			{
				BaseAddress = new Uri("https://blogservice.azurewebsites.net")
			};

		}


	}
}