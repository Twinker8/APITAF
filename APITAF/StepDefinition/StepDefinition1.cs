using System.Net;
using Automation.API.Methods.APIMethods;
using FluentAssertions;
using TechTalk.SpecFlow;

namespace APITAF
{
    [Binding]
    public class StepDefinition1
    {
        private UserEndpoint userEndpoint = new UserEndpoint();

        #region Given
        [Given(@"I send a Get request to (.*) endpoint")]
        public void GivenISendAGetRequestToEndpoint(string endpoint)
        {
            userEndpoint.GetEndpoint(endpoint);
        }

        [Given(@"I send a Post request to (.*) endpoint")]
        public void GivenISendAPostRequestToEndpoint(string endpoint)
        {
            userEndpoint.PostEndpoint(endpoint);
        }
        #endregion

        #region When
        [When(@"the response is received")]
        public void WhenTheResponseIsReceived()
        {
            // Nothing specific needs to be done here since the response is captured in the Given step
        }
        #endregion

        #region Then

        [Then(@"the user should receive a (.*) response code")]
        public void ThenTheUserShouldReceiveACorrectResponseCode(HttpStatusCode statusCode)
        {
            userEndpoint.ValidateHttpStatusCode().Should().Be(statusCode);
        }

        [Then(@"the response should contain a list of users with necessary info should count 10")]
        public void ThenTheResponseShouldContainAListOfUsersWithNecessaryInfo()
        {
            userEndpoint.ThenTheResponseShouldContainAListOfUsersWithNecessaryInfo().Should().Be(10);
        }

        [Then(@"I should see ""(.*)"" in response headers")]
        public void ThenIShouldSeeInResponseHeaders(string header)
        {
            userEndpoint.ResponseHeadersContaints(header).Should().Be(true);
        }

        [Then(@"""(.*)"" header should have value ""(.*)""")]
        public void ThenHeaderShouldHaveValue(string header, string value)
        {
            userEndpoint.HeaderHaveValue(header, value);
        }

        [Then(@"there are no user id duplicates")]
        public void ThenThereAreNoUserIdDuplicates()
        {
            userEndpoint.UserId()
                .Should()
                .BeEmpty("Duplicate user ID found.");
        }

        [Then(@"the new created user in not empty and contains the id value")]
        public void ThenTheNewCreatedUserIsNotEmptyAndContainsTheIdValue()
        {
            var user = userEndpoint.UserInfo();

            user.Name.Should().Be("John Doe");
            user.Username.Should().Be("johndoe");
            user.Id.Should().NotBe(null);
        }
        #endregion
    }
}
