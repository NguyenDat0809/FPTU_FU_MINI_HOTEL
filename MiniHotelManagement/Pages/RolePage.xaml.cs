using HotelManagement_BusinessObject.Models;
using HotelManagement_Services.Implements;
using HotelManagement_Services.Interfaces;
using System.Security.Principal;
using System.Windows;
using System.Windows.Controls;


namespace MiniHotelManagement.Pages
{
    /// <summary>
    /// Interaction logic for RolePage.xaml
    /// </summary>
    public partial class RolePage : Page
    {
        public MainWindow _mainWindow;
        public readonly IRoleService _roleService;
        public RolePage(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            _roleService = new RoleService();
        }

        private bool CheckValidate(Role role)
        {
            if (string.IsNullOrEmpty(role.RoleId.ToString()))
            {
                MessageBox.Show("Role ID is required", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            
            if (string.IsNullOrEmpty(role.RoleName))
            {
                MessageBox.Show("Role Name is required", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }
        private async void btnCreate_Click(object sender, System.Windows.RoutedEventArgs e)
        {
           
            try
            {
                var role = GetRoleFromForm();
                var validatedInput = CheckValidate(role);
                if (!validatedInput) return;
                var duplicatedIdRole = await _roleService.GetRoleById(role.RoleId);
                if (duplicatedIdRole != null)
                {
                    MessageBox.Show($"Role with id {role.RoleId} is duplicated", "Warning", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                var duplicatedNameRole = await _roleService.GetRoleByName(role.RoleName);
                if (duplicatedNameRole != null)
                {
                    MessageBox.Show($"Role with name {role.RoleName} is duplicated", "Warning", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                var addRs = await _roleService.CreateRole(role);
                if (addRs)
                    MessageBox.Show("Created Successfully", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                else
                    MessageBox.Show("Created Failed", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

                LoadRoles();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private async void btnUpdate_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                var role = GetRoleFromForm();
                var validatedInput = CheckValidate(role);
                if (!validatedInput) return;
             
                var duplicatedNameRole = await _roleService.GetRoleByName(role.RoleName);
                if (duplicatedNameRole != null)
                {
                    MessageBox.Show($"Role with name {role.RoleName} is duplicated", "Warning", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                var updateRs = await _roleService.UpdateRole(role);
                if (updateRs)
                    MessageBox.Show("Updated Successfully", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                else
                    MessageBox.Show("Updated Failed", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadRoles();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void btnDelete_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                var role = GetRoleFromForm();
                var deleteRs = await _roleService.DeleteRole(role);
                if (deleteRs)
                    MessageBox.Show("Deleted Successfully", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                else
                    MessageBox.Show("Deleted Failed", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadRoles();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void lvRole_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvRole.SelectedItem is Role chosenRole)
            {
                txtRoleId.Text = chosenRole.RoleId.ToString();
                txtName.Text = chosenRole.RoleName;
            }
        }

        public async void LoadRoles()
        {
            var roles = await _roleService.GetRoles();
            lvRole.ItemsSource = roles;
        }
        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            //load roles
            LoadRoles();

            ChangeActionStatus(false);
        }
        private Role GetRoleFromForm()
        {
            var id = txtRoleId.Text.Trim();
            var name = txtName.Text.Trim();
            var idEnable = int.TryParse(id, out int roleId);
            var role = new Role()
            {
                RoleId = idEnable ? roleId : 0,
                RoleName = name,
            };
            return role;
        }

        private void createCheck_Checked(object sender, RoutedEventArgs e)
        {
            ChangeActionStatus(true);
        }
        private void createCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            ChangeActionStatus(false);
        }
        private void ChangeActionStatus(bool isCreateChecked)
        {
            btnUpdate.IsEnabled = !isCreateChecked;
            btnDelete.IsEnabled = !isCreateChecked;
            btnCreate.IsEnabled = isCreateChecked;
            txtRoleId.IsEnabled = isCreateChecked;
        }
        private void searchBox_LostFocus(object sender, RoutedEventArgs e)
        {
            searchBox.Text = "Search";
        }

        private void searchBox_GotFocus(object sender, RoutedEventArgs e)
        {
            searchBox.Text = string.Empty;

        }
    }
}
