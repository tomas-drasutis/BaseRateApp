using AutoMapper;
using BaseRateApp.Models.Response;
using BaseRateApp.Persistance;
using BaseRateApp.Services.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRateApp.Services.Repositories.Implementations
{
    public abstract class BaseRepository<TReadModel, TWriteModel, TEntity> : IBaseRepository<TReadModel, TWriteModel, Guid>
        where TReadModel : IResponseModel<Guid>
        where TEntity : BaseEntity
    {
        protected readonly IDatabaseContext dbContext;
        protected readonly IMapper Mapper;

        protected BaseRepository(IDatabaseContext databaseContext, IMapper mapper)
        {
            dbContext = databaseContext;
            Mapper = mapper;
        }

        public virtual async Task<TReadModel> GetById(Guid id)
        {
            var entity = await GetEntityById(id);

            return ConvertToResponseModel(entity);
        }

        public virtual async Task<IEnumerable<TReadModel>> GetAll()
        {
            return (await dbContext
                .Set<TEntity>()
                .ToListAsync())
                .Select(ConvertToResponseModel);
        }

        public virtual async Task<TReadModel> Create(TWriteModel model)
        {
            var entity = await ConvertToEntity(model);

            await dbContext.Set<TEntity>().AddAsync(entity);
            await dbContext.SaveChangesAsync();

            return ConvertToResponseModel(entity);
        }

        public virtual async Task<TReadModel> Delete(Guid id)
        {
            var entity = await GetEntityById(id);

            dbContext.Set<TEntity>().Remove(entity);
            await dbContext.SaveChangesAsync();

            return ConvertToResponseModel(entity);
        }

        public virtual async Task CreateRange(IEnumerable<TWriteModel> models)
        {
            var entities = await Task.WhenAll(models.Select(async m => await ConvertToEntity(m)));

            await dbContext.Set<TEntity>().AddRangeAsync(entities);
            await dbContext.SaveChangesAsync();
        }

        public virtual async Task<TReadModel> Update(Guid id, TWriteModel model)
        {
            var entityToUpdate = await GetEntityById(id);
            var updatedEntity = UpdateEntity(entityToUpdate, model);
            await Validate(updatedEntity);

            await dbContext.SaveChangesAsync();

            return ConvertToResponseModel(updatedEntity);
        }

        protected abstract TEntity UpdateEntity(TEntity entity, TWriteModel model);

        protected async Task<TEntity> GetEntityById(Guid id)
        {
            var entity = await dbContext.Set<TEntity>().FirstOrDefaultAsync(e => e.Id == id);

            if (entity == null)
                throw new DomainException(DomainExceptionType.NotFound, $"{typeof(TEntity).Name} (id: {id}) was not found");

            return entity;
        }

        protected virtual Task Validate(TEntity entity)
        {
            return Task.CompletedTask;
        }

        protected TReadModel ConvertToResponseModel(TEntity entity) => Mapper.Map<TReadModel>(entity);

        protected async Task<TEntity> ConvertToEntity(TWriteModel model)
        {
            var entity = Mapper.Map<TEntity>(model);
            await Validate(entity);
            return entity;
        }
    }
}
