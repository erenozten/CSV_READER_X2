namespace CSV_Reader.Services
{
    public interface ICsvUtility
    {
        string GetTodaysDate();
        void ImportCsv(IFormFile file);
    }
}
