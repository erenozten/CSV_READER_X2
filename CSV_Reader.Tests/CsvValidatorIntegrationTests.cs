using CSV_Reader.Models;
using CsvHelper;
using System.Globalization;
using System.IO;
using Xunit;

namespace CSV_Reader.Tests
{
    public class CsvValidatorIntegrationTests
    {
        [Fact]
        public void IsValidCSV()
        {
            using (var reader = new StreamReader("Files/tests_ValidStar.csv"))
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
                        Uri = csv.GetField("uri")
                    };

                    Assert.True(hotel.Stars <= 5, "Expected actualCount to be lower than 5.");
                    Assert.True(hotel.Stars >= 0, "Expected actualCount to be greater than 5.");
                }
            }
        }
    }
}