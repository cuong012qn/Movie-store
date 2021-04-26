using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Movie_Store_API.ViewModels;

namespace Movie_Store_API.Repository.Interface
{
    public interface IProducerRepository
    {
        Task<List<ProducerResponse>> GetProducersAsync();

        Task<ProducerResponse> GetProducerByIDAsync(int id);

        Task<ProducerResponse> AddProducerAsync(ProducerRequest producerRequest);

        Task RemoveProducerAsync(int idProducer);

        Task<ProducerResponse> UpdateProducerAsync(int idProducer, ProducerRequest producerRequest);

        Task SaveChangesAsync();
    }
}
