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
        EYMRepository<Order> orderRepository;
        EYMRepository<Role> roleRepository;
        EYMRepository<User> userRepository;
        EYMContext context;
        User user;


        [TestInitialize]
        public void Init()
        {
            context = new EYMContext("EYMContext");
            orderRepository = new EYMRepository<Order>(context);
            roleRepository = new EYMRepository<Role>(context);
            userRepository = new EYMRepository<User>(context);
        }

        public void TestHelper()
        {
            roleRepository.Add(new Role());
            var roleId = roleRepository.GetAll().First().Id;
            user = new User { RoleId = roleId };
            userRepository.Add(user);
        }

        [TestMethod]
        public void GetAllOrders()
        {
            using (new TransactionScope())
            {
                int count = orderRepository.GetAll().Count();
                Assert.AreEqual(0, count);
            }
        }

        [TestMethod]
        public void AddOrder()
        {
            using (new TransactionScope())
            {
                TestHelper();
                int count = orderRepository.GetAll().Count();
                orderRepository.Add(new Order { UserId = user.Id, Date = DateTime.Now });
                Assert.AreEqual(1, count + 1);
            }
        }

        [TestMethod]
        public void DeleteOrder()
        {
            using (new TransactionScope())
            {
                TestHelper();
                var order1 = new Order {UserId = user.Id, Date = DateTime.Now};
                var order2 = new Order { UserId = user.Id, Date = DateTime.Now };
                orderRepository.Add(order1);
                orderRepository.Add(order2);
                int count = orderRepository.GetAll().Count();
                Assert.AreEqual(2, count);
                orderRepository.Delete(order1);
                count = orderRepository.GetAll().Count();              
                Assert.AreEqual(1, count);
            }
        }

        [TestMethod]
        public void UpdateOrder()
        {
            using (new TransactionScope())
            {
                TestHelper();
                var order = new Order {UserId = user.Id, Date = new DateTime(2015, 11, 4)};
                orderRepository.Add(order);
                order.Date = new DateTime(2011,7,4);
                orderRepository.Update(order);
                Assert.AreEqual(new DateTime(2011, 7, 4), orderRepository.GetAll().First().Date);
            }
        }


        [TestMethod]
        public void GetOrder()
        {
            using (new TransactionScope())
            {
                TestHelper();
                var order = new Order { UserId = user.Id, Date = new DateTime(2015, 11, 4) };
                orderRepository.Add(order);
                order = orderRepository.Get(order.Id);
                Assert.AreEqual(order.Date, new DateTime(2015, 11, 4));
            }
        }

        [TestMethod]
        public void FindOrder()
        {
            using (new TransactionScope())
            {
                TestHelper();
                orderRepository.Add(new Order { Date = new DateTime(2015, 7, 8) , UserId = user.Id});
                DateTime time = new DateTime(2015, 7, 8);
                Order order = orderRepository.FindBy(_order => _order.Date == time).First();
                Assert.IsNotNull(order);
                Assert.AreEqual(order.Date, time);
            }
        }


    }
}

