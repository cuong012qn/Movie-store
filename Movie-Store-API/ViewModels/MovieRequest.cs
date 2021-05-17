using Microsoft.AspNetCore.Http;
using Movie_Store_Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Movie_Store_API.ViewModels
{
    public class MovieRequest
    {
        public int ID { get; set; }

        [Required]
        public string Title { get; set; }

        public IFormFile UploadImage { get; set; }

        [Required]
        public string Description { get; set; }

        [Required, DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [Required]
        public int IDProducer { get; set; }

        public List<Director> Directors { get; set; }
    }
}
