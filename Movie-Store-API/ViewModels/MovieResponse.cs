using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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

        [Required]
        public Producer Producer { get; set; }

        [Required]
        public List<Director> Directors { get; set; }
    }
}
