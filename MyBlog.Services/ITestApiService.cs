using MyBlog.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBlog.Services
{
    public interface ITestApiService
    {
        int GetCount();
        List<TestApi> Gets();
        TestApi Get(int id);
    }
}
