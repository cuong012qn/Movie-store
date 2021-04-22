using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Movie_Store_API.Models
{
    [Table("Producer")]
    public class Producer
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 0)]
        public int ID { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public bool IsOrganization { get; set; }

        public List<Movie> Movies { get; set; }
    }
}
