using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Newtonsoft.Json;
using RestSharp;

namespace APITAF.APIRequest
{
    public class APIMethods
    {
        public List<User> GetUserList(string url)
        {
            var client = new RestClient("https://jsonplaceholder.typicode.com");

            var request = new RestRequest(url, Method.Get);

            var response = client.ExecuteGet(request);

            var content = JsonConvert.DeserializeObject<RootObject>(response.Content);

            return content.Users;

        }
    }
}
