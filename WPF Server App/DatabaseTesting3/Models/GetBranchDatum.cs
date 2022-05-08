using System;
using System.Collections.Generic;

#nullable disable

namespace DatabaseTesting3.Models
{
    public partial class GetBranchDatum
    {
        public string BranchAddress { get; set; }
        public string BankName { get; set; }
        public override string ToString() => $"{this.BankName}: {this.BranchAddress}";
    }
}
