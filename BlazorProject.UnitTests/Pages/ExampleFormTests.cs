using BlazorProject.Pages;
using BlazorProject.UnitTests.TestUtils;
using Bunit;
using FluentAssertions;
using Xunit;

namespace BlazorProject.UnitTests.Pages
{
    public class ExampleFormTests
    {
        [Fact]
        public void SubmittingAValidForm()
        {
            using var context = new TestContext();
            var component = context.RenderComponent<ExampleForm>();

            var forenameInput = component.FindByLabelText("Forename:");
            forenameInput.Change("John");

            var surnameInput = component.FindByLabelText("Surname:");
            surnameInput.Change("Smith");

            var emailInput = component.FindByLabelText("Email Address:");
            emailInput.Change("john.smith@developer.com");

            var submitButton = component.FindByText("Submit");
            submitButton.Click();

            component.FindByText("Your form is Submitted").Should().NotBeNull();
        }
    }
}