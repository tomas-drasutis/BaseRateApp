using BaseRateApp.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BaseRateApp.Services.Repositories
{
    public interface IBaseRepository<TReadModel, TWriteModel, TKey>
        where TReadModel : IResponseModel<TKey>
    {
        Task<TReadModel> GetById(TKey id);
        Task<TReadModel> Create(TWriteModel model);
        Task<TReadModel> Update(TKey id, TWriteModel model);
        Task<IEnumerable<TReadModel>> GetAll();
        Task<TReadModel> Delete(Guid id);
    }
}
