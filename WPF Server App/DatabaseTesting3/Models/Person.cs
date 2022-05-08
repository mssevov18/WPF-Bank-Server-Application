using System;
using System.Collections.Generic;

#nullable disable

namespace DatabaseTesting3.Models
{
    public partial class Person
    {
        public Person()
        {
            Accounts = new HashSet<Account>();
            BankWorkers = new HashSet<BankWorker>();
        }

        public string Egn { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public short Age { get; set; }
        public string Residence { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<BankWorker> BankWorkers { get; set; }

        public Card Card
        {
            get => default;
            set
            {
            }
        }

        public GetAccountDatum GetAccountDatum
        {
            get => default;
            set
            {
            }
        }
    }
}
