using eBank.Infra.Repositories.Interfaces;
using eBank.Infra.Services.Models;
using FluentValidation;
using System.Threading;
using System.Threading.Tasks;

namespace eBank.Infra.Services.Validators
{
    public class BalanceValidator : AbstractValidator<BalanceModel>
    {
        private readonly IBankAccountRepository _bankAccountRepository;
        public BalanceValidator(IBankAccountRepository bankAccountRepository)
        {
            _bankAccountRepository = bankAccountRepository;

            RuleFor(x => x.AccountNumber)
                .NotEmpty().WithMessage("Número de Conta Obrigatório!")
                .GreaterThan(0).WithMessage("O Número de Conta deve ser maior que 0 (zero)!")
                .MustAsync(ExistingAccount).WithMessage("Conta Inexistente!");
        }

        public async Task<bool> ExistingAccount(int AccountNumber, CancellationToken cancellation)
        {
            var account = await _bankAccountRepository.FindAccount(AccountNumber);
            return account != null;
        }

    }
}
