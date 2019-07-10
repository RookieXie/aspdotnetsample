using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Services;

namespace MyBlogWeb.Controllers
{
    [Route("api/myOperation")]
    [ApiController]
    public class MyOperationController : ControllerBase
    {
        public OperationService OperationService { get; }
        public IOperationTransient TransientOperation { get; }
        public IOperationScoped ScopedOperation { get; }
        public IOperationSingleton SingletonOperation { get; }
        public IOperationSingletonInstance SingletonInstanceOperation { get; }
        public MyOperationController(
        OperationService operationService,
        IOperationTransient transientOperation,
        IOperationScoped scopedOperation,
        IOperationSingleton singletonOperation,
        IOperationSingletonInstance singletonInstanceOperation)
        {
            OperationService = operationService;
            TransientOperation = transientOperation;
            ScopedOperation = scopedOperation;
            SingletonOperation = singletonOperation;
            SingletonInstanceOperation = singletonInstanceOperation;
        }

        [HttpGet]
        [Route("index/t")]
        public string Index()
        {
            var controllerStr = $@"Transient:{TransientOperation.OperationId} 
                                Scoped:{ScopedOperation.OperationId}
                                Singleton:{SingletonOperation.OperationId}
                                SingletonInstance:{SingletonInstanceOperation.OperationId}";
            var serviceStr = $@"Transient:{OperationService.TransientOperation.OperationId}
                    Scoped:{OperationService.ScopedOperation.OperationId}
                    Singleton:{OperationService.SingletonOperation.OperationId}
                    SingletonInstance:{OperationService.SingletonInstanceOperation.OperationId}";
            return $@"controller:{controllerStr}


                serviceStr:{serviceStr}";
        }
    }
}