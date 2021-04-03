using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie_store.Models
{
    public class MovieDirector
    {
        public int IDMovie { get; set; }
        public Movie Movie { get; set; }
        public int IDDirector { get; set; }
        public Director Director { get; set; }
    }
}
