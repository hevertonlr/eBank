using eBank.Infra.Repositories.Interfaces;
using eBank.Infra.Services.Models;
using FluentValidation;
using FluentValidation.Validators;
using System.Threading;
using System.Threading.Tasks;

namespace eBank.Infra.Services.Validators
{
    public class WithdrawValidator : AbstractValidator<WithdrawModel>
    {
        private readonly IBankAccountRepository _bankAccountRepository;

        public WithdrawValidator(IBankAccountRepository bankAccountRepository)
        {
            _bankAccountRepository = bankAccountRepository;

            RuleFor(x => x.AccountNumber)
                .NotEmpty().WithMessage("Número de Conta Obrigatório!")
                .GreaterThan(0).WithMessage("O Número de Conta deve ser maior que 0 (zero)!")
                .MustAsync(ExistingAccount).WithMessage("Conta Inexistente!");

            RuleFor(x => x.Ammount)
                .NotEmpty().WithMessage("Valor Obrigatório!")
                .GreaterThan(0.0M).WithMessage("O Valor deve ser maior que 0 (zero)!")
                .MustAsync(HasBalanceAsync).WithMessage("Não há saldo suficiente para saque!");
        }

        public async Task<bool> ExistingAccount(int AccountNumber, CancellationToken cancellation)
        {
            var account = await _bankAccountRepository.FindAccount(AccountNumber);
            return account != null;
        }

        public async Task<bool> HasBalanceAsync(WithdrawModel model, decimal ammount, PropertyValidatorContext context, CancellationToken cancellation)
        {
            var account = await _bankAccountRepository.FindAccount(model.AccountNumber);
            return ammount <= account?.Balance;
        }
    }
}
