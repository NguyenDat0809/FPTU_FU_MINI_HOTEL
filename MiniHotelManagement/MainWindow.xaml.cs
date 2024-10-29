using HotelManagement_BusinessObject.Models;
using HotelManagement_Services.Implements;
using HotelManagement_Services.Interfaces;
using MiniHotelManagement.Pages;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MiniHotelManagement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Account CurrentAccount { get; set; }
        public bool CanModify { get; set; } = false;
        private readonly IRoleService _roleService;
        //private Dictionary<string, Page> pages { get; set; } = new Dictionary<string, Page>();
        private Page GetPage<T>() where T : Page
        {
            var key = typeof(T).Name;
            //if (pages.ContainsKey(key))
            //    return pages[key];

            var newPage = (T)Activator.CreateInstance(typeof(T), this);
            //pages[key] = newPage;
            return newPage;
        }
        public MainWindow()
        {
            _roleService = new RoleService();
            InitializeComponent();

            //Set full frame content
            SetFullFrameContent<LoginPage>();
        }

        public void SetFullFrameContent<T>() where T : Page
        {
            if (contentFrame.Content != null) contentFrame.Content = null;
            fullFrame.Content = GetPage<T>();
            //hidden sidebar
            sideBar.Visibility = Visibility.Hidden;
        }
        public void SetContentFrameContent<T>() where T : Page
        {
            if (fullFrame.Content != null) fullFrame.Content = null;
            contentFrame.Content = GetPage<T>();
            //show sidebar
            sideBar.Visibility = Visibility.Visible;

        }


        private void btnAccountPage_Click(object sender, RoutedEventArgs e)
        {
            SetContentFrameContent<AccountPage>();
        }

        private void btnRolePage_Click(object sender, RoutedEventArgs e)
        {
            SetContentFrameContent<RolePage>();
        }

        private void btnRoomPage_Click(object sender, RoutedEventArgs e)
        {
            SetContentFrameContent<RoomPage>();
        }

        private void btnRoomTypePage_Click(object sender, RoutedEventArgs e)
        {
            SetContentFrameContent<RoomTypePage>();
        }

        private void btnReservationPage_Click(object sender, RoutedEventArgs e)
        {
            SetContentFrameContent<ReservationPage>();
        }

        private async void ChangeContentByRole(int? role)
        {
            var staffRole =  await _roleService.GetRoleByName("Staff");
            var customerRole = await _roleService.GetRoleByName("Customer");
            if (role == null || role == customerRole.RoleId)
            {
                MessageBox.Show("You have no permission");
                return;
            }

            if (role == staffRole.RoleId )
            {
                btnAccountPage.Visibility = Visibility.Collapsed;
                btnRolePage.Visibility = Visibility.Collapsed;
                CanModify = false;
                SetContentFrameContent<RoomPage>();
            }
            else
            {
                btnAccountPage.Visibility = Visibility.Visible;
                btnRolePage.Visibility = Visibility.Visible;
                CanModify = true;
                SetContentFrameContent<AccountPage>();
            }

        }
        public void SetUserContentByRole(Account account)
        {
            if(account != null)
            {
                CurrentAccount = account;
                ChangeContentByRole(account.RoleId);
            }
            
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            CurrentAccount = null;
            SetFullFrameContent<LoginPage>();
        }

        private void StackPanel_Loaded(object sender, RoutedEventArgs e)
        {
            if(CurrentAccount != null)
                ChangeContentByRole(CurrentAccount.RoleId);
        }
    }
}