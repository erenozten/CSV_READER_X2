
using CSV_Reader.Models;

namespace CSV_Reader.Interfaces.Converters
{
    public interface ICsvConverter
    {
        string Convert(HotelViewModel hotelViewModel, string selectedAnswer);
    }
}
