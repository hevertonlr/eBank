using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;

namespace eBank.API.Tests.Steps
{
    [Binding]
    public sealed class BankAccountOperationsSteps
    {
        private readonly HttpClient _client;
        private JObject _sacarResult;
        private JObject _sacarAMaisResult;
        private JObject _depositarResult;
        private JObject _consultarResult;
        private decimal _saldoAtual;

        public BankAccountOperationsSteps()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                       .AddJsonFile("appsettings.json")
                       .Build();

            var server = new TestServer(new WebHostBuilder()
                .UseConfiguration(configuration)
                .UseEnvironment("Production")
                .UseStartup<Startup>()
            );

            _client = server.CreateClient();
        }


        [Given(@"eu consuma a API GraphQL")]
        public async Task DadoEuConsumaAAPIGraphQL()
        {
            var response = await _client.GetAsync("/graphql/");

            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.NotNull(responseString);
        }

        #region Consultar saldo na Conta Corrente

        [When(@"eu chamar a query saldo informando o número da conta")]
        public async Task QuandoEuChamarAQuerySaldoInformandoONumeroDaConta()
        {
            const string query = @"{
                ""query"":"" query { saldo(conta: 123) }""
            }";
            var content = new StringContent(query, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/graphql/", content);

            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.NotNull(responseString);
            _consultarResult = JObject.Parse(responseString);
            _saldoAtual = (decimal)_consultarResult["data"]["saldo"];
            Assert.NotNull(_consultarResult);
        }

        [Then(@"a query retornará o saldo atualizado\.")]
        public void EntaoAQueryRetornaraOSaldoAtualizado_()
        {
            Assert.NotNull(_consultarResult["data"]["saldo"]);
        }
        #endregion

        #region Sacar valor na Conta Corrente

        [When(@"eu chamar a mutation sacar informando o número da conta e um valor válido")]
        public async Task QuandoEuChamarAMutationSacarInformandoONumeroDaContaEUmValorValido()
        {
            const string query = @"{
                ""query"": ""mutation { sacar(conta: 123, valor: 99999){conta,saldo} }""
            }";
            var content = new StringContent(query, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/graphql/", content);

            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.NotNull(responseString);
            _sacarResult = JObject.Parse(responseString);
            _saldoAtual = (decimal)_sacarResult["data"]["sacar"]["saldo"];
            Assert.NotNull(_sacarResult);
        }

        [Then(@"o saldo da minha conta no banco de dados diminuirá de acordo")]
        public void EntaoOSaldoDaMinhaContaNoBancoDeDadosDiminuiraDeAcordo()
        {
            Assert.Equal(123, (int)_sacarResult["data"]["sacar"]["conta"]);
            Assert.Equal(_saldoAtual, (decimal)_sacarResult["data"]["sacar"]["saldo"]);
        }
        #endregion

        #region Sacar valor na Conta Corrente com valor maior ao Saldo

        [When(@"eu chamar a mutation sacar informando o número da conta e um valor maior do que o meu saldo")]
        public async Task QuandoEuChamarAMutationSacarInformandoONumeroDaContaEUmValorMaiorDoQueOMeuSaldo()
        {
            const string query = @"{
                ""query"": ""mutation { sacar(conta: 123, valor: 999999){conta,saldo} }""
            }";
            var content = new StringContent(query, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/graphql/", content);

            var responseString = await response.Content.ReadAsStringAsync();
            Assert.NotNull(responseString);
            _sacarAMaisResult = JObject.Parse(responseString);
            Assert.NotNull(_sacarAMaisResult);
        }

        [Then(@"a mutation me retornará um erro do GraphQL informando que eu não tenho saldo suficiente")]
        public void EntaoAMutationMeRetornaraUmErroDoGraphQLInformandoQueEuNaoTenhoSaldoSuficiente()
        {
            Assert.Equal("Não há saldo suficiente para saque!", _sacarAMaisResult["errors"][0]["message"]);
        }
        #endregion

        #region Depositar valor na Conta Corrente

        [When(@"eu chamar a mutation depositar informando o número da conta e um valor válido")]
        public async Task QuandoEuChamarAMutationDepositarInformandoONumeroDaContaEUmValorValido()
        {
            const string query = @"{
                ""query"":"" mutation { depositar(conta: 123, valor: 99999){ conta, saldo } }""
            }";
            var content = new StringContent(query, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/graphql/", content);

            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.NotNull(responseString);
            _depositarResult = JObject.Parse(responseString);
            _saldoAtual = (decimal)_depositarResult["data"]["depositar"]["saldo"];
            Assert.NotNull(_depositarResult);
        }

        [Then(@"a mutation atualizará o saldo da conta no banco de dados")]
        public void EntaoAMutationAtualizaraOSaldoDaContaNoBancoDeDados()
        {
            Assert.Equal(123, (int)_depositarResult["data"]["depositar"]["conta"]);
            Assert.Equal(_saldoAtual, (decimal)_depositarResult["data"]["depositar"]["saldo"]);
        }
        #endregion




        [Then(@"a mutation retornará o saldo atualizado\.")]
        public void EntaoAMutationRetornaraOSaldoAtualizado_()
        {
            if (_depositarResult != null)
                Assert.NotNull(_depositarResult["data"]["depositar"]["saldo"]);
            if (_sacarResult != null)
                Assert.NotNull(_sacarResult["data"]["sacar"]["saldo"]);
        }
    }
}
