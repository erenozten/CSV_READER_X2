using Microsoft.EntityFrameworkCore;

namespace CSV_Reader.Models
{
    [Keyless]
    public class HotelsGrouped
    {
        public int Star { get; set; }
        public string Count { get; set; }
    }
}
