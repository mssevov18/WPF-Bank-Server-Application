using System;
using System.Collections.Generic;

#nullable disable

namespace DatabaseTesting3.Models
{
    public partial class GetAccountDatum
    {
        public string AccountFullName { get; set; }
        public decimal Balance { get; set; }
        public string BankName { get; set; }
        public int? CardAmount { get; set; }
        override public string ToString() => $"{this.AccountFullName}({this.BankName}): {this.Balance}" + (CardAmount.HasValue ? $", {this.CardAmount}" : "");
    }
}
