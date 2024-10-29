using HotelManagement_BusinessObject.Models;
using HotelManagement_Services.Implements;
using HotelManagement_Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MiniHotelManagement.Pages
{
    /// <summary>
    /// Interaction logic for AccountPage.xaml
    /// </summary>
    public partial class AccountPage : Page
    {
        public MainWindow _mainWindow;
        private IAccountService _accountService;
        private IRoleService _roleService;
        public AccountPage(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            _accountService = new AccountService();
            _roleService = new RoleService();
            InitializeComponent();
            
        }

        private void SaveNewContent(string newText)
        {
            MessageBox.Show($"Content saved: {newText}");
        }
        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            //load accounts
            LoadAccounts();

            //load roles
            LoadRoles();

            //unactive create button
            ChangeActionStatus(false);
        }

        private async void LoadAccounts()
        {
            var accounts = await _accountService.GetAccounts();
            lvAccount.ItemsSource = accounts;
        }
        private async void LoadRoles()
        {
            var roles = await _roleService.GetRoles();
           
            if (roles != null && roles.Any())
            {
                cbRole.ItemsSource = roles;
                cbRole.DisplayMemberPath = "RoleName";
                cbRole.SelectedValuePath = "RoleId";
                cbRole.SelectedValue = roles.First().RoleId;
            }
        }


        private async void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var account = GetAccountFromForm();
                var validatedInput = CheckValidate(account);
                if (!validatedInput) return;
                var duplicatedAccount = await _accountService.GetAccountById(account.AccountId);
                if (duplicatedAccount != null)
                {
                    MessageBox.Show($"Account with id {account.AccountId} is duplicated", "Warning", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                var addRs = await _accountService.CreateAccount(account);
                if (addRs)
                    MessageBox.Show("Created Successfully", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                else
                    MessageBox.Show("Created Failed", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

                LoadAccounts();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private async void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var account = GetAccountFromForm();
                var validatedInput = CheckValidate(account);
                if (!validatedInput) return;
                var updateRs =  await _accountService.UpdateAccount(account);
                if (updateRs)
                    MessageBox.Show("Updated Successfully", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                else
                    MessageBox.Show("Updated Failed", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadAccounts();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var account = GetAccountFromForm();
                var checkedAccount = await _accountService.GetAccountById(account.AccountId);
                var roleAdmin = await _roleService.GetRoleByName("Admin");
                if (checkedAccount.RoleId == roleAdmin.RoleId)
                {
                    MessageBox.Show("Admin account can not be deleted", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                var deleteRs = await _accountService.DeleteAccount(account);
                if (deleteRs)
                    MessageBox.Show("Deleted Successfully", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                else
                    MessageBox.Show("Deleted Failed", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadAccounts();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

       

        private void lvAccount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvAccount.SelectedItem is Account chosenAccount)
            {
                txtAccountId.Text = chosenAccount.AccountId;
                txtEmail.Text = chosenAccount.Email;
                txtFullName.Text = chosenAccount.FullName;
                pw.Password = chosenAccount.Password;
                cbRole.SelectedValue = chosenAccount.RoleId;
            }
        }
        private void ChangeActionStatus(bool isCreateChecked)
        {
            btnUpdate.IsEnabled = !isCreateChecked;
            btnDelete.IsEnabled = !isCreateChecked;
            btnCreate.IsEnabled = isCreateChecked;
            txtAccountId.IsEnabled = isCreateChecked;
        }
        private Account GetAccountFromForm()
        {
            var id = txtAccountId.Text.Trim();
            var email = txtEmail.Text.Trim();
            var fullName = txtFullName.Text.Trim();
            var pwd = pw.Password.Trim();
            var roleId = int.Parse(cbRole.SelectedValue.ToString());
            var account = new Account()
            {
                AccountId = id,
                Email = email,
                FullName = fullName,
                RoleId = roleId,
                Password = pwd
            };
            return account;
        }
        private bool CheckValidate(Account account)
        {
            if (string.IsNullOrEmpty(account.AccountId))
            {
                MessageBox.Show("Account ID is required", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (string.IsNullOrEmpty(account.Email))
            {
                MessageBox.Show("A valid email is required.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (string.IsNullOrEmpty(account.FullName))
            {
                MessageBox.Show("Full name is required.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (string.IsNullOrEmpty(account.Password) || account.Password.Length < 6)
            {
                MessageBox.Show("Password must be at least 6 characters long.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        private void createCheck_Checked(object sender, RoutedEventArgs e)
        {
            ChangeActionStatus(true);
        }
        private void createCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            ChangeActionStatus(false);
        }

        private void searchBox_LostFocus(object sender, RoutedEventArgs e)
        {
            searchBox.Text = "Search by name";
            LoadAccounts();
        }

        private void searchBox_GotFocus(object sender, RoutedEventArgs e)
        {
            searchBox.Text = string.Empty;
            
        }

        private void searchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Search();
        }
        private async void Search()
        {
            var searchKey = searchBox.Text;
            if (!string.IsNullOrEmpty(searchKey))
            {
                var searchRs = await _accountService.GetAccountsByName(searchKey);
                lvAccount.ItemsSource = searchRs;
            }
            else LoadAccounts();
        }
    }
}
