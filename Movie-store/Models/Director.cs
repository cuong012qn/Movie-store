using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Movie_store.Models
{
    [Table("Director")]
    public class Director
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 0)]
        public int ID { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        public string Image { get; set; }

        [NotMapped]
        public IFormFile UploadImage { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public string PlaceofBirth { get; set; }

        public IList<MovieDirector> MovieDirectors { get; set; }
    }
}
