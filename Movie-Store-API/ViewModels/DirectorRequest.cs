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
    public class DirectorRequest
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required, DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }

        [NotMapped]
        public IFormFile UploadImage { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public string PlaceofBirth { get; set; }

        public DirectorResponse ToResponse(Director director)
        {
            return new DirectorResponse
            {
                ID = director.ID,
                BirthDate = director.BirthDate,
                FullName = director.FullName,
                Gender = director.Gender,
                PlaceofBirth = director.PlaceofBirth,
                Image = director.Image
            };
        }
    }
}
