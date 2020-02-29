using eBank.API.Helpers;
using eBank.Infra.Services.Interfaces;
using eBank.Infra.Services.Models;
using GraphQL.Types;

namespace eBank.API.Queries
{
    public class AccountBalanceQuery : ObjectGraphType
    {
        public AccountBalanceQuery(IBankAccountService bankAccountService)
        {
            Field<DecimalGraphType>(
                "saldo",
                arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "conta" }),
                resolve: context => {
                    var result = bankAccountService.AccountBalance(new BalanceModel() { 
                        AccountNumber = context.GetArgument<int>("conta") 
                    });

                    result.HandleErrors();

                    return result;
                }
            );
        }
    }
}
