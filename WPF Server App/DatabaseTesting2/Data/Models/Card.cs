using System;
using System.Collections.Generic;

#nullable disable

namespace DatabaseTesting2.Models
{
    public partial class Card
    {
        public int CardId { get; set; }
        public int AccountHolderId { get; set; }
        public string CardNum { get; set; }
        public string SecurityNum { get; set; }
        public string HolderName { get; set; }
        public string Pin { get; set; }

        public virtual Account AccountHolder { get; set; }
    }
}
