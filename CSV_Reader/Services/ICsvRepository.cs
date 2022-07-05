using CSV_Reader.Models;

namespace CSV_Reader.Services
{
    public interface ICsvRepository
    {
        int GetValidUriCount();
        int GetInvalidUriCount();
        int GetValidStarsCount();
        int GetInvalidStarsCount();
        int GetValidHotelNameCount();
        int GetInvalidHotelNameCount();
        List<Hotel> GetHotels();
        int DeleteAllData();
        void SaveHotels(List<Hotel> hotels);
        List<Hotel> GetOnlyValidHotels(string selectedAnswer);
        public int GetValidRowsCount();
        public int GetInvalidRowsCount();
    }
}
