using CSV_Reader.Data;
using CSV_Reader.Models;
using Microsoft.EntityFrameworkCore;

namespace CSV_Reader.Services
{
    public class CsvRepository: ICsvRepository, IDisposable
    {
        private readonly ApplicationDbContext _context;

        public CsvRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public int GetValidUriCount()
        {
            int validUriCount = _context.Hotels.Count(row => row.HasValidUri == true);
            return validUriCount;
        }

        public int GetInvalidUriCount()
        {
            int invalidUriCount = _context.Hotels.Count(row => row.HasValidUri == false);
            return invalidUriCount;
        }

        public int GetValidStarsCount()
        {
            int validStarsCount = _context.Hotels.Count(row => row.HasValidStars == true);
            return validStarsCount;
        }

        public int GetInvalidStarsCount()
        {
            int invalidStarsCount = _context.Hotels.Count(row => row.HasValidStars == false);
            return invalidStarsCount;
        }

        public int GetValidHotelNameCount()
        {
            int validStarsCount = _context.Hotels.Count(row => row.HasValidHotelName == true);
            return validStarsCount;
        }

        public int GetInvalidHotelNameCount()
        {
            int invalidStarsCount = _context.Hotels.Count(row => row.HasValidHotelName == false);
            return invalidStarsCount;
        }

        public int GetInvalidRowsCount()
        {
            var invalidRowsCount = _context.Hotels.Count(x => x.HasValidStars == false || x.HasValidHotelName == false || x.HasValidUri == false);
            return invalidRowsCount;
        }

        public int GetValidRowsCount()
        {
            var validRowsCount = _context.Hotels.Count(x => x.HasValidStars == true && x.HasValidHotelName == true && x.HasValidUri == true);
            return validRowsCount;
        }

        public List<Hotel> GetHotels()
        {
            var hotels = _context.Hotels.ToList();
            return hotels;
        }

        public int DeleteAllData()
        {
            IQueryable<Hotel> all = from c in _context.Hotels select c;

            _context.Hotels.RemoveRange(all);
            int modifiedCount = _context.SaveChanges();

            return modifiedCount;
        }

        public void SaveHotels(List<Hotel> hotels)
        {
            if (hotels.Count != 0)
            {
                _context.Hotels.AddRange(hotels);
                _context.SaveChanges();
            }
        }

        public List<Hotel> GetOnlyValidHotels(string selectedAnswer)
        {
            var validHotels = _context.Hotels.Where(x => x.HasValidStars == true && x.HasValidHotelName == true && x.HasValidUri == true);

            if (selectedAnswer == "SortByStar")
            {
                validHotels.OrderByDescending(s => s.Stars)
                        .ThenBy(s => s.Name)
                        .ThenBy(s => s.Uri)
                        .ThenBy(s => s.Address);
            }

            if (selectedAnswer == "SortByName")
            {
                validHotels.OrderBy(s => s.Name)
                    .ThenBy(s => s.Stars)
                    .ThenBy(s => s.Uri)
                    .ThenBy(s => s.Address);
            }

            return validHotels.ToList();
        }

        // Used to prevent Dispose method to be called outside of the class.
        void IDisposable.Dispose()
        {
        }
    }
}
