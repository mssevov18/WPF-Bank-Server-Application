using System;
using System.Collections.Generic;

#nullable disable

namespace DatabaseTesting2.Models
{
    public partial class TransactionAccountConnection
    {
        public int ConnectionId { get; set; }
        public int TransactionId { get; set; }
        public int AccountSenderId { get; set; }
        public int AccountRecieverId { get; set; }

        public virtual Account AccountReciever { get; set; }
        public virtual Account AccountSender { get; set; }
        public virtual Transaction Transaction { get; set; }
    }
}
