using CSV_Reader.Services;
using Xunit;

namespace CSV_Reader.Tests
{
    public class CsvValidatorTests
    {
        [Fact]
        public void IsValidHotelName_ReturnsTrue_WhenGivenHotelNameIsValid()
        {
            var hotelName = "Some hotel name goes here A aB bC cD dE eF fG gH hI iJ jK kL lM mN nO oP pQ qR rS sT tU uV vW wX xY yZ z...,/*-0123456789";

            CsvValidator csvService = new CsvValidator();

            var result1 = csvService.IsValidHotelName(hotelName);
            Assert.True(result1);
        }

        [Fact]
        public void IsValidHotelName_ReturnsFalse_WhenGivenHotelNameIsInvalid()
        {
            var hotelName = "Lombardo Neriëë";

            CsvValidator csvService = new CsvValidator();

            var result2 = csvService.IsValidHotelName(hotelName);
            Assert.False(result2);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void IsValidStar_ReturnsTrue_WhenGivenStarIsValid(int givenStar)
        {
            CsvValidator csvService = new CsvValidator();

            var isValidStar = csvService.IsValidStars(givenStar);

            Assert.True(isValidStar);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(6)]
        public void IsValidStar_ReturnsFalsee_WhenGivenStarIsInvalid(int givenStar)
        {
            CsvValidator csvService = new CsvValidator();

            var isValidStar = csvService.IsValidStars(givenStar);

            Assert.False(isValidStar);
        }

        [Theory]
        [InlineData("http://www.araswebsterab.fr/main.html")]
        [InlineData("http://test.com/tags/privacy/")]
        [InlineData("http://www.apartment.de/blog/posts/index.asp")]
        [InlineData("http://www.boutique.com/explore/category/search/post/")]
        [InlineData("https://www.schmidtke.de/category/terms/")]
        [InlineData("http://www.ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz")]
        public void IsValidUri_ReturnsTrue_WhenGivenUriIsValid(string givenUri)
        {
            CsvValidator csvService = new CsvValidator();

            var isValidUri = csvService.IsValidUri(givenUri);

            Assert.True(isValidUri);
        }

        [Theory]
        [InlineData("httq://www.schmidtke.de/category/terms/")]
        [InlineData("htt.ç://comfort.fr/categories/categories/index.php")]
        [InlineData("httq://ww.schmidtke.de/category/terms/-._~:/?#[]@!$&'()*+,;=")]
        public void IsValidUri_ReturnsFalse_WhenGivenUriIsInvalid(string givenUri)
        {
            CsvValidator csvService = new CsvValidator();

            var isValidUri = csvService.IsValidUri(givenUri);

            Assert.False(isValidUri);
        }
    }
}