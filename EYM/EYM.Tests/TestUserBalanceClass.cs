using System;
using System.Linq;
using System.Transactions;
using EYM.Entities;
using EYM.EntityFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EYM.Tests
{
    [TestClass]
    public class TestUserBalanceBalanceClass
    {
        EYMRepository<UserBalance> _repository;
        EYMContext _context;
        EYMRepository<User> _userRepository;
        EYMRepository<Role> _roleRepository;
        User _user;
        Role _role;
        TransactionScope transaction;

        [TestInitialize]
        public void Init()
        {
            _context = new EYMContext("EYMContext");
            _repository = new EYMRepository<UserBalance>(_context);
            _userRepository = new EYMRepository<User>(_context);
            _roleRepository = new EYMRepository<Role>(_context);
            transaction = new TransactionScope();
        }

        public void TestHelper()
        {
            _role = new Role();
            _roleRepository.Add(_role);
            _user = new User { RoleId = _role.Id };
            _userRepository.Add(_user);
        }

        [TestMethod]
        public void AddUserBalance()
        {
           
                TestHelper();
                int count = _repository.GetAll().Count();
                _repository.Add(new UserBalance { UserId = _user.Id, Date = DateTime.Now });
                Assert.AreEqual(count, count + 1);
          
        }

        [TestMethod]
        public void DeleteUserBalance()
        {
          
                TestHelper();
                UserBalance userBalance1 = new UserBalance { UserId = _user.Id , Date = DateTime.Now };
                UserBalance userBalance2 = new UserBalance { UserId = _user.Id , Date = DateTime.Now };
                _repository.Add(userBalance1);
                _repository.Add(userBalance2);
                int count = _repository.GetAll().Count();
                Assert.AreEqual(2, count);
                _repository.Delete(userBalance1);
                Assert.AreEqual(count-1, _repository.GetAll().Count());
           
        }

        [TestMethod]
        public void UpdateUserBalance()
        {
         
                TestHelper();
                UserBalance UserBalance = new UserBalance { UserId = _user.Id , Date = DateTime.Now };
                _repository.Add(UserBalance);
                UserBalance.Credit = 33.3;
                _repository.Update(UserBalance);
                Assert.AreEqual(33.3, _repository.Get(UserBalance.Id).Credit);
          
        }


        [TestMethod]
        public void GetUserBalance()
        {
           
                TestHelper();
                UserBalance userBalance = new UserBalance { UserId = _user.Id, Credit = 20 , Date = DateTime.Now };
                _repository.Add(userBalance);
                UserBalance userBalance2 = _repository.Get(userBalance.Id);
                Assert.AreEqual(userBalance2.Credit, 20);
          
        }

        [TestMethod]
        public void FindUserBalance()
        {
           
                TestHelper();
              _repository.Add(new UserBalance { UserId = _user.Id, Credit = 50, Date = DateTime.Now });
                UserBalance userBalance = _repository.FindBy(_userBalance => _userBalance.Credit == 50).First();
                Assert.IsNotNull(userBalance);
                Assert.AreEqual(userBalance.Credit, 50);
           
        }

        [TestCleanup]
        public void TestCleanREsources()
        {
            transaction.Dispose();
        }

    }
}

