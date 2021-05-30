using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace ObenntouSystem.Models
{
    public partial class ContextModel : DbContext
    {
        public ContextModel()
            : base("name=ContextModel")
        {
        }

        public virtual DbSet<Dish> Dishes { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<OmiseMaster> OmiseMasters { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dish>()
                .Property(e => e.dish_price)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Dish>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Dish)
                .HasForeignKey(e => e.order_dishesid)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Group>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Group)
                .HasForeignKey(e => e.order_groupid)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OmiseMaster>()
                .HasMany(e => e.Dishes)
                .WithRequired(e => e.OmiseMaster)
                .HasForeignKey(e => e.dish_omiseid)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OmiseMaster>()
                .HasMany(e => e.Groups)
                .WithRequired(e => e.OmiseMaster)
                .HasForeignKey(e => e.group_omiseid)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .Property(e => e.user_phone)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.user_mail)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.user_acc)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.user_pwd)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.user_pri)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Groups)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.group_userid)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.order_userid)
                .WillCascadeOnDelete(false);
        }
    }
}
