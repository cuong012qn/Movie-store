using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Movie_Store_FE.ApiClient.Interface;
using Movie_Store_FE.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Movie_Store_FE.ApiClient
{
    public class ProducerApiClient : BaseApiClient, IProducerApiClient
    {
        private readonly IConfiguration _configuration;
        private readonly string api;

        public ProducerApiClient(
            IHttpContextAccessor httpContextAccessor,
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration) : base(httpClientFactory, httpContextAccessor, configuration)
        {
            _configuration = configuration;

            api = _configuration.GetValue<string>("Api");
        }

        public async Task<ProducerResponse> GetProducers()
        {
            return await GetListAsync<ProducerResponse>("/api/producer");
        }
    }
}
