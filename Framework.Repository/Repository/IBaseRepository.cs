namespace Framework.Repository
{
    public interface IBaseRepository<TEntity> where TEntity : BaseModel
    {
        Task<TEntity> Add(TEntity entity);
        Task AddRange(IEnumerable<TEntity> entity);
        Task<TEntity> Update(TEntity entity);
        Task UpdateRange(TEntity entity);
        void Delete(TEntity entity);
        void Delete(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> GetEntity(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> GetFirstOrDefault(Expression<Func<TEntity, bool>> predicate, TEntity defaultEntity = null);

        Task<List<TEntity>> QueryEntity(Expression<Func<TEntity, bool>> predicate, Pages pager = null);
    }
}
