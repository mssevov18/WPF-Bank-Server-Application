using System;
using System.Collections.Generic;

#nullable disable

namespace DatabaseTesting3.Models
{
    public partial class Branch
    {
        public int BranchId { get; set; }
        public string Address { get; set; }
        public int BankId { get; set; }

        public virtual Bank Bank { get; set; }

        public Bank Bank1
        {
            get => default;
            set
            {
            }
        }

        public GetBranchDatum GetBranchDatum
        {
            get => default;
            set
            {
            }
        }
    }
}
