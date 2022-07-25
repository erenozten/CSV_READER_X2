using System.Globalization;
using CSV_Reader.Interfaces.Converters;
using CSV_Reader.Models;
using CSV_Reader.Services;
using CsvHelper;

namespace CSV_Reader.Implementations.Converters
{
    public class CsvToDotNetObjectConverter : ICsvToDotNetObjectConverter
    {
        private readonly ICsvValidator _csvValidator;

        public CsvToDotNetObjectConverter(ICsvValidator csvValidator)
        {
            _csvValidator = csvValidator;
        }

        public HotelViewModel Convert(IFormFile postedFile, string selectedAnswer)
        {
            HotelViewModel hotelViewModel = new HotelViewModel();

            using (var reader = new StreamReader(postedFile.OpenReadStream()))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    var hotel = new Hotel
                    {
                        Name = csv.GetField("name"),
                        Address = csv.GetField("address"),
                        Stars = int.Parse(csv.GetField("stars")),
                        Contact = csv.GetField("contact"),
                        Phone = csv.GetField("phone"),
                        Uri = csv.GetField("uri"),
                        HasValidHotelName = false,
                        HasValidUri = false,
                        HasValidStars = false
                    };

                    if (_csvValidator.IsValidHotelName(hotel.Name))
                    {
                        hotel.HasValidHotelName = true;
                    }

                    if (_csvValidator.IsValidUri(hotel.Uri))
                    {
                        hotel.HasValidUri = true;
                    }

                    if (_csvValidator.IsValidStars(hotel.Stars))
                    {
                        hotel.HasValidStars = true;
                    }

                    hotelViewModel.Hotels.Add(hotel);
                }


                if (selectedAnswer == "SortByName")
                {
                    hotelViewModel.Hotels = hotelViewModel.Hotels.OrderBy(s => s.Name)
                        .ThenBy(s => s.Stars)
                        .ThenBy(s => s.Uri)
                        .ThenBy(s => s.Address)
                        .ToList();
                }

                else if (selectedAnswer == "SortByStar")
                {
                    hotelViewModel.Hotels = hotelViewModel.Hotels.OrderByDescending(s => s.Stars)
                        .ThenBy(s => s.Name)
                        .ThenBy(s => s.Uri)
                        .ThenBy(s => s.Address)
                        .ToList();
                }

                else if (selectedAnswer == "GroupByStar")
                {
                    var hotelsGrouped = hotelViewModel.Hotels
                       .GroupBy(u => u.Stars)
                       .Select(row => new HotelsGrouped{ Star = row.Key, Count = row.Count().ToString() })
                       .Where(star => star.Star > 0 && star.Star < 6)
                       .OrderByDescending(s=>s.Star).ToList();

                    hotelViewModel.HotelsGrouped = hotelsGrouped;
                }
            }

            return hotelViewModel;
        }
    }
}
