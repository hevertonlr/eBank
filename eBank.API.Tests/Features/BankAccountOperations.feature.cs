// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:3.0.0.0
//      SpecFlow Generator Version:3.0.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace eBank.API.Tests.Features
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.0.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public partial class BankAccountOperationsFeature : Xunit.IClassFixture<BankAccountOperationsFeature.FixtureData>, System.IDisposable
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private Xunit.Abstractions.ITestOutputHelper _testOutputHelper;
        
#line 1 "BankAccountOperations.feature"
#line hidden
        
        public BankAccountOperationsFeature(BankAccountOperationsFeature.FixtureData fixtureData, Xunit.Abstractions.ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
            this.TestInitialize();
        }
        
        public static void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("pt-BR"), "BankAccountOperations", "\tSendo eu um correntista do banco\r\n\tPara poder saldar as minhas dívidas", ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        public static void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        public virtual void TestInitialize()
        {
        }
        
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioInitialize(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<Xunit.Abstractions.ITestOutputHelper>(_testOutputHelper);
        }
        
        public virtual void ScenarioStart()
        {
            testRunner.OnScenarioStart();
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        void System.IDisposable.Dispose()
        {
            this.ScenarioTearDown();
        }
        
        [Xunit.FactAttribute(DisplayName="Sacar valor na Conta Corrente")]
        [Xunit.TraitAttribute("FeatureTitle", "BankAccountOperations")]
        [Xunit.TraitAttribute("Description", "Sacar valor na Conta Corrente")]
        public virtual void SacarValorNaContaCorrente()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Sacar valor na Conta Corrente", null, ((string[])(null)));
#line 5
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 6
testRunner.Given("eu consuma a API GraphQL", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Dado ");
#line 7
testRunner.When("eu chamar a mutation sacar informando o número da conta e um valor válido", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quando ");
#line 8
testRunner.Then("o saldo da minha conta no banco de dados diminuirá de acordo", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Então ");
#line 9
testRunner.And("a mutation retornará o saldo atualizado.", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "E ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.FactAttribute(DisplayName="Sacar valor na Conta Corrente com valor maior ao Saldo")]
        [Xunit.TraitAttribute("FeatureTitle", "BankAccountOperations")]
        [Xunit.TraitAttribute("Description", "Sacar valor na Conta Corrente com valor maior ao Saldo")]
        public virtual void SacarValorNaContaCorrenteComValorMaiorAoSaldo()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Sacar valor na Conta Corrente com valor maior ao Saldo", null, ((string[])(null)));
#line 11
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 12
testRunner.Given("eu consuma a API GraphQL", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Dado ");
#line 13
testRunner.When("eu chamar a mutation sacar informando o número da conta e um valor maior do que o" +
                    " meu saldo", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quando ");
#line 14
testRunner.Then("a mutation me retornará um erro do GraphQL informando que eu não tenho saldo sufi" +
                    "ciente", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Então ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.FactAttribute(DisplayName="Depositar valor na Conta Corrente")]
        [Xunit.TraitAttribute("FeatureTitle", "BankAccountOperations")]
        [Xunit.TraitAttribute("Description", "Depositar valor na Conta Corrente")]
        public virtual void DepositarValorNaContaCorrente()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Depositar valor na Conta Corrente", null, ((string[])(null)));
#line 16
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 17
testRunner.Given("eu consuma a API GraphQL", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Dado ");
#line 18
testRunner.When("eu chamar a mutation depositar informando o número da conta e um valor válido", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quando ");
#line 19
testRunner.Then("a mutation atualizará o saldo da conta no banco de dados", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Então ");
#line 20
testRunner.And("a mutation retornará o saldo atualizado.", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "E ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.FactAttribute(DisplayName="Consultar saldo na Conta Corrente")]
        [Xunit.TraitAttribute("FeatureTitle", "BankAccountOperations")]
        [Xunit.TraitAttribute("Description", "Consultar saldo na Conta Corrente")]
        public virtual void ConsultarSaldoNaContaCorrente()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Consultar saldo na Conta Corrente", null, ((string[])(null)));
#line 22
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 23
testRunner.Given("eu consuma a API GraphQL", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Dado ");
#line 24
testRunner.When("eu chamar a query saldo informando o número da conta", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quando ");
#line 25
testRunner.Then("a query retornará o saldo atualizado.", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Então ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.0.0.0")]
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
        public class FixtureData : System.IDisposable
        {
            
            public FixtureData()
            {
                BankAccountOperationsFeature.FeatureSetup();
            }
            
            void System.IDisposable.Dispose()
            {
                BankAccountOperationsFeature.FeatureTearDown();
            }
        }
    }
}
#pragma warning restore
#endregion