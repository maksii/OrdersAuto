﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EYM.Entities;
using EYM.EntityFramework;
using Microsoft.AspNet.Identity;

namespace EYM.UserStore
{
	public class EYMUserStore : IUserStore<User, int>, IUserLoginStore<User, int>, IUserEmailStore<User, int>
	{
		private readonly EYMContext _database;

		public EYMUserStore(EYMContext context)
		{
			_database = context;
		}

		public void Dispose()
		{
			_database.Dispose();
		}

		public Task CreateAsync(User user)
		{
			_database.Users.Add(user);
			return _database.SaveChangesAsync();
		}

		public Task UpdateAsync(User user)
		{
			// TODO
			throw new NotImplementedException();
		}

		public Task DeleteAsync(User user)
		{
			// TODO
			throw new NotImplementedException();
		}

		public Task<User> FindByIdAsync(int userId)
		{
			var user = _database.Users.Where(c => c.Id == userId).FirstOrDefaultAsync();
			return Task.Run(() => user);
		}

		public Task<User> FindByNameAsync(string userName)
		{
			var user = _database.Users.Where(c => string.Compare(c.UserName, userName, StringComparison.OrdinalIgnoreCase) == 0).FirstOrDefaultAsync();
			return user;
		}

		public Task AddLoginAsync(User user, UserLoginInfo login)
		{
			throw new NotImplementedException();
		}

		public Task RemoveLoginAsync(User user, UserLoginInfo login)
		{
			throw new NotImplementedException();
		}

		public Task<IList<UserLoginInfo>> GetLoginsAsync(User user)
		{
			throw new NotImplementedException();
		}

		public Task<User> FindAsync(UserLoginInfo login)
		{
			throw new NotImplementedException();
		}

		public Task SetEmailAsync(User user, string email)
		{
			throw new NotImplementedException();
		}

		public Task<string> GetEmailAsync(User user)
		{
			return Task.Run(() => user.Email);
		}

		public Task<bool> GetEmailConfirmedAsync(User user)
		{
			throw new NotImplementedException();
		}

		public Task SetEmailConfirmedAsync(User user, bool confirmed)
		{
			throw new NotImplementedException();
		}

		public Task<User> FindByEmailAsync(string email)
		{
			var myUser = _database.Users.Where(c => string.Compare(c.Email, email, StringComparison.OrdinalIgnoreCase) == 0).FirstOrDefaultAsync();
			return Task.Run(() => myUser);
		}
	}
}
