using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ImagePosts.Web.Models;
using ImagePosts.Data;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace ImagePosts.Web.Controllers
{
    public class HomeController : Controller
    {
        private string _connection;
        public HomeController(IConfiguration configuration)
        {
            _connection = configuration.GetConnectionString("ConStr");
        }
        public IActionResult Index()
        {
            ImageRepository repository = new ImageRepository(_connection);
            return View(repository.GetImages());
        }
        public IActionResult AddImage()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddImage(Image i)
        {
            ImageRepository repository = new ImageRepository(_connection);
            repository.AddImage(i);
            return Redirect("/");
        }
        public IActionResult EnlargeImage(int imageId)
        {
            ImageRepository repository = new ImageRepository(_connection);
            ImageView view = new ImageView();
           
            if(HttpContext.Session.Get<List<int>>("Likes")!=null && HttpContext.Session.Get<List<int>>("Likes").Contains(imageId)==true)
            {
                view.Liked = true;
            }
            view.Image = repository.GetImageById(imageId);

            return View(view);
        }
        [HttpPost]
        public IActionResult LikeImage(Image image)
        {
            ImageRepository repository = new ImageRepository(_connection);
            int imageCount = repository.LikeImage(image.Id);
            var Likes = new List<int>();
            if (HttpContext.Session.Get<List<int>>("Likes") != null)
            {
                Likes = HttpContext.Session.Get<List<int>>("Likes");
            }
            Likes.Add(image.Id);
            HttpContext.Session.Set("Likes", Likes);

            return Json(imageCount);
        }
        public IActionResult LikesCount(int id)
        {
            ImageRepository repository = new ImageRepository(_connection);
            return Json(repository.GetLikeCount(id));
        }
    }
}
public static class SessionExtensions
{
    public static void Set<T>(this ISession session, string key, T value)
    {
        session.SetString(key, JsonConvert.SerializeObject(value));
    }

    public static T Get<T>(this ISession session, string key)
    {
        string value = session.GetString(key);

        return value == null ? default(T) :
            JsonConvert.DeserializeObject<T>(value);
    }
}
