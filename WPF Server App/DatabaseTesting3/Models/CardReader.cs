﻿using System;
using System.Collections.Generic;

#nullable disable

namespace DatabaseTesting3.Models
{
    public partial class CardReader
    {
        public int ReaderId { get; set; }
        public int BankId { get; set; }
        public int AccountRecieverId { get; set; }

        public virtual Account AccountReciever { get; set; }
        public virtual Bank Bank { get; set; }

        public Bank Bank1
        {
            get => default;
            set
            {
            }
        }
    }
}