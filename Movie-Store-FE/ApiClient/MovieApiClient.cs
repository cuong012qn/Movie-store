using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Movie_Store_FE.ApiClient.Interface;
using Movie_Store_FE.ViewModels;

namespace Movie_Store_FE.ApiClient
{
    public class MovieApiClient : BaseApiClient, IMovieApiClient
    {
        private readonly IConfiguration configuration;

        public MovieApiClient(
            IHttpClientFactory httpClientFactory,
            IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration) : base(httpClientFactory, httpContextAccessor, configuration)
        {
            this.configuration = configuration;
        }

        public async Task AddMovie(MovieRequest movie)
        {
            var multiPart = new MultipartFormDataContent();

            multiPart.Add(new StringContent(movie.Title), "Title");
            multiPart.Add(new StringContent(movie.Description), "Description");
            multiPart.Add(new StringContent(movie.ReleaseDate.ToString("yyyy-MM-dd")), "ReleaseDate");
            multiPart.Add(new StringContent(movie.IDProducer.ToString()), "IDProducer");
            multiPart.Add(new StreamContent(movie.UploadImage.OpenReadStream()), "UploadImage", movie.UploadImage.FileName);

            await PostAysnc("/api/movie", multiPart);
        }

        public async Task<MovieResponse> GetMovie(int id)
        {
            return await GetAsync<MovieResponse>($"/api/movie/{id}");
        }

        public async Task<MovieResponse> GetMovies()
        {
            return await GetListAsync<MovieResponse>("/api/movie");
        }
    }
}
