using System.ComponentModel.DataAnnotations;

namespace CSV_Reader.Models
{
    public class Hotel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int Stars { get; set; }
        public string Contact { get; set; }
        public string Phone { get; set; }
        public string Uri { get; set; }

        [Display(Name = "Valid Hotel Name?")]
        public bool HasValidHotelName { get; set; }

        [Display(Name = "Valid Uri?")]
        public bool HasValidUri { get; set; }

        [Display(Name = "Valid Stars?")]
        public bool HasValidStars { get; set; }
    }
}