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
    public class DirectorApiClient : BaseApiClient, IDirectorApiClient
    {
        private readonly IConfiguration configuration;

        public DirectorApiClient(
            IHttpClientFactory httpClientFactory,
            IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration) : base(httpClientFactory, httpContextAccessor, configuration)
        {
            this.configuration = configuration;
        }


        public Task AddDirector(DirectorRequest director)
        {
            throw new NotImplementedException();
        }

        public Task<DirectorResponse> GetDirector(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<DirectorResponse> GetDirectors()
        {
            return await GetListAsync<DirectorResponse>("/api/director");
        }
    }
}
