using Microsoft.VisualStudio.TestTools.UnitTesting;
using StressBall.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace StressBall.Manager.Tests
{
    [TestClass()]
    public class StressBallDBManagerTests
    {
        private StressBallDBManager _manager;

        [TestInitialize]
        public void SetUp()
        {
            var optionsBuilder = new DbContextOptionsBuilder<StressBallContext>();
            optionsBuilder.UseSqlServer(StressBallContext.ConnectionString);


            StressBallContext dbContext = new StressBallContext(optionsBuilder.Options);

            _manager = new StressBallDBManager(dbContext);
        }
        [TestMethod()]
        public void GetAllTest()
        {

            Assert.IsNotNull(_manager.GetAll(null, null));
        }

        [TestMethod()]
        public void GetByIdTest()
        {
            Assert.IsNotNull(_manager.GetById(1));
            Assert.IsNull(_manager.GetById(0));
            Assert.AreEqual(1, _manager.GetById(1).Id);
        }

        [TestMethod()]
        public void AddDeleteTest()
        {
            int beforeAddCount = _manager.GetAll(null, null).Count;
            int defaultId = 0;

            StressBallData newStressball = new StressBallData() { Id = defaultId, Speed = 44.5, DateTimeNow = DateTime.Now };
            StressBallData addedStressBall = _manager.Add(newStressball);
            int newId = addedStressBall.Id;
            Assert.AreNotEqual(defaultId, newId);
            Assert.AreEqual(beforeAddCount + 1, _manager.GetAll(null, null).Count);

            StressBallData StressBallToBeDeleted = _manager.Delete(newId);
            Assert.AreEqual(beforeAddCount, _manager.GetAll(null, null).Count);
            Assert.IsNull(_manager.Delete(100));


        }
    }
}