using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class AccountModel
    {
        public int Id { get; set; }
        public string Number { get; set; } 
        public double Balance { get; set; }
        public double InterestRate { get; set; }
        public double BalanceLowerLimit { get; set; }
        public double WithdrawlLimit { get; set; } 
    }
}
