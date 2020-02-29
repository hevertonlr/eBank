using eBank.Core.Abstrations;
using System.ComponentModel.DataAnnotations;

namespace eBank.Infra.Data.Entities
{
    public class BankAccount : IBaseDependency
    {
        [Key]
        public int Number { get; set; }
        public decimal Balance { get; set; }
    }
}
