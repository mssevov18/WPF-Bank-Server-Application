using System;
using System.Collections.Generic;

#nullable disable

namespace DatabaseTesting4.Models
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
