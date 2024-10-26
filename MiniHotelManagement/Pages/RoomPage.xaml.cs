using HotelManagement_BusinessObject.Models;
using HotelManagement_Services.Implements;
using HotelManagement_Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;


namespace MiniHotelManagement.Pages
{
    /// <summary>
    /// Interaction logic for RoomPage.xaml
    /// </summary>
    public partial class RoomPage : Page
    {
        private readonly IRoomService _roomService;
        private readonly IRoomTypeService _roomTypeService;
        private readonly IPictureService _pictureService;
        private IReaderService<Room> _readerService;
        public MainWindow _mainWindow;
        public RoomPage(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            _roomService = new RoomService();
            _roomTypeService = new RoomTypeService();
            _pictureService = new PictureService();
            InitializeComponent();

        }
        private Room GetRoomFromForm()
        {
            var id = txtRoomId.Text.Trim();
            var name = txtName.Text.Trim();
            var description = txtDescription.Text.Trim();
            var image = txtImageUrl.Text.Trim();
            var roomTypeId = cbRoom.SelectedValue.ToString().Trim();
            var capacityIntEnable = int.TryParse(txtCapacity.Text.Trim(), out int capacityInt);

            var room = new Room()
            {
                RoomId = id,
                RoomName = name,
                Description = description,
                ImageUrl = image,
                Capacity = capacityIntEnable ? capacityInt : -1,
                RoomTypeId = roomTypeId
            };
            return room;
        }

