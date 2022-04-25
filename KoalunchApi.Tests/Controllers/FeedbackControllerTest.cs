using Xunit;

using koalunch_api.Controllers;
using Moq;
using koalunch_api.Repositories;
using koalunch_api.Models;
using System.Threading.Tasks;
using koalunch_api.Models.Api;
using Microsoft.AspNetCore.Mvc;

namespace KoalunchApi.Controllers.Tests
{
	public class FeedbackControllerTest
	{
		[Fact]
		public async void FeedbackController_PostContactFeedback()
		{
			var repositoryMock = new Mock<FeedbackRepository>();
			repositoryMock
				.Setup(repo => repo.Commit(It.IsAny<FeedbackItem>()))
				.Returns(Task.CompletedTask);

			var controller = new FeedbackController(repositoryMock.Object);

			var result = await controller.PostContactFeedback(new ContactFeedback
			{
				name = "Karel",
				note = "Barel"
			});

			Assert.IsType<CreatedResult>(result);

			repositoryMock.Verify((repo =>
				repo.Commit(It.Is<FeedbackItem>(i => i.Name == "Karel" && i.Note == "Barel"))
			));
		}

		[Fact]
		public async void FeedbackController_PostRestaurantFeedback()
		{
			var repositoryMock = new Mock<FeedbackRepository>();
			repositoryMock
				.Setup(repo => repo.Commit(It.IsAny<FeedbackItem>()))
				.Returns(Task.CompletedTask);

			var controller = new FeedbackController(repositoryMock.Object);

			var result = await controller.PostRestaurantFeedback(new RestaurantFeedback
			{
				name = "Karel",
				note = "Barel",
				restaurantName = "Foo",
				restaurantUrl = "Bar"
			});

			Assert.IsType<CreatedResult>(result);

			repositoryMock.Verify((repo =>
				repo.Commit(It.Is<FeedbackItem>(i =>
					i.Name == "Karel" &&
					i.Note == "Barel" &&
					i.RestaurantName == "Foo" &&
					i.RestaurantUrl == "Bar"
				))
			));
		}
	}
}
