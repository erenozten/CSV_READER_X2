using CSV_Reader.Interfaces.Converters;
using CSV_Reader.Interfaces.Exporters;
using CSV_Reader.Models;
using CSV_Reader.Services;

namespace CSV_Reader.Implementations.FileExporters
{
    public class XmlExporter : IFileExporter, IXmlExporter
    {
        private readonly ICsvToXmlConverter _csvToXmlConverter;
        private readonly ICsvUtility _csvUtility;

        public XmlExporter(ICsvToXmlConverter csvToXmlConverter, ICsvUtility csvUtility)
        {
            _csvToXmlConverter = csvToXmlConverter;
            _csvUtility = csvUtility;
        }

        public void Export(HotelViewModel hotelViewModel, string selectedAnswer)
        {
            string dataToSave = _csvToXmlConverter.Convert(hotelViewModel, selectedAnswer);
            File.WriteAllText(@$"wwwroot\Uploads\xmlFile{_csvUtility.GetTodaysDate()}.xml", dataToSave);
        }
    }
}