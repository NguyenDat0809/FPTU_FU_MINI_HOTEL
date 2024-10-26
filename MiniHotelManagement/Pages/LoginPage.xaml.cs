using HotelManagement_Services.Implements;
using HotelManagement_Services.Interfaces;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace MiniHotelManagement.Pages
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        protected MainWindow _mainWindow;
        private readonly IAccountService _accountService;
        public LoginPage(MainWindow mainWindow) 
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            _accountService = new AccountService();

        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var email = txtEmail.Text.Trim();
            var password = pw.Password.Trim();
            var accountByEmail = await _accountService.GetAccountByEmail(email);
            if(accountByEmail == null)
            {
                MessageBox.Show("Your account not exists", "Warning", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (accountByEmail.Password != password)
            {
                MessageBox.Show("Password is not correct", "Warning", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            //If correct
            _mainWindow.SetUserContentByRole(accountByEmail);
            
        }
   
        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Hand;
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = null;
        }


    }
}
