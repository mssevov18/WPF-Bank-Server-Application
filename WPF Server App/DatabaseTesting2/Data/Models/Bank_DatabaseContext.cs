using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DatabaseTesting2.Models
{
    public partial class Bank_DatabaseContext : DbContext
    {
        public Bank_DatabaseContext()
        {
        }

        public Bank_DatabaseContext(DbContextOptions<Bank_DatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Bank> Banks { get; set; }
        public virtual DbSet<BankWorker> BankWorkers { get; set; }
        public virtual DbSet<Branch> Branches { get; set; }
        public virtual DbSet<Card> Cards { get; set; }
        public virtual DbSet<CardReader> CardReaders { get; set; }
        public virtual DbSet<GetAccountDatum> GetAccountData { get; set; }
        public virtual DbSet<GetBankWorkerDatum> GetBankWorkerData { get; set; }
        public virtual DbSet<GetBranchDatum> GetBranchData { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<TransactionAccountConnection> TransactionAccountConnections { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=Bank_Database;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account");

                entity.Property(e => e.AccountId).HasColumnName("account_id");

                entity.Property(e => e.Balance)
                    .HasColumnType("money")
                    .HasColumnName("balance");

                entity.Property(e => e.BankId).HasColumnName("bank_id");

                entity.Property(e => e.PersonEgn)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("person_egn")
                    .IsFixedLength(true);

                entity.HasOne(d => d.Bank)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.BankId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Account_Bank");

                entity.HasOne(d => d.PersonEgnNavigation)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.PersonEgn)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Account_Person");
            });

            modelBuilder.Entity<Bank>(entity =>
            {
                entity.ToTable("Bank");

                entity.Property(e => e.BankId).HasColumnName("bank_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<BankWorker>(entity =>
            {
                entity.HasKey(e => e.WorkerId);

                entity.ToTable("Bank_Worker");

                entity.Property(e => e.WorkerId).HasColumnName("worker_id");

                entity.Property(e => e.BankId).HasColumnName("bank_id");

                entity.Property(e => e.IsAdmin).HasColumnName("is_admin");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("password");

                entity.Property(e => e.PersonEgn)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("person_egn")
                    .IsFixedLength(true);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("username");

                entity.HasOne(d => d.Bank)
                    .WithMany(p => p.BankWorkers)
                    .HasForeignKey(d => d.BankId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Bank_Worker_Bank");

                entity.HasOne(d => d.PersonEgnNavigation)
                    .WithMany(p => p.BankWorkers)
                    .HasForeignKey(d => d.PersonEgn)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Bank_Worker_Person");
            });

            modelBuilder.Entity<Branch>(entity =>
            {
                entity.ToTable("Branch");

                entity.Property(e => e.BranchId).HasColumnName("branch_id");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasColumnName("address");

                entity.Property(e => e.BankId).HasColumnName("bank_id");

                entity.HasOne(d => d.Bank)
                    .WithMany(p => p.Branches)
                    .HasForeignKey(d => d.BankId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Branch_Bank");
            });

            modelBuilder.Entity<Card>(entity =>
            {
                entity.ToTable("Card");

                entity.Property(e => e.CardId).HasColumnName("card_id");

                entity.Property(e => e.AccountHolderId).HasColumnName("account_holder_id");

                entity.Property(e => e.CardNum)
                    .IsRequired()
                    .HasMaxLength(16)
                    .HasColumnName("card_num")
                    .IsFixedLength(true);

                entity.Property(e => e.HolderName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("holder_name");

                entity.Property(e => e.Pin)
                    .IsRequired()
                    .HasMaxLength(4)
                    .HasColumnName("pin")
                    .IsFixedLength(true);

                entity.Property(e => e.SecurityNum)
                    .IsRequired()
                    .HasMaxLength(3)
                    .HasColumnName("security_num")
                    .IsFixedLength(true);

                entity.HasOne(d => d.AccountHolder)
                    .WithMany(p => p.Cards)
                    .HasForeignKey(d => d.AccountHolderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Card_Account");
            });

            modelBuilder.Entity<CardReader>(entity =>
            {
                entity.HasKey(e => e.ReaderId);

                entity.ToTable("Card_Reader");

                entity.Property(e => e.ReaderId).HasColumnName("reader_id");

                entity.Property(e => e.AccountRecieverId).HasColumnName("account_reciever_id");

                entity.Property(e => e.BankId).HasColumnName("bank_id");

                entity.HasOne(d => d.AccountReciever)
                    .WithMany(p => p.CardReaders)
                    .HasForeignKey(d => d.AccountRecieverId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Card_Reader_Account");

                entity.HasOne(d => d.Bank)
                    .WithMany(p => p.CardReaders)
                    .HasForeignKey(d => d.BankId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Card_Reader_Bank");
            });

            modelBuilder.Entity<GetAccountDatum>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("GetAccountData");

                entity.Property(e => e.AccountFullName)
                    .IsRequired()
                    .HasMaxLength(152)
                    .HasColumnName("Account Full Name");

                entity.Property(e => e.Balance).HasColumnType("money");

                entity.Property(e => e.BankName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Bank Name");

                entity.Property(e => e.CardAmount).HasColumnName("Card Amount");
            });

            modelBuilder.Entity<GetBankWorkerDatum>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("GetBankWorkerData");

                entity.Property(e => e.BankName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Bank Name");

                entity.Property(e => e.WorkerFullName)
                    .IsRequired()
                    .HasMaxLength(152)
                    .HasColumnName("Worker Full Name");
            });

            modelBuilder.Entity<GetBranchDatum>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("GetBranchData");

                entity.Property(e => e.BankName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Bank Name");

                entity.Property(e => e.BranchAddress)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasColumnName("Branch Address");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasKey(e => e.Egn);

                entity.ToTable("Person");

                entity.Property(e => e.Egn)
                    .HasMaxLength(10)
                    .HasColumnName("egn")
                    .IsFixedLength(true);

                entity.Property(e => e.Age).HasColumnName("age");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("first_name");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("last_name");

                entity.Property(e => e.MiddleName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("middle_name");

                entity.Property(e => e.Residence)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasColumnName("residence");
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.ToTable("Transaction");

                entity.Property(e => e.TransactionId).HasColumnName("transaction_id");

                entity.Property(e => e.Amount)
                    .HasColumnType("money")
                    .HasColumnName("amount");
            });

            modelBuilder.Entity<TransactionAccountConnection>(entity =>
            {
                entity.HasKey(e => e.ConnectionId);

                entity.ToTable("TransactionAccountConnection");

                entity.Property(e => e.ConnectionId).HasColumnName("connection_id");

                entity.Property(e => e.AccountRecieverId).HasColumnName("account_reciever_id");

                entity.Property(e => e.AccountSenderId).HasColumnName("account_sender_id");

                entity.Property(e => e.TransactionId).HasColumnName("transaction_id");

                entity.HasOne(d => d.AccountReciever)
                    .WithMany(p => p.TransactionAccountConnectionAccountRecievers)
                    .HasForeignKey(d => d.AccountRecieverId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TransactionAccountConnection_Account1");

                entity.HasOne(d => d.AccountSender)
                    .WithMany(p => p.TransactionAccountConnectionAccountSenders)
                    .HasForeignKey(d => d.AccountSenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TransactionAccountConnection_Account");

                entity.HasOne(d => d.Transaction)
                    .WithMany(p => p.TransactionAccountConnections)
                    .HasForeignKey(d => d.TransactionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TransactionAccountConnection_Transaction");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
