using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using APITAF.APIRequest;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
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

        
        private readonly ScenarioContext _scenarioContext;

        public StepDefinition1(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"I send a (.*) request to (.*) endpoint")]
        public void GivenISendARequestToEndpoint(string verb, string endpoint)
        {
            _client = new RestClient("https://jsonplaceholder.typicode.com");
            _request = new RestRequest(endpoint, Method.Get);
            _response = _client.ExecuteGet(_request);
        }


        [When(@"the response is received")]
        public void WhenTheResponseIsReceived()
        {
            // Nothing specific needs to be done here since the response is captured in the Given step
        }
        
        [Then(@"the user should receive a 200 OK response code")]
        public void ThenTheUserShouldReceiveA200OKResponseCode()
        {
            _response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Then(@"the response should contain a list of users with the following information:")]
        public void ThenTheResponseShouldContainAListOfUsersWithTheFollowingInformation()
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
            _response.ContentHeaders.Any(h=>h.Name==header).Should().BeTrue();
        }
        // randomComments
        [Then(@"""(.*)"" header should have value ""(.*)""")]
        public void ThenHeaderShouldHaveValue(string header, string value)
        {
            var result = _response.ContentHeaders.Select(h => h.Value).First().ToString();
            result.Should().Be(value);
        }

    }
}
