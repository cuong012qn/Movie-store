using Movie_Store_API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie_Store_API.Repository.Interface
{
    public interface IDirectorRepository
    {
        Task<List<DirectorResponse>> GetDirectorsAsync();

        Task<DirectorResponse> GetDirectorByIDAsync(int id);

        Task<DirectorResponse> AddDirectorAsync(DirectorRequest directorRequest);

        Task RemoveDirectorAsync(int idDirector);

        Task<DirectorResponse> UpdateProducerAsync(int idDirector, 
            DirectorRequest producerRequest);

        Task SaveChangesAsync();
    }
}
