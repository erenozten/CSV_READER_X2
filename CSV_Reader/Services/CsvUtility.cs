using System.Globalization;
using CSV_Reader.Models;

namespace CSV_Reader.Services
{
    public class CsvUtility: ICsvUtility, IDisposable
    {
        public string GetTodaysDate()
        {
            var today = DateTime.Now.ToString("_M_dd_yyyy_HH_mm_ss", CultureInfo.InvariantCulture);
            return today;
        }

        public void ImportCsv(IFormFile file)
        {
            string path = @$"wwwroot\Uploads\ImportedCSV_{GetTodaysDate()}.csv";

            if (file.Length > 0)
            {
                using (Stream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
                {
                    file.CopyTo(fileStream);
                }
            }
        }

        // Used to prevent Dispose method to be called outside of the class.
        void IDisposable.Dispose()
        {
        }
    }
}
