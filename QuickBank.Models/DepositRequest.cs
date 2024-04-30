using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBank.Models
{
    public class DepositRequest
    {
        public long AccountId { get; set; }
        public double DepositAmount { get; set; }
        public string Particulars { get; set; }
    }
}
