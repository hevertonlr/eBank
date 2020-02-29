using eBank.Core.Abstrations;
using eBank.Infra.Data.Entities;
using System.Threading.Tasks;

namespace eBank.Infra.Repositories.Interfaces
{
    public interface IBankAccountRepository : IRepository<BankAccount>
    {
        void Set(BankAccount bankAccount);
        void Change(BankAccount bankAccount);
        Task<BankAccount> FindAccount(int number);
    }
}
