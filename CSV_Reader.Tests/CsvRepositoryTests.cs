using CSV_Reader.Data;
using CSV_Reader.Models;
using CSV_Reader.Services;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CSV_Reader.Tests
{
    public class CsvRepositoryTests
    {
        private SqliteConnection _connection;
        private readonly DbContextOptions<ApplicationDbContext> _contextOptions;

        public CsvRepositoryTests()
        {
            // Create and open a connection. This creates the SQLite in-memory database,
            // which will persist until the connection is closed
            // at the end of the test (see Dispose below).
            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();

            // These options will be used by the context instances in this test suite,
            // including the connection opened above.
            _contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlite(_connection)
                .Options;
        }

        [Fact]
        public void GetHotels_ReturnsAllHotels()
        {
            // ARRANGE
            using var context = new ApplicationDbContext(_contextOptions);
            context.Database.EnsureCreated();

            var seedData = CreateHotels(1, 2, 3);
            context.Hotels.AddRange(seedData);
            context.SaveChanges();

            var context2 = new ApplicationDbContext(_contextOptions);

            // ACT
            ICsvRepository repo = new CsvRepository(context2);
            List<Hotel> hotels = repo.GetHotels();

            // ASSERT
            Assert.Equal(3, hotels.Count);
        }

        [Fact]
        public void DeleteAllData_DeletesAllHotels()
        {
            // ARRANGE
            using var context = new ApplicationDbContext(_contextOptions);
            context.Database.EnsureCreated();

            var seedData = CreateHotels(1, 2, 3);
            context.Hotels.AddRange(seedData);
            context.SaveChanges();

            var context2 = new ApplicationDbContext(_contextOptions);

            // ACT
            ICsvRepository repo = new CsvRepository(context2);
            int modifiedRows = repo.DeleteAllData();
            var hotels = context2.Hotels.ToList();

            // ASSERT
            Assert.Equal(3, modifiedRows);
            Assert.Empty(hotels);
        }

        [Fact]
        public void GetInvalidRowsCount_ReturnsTheNumberOfInvalidRows()
        {
            // ARRANGE
            using var context = new ApplicationDbContext(_contextOptions);
            context.Database.EnsureCreated();

            var hotels = CreateHotels(1, 2, 3);

            var invalidHotel = CreateHotel(id: 4);
            invalidHotel.HasValidHotelName = false;

            hotels.Add(invalidHotel);

            context.Hotels.AddRange(hotels);
            context.SaveChanges();

            var context2 = new ApplicationDbContext(_contextOptions);

            // ACT
            ICsvRepository repo = new CsvRepository(context2);
            int invalidRowsCount = repo.GetInvalidRowsCount();

            // ASSERT
            Assert.Equal(1, invalidRowsCount);
        }

        [Fact]
        public void GetValidRowsCount_ReturnsTheNumberOfValidRows()
        {
            // ARRANGE
            using var context = new ApplicationDbContext(_contextOptions);
            context.Database.EnsureCreated();

            var hotels = CreateHotels(1, 2, 3);

            var invalidHotel = CreateHotel(id: 5);
            invalidHotel.HasValidHotelName = false;

            hotels.Add(invalidHotel);

            context.Hotels.AddRange(hotels);
            context.SaveChanges();

            var context2 = new ApplicationDbContext(_contextOptions);

            // ACT
            ICsvRepository repo = new CsvRepository(context2);
            int validRowsCount = repo.GetValidRowsCount();

            // ASSERT
            Assert.Equal(3, validRowsCount);
            Assert.NotEqual(4, validRowsCount); // kolay anlamlandırılabilirlik adına
        }


        private List<Hotel> CreateHotels(params int[] ids)
        {
            return ids.Select(id => new Hotel()
            {
                Id = id,
                Address = $"test_adres {id}",
                Contact = $"test_contact  {id}",
                Name = $"test_name  {id}",
                Phone = $"test_phone {id}",
                HasValidHotelName = true,
                HasValidStars = true,
                HasValidUri = true,
                Stars = 5,
                Uri = "https://google.com"
            }).ToList();
        }

        private Hotel CreateHotel(int id)
        {
            return CreateHotels(id).First();
        }
    }
}
