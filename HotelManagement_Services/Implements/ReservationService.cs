using HotelManagement_BusinessObject.Models;
using HotelManagement_Repositories.Implements;
using HotelManagement_Repositories.Interfaces;
using HotelManagement_Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement_Services.Implements
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository; 
        public ReservationService()
        {
            _reservationRepository =  new ReservationRepository();
        }
        public async Task<bool> CreateReservation(BookingReservation reservation)
        {
            return await _reservationRepository.CreateReservation(reservation);
        }

        public bool DeleteReservation(BookingReservation reservation)
        {
            return _reservationRepository.DeleteReservation(reservation);
        }

        public async Task<BookingReservation?> GetReservationById(string id)
        {
            return await _reservationRepository.GetReservationById(id);
        }

        public async Task<IEnumerable<BookingReservation>> GetReservations()
        {
            return await _reservationRepository.GetReservations();
        }

        public async Task<IEnumerable<BookingReservation>> GetReservationsByDate(DateTime? start, DateTime? end)
        {
            return await _reservationRepository.GetReservationsByDate(start, end);
        }
        public async Task<IEnumerable<BookingReservation>> GetReservationsByDay(DateTime day)
        {
            return await _reservationRepository.GetReservationsByDay(day);
        }

        public async Task<bool> UpdateReservation(BookingReservation reservation)
        {
            var reservationUpdate = await GetReservationById(reservation.BookingReservationId);
            reservationUpdate.BookingDate = reservation.BookingDate;
            reservationUpdate.BookingStatus = reservation.BookingStatus;
            reservationUpdate.CustomerName = reservation.CustomerName;
            reservationUpdate.RoomId = reservation.RoomId;
            reservationUpdate.TotalPrice = reservation.TotalPrice;
            return _reservationRepository.UpdateReservation(reservationUpdate);
        }
    }
}
