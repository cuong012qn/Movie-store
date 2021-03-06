using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Movie_Store_Data.Models
{
    public class User
    {
        [Key]
        public string ID { get; set; }
        
        [Required]
        public string FullName { get; set; }

        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        public string Username { get; set; }

        [Required, JsonIgnore, DataType(DataType.Password)]
        public string Password { get; set; }

        public string Token { get; set; }
    }
}
