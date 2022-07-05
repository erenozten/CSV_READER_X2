using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSV_Reader.Models
{
    [NotMapped]
    public class HotelViewModel
    {
        public List<Hotel> Hotels { get; set; } = new List<Hotel>();

        public List<Hotel> HotelsValidAndInvalid { get; set; } = new List<Hotel>();

        public List<HotelsGrouped> HotelsGrouped { get; set; } = new List<HotelsGrouped>();

        public int ValidUriCount { get; set; }
        public int InvalidUriCount { get; set; }

        public int ValidHotelNameCount { get; set; }
        public int InvalidHotelNameCount { get; set; }

        public int ValidStarsCount { get; set; }
        public int InvalidStarsCount { get; set; }

        public int ValidRowsCount { get; set; }
        public int InvalidRowsCount { get; set; }
        
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Stars { get; set; }
        public string Contact { get; set; }
        public string Phone { get; set; }
        public string Uri { get; set; }

        [Display(Name = "Valid Hotel Name?")]
        public bool HasValidHotelName { get; set; }

        [Display(Name = "Valid Uri?")]
        public bool HasValidUri { get; set; }

        [Display(Name = "Valid Stars?")]
        public bool HasValidStars { get; set; }

        public string Star { get; set; }
        public string Count { get; set; }


        public string[] SortGroupRadioButton = new[] { "İsme Göre Sırala", "Puana göre sırala", "İsme göre grupla", "Puana göre grupla" };

        public enum SortGroupRadioButton2
        {
            SortByName = 1,
            SortByStar = 2,
            GroupByStar = 3
        }

        public string SelectedAnswer { get; set; }
    }
}
