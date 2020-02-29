using eBank.Core.Abstrations;
using eBank.Infra.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eBank.Infra.Data
{
    public class BankAccountContext : DbContext, ISingleUnit
    {
        public BankAccountContext(DbContextOptions<BankAccountContext> options) : base(options) { }

        public DbSet<BankAccount> BankAccounts { get; set; }

        public async Task<bool> CommitAsync() => await base.SaveChangesAsync() > 0;
    }
}
