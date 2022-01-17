using Application.Repositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public abstract class GenericRepository<TModel, TEntity> : IGenericRepository<TModel, TEntity> where TEntity : class
    {
        protected IMapper _mapper;
        protected MyDbContext DbContext { get; }

        public GenericRepository(MyDbContext dbContext, IMapper mapper)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<bool> Create(TModel model)
        {
            var entity = _mapper.Map<TEntity>(model);
            await DbContext.Set<TEntity>().AddAsync(entity);

            try
            {
                var result = await DbContext.SaveChangesAsync();
                DbContext.Entry(entity).State = EntityState.Detached;
                model = _mapper.Map<TModel>(entity);
                return result > 0;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }

        public async Task<bool> BulkCreate(IEnumerable<TModel> model)
        {
            var entities = _mapper.Map<IEnumerable<TEntity>>(model);
            await DbContext.Set<TEntity>().AddRangeAsync(entities);

            try
            {
                var result = await DbContext.SaveChangesAsync();
                model = _mapper.Map<IEnumerable<TModel>>(entities);
                return result > 0;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }

        public async Task<bool> Delete(params object[] id)
        {
            var entity = await DbContext.Set<TEntity>().FindAsync(id);

            if (entity is null)
            {
                return false;
            }

            try
            {
                DbContext.Set<TEntity>().Remove(entity);
                await DbContext.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }

        public async Task<IEnumerable<TModel>> GetAll()
        {
            var entities = await DbContext.Set<TEntity>().ToListAsync();
            var models = _mapper.Map<IEnumerable<TEntity>, IEnumerable<TModel>>(entities);
            return models;
        }

        public async Task<TModel> GetById(params object[] key)
        {
            var entity = await DbContext.Set<TEntity>().FindAsync(key);
            if (entity != null)
                DbContext.Entry<TEntity>(entity).State = EntityState.Detached;

            var model = _mapper.Map<TModel>(entity);
            return model;
        }

        public async Task<bool> Update(TModel model)
        {
            var entity = _mapper.Map<TEntity>(model);
            if (entity != null)
                DbContext.Entry<TEntity>(entity).State = EntityState.Detached;

            DbContext.Set<TEntity>().Update(entity);
            try
            {
                var result = await DbContext.SaveChangesAsync() > 0;
                DbContext.Entry<TEntity>(entity).State = EntityState.Detached;
                return result;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }
    }
}
