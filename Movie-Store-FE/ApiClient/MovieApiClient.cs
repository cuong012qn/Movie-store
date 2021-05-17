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

        public async Task AddDirector(int idMovie, int idDirector)
        {
            var multipart = new MultipartFormDataContent();

            multipart.Add(new StringContent(idMovie.ToString()), "IdMovie");
            multipart.Add(new StringContent(idDirector.ToString()), "IdDirector");

            await PostAsync("/api/movie/adddirector", multipart);
        }

        public async Task AddMovie(MovieRequest movie)
        {
            var multiPart = new MultipartFormDataContent();

            multiPart.Add(new StringContent(movie.Title), "Title");
            multiPart.Add(new StringContent(movie.Description), "Description");
            multiPart.Add(new StringContent(movie.ReleaseDate.ToString("yyyy-MM-dd")), "ReleaseDate");
            multiPart.Add(new StringContent(movie.IDProducer.ToString()), "IDProducer");
            multiPart.Add(new StreamContent(movie.UploadImage.OpenReadStream()), "UploadImage", movie.UploadImage.FileName);

            await PostAsync("/api/movie", multiPart);
        }

        public async Task<MovieResponse> GetMovie(int id)
        {
            return await GetAsync<MovieResponse>($"/api/movie/{id}");
        }

        public async Task<MovieResponse> GetMovies()
        {
            return await GetListAsync<MovieResponse>("/api/movie");
        }

        public async Task RemoveMovie(int id)
        {
            await RemoveAsync<MovieResponse>($"/api/movie/{id}");
        }

        public async Task UpdateMovie(int id, MovieRequest movieRequest)
        {
            var multipart = new MultipartFormDataContent();

            multipart.Add(new StringContent(movieRequest.Title), "Title");
            multipart.Add(new StringContent(movieRequest.Description), "Description");
            multipart.Add(new StringContent(movieRequest.ReleaseDate.ToString("yyyy-MM-dd")), "ReleaseDate");
            multipart.Add(new StringContent(movieRequest.IDProducer.ToString()), "IDProducer");
            if (movieRequest.UploadImage != null)
                multipart.Add(new StreamContent(movieRequest.UploadImage.OpenReadStream()), "UploadImage", movieRequest.UploadImage.FileName);

            await UpdateAsync<MovieResponse>($"/api/movie/{id}", multipart);
        }
    }
}
