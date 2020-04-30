using ImagePosts.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImagePosts.Web.Models
{
    public class ImageView
    {
        public Image Image { get; set; }
        public bool Liked { get; set; }
    }
}
