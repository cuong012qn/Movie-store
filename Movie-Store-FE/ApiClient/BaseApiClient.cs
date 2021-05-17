using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Movie_Store_FE.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Movie_Store_FE.ApiClient
{
    public class BaseApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        protected BaseApiClient(IHttpClientFactory httpClientFactory,
                   IHttpContextAccessor httpContextAccessor,
                    IConfiguration configuration)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _httpClientFactory = httpClientFactory;
        }

        public async Task PostAsync(string url, HttpContent httpContent)
        {
            var sessions = _httpContextAccessor
                .HttpContext
                .Session
                .GetString("Token");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration.GetValue<string>("Api"));

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);


            var result = await client.PostAsync(url, httpContent);
            Console.WriteLine(await result.Content.ReadAsStringAsync());
        }

        public async Task<T> GetAsync<T>(string url)
        {
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration.GetValue<string>("Api"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                return (T)JsonConvert.DeserializeObject(body, typeof(T));
            }

            return default(T);
        }

        public async Task<T> RemoveAsync<T>(string url)
        {
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration.GetValue<string>("Api"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var response = await client.DeleteAsync(url);
            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                var body = await response.Content.ReadAsStringAsync();
                return (T)JsonConvert.DeserializeObject(body, typeof(T));
            }

            return default(T);
        }

        public async Task<T> UpdateAsync<T>(string url, HttpContent content)
        {
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration.GetValue<string>("Api"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var response = await client.PutAsync(url, content);
            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                var body = await response.Content.ReadAsStringAsync();
                return (T)JsonConvert.DeserializeObject(body, typeof(T));
            }

            return default(T);
        }

        public async Task<T> GetListAsync<T>(string url)
        {
            var sessions = _httpContextAccessor
              .HttpContext
              .Session
              .GetString("Token");
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration.GetValue<string>("Api"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var response = await client.GetAsync(url);
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return (T)JsonConvert.DeserializeObject(body, typeof(T));
            }
            return default(T);
        }
    }
}
