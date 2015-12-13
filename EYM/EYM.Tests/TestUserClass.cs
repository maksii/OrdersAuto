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
        EYMRepository<User> _repository;
        EYMContext _context;
        EYMRepository<Role> _roleRepository;
        Role _role;
        TransactionScope _transaction;

        [TestInitialize]
        public void Init()
        {
            _context = new EYMContext("EYMContext");
            _repository = new EYMRepository<User>(_context);
            _roleRepository = new EYMRepository<Role>(_context);
            _transaction = new TransactionScope();

        }

        public void TestHelper()
        {
            _role = new Role();
            _roleRepository.Add(_role);
        }

        [TestMethod]
        public void AddUser()
        {
          
                TestHelper();
                int count = _repository.GetAll().Count();
                _repository.Add(new User {RoleId =_role.Id});
                Assert.AreEqual(count, count + 1);
           
        }

        [TestMethod]
        public void DeleteUser()
        {
          
                TestHelper();
                User user1 = new User { RoleId = _role.Id };
                User user2 = new User { RoleId = _role.Id };
                _repository.Add(user1);
                _repository.Add(user2);
                int count = _repository.GetAll().Count();            
                Assert.AreEqual(2, count);
                _repository.Delete(user1);
                Assert.AreEqual(count-1, _repository.GetAll().Count());
           
        }

        [TestMethod]
        public void UpdateUser()
        {
           
                TestHelper();
                User user = new User {RoleId = _role.Id};
                _repository.Add(user);
                user.Email = "test@mail.ru";
                _repository.Update(user);
                Assert.AreEqual("test@mail.ru", _repository.Get(user.Id).Email);
           
        }


        [TestMethod]
        public void GetUser()
        {
           
                TestHelper();
                User user = new User {RoleId = _role.Id, FirstName = "EYMtestUser"};
                _repository.Add(user);           
                User user2 = _repository.Get(user.Id);
                Assert.AreEqual(user2.FirstName, "EYMtestUser");
           
        }

        [TestMethod]
        public void FindUser()
        {
          
                TestHelper();
                _roleRepository.Add(new Role());
                _repository.Add(new User { RoleId = _role.Id, FirstName = "EYMtestUser" });
                User user = _repository.FindBy(_user => _user.FirstName == "EYMtestUser").First();
                Assert.IsNotNull(user);
                Assert.AreEqual(user.FirstName, "EYMtestUser");
           
        }

        [TestCleanup]
        public void TestCleanREsources()
        {
            _transaction.Dispose();
        }

    }
}

