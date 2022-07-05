using CSV_Reader.Models;

namespace CSV_Reader.Interfaces.Exporters
{
    public interface IXmlExporter
    {
        void Export(HotelViewModel hotelViewModel, string selectedAnswer);
    }
}


