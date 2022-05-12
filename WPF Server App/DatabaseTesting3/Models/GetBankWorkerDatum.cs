using System;
using System.Collections.Generic;

#nullable disable

namespace DatabaseTesting3.Models
{
    public partial class GetBankWorkerDatum
    {
        public string WorkerFullName { get; set; }
        public string BankName { get; set; }
        public override string ToString() => $"{this.WorkerFullName}({this.BankName})";
    }
}
