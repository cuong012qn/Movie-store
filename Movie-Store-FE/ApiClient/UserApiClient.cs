using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Movie_Store_FE.ApiClient.Interface;
using Movie_Store_FE.ViewModels;
using Newtonsoft.Json;
using Movie_Store_Data.Models;
using Microsoft.AspNetCore.Http;

namespace Movie_Store_FE.ApiClient
{
    public class UserApiClient : IUserApiClient
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly string api;

        public UserApiClient(
            IHttpContextAccessor httpContextAccessor,
            IHttpClientFactory httpClientFactory, 
            IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;

            api = _configuration.GetValue<string>("Api");
        }

        public async Task<UserResponse> Authencate(User user)
        {
            var json = JsonConvert.SerializeObject(user);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(api);

            var response = await client.PostAsync("/api/user/login", httpContent);
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<UserResponse>(body);
            }

            return JsonConvert.DeserializeObject<UserResponse>(body);
        }

        public async Task<UserResponse> Register(User user)
        {
            var httpContent = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(api);

            var response = await client.PostAsync("/api/user/register", httpContent);
            var body = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == System.Net.HttpStatusCode.Created)
            {
                return JsonConvert.DeserializeObject<UserResponse>(body);
            }

            return JsonConvert.DeserializeObject<UserResponse>(body);
        }
    }
}
