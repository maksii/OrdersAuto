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

        [TestInitialize]
        public void Init()
        {
            _context = new EYMContext("EYMContext");
            _productTypeRepository = new EYMRepository<ProductType>(_context);
        }


        //========= Add method tests ================


        [TestMethod]
        public void AddProductType()
        {
            using (new TransactionScope())
            {
                int count = _productTypeRepository.GetAll().Count();
                _productTypeRepository.Add(new ProductType());
                Assert.AreEqual(1, count + 1);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddNullProductType()
        {
            using (new TransactionScope())
            {
                _productTypeRepository.Add(null);
            }
        }

        [TestMethod]
        public void AddNegativeIdProductType()
        {
            using (new TransactionScope())
            {
                int count = _productTypeRepository.GetAll().Count();
                _productTypeRepository.Add(new ProductType { Id = -1 });
                Assert.AreEqual(1, count + 1);
            }
        }

        //============== GetAll tests=====================

        [TestMethod]
        public void GetAllProductTypes()
        {
            using (new TransactionScope())
            {
                int count = _productTypeRepository.GetAll().Count();
                Assert.AreEqual(0, count);
            }
        }


        //============== Delete tests=====================
        [TestMethod]
        public void DeleteProductType()
        {
            using (new TransactionScope())
            {
                ProductType ProductType1 = new ProductType();
                ProductType ProductType2 = new ProductType();
                _productTypeRepository.Add(ProductType1);
                _productTypeRepository.Add(ProductType2);
                int count = _productTypeRepository.GetAll().Count();
                Assert.AreEqual(2, count);
                _productTypeRepository.Delete(_productTypeRepository.Get(ProductType1.Id));
                count = _productTypeRepository.GetAll().Count();
                Assert.AreEqual(1, count);
            }
        }

        //============== Update tests=====================
        [TestMethod]
        public void UpdateNameProductType()
        {
            using (new TransactionScope())
            {
                ProductType ProductType = new ProductType();
                ProductType.Name = "testProductType";
                _productTypeRepository.Add(ProductType);
                ProductType = _productTypeRepository.Get(ProductType.Id);
                ProductType.Name = "test2";
                _productTypeRepository.Update(ProductType);
                Assert.AreEqual("test2", _productTypeRepository.Get(ProductType.Id).Name);
            }
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UpdateNullProductType()
        {
            using (new TransactionScope())
            {
                _productTypeRepository.Update(null);
            }
        }

        //============== Get tests=====================
        [TestMethod]
        public void GetProductType()
        {
            using (new TransactionScope())
            {
                ProductType ProductType = new ProductType();
                ProductType.Name = "testProductType";
                _productTypeRepository.Add(ProductType);
                ProductType = _productTypeRepository.Get(ProductType.Id);
                Assert.AreEqual(ProductType.Name, "testProductType");
            }
        }

        [TestMethod]
        public void FindProductType()
        {
            using (new TransactionScope())
            {
                ProductType ProductType = new ProductType { Name = "testProductType" };
                _productTypeRepository.Add(ProductType);
                ProductType = _productTypeRepository.FindBy(_ProductType => _ProductType.Name == "testProductType").First();
                Assert.IsNotNull(ProductType);
                Assert.AreEqual(ProductType.Name, "testProductType");
            }
        }


    }
}

