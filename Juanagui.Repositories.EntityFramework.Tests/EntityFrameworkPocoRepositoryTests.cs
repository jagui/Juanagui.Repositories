using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Entity;

namespace Juanagui.Repositories.EntityFramework.Tests
{
    [TestClass]
    public class EntityFrameworkPocoRepositoryTests
    {
        private String _connectionString;
        private EntityFrameworkPocoRepository<Parent> _repository;

        [TestInitialize]
        public void Initialize()
        {
            var dbContext = new TestContext();
            _connectionString = dbContext.Database.Connection.ConnectionString;
            new TestContextInitializer().InitializeDatabase(dbContext);
            _repository = new EntityFrameworkPocoRepository<Parent>(dbContext);
        }

        [TestCleanup]
        public void Cleanup()
        {

            _repository.Dispose();
            Database.Delete(_connectionString);
        }

        [TestMethod]
        // ReSharper disable InconsistentNaming
        public void AddNewEntity_RetrieveFromRepository_MatchOnOneProperty()
        // ReSharper restore InconsistentNaming
        {
            var father = new Father { FirstName = "John", LastName = "Doe", DateOfBirth = DateTime.Today.AddYears(-34) };
            _repository.Add(father);
            _repository.PersistAll();
            Assert.AreEqual(father.FirstName, _repository.All().Single().FirstName);
        }

        [TestMethod]
        // ReSharper disable InconsistentNaming
        public void AddNewEntity_ThenDelete_NoMatches()
        // ReSharper restore InconsistentNaming
        {
            Parent father = new Father { FirstName = "John", LastName = "Doe", DateOfBirth = DateTime.Today.AddYears(-34) };
            _repository.Add(father);
            _repository.PersistAll();
            father = _repository.All().Single();
            _repository.Delete(father);
            _repository.PersistAll();
            Assert.IsFalse(_repository.All().Any());
        }

        [TestMethod]
        public void QueryManyToManyRelationships()
        {
            var father = new Father { FirstName = "John", LastName = "Doe", DateOfBirth = DateTime.Today.AddYears(-34) };
            var mother = new Mother { FirstName = "Caroline", LastName = "Doe", MaidenName = "Smith", DateOfBirth = DateTime.Today.AddYears(-34) };
            var child = new Child { FirstName = "Sonny", LastName = "Doe", DateOfBirth = DateTime.Today.AddYears(-10) };
            father.AddChild(child);
            mother.AddChild(child);
            mother.AddChild(new Child { FirstName = "Laura", LastName = "Smith", DateOfBirth = DateTime.Today.AddYears(-12) });
            _repository.Add(mother);
            _repository.Add(father);
            _repository.PersistAll();

            mother = _repository.Query().OfType<Mother>().Single();
            father = _repository.Query().OfType<Father>().Single();
            Assert.IsTrue(new[] {"Sonny", "Laura"}.SequenceEqual(mother.Children.Select(c => c.FirstName)));
            Assert.IsTrue(mother.Children.Contains(father.Children.Single()));
        }
    }
}

