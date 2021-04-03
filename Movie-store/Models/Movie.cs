using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Movie_store.Models
{
    [Table("Movie")]
    public class Movie
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 0)]
        public int ID { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Image { get; set; }

        [NotMapped]
        public IFormFile UploadImage { get; set; }

        [Required]
        public string Description { get; set; }

        [Required, DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [Required]
        public int IDProducer { get; set; }

        [ForeignKey("IDProducer")]
        public Producer Producer { get; set; }

        public IList<MovieDirector> MovieDirectors { get; set; }

    }
}
