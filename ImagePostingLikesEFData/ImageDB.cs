using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ImagePostingLikesEFData
{
    public class ImageDB
    {
        private string _connectionString;

        public ImageDB(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void UploadImage(Image image)
        {
            using var context = new ImageDbContext(_connectionString);
            context.Images.Add(image);
            context.SaveChanges();
        }

        public List<Image> GetImages()
        {
            using var context = new ImageDbContext(_connectionString);
            return context.Images.ToList();
        }

        public Image GetImageById(int id)
        {
            using var context = new ImageDbContext(_connectionString);
            return context.Images.FirstOrDefault(image => image.Id == id);
        }

        public void LikeImage(int id)
        {
            using var context = new ImageDbContext(_connectionString);
            context.Database.ExecuteSqlInterpolated($"UPDATE Images SET LIKES = likes+1 WHERE Id = {id}");
        }

        public int GetLikes(int id)
        {
            using var context = new ImageDbContext(_connectionString);
            Image img = context.Images.FirstOrDefault(image => image.Id == id);
            return img.Likes;

        }

    }
}
