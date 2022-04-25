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
    public class MenuControllerTest
    {
        private Mock<IRepository<MenuSource>> PrepareRepository()
        {
            var dummyParser = new Mock<Parser>();
            dummyParser
                .Setup(p => p.ParseDay(It.IsAny<IDocument>()))
                .Returns((IDocument doc) => Task.FromResult(new MenuSection[] {
                    new MenuSection {
                        name = doc.Url
                    }
                }));

            var fakeMenuSourceData = new MenuSource[] {
                new MenuSource {
                    MenuUrl = "Menu 1",
                    Parser = dummyParser.Object,
                    Restaurant = new Restaurant(),
                    Type = MenuType.Standard
                },
                new MenuSource {
                    MenuUrl = "Menu 2",
                    Parser = dummyParser.Object,
                    Restaurant = new Restaurant(),
                    Type = MenuType.Standard
                }
            };

            var repositoryMock = new Mock<IRepository<MenuSource>>();
            repositoryMock
                .Setup(repo => repo.GetAll())
                .Returns(Task.FromResult(fakeMenuSourceData));

            repositoryMock
                .Setup(repo => repo.GetById("menu1"))
                .Returns(Task.FromResult(fakeMenuSourceData[0]));

            repositoryMock
                .Setup(repo => repo.GetById("menu2"))
                .Returns(Task.FromResult(fakeMenuSourceData[1]));

            return repositoryMock;
        }

        private Mock<IHtmlDocumentContext> PrepareDocumentContext()
        {
            var htmlContextMock = new Mock<IHtmlDocumentContext>();
            htmlContextMock
                .Setup(c => c.LoadDocument(It.IsAny<string>()))
                .Returns((string url) =>
                {
                    var fakeDocument = new Mock<IDocument>();
                    fakeDocument.SetupGet(c => c.Url).Returns(url);
                    return Task.FromResult(fakeDocument.Object);
                });

            return htmlContextMock;
        }

        private MenuController PrepareController()
        {
            return new MenuController(
                PrepareRepository().Object,
                PrepareDocumentContext().Object
            );
        }

        [Fact]
        public async void MenuController_Get()
        {
            var controller = PrepareController();

            var response = await controller.Get();
            var result = (response as IEnumerable<Menu>).ToArray();

            Assert.Equal(result.Length, 2);
            Assert.Equal(result[0].menus[0].name, "Menu 1");
            Assert.Equal(result[1].menus[0].name, "Menu 2");
        }

        [Fact]
        public async void MenuController_GetSingle()
        {
            var controller = PrepareController();

            var result = await controller.GetSingle("menu2") as Menu;

            Assert.NotNull(result);
            Assert.Equal(result.menus[0].name, "Menu 2");
        }
    }
}
