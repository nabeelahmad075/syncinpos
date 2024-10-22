using System.Threading.Tasks;
using syncinpos.Models.TokenAuth;
using syncinpos.Web.Controllers;
using Shouldly;
using Xunit;

namespace syncinpos.Web.Tests.Controllers
{
    public class HomeController_Tests: syncinposWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            await AuthenticateAsync(null, new AuthenticateModel
            {
                UserNameOrEmailAddress = "admin",
                Password = "123qwe"
            });

            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}