using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Movie_Store_Data.Models;

namespace Movie_Store_FE.ViewModels
{
    public class MovieResponse : BaseResponse
    {
        public List<Movie> Movies { get; set; }

        [DefaultValue(null)]
        public Movie Movie { get; set; }
    }

    public class Movie
    {
        public int ID { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string ImagePath { get; set; }

        public IFormFile UploadImage { get; set; }

        [Required]
        public int IDProducer { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public String ReleaseDate { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Producer Producer { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<Director> Directors { get; set; }
    }
}
