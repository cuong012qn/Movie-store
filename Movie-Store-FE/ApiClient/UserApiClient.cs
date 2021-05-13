using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Movie_Store_FE.ApiClient.Interface;
using Movie_Store_FE.Models;
using Movie_Store_FE.ViewModels;
using Newtonsoft.Json;

namespace Movie_Store_FE.ApiClient
{
    public class UserApiClient : IUserApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public UserApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<UserResponse> Authencate(User user)
        {
            var json = JsonConvert.SerializeObject(user);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("http://35.247.188.44");

            var response = await client.PostAsync("/api/user/login", httpContent);
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<UserResponse>(await response.Content.ReadAsStringAsync());
            }

            return new UserResponse
            {
                Success = false,
                Token = string.Empty
            };
        }
    }
}
