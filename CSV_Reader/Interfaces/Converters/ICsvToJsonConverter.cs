using CSV_Reader.Models;

namespace CSV_Reader.Interfaces.Converters
{
    public interface ICsvToJsonConverter
    {
        string Convert(HotelViewModel hotelViewModel, string selectedAnswer);
    }
}
