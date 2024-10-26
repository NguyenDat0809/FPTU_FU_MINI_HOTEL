using HotelManagement_BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement_Repositories.Interfaces
{
    public interface IReservationRepository
    {
        Task<BookingReservation?> GetReservationById(string id);
        Task<IEnumerable<BookingReservation>> GetReservations();
        Task<IEnumerable<BookingReservation>> GetReservationsByDate(DateTime? start, DateTime? end);
        Task<bool> CreateReservation(BookingReservation reservation);
        bool UpdateReservation(BookingReservation reservation);
        bool DeleteReservation(BookingReservation reservation);
    }
}
