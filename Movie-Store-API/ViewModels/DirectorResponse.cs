using Microsoft.AspNetCore.Http;
using Movie_Store_Data.BaseModels;
using Movie_Store_Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Movie_Store_API.ViewModels
{
    public class DirectorResponse
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required, DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Required]
        public string Image { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public string PlaceofBirth { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<MovieResponse> Movies { get; set; }
    }
}
