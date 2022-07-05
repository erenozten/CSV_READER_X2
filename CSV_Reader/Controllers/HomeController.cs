using CSV_Reader.Interfaces.Converters;
using CSV_Reader.Interfaces.Exporters;
using CSV_Reader.Models;
using Microsoft.AspNetCore.Mvc;
using CSV_Reader.Services;
//using Microsoft.AspNetCore.Authorization;

namespace CSV_Reader.Controllers
{

    //[Authorize]
    public class HomeController : Controller
    {
        private readonly ICsvToDotNetObjectConverter _csvToDotNetObjectConverter;

        private readonly IJsonExporter _jsonExporter;
        private readonly IXmlExporter _xmlExporter;
        private readonly IYamlExporter _yamlExporter;
        private readonly IHtmlExporter _htmlExporter;
        private readonly ICsvExporter _csvExporter;
        private readonly ICsvRepository _csvRepository;
        private readonly ICsvUtility _csvUtility;

        public HomeController(
            IJsonExporter jsonExporter, 
            IXmlExporter xmlExporter, 
            IYamlExporter yamlExporter, 
            IHtmlExporter htmlExporter, 
            ICsvToDotNetObjectConverter csvToDotNetObjectConverter, 
            ICsvExporter csvExporter, 
            ICsvRepository csvRepository, 
            ICsvUtility csvUtility)
        {
            _csvToDotNetObjectConverter = csvToDotNetObjectConverter;
            _jsonExporter = jsonExporter;
            _xmlExporter = xmlExporter;
            _yamlExporter = yamlExporter;
            _htmlExporter = htmlExporter;
            _csvExporter = csvExporter;
            _csvRepository = csvRepository;
            _csvUtility = csvUtility;
        }

        public IActionResult Index(HotelViewModel returnedHotelViewModel)
        {
            // Get valid and invalid data count to inform user:
            var validUriCount = _csvRepository.GetValidUriCount();
            var invalidUriCount = _csvRepository.GetInvalidUriCount();

            var validStarsCount = _csvRepository.GetValidStarsCount();
            var invalidStarsCount = _csvRepository.GetInvalidStarsCount();

            var validHotelNameCount = _csvRepository.GetValidHotelNameCount();
            var invalidHotelNameCount = _csvRepository.GetInvalidHotelNameCount();

            var validRowsCount = _csvRepository.GetValidRowsCount();
            var invalidRowsCount = _csvRepository.GetInvalidRowsCount();

            var hotels = _csvRepository.GetOnlyValidHotels("");

            returnedHotelViewModel.Hotels = hotels;
            returnedHotelViewModel.ValidUriCount = validUriCount;
            returnedHotelViewModel.InvalidUriCount = invalidUriCount;
            returnedHotelViewModel.ValidStarsCount = validStarsCount;
            returnedHotelViewModel.InvalidStarsCount = invalidStarsCount;
            returnedHotelViewModel.ValidHotelNameCount = validHotelNameCount;
            returnedHotelViewModel.InvalidHotelNameCount = invalidHotelNameCount;
            returnedHotelViewModel.ValidRowsCount = validRowsCount;
            returnedHotelViewModel.InvalidRowsCount = invalidRowsCount;

            return View(returnedHotelViewModel);
        }

        public IActionResult DeleteAllData()
        {
            var deletedHotelCount = _csvRepository.DeleteAllData();

            if (deletedHotelCount == 0)
            {
                TempData["ClearDbMessage"] = "Nothing is deleted since database is already empty! Please import a CSV file.";
            }
            else
            {
                TempData["ClearDbMessage"] = $"Deleted {deletedHotelCount} rows successfully!";
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult PostFile(IFormFile postedFile, string selectedAnswer)
        {
            TempData["ImportFileError"] = null;
            var hotelsVm = new HotelViewModel();

            if (postedFile == null)
            {
                TempData["ImportFileError"] = "No file selected!";
                return RedirectToAction("Index");
            }

            if (postedFile.FileName.EndsWith(".csv"))
            {
                hotelsVm = _csvToDotNetObjectConverter.Convert(postedFile, selectedAnswer);

                // clear db before trying to import a new file
                _csvRepository.DeleteAllData();

                _csvRepository.SaveHotels(hotelsVm.Hotels);

                if (hotelsVm.Hotels.Count == 0)
                {
                    return RedirectToAction("Index");
                }

                // Get only valid hotels from db:
                var validHotels = _csvRepository.GetOnlyValidHotels(selectedAnswer);

                hotelsVm.Hotels = validHotels;

                // If valid hotel count is not equal to 0, export hotels as files (JSON, XML, HTML, YAML, CSV files).
                if (validHotels.Count != 0)
                {
                    _csvUtility.ImportCsv(postedFile);
                    _csvExporter.Export(hotelsVm, selectedAnswer);
                    _jsonExporter.Export(hotelsVm, selectedAnswer);
                    _xmlExporter.Export(hotelsVm, selectedAnswer);
                    _yamlExporter.Export(hotelsVm, selectedAnswer);
                    _htmlExporter.Export(hotelsVm, selectedAnswer);
                }
            }
            else
            {
                TempData["ImportFileError"] = "Selected file format is not .CSV or selected data contains no valid data!";
            }

            return RedirectToAction("Index", new { selectedAnswer = selectedAnswer });
        }
    }
}
