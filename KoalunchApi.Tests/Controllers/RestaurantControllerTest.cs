using Xunit;

using koalunch_api.Controllers;
using Moq;
using koalunch_api.Repositories;
using koalunch_api.Models;
using System.Threading.Tasks;
using koalunch_api.MenuParsers;
using koalunch_api.Models.Api;
using AngleSharp.Dom;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace KoalunchApi.Controllers.Tests
{
	public class RestaurantControllerTest
	{
		private Mock<IRepository<Restaurant>> PrepareRepository()
		{
			var fakeMenuSourceData = new Restaurant[] {
				new Restaurant {
					id = "Rest1",
					name = "Rest 1",
					url = "Url1"
				},
				new Restaurant {
					id = "Rest2",
					name = "Rest 2",
					url = "Url2"
				}
			};

			var repositoryMock = new Mock<IRepository<Restaurant>>();
			repositoryMock
				.Setup(repo => repo.GetAll())
				.Returns(Task.FromResult(fakeMenuSourceData));

			repositoryMock
				.Setup(repo => repo.GetById("Rest1"))
				.Returns(Task.FromResult(fakeMenuSourceData[0]));

			repositoryMock
				.Setup(repo => repo.GetById("Rest2"))
				.Returns(Task.FromResult(fakeMenuSourceData[1]));

			return repositoryMock;
		}

		private RestaurantController PrepareController()
		{
			return new RestaurantController(
				PrepareRepository().Object
			);
		}

		[Fact]
		public async void RestaurantController_GetAll()
		{
			var controller = PrepareController();

			var response = await controller.GetAll();
			var result = (response as IEnumerable<Restaurant>).ToArray();

			Assert.Equal(result.Length, 2);
			Assert.Equal(result[0].id, "Rest1");
			Assert.Equal(result[1].id, "Rest2");
		}
	}
}
