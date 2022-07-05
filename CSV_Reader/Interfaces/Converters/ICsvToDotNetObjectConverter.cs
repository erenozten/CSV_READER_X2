
using CSV_Reader.Models;

namespace CSV_Reader.Interfaces.Converters
{
    public interface ICsvToDotNetObjectConverter
    {
        HotelViewModel Convert(IFormFile postedFile, string selectedAnswer);
    }
}
