using BlazorProject.Pages;
using BlazorProject.UnitTests.TestUtils;
using Bunit;
using FluentAssertions;
using Xunit;

namespace BlazorProject.UnitTests.Pages
{
    public class CounterTests
    {
        [Fact]
        public void ShouldIncrementValueOnClick()
        {
            using var context = new TestContext();
            var component = context.RenderComponent<Counter>();

            component.Find("[testId=incrementButton]").Click();

            // var diffs = component.GetChangesSinceFirstRender();
            // diffs.ShouldHaveChanges(
            //     diff => diff.ShouldBeTextChange("Current count: 1")
            // );

            component.FindByText("Current count: 1").Should().NotBeNull();
        }
        
        [Fact]
        public void CanIncrementMultipleTimes()
        {
            using var context = new TestContext();
            var component = context.RenderComponent<Counter>();

            for (var i = 0; i < 5; i++)
            {
                component.Find("[testId=incrementButton]").Click();
            }

            // var diffs = component.GetChangesSinceFirstRender();
            // diffs.ShouldHaveChanges(
            //     diff => diff.ShouldBeTextChange("Current count: 5")
            // );
            
            component.FindByText("Current count: 5").Should().NotBeNull();
        }
        
        [Fact]
        public void ResetButtonShouldResetTheCounter()
        {
            using var context = new TestContext();
            var component = context.RenderComponent<Counter>();

            for (var i = 0; i < 5; i++)
            {
                component.Find("[testId=incrementButton]").Click();
            }

            // var diffs = component.GetChangesSinceFirstRender();
            // diffs.ShouldHaveChanges(
            //     diff => diff.ShouldBeTextChange("Current count: 5")
            // );
            // component.SaveSnapshot();
            
            component.Find("[testId=resetButton]").Click();
            
            // diffs = component.GetChangesSinceSnapshot();
            // diffs.ShouldHaveChanges(
            //     diff => diff.ShouldBeTextChange("Current count: 0")
            // );
            
            component.FindByText("Current count: 0").Should().NotBeNull();
        }
    }
}