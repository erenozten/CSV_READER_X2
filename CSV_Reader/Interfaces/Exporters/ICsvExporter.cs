using CSV_Reader.Models;

namespace CSV_Reader.Interfaces.Exporters
{
    public interface ICsvExporter
    {
        void Export(HotelViewModel hotelViewModel, string selectedAnswer);

    }
}