using System;
using System.Linq;
using System.Transactions;
using EYM.Entities;
using EYM.EntityFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EYM.Tests
{
    [TestClass]
    public class TestRoleClass
    {

        EYMContext _context;
        EYMRepository<Role> _roleRepository;

        [TestInitialize]
        public void Init()
        {
            _context = new EYMContext("EYMContext");
            _roleRepository = new EYMRepository<Role>(_context);
        }


        //========= Add method tests ================


        [TestMethod]
        public void AddRole()
        {
            using (new TransactionScope())
            {
                int count = _roleRepository.GetAll().Count();
                _roleRepository.Add(new Role());
                Assert.AreEqual(1, count + 1);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddNullRole()
        {
            using (new TransactionScope())
            {
                _roleRepository.Add(null);
            }
        }

        [TestMethod]
        public void AddNegativeIdRole()
        {
            using (new TransactionScope())
            {
                int count = _roleRepository.GetAll().Count();
                _roleRepository.Add(new Role { Id = -1 });
                Assert.AreEqual(1, count + 1);
            }
        }

        //============== GetAll tests=====================

        [TestMethod]
        public void GetAllRoles()
        {
            using (new TransactionScope())
            {
                int count = _roleRepository.GetAll().Count();
                Assert.AreEqual(0, count);
            }
        }


        //============== Delete tests=====================
        [TestMethod]
        public void DeleteRole()
        {
            using (new TransactionScope())
            {
                Role role1 = new Role();
                Role role2 = new Role();
                _roleRepository.Add(role1);
                _roleRepository.Add(role2);
                int count = _roleRepository.GetAll().Count();
                Assert.AreEqual(2, count);
                _roleRepository.Delete(_roleRepository.Get(role1.Id));
                count = _roleRepository.GetAll().Count();
                Assert.AreEqual(1, count);
            }
        }

        //============== Update tests=====================
        [TestMethod]
        public void UpdateNameRole()
        {
            using (new TransactionScope())
            {
                Role role = new Role();
                role.Name = "testRole";
                _roleRepository.Add(role);
                role = _roleRepository.Get(role.Id);
                role.Name = "test2";
                _roleRepository.Update(role);
                Assert.AreEqual("test2", _roleRepository.Get(role.Id).Name);
            }
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UpdateNullRole()
        {
            using (new TransactionScope())
            {               
                _roleRepository.Update(null);            
            }
        }

        //============== Get tests=====================
        [TestMethod]
        public void GetRole()
        {
            using (new TransactionScope())
            {
                Role role = new Role();
                role.Name = "testRole";
                _roleRepository.Add(role);
                role = _roleRepository.Get(role.Id);
                Assert.AreEqual(role.Name, "testRole");
            }
        }

        [TestMethod]
        public void FindRole()
        {
            using (new TransactionScope())
            {
                Role role = new Role { Name = "testRole" };
                _roleRepository.Add(role);
                role = _roleRepository.FindBy(_role => _role.Name == "testRole").First();
                Assert.IsNotNull(role);
                Assert.AreEqual(role.Name, "testRole");
            }
        }


    }
}

