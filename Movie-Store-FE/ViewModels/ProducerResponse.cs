using Movie_Store_Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Movie_Store_FE.ViewModels
{
    public class ProducerResponse : BaseResponse
    {
        public List<Producer> Producers { get; set; }
    }

    public class Producer
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [DisplayName("Fullname")]
        public string FullName { get; set; }

        [Required]
        [DisplayName("Organization")]
        public bool IsOrganization { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<MovieResponse> Movies { get; set; }
    }
}
