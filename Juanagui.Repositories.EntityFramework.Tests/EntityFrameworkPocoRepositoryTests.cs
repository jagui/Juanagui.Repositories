using System;
using System.Data.Common;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Entity;

namespace Juanagui.Repositories.EntityFramework.Tests
{
    [TestClass]
    public class EntityFrameworkPocoRepositoryTests
    {
        private TestContext _dbContext;
        private EntityFrameworkPocoRepository<Parent> _repository;

        [TestInitialize]
        public void Initialize()
        {
            _dbContext = new TestContext();
            new TestContextInitializer().InitializeDatabase(_dbContext);
            _repository = new EntityFrameworkPocoRepository<Parent>(_dbContext);
        }

        [TestCleanup]
        public void Cleanup()
        {
            var connectionString = _dbContext.Database.Connection.ConnectionString;
            _repository.Dispose();
            Database.Delete(connectionString);
        }

        [TestMethod]
        // ReSharper disable InconsistentNaming
        public void AddNewEntity_RetrieveFromRepository_MatchOnOneProperty()
        // ReSharper restore InconsistentNaming
        {
            var father = new Father() { FirstName = "John", LastName = "Doe", DateOfBirth = DateTime.Today.AddYears(-34) };
            _repository.Add(father);
            _repository.PersistAll();
            Assert.AreEqual(father.FirstName, _repository.All().Single().FirstName);
        }

        [TestMethod]
        // ReSharper disable InconsistentNaming
        public void AddNewEntity_ThenDelete_NoMatches()
        // ReSharper restore InconsistentNaming
        {

            Parent father = new Father() { FirstName = "John", LastName = "Doe", DateOfBirth = DateTime.Today.AddYears(-34) };
            _repository.Add(father);
            _repository.PersistAll();
            father = _repository.All().Single();
            _repository.Delete(father);
            _repository.PersistAll();
            Assert.IsFalse(_repository.All().Any());
        }
    }
}
