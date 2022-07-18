using System.Text;

namespace CSV_Reader.Services
{
    public class CsvValidator : ICsvValidator, IDisposable
    {
        public bool IsValidHotelName(string hotelName)
        {
            var asciiBytesCount = Encoding.ASCII.GetByteCount(hotelName);
            var unicodBytesCount = Encoding.UTF8.GetByteCount(hotelName);
            return asciiBytesCount == unicodBytesCount;
        }

        public bool IsValidUri(string uri)
        {
            if (Uri.TryCreate(uri, UriKind.Absolute, out Uri validatedUri))
            {
                return (validatedUri.Scheme == Uri.UriSchemeHttp || validatedUri.Scheme == Uri.UriSchemeHttps);
            }

            return false;
        }

        public bool IsValidStars(int star)
        {
            //if (Enumerable.Range(1, 5).Contains(star))
            //{
            //    return true;
            //}

            //return false;

            return star >= 1 && star <= 5;
        }

        // Used to prevent Dispose method to be called outside of the class.
        void IDisposable.Dispose()
        {
        }
    }
}
