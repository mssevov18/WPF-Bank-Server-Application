using System;
using System.Collections.Generic;

#nullable disable

namespace ServerApp_v0._1.Data.Models
{
    public partial class CardReader
    {
        public int ReaderId { get; set; }
        public int BankId { get; set; }
        public int AccountRecieverId { get; set; }

        public virtual Account AccountReciever { get; set; }
        public virtual Bank Bank { get; set; }
    }
}
