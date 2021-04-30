using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Movie_Store_Data.Models;

namespace Movie_Store_API.ViewModels
{
    public class MovieResponse
    {
        public int ID { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string ImagePath { get; set; }

        [Required]
        public string Description { get; set; }

        [Required, DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ProducerResponse Producer { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<DirectorResponse> Directors { get; set; }
    }
}
