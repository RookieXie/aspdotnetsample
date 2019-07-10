using System;
using System.Collections.Generic;
using System.Text;

namespace MyBlog.Services
{
    public class MyOperation : IOperationTransient,
    IOperationScoped,
    IOperationSingleton,
    IOperationSingletonInstance
    {
        public MyOperation() : this(Guid.NewGuid())
        {
        }

        public MyOperation(Guid id)
        {
            OperationId = id;
        }

        public Guid OperationId { get; private set; }
    }
}
