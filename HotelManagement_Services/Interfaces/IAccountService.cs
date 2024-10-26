using HotelManagement_BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement_Services.Interfaces
{
    public interface IAccountService
    {
        Task<Account?> GetAccountById(string id);
        Task<Account?> GetAccountByEmail(string email);
        Task<IEnumerable<Account>> GetAccountsByName(string name);
        Task<bool> CreateAccount(Account account);
        Task<IEnumerable<Account>> GetAccounts();
        Task<bool> UpdateAccount(Account account);
        Task<bool> DeleteAccount(Account account);

    }
}
