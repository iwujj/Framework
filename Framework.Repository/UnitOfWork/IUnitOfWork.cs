namespace Framework.Repository.UnitOfWork
{
    public interface IUnitOfWork
    {
        IDbContextTransaction CurrentTransaction { get; }
        Task SaveChangesAsync();
        void Clean();
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
    }
}
