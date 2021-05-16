using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Movie_Store_FE.ViewModels
{
    public class MovieRequest
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public IFormFile UploadImage { get; set; }

        [Required]
        public string Description { get; set; }

        [Required, DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [Required]
        public int IDProducer { get; set; }
    }
}
