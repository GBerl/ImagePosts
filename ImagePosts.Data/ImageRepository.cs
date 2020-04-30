using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImagePosts.Data
{
    public class ImageRepository
    {
        private string _connection;
        public ImageRepository(string connection)
        {
            _connection = connection;
        }
        public List<Image>GetImages()
        {
            using(ImageContext context = new ImageContext(_connection))
            {
                return context.Images.OrderByDescending(i => i.Id).ToList();
            }
        }
        public void AddImage(Image image)
        {
            using (ImageContext context = new ImageContext(_connection))
            {
                context.Add(image);
                context.SaveChanges();
            }
        }
        public Image GetImageById(int id)
        {
            using (ImageContext context = new ImageContext(_connection))
            {
                return context.Images.FirstOrDefault(i => i.Id == id);
            }
        }
        public int LikeImage(int id)
        {
            using (ImageContext context = new ImageContext(_connection))
            {
                Image i = GetImageById(id);
                int likes = i.Likes;
                likes++;
                i.Likes = likes;
                context.Images.Attach(i);
                context.Entry(i).State = EntityState.Modified;
                context.SaveChanges();
                return likes;
            }
        }
        public int GetLikeCount(int id)
        {
            using (ImageContext context = new ImageContext(_connection))
            {
                Image image = context.Images.FirstOrDefault(i => i.Id == id);
                return image.Likes;
            }
        }
    }
}