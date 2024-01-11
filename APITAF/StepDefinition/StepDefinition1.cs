using System.Collections.Generic;
using System.Linq;
using System.Net;
using FluentAssertions;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Portable;
using TechTalk.SpecFlow;
using Method = RestSharp.Method;
using RestRequest = RestSharp.RestRequest;
using RestResponse = RestSharp.RestResponse;

namespace APITAF
{
    [Binding]
    public class StepDefinition1
    {

        private RestClient _client;

        private RestRequest _request;

        private RestResponse _response;

        #region Given
        [Given(@"I send a Get request to (.*) endpoint")]
        public void GivenISendAGetRequestToEndpoint(string endpoint)
        {
            _client = new RestClient("https://jsonplaceholder.typicode.com");
            _request = new RestRequest(endpoint, Method.Get);
            _response = _client.ExecuteGet(_request);
        }
        
        [Given(@"I send a Post request to (.*) endpoint")]
        public void GivenISendAPostRequestToEndpoint(string endpoint)
        {
            _client = new RestClient("https://jsonplaceholder.typicode.com");

            var NewUser = new
            {
                Name = "John Doe",
                Username = "johndoe"
            };


            _request = new RestRequest(endpoint, Method.Post);
            _request.AddJsonBody(NewUser);
            _response = _client.ExecutePost(_request);
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

        [Then(@"the user should receive a 200 OK response code")]
        public void ThenTheUserShouldReceiveA200OKResponseCode()
        {
            _response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Then(@"the user should receive a 201 Created response code")]
        public void ThenTheUserShouldReceiveA201CreatedResponseCode()
        {
            _response.StatusCode.Should().Be(HttpStatusCode.Created);
        }
        [Then(@"the user should receive a 404 Not Found response code")]
        public void ThenTheUserShouldReceiveA404NotFoundResponseCode()
        {
            _response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Then(@"the response should contain a list of users with necessary info should count 10")]
        public void ThenTheResponseShouldContainAListOfUsersWithNecessaryInfo()
        {
            var content = JsonConvert.DeserializeObject<List<User>>(_response.Content);

            content.Count(u => u.Id != null &&
                                     u.Address != null &&
                                     u.Company != null &&
                                     u.Email != null &&
                                     u.Name != null &&
                                     u.Username != null &&
                                     u.Phone != null &&
                                     u.Website != null).Should().Be(10);
        }

        [Then(@"I should see ""(.*)"" in response headers")]
        public void ThenIShouldSeeInResponseHeaders(string header)
        {
            _response.ContentHeaders.Any(h => h.Name == header).Should().BeTrue();
        }
        // randomComments
        [Then(@"""(.*)"" header should have value ""(.*)""")]
        public void ThenHeaderShouldHaveValue(string header, string value)
        {
            var result = _response.ContentHeaders.Select(h => h.Value).First().ToString();
            result.Should().Be(value);
        }

        [Then(@"there are no user id duplicates")]
        public void ThenThereAreNoUserIdDuplicates()
        {
            var users = JsonConvert.DeserializeObject<List<User>>(_response.Content);
            users
                .GroupBy(user => user.Id)
                .Where(group => group.Count() > 1)
                .SelectMany(group => group)
                .Should()
                .BeEmpty("because duplicate user ID found.");
        }

        [Then(@"the new created user in not empty and contains the id value")]
        public void ThenTheNewCreatedUserIsNotEmptyAndContainsTheIdValue()
        {
            var user = JsonConvert.DeserializeObject<User>(_response.Content);

            user.Name.Should().Be("John Doe");
            user.Username.Should().Be("johndoe");
            user.Id.Should().NotBe(null);
        }
        #endregion
    }
}
