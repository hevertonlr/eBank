using AutoMapper;
using eBank.Infra.Repositories.Interfaces;
using eBank.Infra.Services.Interfaces;
using eBank.Infra.Services.Models;
using eBank.Infra.Services.Validators;
using System;
using System.Threading.Tasks;

namespace eBank.Infra.Services.Services
{
    public class BankAccountService : IBankAccountService
    {
        private readonly IMapper _mapper;
        private readonly IBankAccountRepository _bankAccountRepository;

        public BankAccountService(IMapper mapper, IBankAccountRepository bankAccountRepository)
        {
            _mapper = mapper;
            _bankAccountRepository = bankAccountRepository;
        }

        public async Task<BankAccountModel> Deposit(DepositModel model)
        {
            var validator = new DepositValidator(_bankAccountRepository);
            
            var result = validator.Validate(model);
            if (!result.IsValid)
                throw new Exception(string.Join("; ", result.Errors));

            var account = await _bankAccountRepository.FindAccount(model.AccountNumber);
            account.Balance += model.Ammount;
            _bankAccountRepository.Change(account);

            await _bankAccountRepository.SingleUnit.CommitAsync();

            return _mapper.Map<BankAccountModel>(account);
        }

        public async Task<BankAccountModel> Withdraw(WithdrawModel model)
        {
            var validator = new WithdrawValidator(_bankAccountRepository);

            var result = validator.Validate(model);
            if (!result.IsValid)
                throw new Exception(string.Join("; ", result.Errors));

            var account = await _bankAccountRepository.FindAccount(model.AccountNumber);
            account.Balance -= model.Ammount;
            _bankAccountRepository.Change(account);

            await _bankAccountRepository.SingleUnit.CommitAsync();

            return _mapper.Map<BankAccountModel>(account);
        }

        public async Task<decimal> AccountBalance(BalanceModel model)
        {
            var validator = new BalanceValidator(_bankAccountRepository);
            
            var result = validator.Validate(model);
            if (!result.IsValid)
                throw new Exception(string.Join("; ", result.Errors));

            var account = await _bankAccountRepository.FindAccount(model.AccountNumber);

            return account.Balance;
        }


        public void Dispose() => _bankAccountRepository?.Dispose();

    }
}
