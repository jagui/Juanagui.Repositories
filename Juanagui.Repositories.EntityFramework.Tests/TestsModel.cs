using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace Juanagui.Repositories.EntityFramework.Tests
{
    public abstract class Parent
    {
        public long Id { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        private IEnumerable<Child> Children { get; set; }
    }

    public class Father : Parent
    {
    }

    public class Mother : Parent
    {
        public String MaidenName { get; set; }
    }

    public class Child
    {
        public long Id { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        private IEnumerable<Parent> Parents { get; set; }
    }

    public class TestContext : DbContext
    {
        private DbSet<Parent> Parents { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Parent>().HasKey(p => p.Id);
            modelBuilder.Entity<Child>().HasKey(p => p.Id);
            base.OnModelCreating(modelBuilder);
        }
    }

    public class TestContextInitializer : DropCreateDatabaseAlways<TestContext>
    {
        protected override void Seed(TestContext context)
        {
            base.Seed(context);
        }
    }
}

