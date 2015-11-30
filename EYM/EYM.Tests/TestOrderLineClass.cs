using System;
using System.Linq;
using System.Transactions;
using EYM.Entities;
using EYM.EntityFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EYM.Tests
{
    [TestClass]
    public class TestOrderLineClass
    {
        EYMRepository<Product> _productRepository;
        EYMRepository<Provider> _providerRepository;
        EYMRepository<ProductTemplate> _productTemplateRepository;
        EYMRepository<Order> _orderRepository;
        EYMRepository<Role> _roleRepository;
        EYMRepository<User> _userRepository;
        EYMRepository<OrderLine> _orderLeneRepository;
        EYMContext _context;
        ProductTemplate _productTemplate;
        EYMRepository<ProductType> _productTypeRepository;
        Product _product;
        Order _order;
        User _user;


        [TestInitialize]
        public void Init()
        {
            _context = new EYMContext("EYMContext");
            _productRepository = new EYMRepository<Product>(_context);
            _providerRepository = new EYMRepository<Provider>(_context);
            _productTemplateRepository = new EYMRepository<ProductTemplate>(_context);
            _productTypeRepository = new EYMRepository<ProductType>(_context);
            _orderRepository = new EYMRepository<Order>(_context);
            _orderLeneRepository = new EYMRepository<OrderLine>(_context);
            _roleRepository = new EYMRepository<Role>(_context);
            _userRepository = new EYMRepository<User>(_context);

        }

        public void TestHelper()
        {
            _roleRepository.Add(new Role());
            var roleId = _roleRepository.GetAll().First().Id;
            _user = new User { RoleId = roleId };
            _userRepository.Add(_user);
            var productType = new ProductType();
            _productTypeRepository.Add(productType);
            var provider = new Provider();
            _providerRepository.Add(provider);
            _productTemplate = new ProductTemplate {ProviderId = provider.Id, ProductTypeId = productType.Id};
            _productTemplateRepository.Add(_productTemplate);
            _product = new Product {ProductTemplateId = _productTemplate.Id, DateToOrder = DateTime.Now};
            _productRepository.Add(_product);
            _order = new Order {UserId = _user.Id, Date = DateTime.Now};
            _orderRepository.Add(_order);
        }
    
    

        [TestMethod]
        public void GetAllOrderLines()
        {
            using (new TransactionScope())
            {
                int count = _orderLeneRepository.GetAll().Count();
                Assert.AreEqual(0, count);
            }
        }

        [TestMethod]
        public void AddOrderLine()
        {
            using (new TransactionScope())
            {
                TestHelper();
                int count = _orderLeneRepository.GetAll().Count();
                _orderLeneRepository.Add(new OrderLine { ProductId = _product.Id, OrderId = _order.Id});
                Assert.AreEqual(1, count + 1);
            }
        }

        [TestMethod]
        public void DeleteOrderLine()
        {
            using (new TransactionScope())
            {
                TestHelper();
                var orderLine1 = new OrderLine { ProductId = _product.Id, OrderId = _order.Id};
                var orderLine2 = new OrderLine { ProductId = _product.Id, OrderId = _order.Id };
                _orderLeneRepository.Add(orderLine1);
                _orderLeneRepository.Add(orderLine2);
                int count = _orderLeneRepository.GetAll().Count();
                Assert.AreEqual(2, count);
                _orderLeneRepository.Delete(orderLine2);
                count = _orderLeneRepository.GetAll().Count();
                Assert.AreEqual(1, count);
            }
        }

        [TestMethod]
        public void UpdateOrderLine()
        {
            using (new TransactionScope())
            {
                TestHelper();
                var orderLine = new OrderLine { ProductId = _product.Id, OrderId = _order.Id, Comment = "Test"};
                _orderLeneRepository.Add(orderLine);
                orderLine.Comment = "Test2";
                _orderLeneRepository.Update(orderLine);
                Assert.AreEqual("Test2", _orderLeneRepository.GetAll().First().Comment);
            }
        }


        [TestMethod]
        public void GetOrderLine()
        {
            using (new TransactionScope())
            {
                TestHelper();
                var orderLine = new OrderLine { ProductId = _product.Id, OrderId = _order.Id, Comment = "Test" };
                _orderLeneRepository.Add(orderLine);
                orderLine = _orderLeneRepository.Get(orderLine.Id);
                Assert.AreEqual(orderLine.Comment, "Test");
            }
        }

        [TestMethod]
        public void FindOrderLine()
        {
            using (new TransactionScope())
            {
                TestHelper();
                var orderLine = new OrderLine { ProductId = _product.Id, OrderId = _order.Id, Comment = "Test" };
                _orderLeneRepository.Add(orderLine);
                orderLine = _orderLeneRepository.FindBy(_orderLine => _orderLine.Comment == "Test").First();
                Assert.IsNotNull(orderLine);
                Assert.AreEqual(orderLine.Comment, "Test");
            }
        }


    }
}

