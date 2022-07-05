using System.Text.Json;
using CSV_Reader.Interfaces.Converters;
using CSV_Reader.Models;

namespace CSV_Reader.Implementations.Converters
{
    public class CsvToJsonConverter : ICsvConverter, ICsvToJsonConverter
    {
        public string Convert(HotelViewModel hotelViewModel, string selectedAnswer)
        {
            if (selectedAnswer == "GroupByStar")
            {
                string json = JsonSerializer.Serialize(hotelViewModel.HotelsGrouped);
                return json;
            }
            else
            {
                string json = JsonSerializer.Serialize(hotelViewModel.Hotels);
                return json;
            }
        }
    }
}