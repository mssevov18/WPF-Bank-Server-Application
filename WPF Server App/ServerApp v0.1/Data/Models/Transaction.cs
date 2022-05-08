using System;
using System.Collections.Generic;

#nullable disable

namespace ServerApp_v0._1.Data.Models
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
    }
}
