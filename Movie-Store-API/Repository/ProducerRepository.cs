using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Movie_Store_API.Repository.Interface;
using Movie_Store_API.ViewModels;
using Movie_Store_Data.Data;
using Microsoft.EntityFrameworkCore;
using Movie_Store_Data.Models;

namespace Movie_Store_API.Repository
{
    public class ProducerRepository : IProducerRepository
    {
        private readonly MovieDBContext _context;

        public ProducerRepository(MovieDBContext context)
        {
            _context = context;
        }

        public async Task AddProducerAsync(ProducerRequest producerRequest)
        {
            await _context.Producers.AddAsync(new Producer
            {
                FullName = producerRequest.FullName,
                IsOrganization = producerRequest.IsOrganization
            });
        }

        public async Task<ProducerResponse> GetProducerByIDAsync(int id)
        {
            var producer = await _context.Producers
                .Include(x=> x.Movies)
                .SingleOrDefaultAsync(x => x.ID.Equals(id));

            return new ProducerResponse()
            {
                ID = producer.ID,
                FullName = producer.FullName,
                IsOrganization = producer.IsOrganization,
                Movies = producer.Movies
            };
        }

        public async Task<List<ProducerResponse>> GetProducersAsync()
        {
            return await _context.Producers
                .Include(movie => movie.Movies)
                .Select(x => new ProducerResponse {
                ID = x.ID,
                FullName = x.FullName,
                IsOrganization = x.IsOrganization,
                Movies = x.Movies
            }).ToListAsync();
        }

        public async Task RemoveProducerAsync(ProducerRequest producerRequest)
        {
            var producer = await _context.Producers.SingleOrDefaultAsync(x => x.ID.Equals(producerRequest.ID));

            _context.Remove(producer);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void UpdateProducer(ProducerRequest producerRequest)
        {
            throw new NotImplementedException();
        }
    }
}
