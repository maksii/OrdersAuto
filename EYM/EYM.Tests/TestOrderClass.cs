using System;
using System.Linq;
using System.Transactions;
using EYM.Entities;
using EYM.EntityFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EYM.Tests
{
    [TestClass]
    public class TestOrderClass
    {
        EYMRepository<Order> _orderRepository;
        EYMRepository<Role> _roleRepository;
        EYMRepository<User> _userRepository;
        EYMContext _context;
        User _user;
        TransactionScope _transaction;


        [TestInitialize]
        public void Init()
        {
            _context = new EYMContext("EYMContext");
            _orderRepository = new EYMRepository<Order>(_context);
            _roleRepository = new EYMRepository<Role>(_context);
            _userRepository = new EYMRepository<User>(_context);
            _transaction = new TransactionScope();
        }

        public void TestHelper()
        {
            _roleRepository.Add(new Role());
            var roleId = _roleRepository.GetAll().First().Id;
            _user = new User { RoleId = roleId };
            _userRepository.Add(_user);
        }

      [TestMethod]
        public void AddOrder()
        {
                TestHelper();
                int count = _orderRepository.GetAll().Count();
                _orderRepository.Add(new Order { UserId = _user.Id, Date = DateTime.Now });
                Assert.AreEqual(count, count + 1);
        }

        [TestMethod]
        public void DeleteOrder()
        {           
                TestHelper();
                var order1 = new Order {UserId = _user.Id, Date = DateTime.Now};
                var order2 = new Order { UserId = _user.Id, Date = DateTime.Now };
                _orderRepository.Add(order1);
                _orderRepository.Add(order2);
                int count = _orderRepository.GetAll().Count();
                Assert.AreEqual(2, count);
                _orderRepository.Delete(order1);
                count = _orderRepository.GetAll().Count();              
                Assert.AreEqual(count, count-1);
        }

        [TestMethod]
        public void UpdateOrder()
        {           
                TestHelper();
                var order = new Order {UserId = _user.Id, Date = new DateTime(2015, 11, 4)};
                _orderRepository.Add(order);
                order.Date = new DateTime(2011,7,4);
                _orderRepository.Update(order);
                Assert.AreEqual(new DateTime(2011, 7, 4), _orderRepository.GetAll().First().Date);
        }


        [TestMethod]
        public void GetOrder()
        {
           
                TestHelper();
                var order = new Order { UserId = _user.Id, Date = new DateTime(2015, 11, 4) };
                _orderRepository.Add(order);
                order = _orderRepository.Get(order.Id);
                Assert.AreEqual(order.Date, new DateTime(2015, 11, 4));       
        }

        [TestMethod]
        public void FindOrder()
        {           
                TestHelper();
                _orderRepository.Add(new Order { Date = new DateTime(2015, 7, 8) , UserId = _user.Id});
                DateTime time = new DateTime(2015, 7, 8);
                Order order = _orderRepository.FindBy(_order => _order.Date == time).First();
                Assert.IsNotNull(order);
                Assert.AreEqual(order.Date, time);        
        }

        [TestCleanup]
        public void TestCleanREsources()
        {
            _transaction.Dispose();
        }


    }
}

