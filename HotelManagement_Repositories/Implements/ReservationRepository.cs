using HotelManagement_BusinessObject.Models;
using HotelManagement_DAO;
using HotelManagement_Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement_Repositories.Implements
{
    public class ReservationRepository : IReservationRepository
    {
        public async Task<bool> CreateReservation(BookingReservation reservation)
        {
            return await GenericDAO<BookingReservation>.Instance.InsertAsync(reservation);
        }

        public bool DeleteReservation(BookingReservation reservation)
        {
            return GenericDAO<BookingReservation>.Instance.Delete(reservation);
        }

        public async Task<BookingReservation?> GetReservationById(string id)
        {
            return await GenericDAO<BookingReservation>.Instance.SingleOrDefaultAsync(x => x.BookingReservationId == id);
        }

        public async Task<IEnumerable<BookingReservation>> GetReservations()
        {
            return await GenericDAO<BookingReservation>.Instance.GetListAsync(
                include: r => r.Include(r => r.Room)
                );
        }

        public async Task<IEnumerable<BookingReservation>> GetReservationsByDate(DateTime? start, DateTime? end)
        {
            if(start == null)
                return await GenericDAO<BookingReservation>.Instance.GetListAsync(x => x.BookingDate <= end);
            if (end == null)
                return await GenericDAO<BookingReservation>.Instance.GetListAsync(x => x.BookingDate >= start);
            return await GenericDAO<BookingReservation>.Instance.GetListAsync(x => x.BookingDate >= start && x.BookingDate <= end);
        }
        public async Task<IEnumerable<BookingReservation>> GetReservationsByDate(DateTime day)
        {
           
            return await GenericDAO<BookingReservation>.Instance.GetListAsync(x => x.BookingDate == day);
        }

        public async Task<IEnumerable<BookingReservation>> GetReservationsByDay(DateTime day)
        {
            return await GenericDAO<BookingReservation>.Instance.GetListAsync(x => x.BookingDate == day);
        }

        public bool UpdateReservation(BookingReservation reservation)
        {
            return GenericDAO<BookingReservation>.Instance.Update(reservation);
        }
    }
}
