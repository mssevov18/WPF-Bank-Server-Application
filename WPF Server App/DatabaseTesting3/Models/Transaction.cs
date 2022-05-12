using System;
using System.Collections.Generic;

#nullable disable

namespace DatabaseTesting3.Models
{
    public partial class Transaction
    {
        public Transaction()
        {
            TransactionAccountConnections = new HashSet<TransactionAccountConnection>();
        }

        public int TransactionId { get; set; }
        public decimal Amount { get; set; }

        public virtual ICollection<TransactionAccountConnection> TransactionAccountConnections { get; set; }

        public TransactionAccountConnection TransactionAccountConnection
        {
            get => default;
            set
            {
            }
        }
    }
}
