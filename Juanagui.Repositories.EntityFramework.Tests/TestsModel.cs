using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private ICollection<Child> _children = new Collection<Child>();
        public virtual ICollection<Child> Children
        {
            get { return _children; }
            set { _children = value; }
        }

        public void AddChild(Child child)
        {
            child.Parents.Add(this);
            Children.Add(child);
        }
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
        private ICollection<Parent> _parents = new Collection<Parent>() ;
        public virtual ICollection<Parent> Parents
        {
            get { return _parents; }
            set { _parents = value; }
        }
    }

    public class TestContext : DbContext
    {
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

