﻿using System;
using System.Linq;
using System.Transactions;
using EYM.Entities;
using EYM.EntityFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EYM.Tests
{
    [TestClass]
    public class TestProviderClass
    {

        EYMContext _context;
        EYMRepository<Provider> _provideRepository;
        TransactionScope transaction;

        [TestInitialize]
        public void Init()
        {
            _context = new EYMContext("EYMContext");
            _provideRepository = new EYMRepository<Provider>(_context);
            transaction = new TransactionScope();
        }


        //========= Add method tests ================


        [TestMethod]
        public void AddProvider()
        {
          
                int count = _provideRepository.GetAll().Count();
                _provideRepository.Add(new Provider());
                Assert.AreEqual(1, count + 1);
           
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddNullProvider()
        {
          
                _provideRepository.Add(null);
           
        }

        [TestMethod]
        public void AddNegativeIdProvider()
        {
           
                int count = _provideRepository.GetAll().Count();
                _provideRepository.Add(new Provider { Id = -1 });
                Assert.AreEqual(count, count + 1);
           
        }

       //============== Delete tests=====================
        [TestMethod]
        public void DeleteProvider()
        {
           
                Provider provider1 = new Provider();
                Provider provider2 = new Provider();
                _provideRepository.Add(provider1);
                _provideRepository.Add(provider2);
                int count = _provideRepository.GetAll().Count();
                Assert.AreEqual(2, count);
                _provideRepository.Delete(_provideRepository.Get(provider1.Id));
                count = _provideRepository.GetAll().Count();
                Assert.AreEqual(count-1, count);
           
        }

        //============== Update tests=====================
        [TestMethod]
        public void UpdateNameProvider()
        {
          
                Provider provider = new Provider();
                provider.Name = "testRole";
                _provideRepository.Add(provider);
                provider = _provideRepository.Get(provider.Id);
                provider.Name = "test2";
                _provideRepository.Update(provider);
                Assert.AreEqual("test2", _provideRepository.Get(provider.Id).Name);
            
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UpdateNullProvider()
        {
           
                _provideRepository.Update(null);
          
        }

        //============== Get tests=====================
        [TestMethod]
        public void GetProvider()
        {
           
                Provider provider = new Provider();
                provider.Name = "testRole";
                _provideRepository.Add(provider);
                provider = _provideRepository.Get(provider.Id);
                Assert.AreEqual(provider.Name, "testRole");
           
        }

        [TestMethod]
        public void FindProvider()
        {
          
                Provider provider = new Provider { Name = "testRole" };
                _provideRepository.Add(provider);
                provider = _provideRepository.FindBy(_provider => _provider.Name == "testRole").First();
                Assert.IsNotNull(provider);
                Assert.AreEqual(provider.Name, "testRole");
           
        }
        [TestCleanup]
        public void TestCleanREsources()
        {
            transaction.Dispose();
        }

    }
}

