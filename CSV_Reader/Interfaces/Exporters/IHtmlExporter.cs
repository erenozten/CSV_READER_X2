using CSV_Reader.Models;

namespace CSV_Reader.Interfaces.Exporters
{
    public interface IHtmlExporter
    {
        void Export(HotelViewModel hotelViewModel, string selectedAnswer);
    }
}



