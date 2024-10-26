using HotelManagement_Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HotelManagement_Services.Implements
{
    public class XmlReaderService<T> : IReaderService<T> where T : class
    {
        public bool Export(List<T> items, string filePath)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
                using (TextWriter textWriter = new StreamWriter(filePath))
                {
                    serializer.Serialize(textWriter, items);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<T> Import(string filePath)
        {

            using var reader = new StreamReader(filePath);
            var serializer = new XmlSerializer(typeof(List<T>));
            var data = (List<T>)serializer.Deserialize(reader);
            return data ?? new List<T>();
        }
    }
}
