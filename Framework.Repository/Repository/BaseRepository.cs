namespace Framework.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseModel
    {
        readonly DbContext _dbContext;
        readonly IBaseDbContextProvider<FrameworkContext> _deContextProvider;
        public BaseRepository(IBaseDbContextProvider<FrameworkContext> dbContextProvider)
        {
            _deContextProvider = dbContextProvider;
            _dbContext=dbContextProvider.GetDbContext();
        }
        public async Task<TEntity> Add(TEntity entity)
        {
            var model = await _dbContext.Set<TEntity>().AddAsync(entity);
            return model.Entity;
        }
        public async Task AddRange(IEnumerable<TEntity> entity)
        {
            await _dbContext.Set<TEntity>().AddRangeAsync(entity);
        }
        public async Task<TEntity> Update(TEntity entity)
        {
            var model = _dbContext.Set<TEntity>().Update(entity);
            return await Task.FromResult(model.Entity);
        }
        public async Task UpdateRange(TEntity entity)
        {
            await Task.Run(() =>
            {
                _dbContext.Set<TEntity>().UpdateRange(entity);
            });
        }
        public void Delete(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }
        public void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            _dbContext.Set<TEntity>().RemoveRange(_dbContext.Set<TEntity>().Where(predicate));
        }
        public async Task<TEntity> GetEntity(Expression<Func<TEntity, bool>> predicate)
        {
            var enetity = await _dbContext.Set<TEntity>().FirstOrDefaultAsync(predicate);
            if (enetity == null)
            {
                throw new ApplicationException($"{nameof(TEntity)}不存在");
            }
            else
            {
                return enetity;
            }
        }
        public async Task<TEntity> GetFirstOrDefault(Expression<Func<TEntity, bool>> predicate, TEntity defaultEntity = null)
        {
            var enetity = await _dbContext.Set<TEntity>().FirstOrDefaultAsync(predicate);
            if (enetity == null)
            {
                return defaultEntity;
            }
            else
            {
                return enetity;
            }
        }
        public async Task<List<TEntity>> QueryEntity(Expression<Func<TEntity, bool>> predicate, Pages pager = null)
        {
            return await Pager.SplitPage(_dbContext.Set<TEntity>().Where(predicate), pager).ToListAsync();
        }
    }
}
