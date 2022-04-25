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
using System;

namespace KoalunchApi.Controllers.Tests
{
	public class VisitorControllerTest
	{
		private Mock<IRepository<Visitors>> PrepareRepository()
		{
			var fakeMenuSourceData = new Visitors[] {
				new Visitors {
					count = 15
				},
				new Visitors {
					count = 30
				}
			};

			var repositoryMock = new Mock<IRepository<Visitors>>();
			repositoryMock
				.Setup(repo => repo.GetAll())
				.Returns(Task.FromResult(fakeMenuSourceData));

			return repositoryMock;
		}

		private VisitorController PrepareController()
		{
			return new VisitorController(
				PrepareRepository().Object
			);
		}

		[Fact]
		public async void VisitorController_GetAll()
		{
			var controller = PrepareController();

			var response = await controller.GetAll();
			var result = (response as IEnumerable<Visitors>).ToArray();

			Assert.Equal(result.Length, 2);
			Assert.Equal(result[0].count, 15);
			Assert.Equal(result[1].count, 30);
		}
	}
}
