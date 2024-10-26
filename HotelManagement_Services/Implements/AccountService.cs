using HotelManagement_BusinessObject.Models;
using HotelManagement_Repositories.Implements;
using HotelManagement_Repositories.Interfaces;
using HotelManagement_Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement_Services.Implements
{
    public class AccountService : IAccountService
    {
        public IAccountRepository _accountRepo { get; set; }
        public AccountService()
        {
            _accountRepo = new AccountRepository();
        }
        public async Task<Account?> GetAccountByEmail(string email)
        {
            return await _accountRepo.GetAccountByEmail(email);
        }

        public async Task<bool> CreateAccount(Account account)
        {
            return await _accountRepo.CreateAccount(account);
        }

        public async Task<IEnumerable<Account>> GetAccounts()
        {
            return await _accountRepo.GetAccounts();
        }

        public async Task<bool> UpdateAccount(Account account)
        {
            var accountUpdate = await GetAccountById(account.AccountId);
            accountUpdate.AccountId = account.AccountId;
            accountUpdate.Email = account.Email;
            accountUpdate.Password = account.Password;
            accountUpdate.FullName = account.FullName;
            accountUpdate.RoleId = account.RoleId;
            return _accountRepo.UpdateAccount(accountUpdate);
        }

        public async Task<bool> DeleteAccount(Account account)
        {
            var accountDelete = await GetAccountById(account.AccountId);
            return _accountRepo.DeleteAccount(accountDelete);
        }

        public Task<Account?> GetAccountById(string id)
        {
            return _accountRepo.GetAccountById(id);
        }

        public Task<IEnumerable<Account>> GetAccountsByName(string name)
        {
            return _accountRepo.GetAccountsByName(name);
        }
    }
}
