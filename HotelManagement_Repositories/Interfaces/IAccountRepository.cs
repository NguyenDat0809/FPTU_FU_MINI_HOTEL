using HotelManagement_BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement_Repositories.Interfaces
{
    public interface IAccountRepository
    {
        Task<Account?> GetAccountById(string id);
        Task<Account?> GetAccountByEmail(string email);
        Task<IEnumerable<Account>> GetAccountsByName(string name);
        Task<bool> CreateAccount(Account account);
        Task<IEnumerable<Account>> GetAccounts();
        bool UpdateAccount(Account account);
        bool DeleteAccount(Account account);
    }
}
