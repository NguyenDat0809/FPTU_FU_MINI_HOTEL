using HotelManagement_Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HotelManagement_Services.Implements
{
    public class JsonReaderService<T> : IReaderService<T> where T : class
    {
        public bool Export(List<T> data, string filePath)
        {
            try
            {
                var option = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles
                };
                string json = JsonSerializer.Serialize(data, option);
                File.WriteAllText(filePath, json);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<T> Import(string filePath)
        {
            try
            {
                // Read JSON file asynchronously
                string jsonData =  File.ReadAllText(filePath);

                // Deserialize JSON data to List<T>
                List<T> list = JsonSerializer.Deserialize<IEnumerable<T>>(jsonData).ToList();

                return list;
            }
            catch (Exception ex)
            {
                return new List<T>(); 
            }
        }
    }
}
