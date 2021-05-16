﻿using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Movie_Store_FE.ViewModels
{
    public class DirectorResponse : BaseResponse
    {
        public List<Director> Directors { get; set; }
        public Director Director { get; set; }
    }

    public class Director
    {

        [Key]
        public int ID { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public String BirthDate { get; set; }

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
