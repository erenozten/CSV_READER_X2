using CSV_Reader.Models;

namespace CSV_Reader.Interfaces.Exporters
{
    public interface IFileExporter
    {
        void Export(HotelViewModel hotelViewModel, string selectedAnswer);
    }
}
