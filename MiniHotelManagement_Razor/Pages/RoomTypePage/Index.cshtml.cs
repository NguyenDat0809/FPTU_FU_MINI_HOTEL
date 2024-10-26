using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HotelManagement_BusinessObject.Context;
using HotelManagement_BusinessObject.Models;
using Microsoft.AspNetCore.Authorization;
using HotelManagement_Services.Interfaces;
using HotelManagement_Services.Implements;

namespace MiniHotelManagement_Razor.Pages.RoomTypePage
{

    public class IndexModel : PageModel
    {
        private readonly IRoomTypeService _roomTypeService;
        private IReaderService<RoomType> _readerService;

        public IndexModel(IRoomTypeService roomTypeService)
        {
            _roomTypeService = roomTypeService;
        }

        public IList<RoomType> RoomType { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_roomTypeService != null)
            {
                RoomType = (IList<RoomType>)await _roomTypeService.GetRoomTypes();
            }
        }

        public async Task OnPostJsonReader(string action)
        {
            _readerService = new JsonReaderService<RoomType>();
            if (_roomTypeService != null)
            {
                switch (action)
                {
                    case "import":
                            RoomType = _readerService.Import($"C:\\Users\\84859\\Desktop\\handon\\MiniHotelManagement\\MiniHotelManagement_Razor\\rooms.json");
                            break;
                    case "export":
                        RoomType = (IList<RoomType>)await _roomTypeService.GetRoomTypes();
                            var exportRs = _readerService.Export((List<RoomType>)RoomType, $"C:\\Users\\84859\\Desktop\\handon\\MiniHotelManagement\\MiniHotelManagement_Razor\\rooms.json");
                        if (exportRs)
                            TempData["SuccessMessage"] = "Export Success";
                        else TempData["ErrorMessage"] = "Load Failed";
                        break;
                    default:
                        TempData["ErrorMessage"] = "Load Failed";
                        break;
                }
            }
        }
        public async Task OnPostXmlReader(string action)
        {
            _readerService = new XmlReaderService<RoomType>();
            if (_roomTypeService != null)
            {
                switch (action)
                {
                    case "import":
                        RoomType = _readerService.Import($"C:\\Users\\84859\\Desktop\\handon\\MiniHotelManagement\\MiniHotelManagement_Razor\\rooms.xml");
                        break;
                    case "export":
                        RoomType = (IList<RoomType>)await _roomTypeService.GetRoomTypes();
                        var exportRs = _readerService.Export((List<RoomType>)RoomType, $"C:\\Users\\84859\\Desktop\\handon\\MiniHotelManagement\\MiniHotelManagement_Razor\\rooms.xml");
                        if (exportRs)
                            TempData["SuccessMessage"] = "Export Success";
                        else TempData["ErrorMessage"] = "Load Failed";
                        break;
                    default:
                        TempData["ErrorMessage"] = "Load Failed";
                        break;
                }
            }
        }
    }
}
