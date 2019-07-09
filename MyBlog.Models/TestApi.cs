using System;
using System.Collections.Generic;
using System.Text;

namespace MyBlog.Models
{
    public class TestApi
    {
        public int Id { get; set; }
        public string ApiUrl { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public DateTime RequestDate { get; set; }
    }
}
