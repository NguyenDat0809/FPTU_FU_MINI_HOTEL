using HotelManagement_BusinessObject.Models;
using HotelManagement_Services.Implements;
using HotelManagement_Services.Interfaces;
using System.Windows;
using System.Windows.Controls;


namespace MiniHotelManagement.Pages
{
    /// <summary>
    /// Interaction logic for RoomTypePage.xaml
    /// </summary>
    public partial class RoomTypePage : Page
    {
        public MainWindow _mainWindow;
        private readonly IRoomTypeService _roomTypeService;
        public RoomTypePage(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            _roomTypeService = new RoomTypeService();
        }

        private async void btnCreate_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                var rt = GetRoomTypeFromForm();
                var validatedInput = CheckValidate(rt);
                if (!validatedInput) return;
                var duplicatedIdRole = await _roomTypeService.GetRoomTypeById(rt.RoomTypeId);
                if (duplicatedIdRole != null)
                {
                    MessageBox.Show($"Room Type with id {rt.RoomTypeId} is duplicated", "Warning", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                var duplicatedNameRole = await _roomTypeService.GetRoomTypeByname(rt.RoomTypeName);
                if (duplicatedNameRole != null)
                {
                    MessageBox.Show($"Room Type with name {rt.RoomTypeName} is duplicated", "Warning", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                var addRs = await _roomTypeService.CreateRoomType(rt);
                if (addRs)
                    MessageBox.Show("Created Successfully", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                else
                    MessageBox.Show("Created Failed", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

                LoadRoomsType();
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
                var rt = GetRoomTypeFromForm();
                var validatedInput = CheckValidate(rt);
                if (!validatedInput) return;

                var duplicatedNameRole = await _roomTypeService.GetRoomTypeByname(rt.RoomTypeName);
                if (duplicatedNameRole != null)
                {
                    MessageBox.Show($"Room Type with name {rt.RoomTypeName} is duplicated", "Warning", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                var updateRs = await _roomTypeService.UpdateRoomType(rt);
                if (updateRs)
                    MessageBox.Show("Updated Successfully", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                else
                    MessageBox.Show("Updated Failed", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadRoomsType();
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
                var rt = GetRoomTypeFromForm();
                var deleteRs = await _roomTypeService.DeleteRoomType(rt);
                if (deleteRs)
                    MessageBox.Show("Deleted Successfully", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                else
                    MessageBox.Show("Deleted Failed", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadRoomsType();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        private void lvRoomType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvRoomType.SelectedItem is RoomType chosen)
            {
                txtRoomTypeId.Text = chosen.RoomTypeId.ToString();
                txtName.Text = chosen.RoomTypeName;
                txtDescription.Text = chosen.Description;
            }
        }
        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            LoadRoomsType();

            ChangeActionStatus(false);

            setContentByRole();
        }
        private void setContentByRole()
        {
            if (!_mainWindow.CanModify)
                btnSection.IsEnabled = false;
        }
        private bool CheckValidate(RoomType roomType)
        {
            if (string.IsNullOrEmpty(roomType.RoomTypeId.ToString()))
            {
                MessageBox.Show("Room Type ID is required", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (string.IsNullOrEmpty(roomType.RoomTypeName))
            {
                MessageBox.Show("Room Type Name is required", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (string.IsNullOrEmpty(roomType.Description))
            {
                MessageBox.Show("Description is required", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }
        public async void LoadRoomsType()
        {
            var rts = await _roomTypeService.GetRoomTypes();
            lvRoomType.ItemsSource = rts;
        }
        private RoomType GetRoomTypeFromForm()
        {
            var id = txtRoomTypeId.Text.Trim();
            var name = txtName.Text.Trim();
            var description = txtDescription.Text.Trim();
            var rt = new RoomType()
            {
                RoomTypeId = id,
                RoomTypeName = name,
                Description = description,
            };
            return rt;
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
            btnDelete.IsEnabled = !isCreateChecked && _mainWindow.CanModify;
            btnCreate.IsEnabled = isCreateChecked;
            txtRoomTypeId.IsEnabled = isCreateChecked;
        }

        
    }
}
