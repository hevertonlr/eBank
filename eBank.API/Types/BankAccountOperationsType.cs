using eBank.Infra.Services.Models;
using GraphQL.Types;

namespace eBank.API.Types
{
    public class BankAccountOperationsType : ObjectGraphType<BankAccountModel>
    {
        public BankAccountOperationsType()
        {
            Name = "ContaCorrente";
            Field(x => x.Conta, type: typeof(IntGraphType)).Description("Número da Conta");
            Field(x => x.Saldo, type: typeof(DecimalGraphType)).Description("Saldo da Conta");
        }
    }
}
