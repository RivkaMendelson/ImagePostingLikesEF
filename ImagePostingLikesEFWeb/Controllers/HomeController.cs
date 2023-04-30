using ImagePostingLikesEFData;
using ImagePostingLikesEFWeb.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ImagePostingLikesEFWeb.Controllers
{
    public class HomeController : Controller
    {
        private IWebHostEnvironment _webHostEnvironment;
        private string _connectionString;

        public HomeController(IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _webHostEnvironment = webHostEnvironment;
            _connectionString = configuration.GetConnectionString("ConStr");
        }

        public IActionResult Index()
        {

            ImageDB db = new(_connectionString);
            IndexPageViewModel vm = new()
            {
                images = db.GetImages()
            };
            return View(vm);
        }
        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Upload(IFormFile image, string title)
        {
            var fileName = $"{Guid.NewGuid()}-{image.FileName}";
            var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", fileName);
            using var fs = new FileStream(filePath, FileMode.CreateNew);
            image.CopyTo(fs);

            Image img = new()
            {
                FileName = fileName,
                Title = title,
                Likes = 0,
                Date = DateTime.Now
            };

            ImageDB db = new(_connectionString);
            db.UploadImage(img);
            return Redirect("/home/index");
        }

        public IActionResult ViewImage(int id)
        {
            if (id == 0)
            {
                return Redirect("/home/index");
            }

            ImageDB db = new(_connectionString);
            Image image = db.GetImageById(id);

            return View(image);
        }

        public void LikeImage(int id)
        {
            ImageDB db = new(_connectionString);
            db.LikeImage(id);
        }

        public int GetLikes(int id)
        {
            ImageDB db = new(_connectionString);
            return (db.GetLikes(id));
        }


    }
}