        private async void btnCreate_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                var room = GetRoomFromForm();
                var validatedInput = CheckValidate(room);
                if (!validatedInput) return;
                var duplicatedRoom = await _roomService.GetRoomById(room.RoomId);
                if (duplicatedRoom != null)
                {
                    MessageBox.Show($"Room with id {room.RoomId} is duplicated", "Warning", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                var addRs = await _roomService.CreateRoom(room);
                if (addRs)
                    MessageBox.Show("Created Successfully", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                else
                    MessageBox.Show("Created Failed", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

                LoadRooms();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private bool CheckValidate(Room room)
        {
            if (string.IsNullOrEmpty(room.RoomId))
            {
                MessageBox.Show("Room ID is required", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (string.IsNullOrEmpty(room.RoomName))
            {
                MessageBox.Show("A room name is required.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (string.IsNullOrEmpty(room.Description))
            {
                MessageBox.Show("Description is required.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (room.Capacity <= 0)
            {
                MessageBox.Show("Capacity has to greater than 0", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        private async void btnUpdate_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                var account = GetRoomFromForm();
                var validatedInput = CheckValidate(account);
                if (!validatedInput) return;
                var updateRs = await _roomService.UpdateRoom(account);
                if (updateRs)
                    MessageBox.Show("Updated Successfully", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                else
                    MessageBox.Show("Updated Failed", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadRooms();
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
                var room = GetRoomFromForm();
                var deleteRs = await _roomService.DeleteRoom(room);
                if (deleteRs)
                    MessageBox.Show("Deleted Successfully", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                else
                    MessageBox.Show("Deleted Failed", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadRooms();
            }
            catch (DbUpdateException dbex)
            {
                MessageBox.Show("This room can not be deleted right now", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void createCheck_Checked(object sender, RoutedEventArgs e)
        {
            ChangeActionStatus(true);
        }
        private void createCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            ChangeActionStatus(false);
        }
        private async void LoadRoomTypes()
        {
            var roomTypes = await _roomTypeService.GetRoomTypes();

            if (roomTypes != null && roomTypes.Any())
            {
                cbRoom.ItemsSource = roomTypes;
                cbRoom.DisplayMemberPath = "RoomTypeName";
                cbRoom.SelectedValuePath = "RoomTypeId";
                cbRoom.SelectedValue = roomTypes.First().RoomTypeId;
            }
        }
        private void ChangeActionStatus(bool isCreateChecked)
        {
            btnUpdate.IsEnabled = !isCreateChecked;
            btnDelete.IsEnabled = !isCreateChecked && _mainWindow.CanModify;
            btnCreate.IsEnabled = isCreateChecked;
            txtRoomId.IsEnabled = isCreateChecked;
        }
        private void OnLoaded(object sender, RoutedEventArgs e)
        {

            LoadRooms();
            LoadRoomTypes();
            ChangeActionStatus(false);
            setContentByRole();
        }
        private void setContentByRole()
        {
            if (!_mainWindow.CanModify)
                btnSection.IsEnabled = false;
        }
        private async void LoadRooms()
        {
            var rooms = await _roomService.GetRooms();
            lvRoom.ItemsSource = rooms;
        }

        private void lvRoom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvRoom.SelectedItem is Room chosenRoom)
            {
                txtRoomId.Text = chosenRoom.RoomId;
                txtName.Text = chosenRoom.RoomName;
                txtDescription.Text = chosenRoom.Description;
                txtCapacity.Text = chosenRoom.Capacity.ToString();
                txtImageUrl.Text = chosenRoom.ImageUrl;
                cbRoom.SelectedValue = chosenRoom.RoomTypeId;
            }
        }
        //Pick image
        private void btnUpload_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files|*.bmp;*.jpg;*.png";
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == true)
            {
                var destinationPath = _pictureService.SaveImageToEnv(openFileDialog.FileName);
                txtImageUrl.Text = destinationPath;
            }
        }

        private void btnShow_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtImageUrl.Text))
                MessageBox.Show("No Image chosen", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            else
            {
                ImageShowWindow imageShowWindow = new ImageShowWindow(txtImageUrl.Text);
                imageShowWindow.ShowDialog();
            }
        }

        private void searchBox_LostFocus(object sender, RoutedEventArgs e)
        {
            searchBox.Text = "Search";
            LoadRooms();
        }

        private void searchBox_GotFocus(object sender, RoutedEventArgs e)
        {
            searchBox.Text = string.Empty;

        }

        private async void searchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Search();
        }
        private async void Search()
        {
            var searchKey = searchBox.Text;
            if (!string.IsNullOrEmpty(searchKey))
            {
                var searchRs = await _roomService.GetRoomsByName(searchKey);
                lvRoom.ItemsSource = searchRs;
            }
            else LoadRooms();
        }

        private async void btnImport_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<Room> list = new List<Room>();
            var (format, filePath) = ChoseDataType();
            if (string.IsNullOrEmpty(filePath))
            {
                MessageBox.Show("Invalid file", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            
                switch (format.ToLower())
                {
                    case "json":
                        _readerService = new JsonReaderService<Room>();
                        var jsonData = _readerService.Import("C:\\Users\\84859\\Desktop\\handon\\MiniHotelManagement\\MiniHotelManagement\\rooms.json");
                        list = jsonData;
                        break;
                    case "xml":
                        _readerService = new XmlReaderService<Room>();
                        var xmlData = _readerService.Import("C:\\Users\\84859\\Desktop\\handon\\MiniHotelManagement\\MiniHotelManagement\\rooms.xml");
                        list = xmlData;
                        break;
                    default:
                        break;
                }
                lvRoom.ItemsSource = list;
   
        }

        private async void btnExport_Click(object sender, RoutedEventArgs e)
        {
            var isSuccess = false;
            var (format,filePath) = ChoseDataType();
            if (string.IsNullOrEmpty(filePath))
            {
                MessageBox.Show("Invalid file", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            var data = await _roomService.GetRooms();
            switch (format.ToLower())
            {
                case "xml":
                    _readerService = new XmlReaderService<Room>();
                    var xmlResult = _readerService.Export(data as List<Room>, filePath);
                    isSuccess = xmlResult;
                    break;
                case "json":
                    _readerService = new JsonReaderService<Room>();
                    var jsonResult = _readerService.Export((List<Room>)data, filePath);
                    isSuccess = jsonResult;
                    break;
                default:
                    break;
            }
            if (!isSuccess)
            {
                MessageBox.Show("Fail", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            MessageBox.Show("Success", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private (string format,string filePath) ChoseDataType()
        {
            FormatSelectWindow selectFormatWindow = new FormatSelectWindow();
            if (selectFormatWindow.ShowDialog() == true)
            {
                string selectedFormat = selectFormatWindow.SelectedFormat;
                string filePath = selectFormatWindow.FilePath;
                return (selectedFormat, filePath);
            }
            return (string.Empty, string.Empty);
        }
    }
}
