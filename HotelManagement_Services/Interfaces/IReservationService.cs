using HotelManagement_BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement_Services.Interfaces
{
    public interface IReservationService
    {
        Task<BookingReservation?> GetReservationById(string id);
        Task<IEnumerable<BookingReservation>> GetReservations();
        Task<IEnumerable<BookingReservation>> GetReservationsByDate(DateTime? start, DateTime? end);
        Task<IEnumerable<BookingReservation>> GetReservationsByDay(DateTime day);

        Task<bool> CreateReservation(BookingReservation reservation);
        Task<bool> UpdateReservation(BookingReservation reservation);
        bool DeleteReservation(BookingReservation reservation);
    }
}
