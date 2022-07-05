using System.Xml;
using System.Xml.Serialization;
using CSV_Reader.Interfaces.Converters;
using CSV_Reader.Models;

namespace CSV_Reader.Implementations.Converters
{
    public class CsvToXmlConverter : ICsvConverter, ICsvToXmlConverter
    {
        public string Convert(HotelViewModel hotelViewModel, string selectedAnswer)
        {
            string hotelsXml;

            if (selectedAnswer == "GroupByStar")
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<HotelsGrouped>));
                using (var stringWriter = new StringWriter())
                {
                    using (XmlWriter writer = XmlWriter.Create(stringWriter))
                    {
                        serializer.Serialize(writer, hotelViewModel.HotelsGrouped.ToList());
                        hotelsXml = stringWriter.ToString();
                    }
                }
                return hotelsXml;
            }
            else
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Hotel>));
                using (var stringWriter = new StringWriter())
                {
                    using (XmlWriter writer = XmlWriter.Create(stringWriter))
                    {
                        serializer.Serialize(writer, hotelViewModel.Hotels);
                        hotelsXml = stringWriter.ToString();
                    }
                }
                return hotelsXml;
            }

        }
    }
}
