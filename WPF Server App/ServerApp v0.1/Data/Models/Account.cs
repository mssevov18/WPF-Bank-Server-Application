using System;
using System.Collections.Generic;

#nullable disable

namespace ServerApp_v0._1.Data.Models
{
    public partial class Account
    {
        public Account()
        {
            CardReaders = new HashSet<CardReader>();
            Cards = new HashSet<Card>();
            TransactionAccountConnectionAccountRecievers = new HashSet<TransactionAccountConnection>();
            TransactionAccountConnectionAccountSenders = new HashSet<TransactionAccountConnection>();
        }

        public int AccountId { get; set; }
        public decimal Balance { get; set; }
        public int BankId { get; set; }
        public string PersonEgn { get; set; }

        public virtual Bank Bank { get; set; }
        public virtual Person PersonEgnNavigation { get; set; }
        public virtual ICollection<CardReader> CardReaders { get; set; }
        public virtual ICollection<Card> Cards { get; set; }
        public virtual ICollection<TransactionAccountConnection> TransactionAccountConnectionAccountRecievers { get; set; }
        public virtual ICollection<TransactionAccountConnection> TransactionAccountConnectionAccountSenders { get; set; }
    }
}
