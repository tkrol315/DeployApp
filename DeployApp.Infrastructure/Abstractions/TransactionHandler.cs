using DeployApp.Application.Abstractions;
using DeployApp.Infrastructure.EF.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace DeployApp.Infrastructure.Abstractions
{
    public class TransactionHandler : ITransactionHandler
    {
        private readonly DeployAppDbContext _context;

        public TransactionHandler(DeployAppDbContext context)
        {
            _context = context;
        }

        public IDbTransaction BeginTransaction()
        {
            var transaction = _context.Database.BeginTransaction();
            return transaction.GetDbTransaction();
        }
    }
}
