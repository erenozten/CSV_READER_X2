using CSV_Reader.Interfaces.Converters;
using CSV_Reader.Interfaces.Exporters;
using CSV_Reader.Models;
using CSV_Reader.Services;

namespace CSV_Reader.Implementations.FileExporters
{
    public class YamlExporter : IFileExporter, IYamlExporter
    {
        private readonly ICsvToYamlConverter _csvToYamlConverter;
        private readonly ICsvUtility _csvUtility;

        public YamlExporter(ICsvToYamlConverter csvToYamlConverter, ICsvUtility csvUtility)
        {
            _csvToYamlConverter = csvToYamlConverter;
            _csvUtility = csvUtility;
        }

        public void Export(HotelViewModel hotelViewModel, string selectedAnswer)
        {
            string dataToSave = _csvToYamlConverter.Convert(hotelViewModel, selectedAnswer);
            File.WriteAllText(@$"wwwroot\Uploads\yamlFile{_csvUtility.GetTodaysDate()}.yml", dataToSave);
        }
    }
}
