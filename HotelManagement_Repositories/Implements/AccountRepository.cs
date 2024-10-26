using HotelManagement_BusinessObject.Models;
using HotelManagement_DAO;
using HotelManagement_Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement_Repositories.Implements
{
    public class AccountRepository : IAccountRepository
    {
        public async Task<bool> CreateAccount(Account account)
        {
          
            var reuslt =  await GenericDAO<Account>.Instance.InsertAsync(account);
            return reuslt;
        }

        public bool DeleteAccount(Account account)
        {
            return GenericDAO<Account>.Instance.Delete(account);
        }

        public async Task<Account?> GetAccountByEmail(string email)
        {
            return await GenericDAO<Account>.Instance.SingleOrDefaultAsync(x => x.Email == email);
        }

        public async Task<Account?> GetAccountById(string id)
        {
            return await GenericDAO<Account>.Instance.SingleOrDefaultAsync(x => x.AccountId == id);
        }

        public async Task<IEnumerable<Account>> GetAccounts()
        {
            return await GenericDAO<Account>.Instance.GetListAsync(
                include: a => a.Include(x => x.Role)
                );
        }

        public async Task<IEnumerable<Account>> GetAccountsByName(string name)
        {
            return await GenericDAO<Account>.Instance.GetListAsync(a => a.FullName.Contains(name), include: a => a.Include(a => a.Role));
        }

        public bool UpdateAccount(Account account)
        {
     
            return GenericDAO<Account>.Instance.Update(account);
        }
    }
}
