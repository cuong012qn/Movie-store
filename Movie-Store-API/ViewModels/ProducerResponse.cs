using Movie_Store_Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Movie_Store_API.ViewModels
{
    public class ProducerResponse
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public bool IsOrganization { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<MovieResponse> Movies { get; set; }
    }
}
