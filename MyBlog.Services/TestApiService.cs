using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyBlog.Core;
using MyBlog.DBContext;
using MyBlog.Models;

namespace MyBlog.Services
{
    public class TestApiService : ITestApiService,IAutoInjectScope
    {
        private readonly EFContext eFContext;
        public TestApiService( EFContext eFContext)
        {
            this.eFContext = eFContext;
        }
        public TestApi Get(int id)
        {
            return eFContext.TestApis.Where(a=>a.Id==id).FirstOrDefault();
        }

        public int GetCount()
        {
            return eFContext.TestApis.Count();
        }

        public List<TestApi> Gets()
        {
            throw new NotImplementedException();
        }
    }
}
