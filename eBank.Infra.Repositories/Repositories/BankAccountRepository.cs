using eBank.Core.Abstrations;
using eBank.Infra.Data;
using eBank.Infra.Data.Entities;
using eBank.Infra.Repositories.Interfaces;
using System.Threading.Tasks;

namespace eBank.Infra.Repositories.Repositories
{
    public class BankAccountRepository : IBankAccountRepository
    {
        private readonly BankAccountContext _context;

        public BankAccountRepository(BankAccountContext context) => _context = context;

        public ISingleUnit SingleUnit => _context;

        public void Set(BankAccount bankAccount) => _context.BankAccounts.Add(bankAccount);

        public void Change(BankAccount bankAccount) => _context.BankAccounts.Update(bankAccount);

        public async Task<BankAccount> FindAccount(int number) => await _context.BankAccounts.FindAsync(number);

        public void Dispose() => _context?.Dispose();
    }
}
