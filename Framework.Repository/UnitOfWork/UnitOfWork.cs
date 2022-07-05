

namespace Framework.Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {

        FrameworkDBContextFactory<FrameworkContext> _contextFactory;
        FrameworkContext _dbContext { get { return _contextFactory.Current; } }
        public UnitOfWork(FrameworkDBContextFactory<FrameworkContext> contextFactory)
        {

            _contextFactory = contextFactory;
        }
        public IDbContextTransaction CurrentTransaction { get {  _contextFactory.SetReadWrite(ReadWriteType.ForceWrite); return _dbContext.Database.CurrentTransaction; } }

        public void BeginTransaction()
        {
            if (CurrentTransaction == null)
            {
                _dbContext.Database.BeginTransaction();
            }
        }

        public void Clean()
        {
            _dbContext.ChangeTracker.DetectChanges();

            // 有Unchanged的对象
            List<EntityEntry> objects = _dbContext.ChangeTracker.Entries().Where(x => x.State == EntityState.Unchanged).ToList();

            foreach (EntityEntry obj in objects)
            {
                // Detach对象
                obj.State = EntityState.Detached;
            }
        }

        public void CommitTransaction()
        {
            if (CurrentTransaction != null)
            {
                _dbContext.Database.CommitTransaction();
            }
        }

        public void RollbackTransaction()
        {
            if (CurrentTransaction != null)
            {
                _dbContext.Database.RollbackTransaction();
            }
        }

        public async Task SaveChangesAsync()
        {
            // 共通字段            
            _dbContext.ChangeTracker.DetectChanges();

            //// 有新增的对象
            //foreach (object entity in _dbContext.ChangeTracker.Entries().Where(x => x.State == EntityState.Added).Select(x => x.Entity))
            //{
            //    if (entity.GetType().GetProperty("CreateUserID") != null && entity.GetType().GetProperty("CreateUserID").GetValue(entity) == null)
            //    {
            //        entity.GetType().GetProperty("CreateUserID").SetValue(entity, _user.ID);
            //    }

            //    if (entity.GetType().GetProperty("CreateTime") != null)
            //    {
            //        entity.GetType().GetProperty("CreateTime").SetValue(entity, DateTime.Now);
            //    }

            //    if (entity.GetType().GetProperty("UpdateUserID") != null && entity.GetType().GetProperty("UpdateUserID").GetValue(entity) == null)
            //    {
            //        entity.GetType().GetProperty("UpdateUserID").SetValue(entity, _user.ID);
            //    }

            //    if (entity.GetType().GetProperty("UpdateTime") != null)
            //    {
            //        entity.GetType().GetProperty("UpdateTime").SetValue(entity, DateTime.Now);
            //    }
            //}

            //// 有更新的对象
            //foreach (object entity in _dbContext.ChangeTracker.Entries().Where(x => x.State == EntityState.Modified).Select(x => x.Entity))
            //{
            //    if (entity.GetType().GetProperty("UpdateUserID") != null)
            //    {
            //        entity.GetType().GetProperty("UpdateUserID").SetValue(entity, _user.ID);
            //    }

            //    if (entity.GetType().GetProperty("UpdateTime") != null)
            //    {
            //        entity.GetType().GetProperty("UpdateTime").SetValue(entity, DateTime.Now);
            //    }
            //}

            await _dbContext.SaveChangesAsync();
        }
    }
}
