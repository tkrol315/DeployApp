using System.Data;

namespace DeployApp.Application.Abstractions
{
    public interface ITransactionHandler
    {
        IDbTransaction BeginTransaction();
    }
}
