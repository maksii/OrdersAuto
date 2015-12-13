using System;
using System.Linq;
using System.Transactions;
using EYM.Entities;
using EYM.EntityFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EYM.Tests
{
    [TestClass]
    public class TestProductClass
    {
        EYMRepository<Product> _productRepository;
        EYMRepository<Provider> _providerRepository;
        EYMRepository<ProductTemplate> _productTemplateRepository;
        EYMContext _context;
        ProductTemplate _productTemplate;
        EYMRepository<ProductType>  _productTypeRepository;
        TransactionScope _transaction;


        [TestInitialize]
        public void Init()
        {
            _context = new EYMContext("EYMContext");
            _productRepository = new EYMRepository<Product>(_context);
            _providerRepository = new EYMRepository<Provider>(_context);
            _productTemplateRepository = new EYMRepository<ProductTemplate>(_context);
            _productTypeRepository = new EYMRepository<ProductType>(_context);
            _transaction = new TransactionScope();

        }

        public void TestHelper()
        {
            var productType = new ProductType();
            _productTypeRepository.Add(productType);
            var provider = new Provider();
            _providerRepository.Add(provider);           
            _productTemplate = new ProductTemplate { ProviderId = provider.Id, ProductTypeId = productType.Id };
            _productTemplateRepository.Add(_productTemplate);

        }

       
        [TestMethod]
        public void AddProduct()
        {
          
                TestHelper();
                int count = _productRepository.GetAll().Count();
                _productRepository.Add(new Product { ProductTemplateId = _productTemplate.Id, DateToOrder = DateTime.Now });
                Assert.AreEqual(count, count + 1);
           
        }

        [TestMethod]
        public void DeleteProduct()
        {
           
                TestHelper();
                var product1 = new Product { ProductTemplateId = _productTemplate.Id, DateToOrder = DateTime.Now };
                var product2 = new Product { ProductTemplateId = _productTemplate.Id, DateToOrder = DateTime.Now };
                _productRepository.Add(product1);
                _productRepository.Add(product2);
                int count = _productRepository.GetAll().Count();
                Assert.AreEqual(2, count);
                _productRepository.Delete(product1);
                count = _productRepository.GetAll().Count();
                Assert.AreEqual(count, count-1);
           
        }

        [TestMethod]
        public void UpdateProduct()
        {
           
                TestHelper();
                var product = new Product { ProductTemplateId = _productTemplate.Id, DateToOrder = new DateTime(2015, 11, 4) };
                _productRepository.Add(product);
                product.DateToOrder = new DateTime(2011, 7, 4);
                _productRepository.Update(product);
                Assert.AreEqual(new DateTime(2011, 7, 4), _productRepository.GetAll().First().DateToOrder);
           
        }


        [TestMethod]
        public void GetProduct()
        {
           
                TestHelper();
                var product = new Product { ProductTemplateId = _productTemplate.Id, DateToOrder = new DateTime(2015, 11, 4) };
                _productRepository.Add(product);
                product = _productRepository.Get(product.Id);
                Assert.AreEqual(product.DateToOrder, new DateTime(2015, 11, 4));
            
        }

        [TestMethod]
        public void FindProduct()
        {
            
                TestHelper();
                _productRepository.Add(new Product { DateToOrder = new DateTime(2015, 7, 8) , ProductTemplateId = _productTemplate.Id });
                DateTime time = new DateTime(2015, 7, 8);
                Product product = _productRepository.FindBy(_product => _product.DateToOrder == time).First();
                Assert.IsNotNull(product);
                Assert.AreEqual(product.DateToOrder, time);
           
        }

        [TestCleanup]
        public void TestCleanREsources()
        {
            _transaction.Dispose();
        }
    }
}

