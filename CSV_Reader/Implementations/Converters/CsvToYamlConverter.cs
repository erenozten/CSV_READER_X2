using CSV_Reader.Interfaces.Converters;
using CSV_Reader.Models;
using SharpYaml.Serialization;

namespace CSV_Reader.Implementations.Converters
{
    public class CsvToYamlConverter : ICsvConverter, ICsvToYamlConverter
    {
        public string Convert(HotelViewModel hotelViewModel, string selectedAnswer)
        {
            var serializer = new Serializer();
            
            if (selectedAnswer == "GroupByStar")
            {
                var list = hotelViewModel.HotelsGrouped.ToList();
                string hotelsYaml = serializer.Serialize(new { list });
                return hotelsYaml;
            }
            else
            {
                string hotelsYaml = serializer.Serialize(new { hotelViewModel.Hotels });
                return hotelsYaml;
            }
        }
    }
}
