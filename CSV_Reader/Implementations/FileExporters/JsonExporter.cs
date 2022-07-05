using CSV_Reader.Interfaces.Converters;
using CSV_Reader.Interfaces.Exporters;
using CSV_Reader.Models;
using CSV_Reader.Services;

namespace CSV_Reader.Implementations.FileExporters
{
    public class JsonExporter : IFileExporter, IJsonExporter
    {
        private readonly ICsvToJsonConverter _csvToJsonConverter;
        private readonly ICsvUtility _csvUtility;

        public JsonExporter(ICsvToJsonConverter csvToJsonConverter, ICsvUtility csvUtility)
        {
            _csvToJsonConverter = csvToJsonConverter;
            _csvUtility = csvUtility;
        }
        public void Export(HotelViewModel hotelViewModel, string selectedAnswer)
        {
            var dataToSave = _csvToJsonConverter.Convert(hotelViewModel, selectedAnswer);
            File.WriteAllText(@$"wwwroot\Uploads\jsonFile{_csvUtility.GetTodaysDate()}.json", dataToSave); 
        }
    }
}
