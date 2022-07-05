using System.Globalization;
using CSV_Reader.Interfaces.Exporters;
using CSV_Reader.Models;
using CSV_Reader.Services;
using CsvHelper;

namespace CSV_Reader.Implementations.FileExporters
{
    public class CsvExporter : ICsvExporter
    {
        private readonly ICsvUtility _csvUtility;

        public CsvExporter(ICsvUtility csvUtility)
        {
            _csvUtility = csvUtility;
        }

        public void Export(HotelViewModel hotelViewModel, string selectedAnswer)
        {
            if (selectedAnswer == "GroupByStar")
            {
                using (var writer = new StreamWriter(@$"wwwroot\Uploads\ExportedCSV{_csvUtility.GetTodaysDate()}.csv"))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(hotelViewModel.HotelsGrouped);
                }
            }
            else
            {
                using (var writer = new StreamWriter(@$"wwwroot\Uploads\ExportedCSV{_csvUtility.GetTodaysDate()}.csv"))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(hotelViewModel.Hotels);
                }
            }
        }
    }
}