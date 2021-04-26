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

        public async Task<ProducerResponse> AddProducerAsync(ProducerRequest producerRequest)
        {
            var producer = new Producer
            {
                FullName = producerRequest.FullName,
                IsOrganization = producerRequest.IsOrganization
            };

            await _context.Producers.AddAsync(producer);
            await SaveChangesAsync();

            return new ProducerResponse
            {
                ID = producer.ID,
                IsOrganization = producer.IsOrganization,
                FullName = producer.FullName
            };
        }

        public async Task<ProducerResponse> GetProducerByIDAsync(int id)
        {
            var producer = await _context.Producers
                .Include(x => x.Movies)
                .SingleOrDefaultAsync(x => x.ID.Equals(id));

            if (producer == null) return null;

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
                .Select(x => new ProducerResponse
                {
                    ID = x.ID,
                    FullName = x.FullName,
                    IsOrganization = x.IsOrganization,
                    Movies = x.Movies
                }).ToListAsync();
        }

        public async Task RemoveProducerAsync(int idProducer)
        {
            var producer = await _context.Producers.SingleOrDefaultAsync(x => x.ID.Equals(idProducer));

            _context.Remove(producer);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<ProducerResponse> UpdateProducerAsync(int idProducer, 
            ProducerRequest producerRequest)
        {
            var findProducer = await _context.Producers.SingleOrDefaultAsync(x => x.ID.Equals(idProducer));

            if (findProducer != null)
            {
                findProducer.FullName = producerRequest.FullName;
                findProducer.IsOrganization = producerRequest.IsOrganization;

                _context.Producers.Update(findProducer);
                await SaveChangesAsync();

                return new ProducerResponse
                {
                    ID = findProducer.ID,
                    FullName = findProducer.FullName,
                    IsOrganization = findProducer.IsOrganization,
                    Movies = findProducer.Movies
                };
            }
            return null;
        }
    }
}
