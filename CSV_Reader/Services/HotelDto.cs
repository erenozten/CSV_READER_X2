using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSV_Reader.Models
{
    [NotMapped]
    public class HotelDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Stars { get; set; }
        public string Contact { get; set; }
        public string Phone { get; set; }
        public string Uri { get; set; }

        public IEnumerable<HotelsGrouped> HotelsGrouped { get; set; }

        [Display(Name = "Valid Hotel Name?")]
        public bool HasValidHotelName { get; set; }

        [Display(Name = "Valid Uri?")]
        public bool HasValidUri { get; set; }

        [Display(Name = "Valid Stars?")]
        public bool HasValidStars { get; set; }

        public string Star { get; set; }
        public string Count { get; set; }
    }
}
