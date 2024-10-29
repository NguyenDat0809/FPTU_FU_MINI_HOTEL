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
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MiniHotelManagement.Pages
{
    /// <summary>
    /// Interaction logic for ReservationPage.xaml
    /// </summary>
    public partial class ReservationPage : Page
    {
        private readonly MainWindow _mainWindow;
        private readonly IReservationService _reservationService;
        private readonly IRoomService _roomService;
        public ReservationPage(MainWindow mainWindow)
        {
            InitializeComponent();
            _reservationService = new ReservationService();
            _roomService = new RoomService();
            _mainWindow = mainWindow;
        }



        private async void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var reservation = GetReservationFromForm();
                var validate = CheckValidate(reservation);
                if (!validate) {
                    return;
                }
                var duplicatedReservation = await _reservationService.GetReservationById(reservation.BookingReservationId);
                if (duplicatedReservation != null)
                {
                    MessageBox.Show("Duplicated Reservation id", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                var duplicatedDayReservation = await _reservationService.GetReservationsByDay(reservation.BookingDate.Value);
                if (duplicatedDayReservation != null)
                {
                    var isSameRoomInDay = false;
                    foreach (var roomInDay in duplicatedDayReservation) {
                        if(roomInDay.RoomId == reservation.RoomId)
                        {
                            isSameRoomInDay = true;
                            break;
                        }
                    }
                    if (isSameRoomInDay)
                    {
                        MessageBox.Show($"This room is ordered in {reservation.BookingDate}", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                  
                }
                var addRs = await _reservationService.CreateReservation(reservation);
                if (addRs)
                    MessageBox.Show("Created Successfully", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                else
                    MessageBox.Show("Created Failed", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

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
                var reservation = GetReservationFromForm();
                var validate = CheckValidate(reservation);
                if (!validate) return;
                var updateRs = await _reservationService.UpdateReservation(reservation);
                if (updateRs)
                    MessageBox.Show("Updated Successfully", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                else
                    MessageBox.Show("Updated Failed", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadReservations();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var reservation = GetReservationFromForm();
                var deleteRs = _reservationService.DeleteReservation(reservation);
                if (deleteRs)
                    MessageBox.Show("Deleted Successfully", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                else
                    MessageBox.Show("Deleted Failed", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadReservations();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            LoadRooms();

            LoadReservations();

            ChangeActionStatus(false);
            setContentByRole();
        }
        private void setContentByRole()
        {
            if (!_mainWindow.CanModify)
                btnDelete.IsEnabled = false;
        }

        private async void LoadReservations()
        {
            var reservations = await _reservationService.GetReservations();
            lvReservation.ItemsSource = reservations;
        }
        private async void LoadRooms()
        {
            var rooms = await _roomService.GetRooms();

            if (rooms != null && rooms.Any())
            {
                cbRoom.ItemsSource = rooms;
                cbRoom.DisplayMemberPath = "RoomName";
                cbRoom.SelectedValuePath = "RoomId";
                cbRoom.SelectedValue = rooms.First().RoomId;
            }
        }


        private void lvReservation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvReservation.SelectedItem is BookingReservation chosen)
            {
                txtId.Text = chosen.BookingReservationId;
                dpBookingDate.SelectedDate = chosen.BookingDate;
                txtTotalPrice.Text = chosen.TotalPrice.ToString();
                txtCustomerName.Text = chosen.CustomerName;
                cbRoom.SelectedValue = chosen.RoomId;
                cbBookingStatus.SelectedIndex = (int)chosen.BookingStatus;
            }
        }
        private void ChangeActionStatus(bool isCreateChecked)
        {
            btnUpdate.IsEnabled = !isCreateChecked;
            btnDelete.IsEnabled = !isCreateChecked && _mainWindow.CanModify;
            btnCreate.IsEnabled = isCreateChecked;
            txtId.IsEnabled = isCreateChecked;

            cbBookingStatus.SelectedIndex = 0;
            cbBookingStatus.IsEnabled = !isCreateChecked;
        }
        private BookingReservation GetReservationFromForm()
        {
            var id = txtId.Text.Trim();
            var bookingDate = dpBookingDate.SelectedDate;
            decimal totalPrice = Decimal.Parse(txtTotalPrice.Text.Trim());
            var fullName = txtCustomerName.Text.Trim();
            var roomId = cbRoom.SelectedValue;
            var bookingStatus = cbBookingStatus.SelectedIndex;
            var reservation = new BookingReservation()
            {
                BookingReservationId = id,
                BookingDate = bookingDate,
                TotalPrice = totalPrice,
                CustomerName = fullName,
                RoomId = roomId.ToString(),
                BookingStatus = (byte?)bookingStatus,
            };
            return reservation;
        }
        private bool CheckValidate(BookingReservation reservation)
        {
            if (string.IsNullOrEmpty(reservation.BookingReservationId))
            {
                MessageBox.Show("Reservation ID is required", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (reservation.BookingDate != null && reservation.BookingDate > DateTime.Now)
            {
                MessageBox.Show("Invalid Booking Date", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (string.IsNullOrEmpty(reservation.CustomerName))
            {
                MessageBox.Show("Customer name is required.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (reservation.TotalPrice > 100000)
            {
                MessageBox.Show("Total price is greater than 100.000", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
    }
}
