
namespace CSV_Reader.Services
{
    public interface ICsvValidator
    {
        bool IsValidHotelName(string hotelName);
        bool IsValidUri(string uri);
        bool IsValidStars(int star);
    }
}
