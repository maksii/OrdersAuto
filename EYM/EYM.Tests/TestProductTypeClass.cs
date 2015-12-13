using System;
using System.Linq;
using System.Transactions;
using EYM.Entities;
using EYM.EntityFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EYM.Tests
{
    [TestClass]
    public class TestProductTypeClass
    {

        EYMContext _context;
        EYMRepository<ProductType> _productTypeRepository;
        TransactionScope transaction;

        [TestInitialize]
        public void Init()
        {
            _context = new EYMContext("EYMContext");
            _productTypeRepository = new EYMRepository<ProductType>(_context);
            transaction = new TransactionScope();
        }


        //========= Add method tests ================


        [TestMethod]
        public void AddProductType()
        {
         
                int count = _productTypeRepository.GetAll().Count();
                _productTypeRepository.Add(new ProductType());
                Assert.AreEqual(1, count + 1);
           
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddNullProductType()
        {
           
                _productTypeRepository.Add(null);
           
        }

        [TestMethod]
        public void AddNegativeIdProductType()
        {
           
                int count = _productTypeRepository.GetAll().Count();
                _productTypeRepository.Add(new ProductType { Id = -1 });
                Assert.AreEqual(count, count + 1);
           
        }

        //============== Delete tests=====================
        [TestMethod]
        public void DeleteProductType()
        {
           
                ProductType ProductType1 = new ProductType();
                ProductType ProductType2 = new ProductType();
                _productTypeRepository.Add(ProductType1);
                _productTypeRepository.Add(ProductType2);
                int count = _productTypeRepository.GetAll().Count();
                Assert.AreEqual(2, count);
                _productTypeRepository.Delete(_productTypeRepository.Get(ProductType1.Id));
                count = _productTypeRepository.GetAll().Count();
                Assert.AreEqual(count-1, count);
           
        }

        //============== Update tests=====================
        [TestMethod]
        public void UpdateNameProductType()
        {
          
                ProductType ProductType = new ProductType();
                ProductType.Name = "testProductType";
                _productTypeRepository.Add(ProductType);
                ProductType = _productTypeRepository.Get(ProductType.Id);
                ProductType.Name = "test2";
                _productTypeRepository.Update(ProductType);
                Assert.AreEqual("test2", _productTypeRepository.Get(ProductType.Id).Name);
          
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UpdateNullProductType()
        {
          
                _productTypeRepository.Update(null);
           
        }

        //============== Get tests=====================
        [TestMethod]
        public void GetProductType()
        {
          
                ProductType ProductType = new ProductType();
                ProductType.Name = "testProductType";
                _productTypeRepository.Add(ProductType);
                ProductType = _productTypeRepository.Get(ProductType.Id);
                Assert.AreEqual(ProductType.Name, "testProductType");
            
        }

        [TestMethod]
        public void FindProductType()
        {
          
                ProductType ProductType = new ProductType { Name = "testProductType" };
                _productTypeRepository.Add(ProductType);
                ProductType = _productTypeRepository.FindBy(_ProductType => _ProductType.Name == "testProductType").First();
                Assert.IsNotNull(ProductType);
                Assert.AreEqual(ProductType.Name, "testProductType");
           
        }

        [TestCleanup]
        public void TestCleanREsources()
        {
            transaction.Dispose();
        }

    }
}

