using System.Linq;
using System.Transactions;
using EYM.Entities;
using EYM.EntityFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EYM.Tests
{
    [TestClass]
    public class TestProductTemplateClass
    {
        EYMRepository<ProductTemplate> _repository;
        EYMContext _context;
        EYMRepository<Provider> _providerRepository;
        EYMRepository<ProductType> _poductTypeRepository;
        Provider _provider;
        ProductType _productType;

        [TestInitialize]
        public void Init()
        {
            _context = new EYMContext("EYMContext");
            _repository = new EYMRepository<ProductTemplate>(_context);
            _providerRepository = new EYMRepository<Provider>(_context);
            _poductTypeRepository = new EYMRepository<ProductType>(_context);
        }

        public void TestHelper()
        {
            _provider = new Provider();
            _productType = new ProductType();
            _providerRepository.Add(_provider);
            _poductTypeRepository.Add(_productType);
        }

        [TestMethod]
        public void GetAllProductTemplate()
        {
            using (new TransactionScope())
            {
                int count = _repository.GetAll().Count();
                Assert.AreEqual(0, count);
            }
        }

        [TestMethod]
        public void AddProductTemplate()
        {
            using (new TransactionScope())
            {
                TestHelper();
                int count = _repository.GetAll().Count();
                _repository.Add(new ProductTemplate() { ProviderId = _provider.Id, ProductTypeId = _productType.Id});
                Assert.AreEqual(1, count + 1);
            }
        }

        [TestMethod]
        public void DeleteProductTemplate()
        {
            using (new TransactionScope())
            {
                TestHelper();
                ProductTemplate productTemplate1 = new ProductTemplate { ProviderId = _provider.Id , ProductTypeId = _productType.Id };
                ProductTemplate productTemplate2 = new ProductTemplate { ProviderId = _provider.Id, ProductTypeId = _productType.Id };
                _repository.Add(productTemplate1);
                _repository.Add(productTemplate2);
                int count = _repository.GetAll().Count();
                Assert.AreEqual(2, count);
                _repository.Delete(productTemplate1);
                Assert.AreEqual(1, _repository.GetAll().Count());
            }
        }

        [TestMethod]
        public void UpdateProductTemplate()
        {
            using (new TransactionScope())
            {
                TestHelper();
                ProductTemplate productTemplate = new ProductTemplate { ProviderId = _provider.Id, ProductTypeId = _productType.Id };
                _repository.Add(productTemplate);
                productTemplate.Name = "test";
                _repository.Update(productTemplate);
                Assert.AreEqual("test", _repository.Get(productTemplate.Id).Name);
            }
        }


        [TestMethod]
        public void GetProductTemplate()
        {
            using (new TransactionScope())
            {
                TestHelper();
                ProductTemplate productTemplate = new ProductTemplate { ProviderId = _provider.Id, Name = "EYMtestUser" , ProductTypeId = _productType.Id };
                _repository.Add(productTemplate);
                ProductTemplate user2 = _repository.Get(productTemplate.Id);
                Assert.AreEqual(user2.Name, "EYMtestUser");
            }
        }

        [TestMethod]
        public void FindProductTemplate()
        {
            using (new TransactionScope())
            {
                TestHelper();
                _repository.Add(new ProductTemplate { ProviderId = _provider.Id, Name = "EYMtestUser",  ProductTypeId = _productType.Id });
                ProductTemplate productTemplate = _repository.FindBy(_producttemp => _producttemp.Name == "EYMtestUser").First();
                Assert.IsNotNull(productTemplate);
                Assert.AreEqual(productTemplate.Name, "EYMtestUser");
            }
        }


    }
}

