using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Repository.UnitOfWork
{
    public interface IUintOfWork
    {
        IDbContextTransaction CurrentTransaction { get; }
        Task SaveChangesAsync();
        void Clean();
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
    }
}
