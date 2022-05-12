using System;
using System.Collections.Generic;

#nullable disable

namespace ServerApp_v0._1.Data.Models
{
    public partial class TransactionAccountConnection
    {
        public int ConnectionId { get; set; }
        public int TransactionId { get; set; }
        public string AccountSenderIban { get; set; }
        public string AccountRecieverIban { get; set; }

        public virtual Account AccountRecieverIbanNavigation { get; set; }
        public virtual Account AccountSenderIbanNavigation { get; set; }
        public virtual Transaction Transaction { get; set; }
    }
}
