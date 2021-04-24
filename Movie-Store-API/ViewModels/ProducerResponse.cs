using Movie_Store_Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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

        public List<Movie> Movies { get; set; }
    }
}
