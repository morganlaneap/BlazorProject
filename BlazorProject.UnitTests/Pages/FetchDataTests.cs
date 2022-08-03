using BlazorProject.Pages;
using BlazorProject.UnitTests.TestUtils;
using Bunit;
using FluentAssertions;
using RichardSzalay.MockHttp;
using Xunit;

namespace BlazorProject.UnitTests.Pages
{
    public class FetchDataTests
    {
        [Fact]
        public void ShouldRenderDataOnPageWhenReady()
        {
            using var context = new TestContext();
            var httpMock = context.Services.AddMockHttpClient();
            var testWeather = TestData.ValidWeatherForecastData;
            httpMock.When("/sample-data/weather.json").RespondJson(testWeather);
            
            var component = context.RenderComponent<FetchData>();

            component.FindByText("Loading...").Should().NotBeNull();
            component.WaitForElement("table");
            component.FindByText(testWeather[0].Summary).Should().NotBeNull();
        }
    }
}