using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Movie_store.Data;
using Movie_store.Models;

namespace Movie_store.Data
{
    public class SeedData
    {
        private readonly MovieDBContext _context;
        private readonly string DatetimeFormat = "yyyy-MM-dd";
        private readonly CultureInfo culture = CultureInfo.InvariantCulture;
        public SeedData(MovieDBContext context)
        {
            _context = context;
        }

        private void AddTheFlash()
        {
            //The Flash
            var producerFlash = new Producer()
            {
                FullName = "The CW",
                IsOrganization = true
            };

            _context.Producers.Add(producerFlash);
            _context.SaveChanges();

            var movie = new Movie()
            {
                Title = "The Flash",
                Description = @"After a particle accelerator causes a freak storm,
                CSI Investigator Barry Allen is struck by lightning and falls into a coma.Months later he awakens with the power of super speed,
                granting him the ability to move through Central City like an unseen guardian angel.Though initially excited by his newfound powers,
                Barry is shocked to discover he is not the only ""meta - human"" who was created in the wake of the accelerator explosion-- and not everyone is using their new powers for good.Barry partners with S.T.A.R.Labs and dedicates his life to protect the innocent.For now, only a few close friends and associates know that Barry is literally the fastest man alive, but it won't be long before the world learns what Barry Allen has become...The Flash.",

                Image = "the-flashjpg.jpg",
                ReleaseDate = DateTime.ParseExact("07/10/2014", "dd/MM/yyyy", culture),
                IDProducer = producerFlash.ID
            };


            _context.Movies.Add(movie);
            _context.SaveChanges();

            List<Director> directors = new List<Director>();
            directors.Add(new Director()
            {
                FullName = "Grant Gustin",
                BirthDate = DateTime.ParseExact("1990-01-14", DatetimeFormat, culture),
                Image = "grant-gustin.jpg",
                PlaceofBirth = "Cali, Colombia",
                Gender = "Male"
            });

            directors.Add(new Director()
            {
                FullName = "Carlos Valdes",
                BirthDate = DateTime.ParseExact("1989-04-20", DatetimeFormat, culture),
                Image = "carlos-valdes.jpg",
                PlaceofBirth = "Cali, Colombia",
                Gender = "Male"
            });

            directors.Add(new Director()
            {
                FullName = "Danielle Panabaker",
                BirthDate = DateTime.ParseExact("1987-09-19", DatetimeFormat, culture),
                Image = "danielle-panabaker.jpg",
                PlaceofBirth = "Augusta, Georgia, USA",
                Gender = "Female"
            });

            directors.Add(new Director()
            {
                FullName = "Candice Patton",
                BirthDate = DateTime.ParseExact("1987-09-19", DatetimeFormat, culture),
                Image = "candice-patton.jpg",
                PlaceofBirth = "Augusta, Georgia, USA",
                Gender = "Female"
            });

            _context.Directors.AddRange(directors);
            _context.SaveChanges();

            List<MovieDirector> movieDirectors = new List<MovieDirector>();

            for (int i = 0; i < directors.Count; i++)
            {
                movieDirectors.Add(new MovieDirector()
                {
                    IDDirector = directors[i].ID,
                    IDMovie = movie.ID
                });
            }

            _context.MovieDirectors.AddRange(movieDirectors);
            _context.SaveChanges();

        }

        private void AddGreyAnatomy()
        {
            Producer producer = new Producer()
            {
                FullName = "American Broadcasting Company",
                IsOrganization = true
            };

            _context.Producers.Add(producer);
            _context.SaveChanges();

            Movie movie = new Movie()
            {
                Description = "Follows the personal and professional lives of a group of doctors at Seattle’s Grey Sloan Memorial Hospital.",
                Title = "Grey's Anatomy",
                Image = "grey-anatomy.jpg",
                ReleaseDate = DateTime.ParseExact("27/03/2005","dd/MM/yyyy",culture),
                IDProducer = producer.ID
            };

            _context.Movies.Add(movie);
            _context.SaveChanges();

            List<Director> directors = new List<Director>();
            directors.Add(new Director()
            {
                FullName = "Chandra Wilson",
                BirthDate = DateTime.ParseExact("1969-08-27", DatetimeFormat, culture),
                PlaceofBirth = "Houston, Texas, USA",
                Gender = "Female",
                Image = "chandra-wilson.jpg"
            });

            directors.Add(new Director()
            {
                FullName = "Ellen Pompeo",
                BirthDate = DateTime.ParseExact("1969-11-10",  DatetimeFormat, culture),
                PlaceofBirth = "Everett, Massachusetts, USA",
                Gender = "Female",
                Image = "ellen-pompeo.jpg"
            });

            directors.Add(new Director()
            {
                FullName = "James Pickens Jr.",
                BirthDate = DateTime.ParseExact("1954-10-26", DatetimeFormat, culture),
                PlaceofBirth = "Cleveland, Ohio, USA",
                Gender = "Male",
                Image = "james-pickens-jr.jpg"
            });

            directors.Add(new Director()
            {
                FullName = "Justin Chambers",
                BirthDate = DateTime.ParseExact("1970-07-11", DatetimeFormat, culture),
                PlaceofBirth = "Springfield, Ohio, USA",
                Gender = "Male",
                Image = "justin-chambers.jpg"
            });

            _context.Directors.AddRange(directors);
            _context.SaveChanges();

            for (int i = 0; i < directors.Count; i++)
            {
                _context.MovieDirectors.Add(new MovieDirector()
                {
                    IDDirector = directors[i].ID,
                    IDMovie = movie.ID
                });
            }

            _context.SaveChanges();
        }

        public void Seed()
        {
            if (!_context.Producers.Any() && !_context.Movies.Any() && !_context.Movies.Any())
            {
                AddTheFlash();
                AddGreyAnatomy();
            }
        }
    }
}
