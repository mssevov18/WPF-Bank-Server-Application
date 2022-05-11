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
            TransactionAccountConnectionAccountRecieverIbanNavigations = new HashSet<TransactionAccountConnection>();
            TransactionAccountConnectionAccountSenderIbanNavigations = new HashSet<TransactionAccountConnection>();
        }

        public string AccountIban { get; set; }
        public decimal Balance { get; set; }
        public int BankId { get; set; }
        public string PersonEgn { get; set; }
        public DateTime CreationDate { get; set; }
        public int AccountId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public virtual Bank Bank { get; set; }
        public virtual Person PersonEgnNavigation { get; set; }
        public virtual ICollection<CardReader> CardReaders { get; set; }
        public virtual ICollection<Card> Cards { get; set; }
        public virtual ICollection<TransactionAccountConnection> TransactionAccountConnectionAccountRecieverIbanNavigations { get; set; }
        public virtual ICollection<TransactionAccountConnection> TransactionAccountConnectionAccountSenderIbanNavigations { get; set; }
    }
}
