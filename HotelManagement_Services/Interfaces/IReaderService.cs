using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement_Services.Interfaces
{
    public interface IReaderService<T> where T : class
    {
        List<T> Import(string filePath);
        bool  Export(List<T> items, string filePath);
    }
}
