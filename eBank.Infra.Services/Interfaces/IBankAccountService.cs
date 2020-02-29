using eBank.Infra.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eBank.Infra.Services.Interfaces
{
    public interface IBankAccountService : IDisposable
    {
        Task<BankAccountModel> Deposit(DepositModel model);
        Task<BankAccountModel> Withdraw(WithdrawModel model);
        Task<decimal> AccountBalance(BalanceModel model);
    }
}
