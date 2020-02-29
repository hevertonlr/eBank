using eBank.API.Mutations;
using eBank.API.Queries;
using GraphQL;
using GraphQL.Types;

namespace eBank.API.Schemas
{
    public class BankAccountSchema : Schema
    {
        public BankAccountSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<AccountBalanceQuery>();
            Mutation = resolver.Resolve<BankAccountMutation>();
        }
    }
}
