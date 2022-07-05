using System.Text;
using CSV_Reader.Interfaces.Converters;
using CSV_Reader.Models;

namespace CSV_Reader.Implementations.Converters
{
    public class CsvToHtmlConverter : ICsvConverter, ICsvToHtmlConverter
    {
        public string Convert(HotelViewModel hotelViewModel, string selectedAnswer)
        {
            //List<HotelDto> hotelsDto = _mapper.Map<List<HotelDto>>(hotelViewModel.HotelsGrouped);

            if (selectedAnswer == "GroupByStar")
            {
                if (hotelViewModel.HotelsGrouped.Any())
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.Append("<table><tr><th>Star</th><th>Count</th></tr>");

                    foreach (var hotel in hotelViewModel.HotelsGrouped)
                    {
                        stringBuilder.Append("<tr><td>" + hotel.Star + "</td><td>" + hotel.Count + "</td></tr>");
                    }

                    stringBuilder.Append("</table>");

                    return stringBuilder.ToString();
                }
            }
            else
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("<table><tr><th>name</th><th>address</th><th>stars</th><th>contact</th><th>phone</th><th>uri</th><th>Valid Hotel Name?</th><th>Valid Uri?</th><th>Valid Stars?</th></tr>");

                //foreach (var hotel in hotelsDto)
                foreach (var hotel in hotelViewModel.Hotels)
                {
                    stringBuilder.Append("<tr><td>" + hotel.Name + "</td><td>" + hotel.Address + "</td><td>" + hotel.Stars + "</td><td>" + hotel.Contact + " </td><td>" + hotel.Phone + "</td><td>" + hotel.Uri + "</td><td>" + hotel.HasValidHotelName + "</td><td>" + hotel.HasValidUri + "</td><td>" + hotel.HasValidStars + "</td></tr>");
                }

                stringBuilder.Append("</table>");

                return stringBuilder.ToString();
            }

            return "";
        }
    }
}
