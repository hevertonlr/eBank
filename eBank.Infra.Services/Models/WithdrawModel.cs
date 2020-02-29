using System;
using System.Collections.Generic;
using System.Text;

namespace eBank.Infra.Services.Models
{
    public class WithdrawModel
    {
        public int AccountNumber { get; set; }
        public decimal Ammount { get; set; }
    }
}
