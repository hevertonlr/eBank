using eBank.API.Helpers;
using eBank.API.Types;
using eBank.Infra.Services.Interfaces;
using eBank.Infra.Services.Models;
using GraphQL.Types;

namespace eBank.API.Mutations
{
    public class BankAccountMutation : ObjectGraphType
    {
        public BankAccountMutation(IBankAccountService bankAccountService)
        {
            Name = "ContaCorrenteMutation";

            Field<BankAccountOperationsType>(
                "sacar",
                arguments: new QueryArguments(
                    new QueryArgument<IntGraphType> { Name = "conta" },
                    new QueryArgument<DecimalGraphType> { Name = "valor" }
                ),
                resolve: context =>
                {
                    var result = bankAccountService.Withdraw(new WithdrawModel()
                    {
                        AccountNumber = context.GetArgument<int>("conta"),
                        Ammount = context.GetArgument<decimal>("valor")
                    });
                    
                    result.HandleErrors();

                    return result;
                });

            Field<BankAccountOperationsType>(
                "depositar",
                arguments: new QueryArguments(
                    new QueryArgument<IntGraphType> { Name = "conta" },
                    new QueryArgument<DecimalGraphType> { Name = "valor" }
                ),
                resolve: context =>
                {
                    var result = bankAccountService.Deposit(new DepositModel()
                    {
                        AccountNumber = context.GetArgument<int>("conta"),
                        Ammount = context.GetArgument<decimal>("valor")
                    });

                    result.HandleErrors();

                    return result;
                });
        }
    }
}
