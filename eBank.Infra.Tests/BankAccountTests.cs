using AutoMapper;
using eBank.API.Config;
using eBank.Infra.Data.Entities;
using eBank.Infra.Repositories.Interfaces;
using eBank.Infra.Services.Interfaces;
using eBank.Infra.Services.Models;
using eBank.Infra.Services.Services;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace eBank.Infra.Tests
{
    public class BankAccountTests
    {
        private readonly Mock<IBankAccountRepository> _bankAccountRepository;
        private readonly IBankAccountService _bankAccountService;
        private readonly MapperConfiguration mapperConfiguration;
        private readonly IMapper _mapper;

        public BankAccountTests()
        {
            _bankAccountRepository = new Mock<IBankAccountRepository>();
            mapperConfiguration = new MapperConfiguration(cfg => { cfg.AddProfile<MappingProfile>(); });
            _mapper = mapperConfiguration.CreateMapper();
            _bankAccountService = new BankAccountService(_mapper, _bankAccountRepository.Object);
        }

        [Fact(DisplayName = "Validar Mappeamento de Dependencias")]
        public void ValidarMapeamento()
        {
            mapperConfiguration.AssertConfigurationIsValid();
        }

        [Fact(DisplayName = "Buscar Saldo de Conta Inexistente")]
        public async Task BuscarSaldoDeContaInexistente()
        {
            int AccountNumber = 123;
            decimal BalanceAmmout = 120;
            _bankAccountRepository.Setup(x => x.FindAccount(AccountNumber))
                .Returns(Task.FromResult(new BankAccount { Number = AccountNumber, Balance = BalanceAmmout }));


            var balanceModel = new BalanceModel() { AccountNumber = 222 };

            await Assert.ThrowsAsync<Exception>(async () => await _bankAccountService.AccountBalance(balanceModel));
        }

        [Fact(DisplayName = "Buscar Saldo da Conta Corrente")]
        public async Task BuscarSaldoContaCorrente()
        {
            int AccountNumber = 123;
            decimal BalanceAmmout = 120;

            _bankAccountRepository.Setup(x => x.FindAccount(AccountNumber))
                .Returns(Task.FromResult(new BankAccount { Number = AccountNumber, Balance = BalanceAmmout }));

            var balanceModel = new BalanceModel() { AccountNumber = AccountNumber };
            var result = await _bankAccountService.AccountBalance(balanceModel);

            Assert.Equal(120, result);
        }

        [Fact(DisplayName = "Sacar Valor Válido na Conta Corrente")]
        public async Task SacarValorValidoDaContaCorrente()
        {
            int AccountNumber = 123;
            decimal BalanceAmmout = 120;
            _bankAccountRepository.Setup(x => x.FindAccount(AccountNumber))
                .Returns(Task.FromResult(new BankAccount { Number = AccountNumber, Balance = BalanceAmmout }));
            _bankAccountRepository.Setup(r => r.SingleUnit.CommitAsync()).Returns(Task.FromResult(true));

            var withdrawModel = new WithdrawModel() { AccountNumber = 123, Ammount = 20 };
            var atual = await _bankAccountService.Withdraw(withdrawModel);

            Assert.Equal(100, atual.Saldo);
        }

        [Theory(DisplayName = "Informações da Operação Incorretas para Saque")]
        [InlineData(0, -100.21d)]
        [InlineData(-5, -1d)]
        [InlineData(-1, 0d)]
        [InlineData(-522, 0.1d)]
        public async Task InformacoesDaOperacaoIncorretasParaSaque(int conta, decimal valor)
        {
            var withdrawModel = new WithdrawModel() { AccountNumber = conta, Ammount = valor };

            await Assert.ThrowsAsync<Exception>(async () => await _bankAccountService.Withdraw(withdrawModel));
        }

        [Fact(DisplayName = "Saque Valor Maior que o Saldo na Conta Corrente")]
        public async Task SacarValorSuperiorDaContaCorrente()
        {
            int AccountNumber = 123;
            decimal BalanceAmmout = 120;
            _bankAccountRepository.Setup(x => x.FindAccount(AccountNumber))
                .Returns(Task.FromResult(new BankAccount { Number = AccountNumber, Balance = BalanceAmmout }));
            _bankAccountRepository.Setup(r => r.SingleUnit.CommitAsync()).Returns(Task.FromResult(true));

            var withdrawModel = new WithdrawModel() { AccountNumber = AccountNumber, Ammount = 2000 };

            await Assert.ThrowsAsync<Exception>(async () => await _bankAccountService.Withdraw(withdrawModel));
        }

        [Fact(DisplayName = "Depositar Valor na Conta Corrente")]
        public async Task DepositarValorNaContaCorrente()
        {
            int AccountNumber = 123;
            decimal BalanceAmmout = 120;
            _bankAccountRepository.Setup(x => x.FindAccount(AccountNumber))
                 .Returns(Task.FromResult(new BankAccount { Number = AccountNumber, Balance = BalanceAmmout }));
            _bankAccountRepository.Setup(r => r.SingleUnit.CommitAsync()).Returns(Task.FromResult(true));

            var depositModel = new DepositModel() { AccountNumber = AccountNumber, Ammount = 30 };

            var atual = await _bankAccountService.Deposit(depositModel);

            Assert.Equal(150, atual.Saldo);
        }

        [Fact(DisplayName = "Conta Corrente Não Encontrada Para Depositar")]
        public async Task ContaCorrenteNaoEncontradaParaDepositar()
        {

            int AccountNumber = 123;
            decimal BalanceAmmout = 120;
            _bankAccountRepository.Setup(x => x.FindAccount(AccountNumber))
                 .Returns(Task.FromResult(new BankAccount { Number = AccountNumber, Balance = BalanceAmmout }));
            _bankAccountRepository.Setup(r => r.SingleUnit.CommitAsync()).Returns(Task.FromResult(true));

            var depositModel = new DepositModel() { AccountNumber = 2222, Ammount = 2000 };

            await Assert.ThrowsAsync<Exception>(async () => await _bankAccountService.Deposit(depositModel));
        }

        [Fact(DisplayName = "Conta Corrente Não Encontrada Para Sacar")]
        public async Task ContaCorrenteNaoEncontradaParaSacar()
        {

            int AccountNumber = 123;
            decimal BalanceAmmout = 120;
            _bankAccountRepository.Setup(x => x.FindAccount(AccountNumber))
                 .Returns(Task.FromResult(new BankAccount { Number = AccountNumber, Balance = BalanceAmmout }));
            _bankAccountRepository.Setup(r => r.SingleUnit.CommitAsync()).Returns(Task.FromResult(true));

            var withdrawModel = new WithdrawModel() { AccountNumber = 2222, Ammount = 2000 };

            await Assert.ThrowsAsync<Exception>(async () => await _bankAccountService.Withdraw(withdrawModel));
        }



    }
}
