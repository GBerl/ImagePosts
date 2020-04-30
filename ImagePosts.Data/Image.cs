using System;
using System.Collections.Generic;
using System.Text;

namespace ImagePosts.Data
{
    public class Image
    {
        public int Id { get; set; }
        public string ImageURL { get; set; }
        public string Text { get; set; }
        public int Likes { get; set; }
    }
}
