using System.Linq;
using System.Transactions;
using EYM.Entities;
using EYM.EntityFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EYM.Tests
{
    [TestClass]
    public class TestUserClass
    {
        EYMRepository<User> repository;
        EYMContext context;
        EYMRepository<Role> roleRepository;
        Role role;

        [TestInitialize]
        public void Init()
        {
            context = new EYMContext("EYMContext");
            repository = new EYMRepository<User>(context);
            roleRepository = new EYMRepository<Role>(context);
        }

        public void TestHelper()
        {
            role = new Role();
            roleRepository.Add(role);
        }

        [TestMethod]
        public void GetAllUser()
        {
            using (new TransactionScope())
            {
                int count = repository.GetAll().Count();
                Assert.AreEqual(0, count);
            }
        }

        [TestMethod]
        public void AddUser()
        {
            using (new TransactionScope())
            {
                TestHelper();
                int count = repository.GetAll().Count();
                repository.Add(new User {RoleId =role.Id});
                Assert.AreEqual(1, count + 1);
            }
        }

        [TestMethod]
        public void DeleteUser()
        {
            using (new TransactionScope())
            {
                TestHelper();
                User user1 = new User { RoleId = role.Id };
                User user2 = new User { RoleId = role.Id };
                repository.Add(user1);
                repository.Add(user2);
                int count = repository.GetAll().Count();            
                Assert.AreEqual(2, count);
                repository.Delete(user1);
                Assert.AreEqual(1, repository.GetAll().Count());
            }
        }

        [TestMethod]
        public void UpdateUser()
        {
            using (new TransactionScope())
            {
                TestHelper();
                User user = new User {RoleId = role.Id};
                repository.Add(user);
                user.Email = "test@mail.ru";
                repository.Update(user);
                Assert.AreEqual("test@mail.ru", repository.Get(user.Id).Email);
            }
        }


        [TestMethod]
        public void GetUser()
        {
            using (new TransactionScope())
            {
                TestHelper();
                User user = new User {RoleId = role.Id, FirstName = "EYMtestUser"};
                repository.Add(user);           
                User user2 = repository.Get(user.Id);
                Assert.AreEqual(user2.FirstName, "EYMtestUser");
            }
        }

        [TestMethod]
        public void FindUser()
        {
            using (new TransactionScope())
            {
                TestHelper();
                roleRepository.Add(new Role());
                repository.Add(new User { RoleId = role.Id, FirstName = "EYMtestUser" });
                User user = repository.FindBy(_user => _user.FirstName == "EYMtestUser").First();
                Assert.IsNotNull(user);
                Assert.AreEqual(user.FirstName, "EYMtestUser");
            }
        }


    }
}

