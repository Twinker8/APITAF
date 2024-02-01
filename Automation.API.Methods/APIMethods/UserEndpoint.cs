using Automation.API.Mappers.APIMaps;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;

namespace Automation.API.Methods.APIMethods
{
    public class UserEndpoint
    {
        private RestClient _client;

        private RestRequest _request;

        private RestResponse _response;

        private string baseURL = "https://jsonplaceholder.typicode.com";
        
        public void GetEndpoint(string endPoint)
        {
            _client = new RestClient(baseURL);
            _request = new RestRequest(endPoint, Method.Get);
            _response = _client.ExecuteGet(_request);
        }
        public HttpStatusCode ValidateHttpStatusCode()
        {
            return _response.StatusCode;
        }
        public int ThenTheResponseShouldContainAListOfUsersWithNecessaryInfo()
        {
            var content = JsonConvert.DeserializeObject<List<User>>(_response.Content);

            return content.Count(u => u.Id != null &&
                                     u.Address != null &&
                                     u.Company != null &&
                                     u.Email != null &&
                                     u.Name != null &&
                                     u.Username != null &&
                                     u.Phone != null &&
                                     u.Website != null);
        }

        public void PostEndpoint(string endPoint)
        {
            _client = new RestClient("https://jsonplaceholder.typicode.com");

            var NewUser = new
            {
                Name = "John Doe",
                Username = "johndoe"
            };

            _request = new RestRequest(endPoint, Method.Post);
            _request.AddJsonBody(NewUser);
            _response = _client.ExecutePost(_request);
        }
        public bool ResponseHeadersContaints(string header)
        {
            return _response.ContentHeaders.Any(h => h.Name == header);
        }
        public string HeaderHaveValue(string header, string value)
        {
            return _response.ContentHeaders.Select(h => h.Value).First().ToString();
        }
        public IEnumerable<User> UserId()
        {
            var users = JsonConvert.DeserializeObject<List<User>>(_response.Content);
            return users
                .GroupBy(user => user.Id)
                .Where(group => group.Count() > 1)
                .SelectMany(group => group);
        }
        public User UserInfo()
        {
            return JsonConvert.DeserializeObject<User>(_response.Content);
        }
    }
}
