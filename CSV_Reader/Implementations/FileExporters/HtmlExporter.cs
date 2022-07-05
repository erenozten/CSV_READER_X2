using CSV_Reader.Interfaces.Converters;
using CSV_Reader.Interfaces.Exporters;
using CSV_Reader.Models;
using CSV_Reader.Services;

namespace CSV_Reader.Implementations.FileExporters
{
    public class HtmlExporter : IFileExporter, IHtmlExporter
    {
        private readonly ICsvToHtmlConverter _csvToHtmlConverter;
        private readonly ICsvUtility _csvUtility;

        public HtmlExporter(ICsvToHtmlConverter csvToHtmlConverter, ICsvUtility csvUtility)
        {
            _csvToHtmlConverter = csvToHtmlConverter;
            _csvUtility = csvUtility;
        }

        public void Export(HotelViewModel hotelViewModel, string selectedAnswer)
        {
                string dataToSave = _csvToHtmlConverter.Convert(hotelViewModel, selectedAnswer);
                File.WriteAllText(@$"wwwroot\Uploads\htmlFile{_csvUtility.GetTodaysDate()}.html", dataToSave);
        }
    }
}
