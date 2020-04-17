using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ioc
{
    public class EmitInstanceCreator : IInstanceCreator
    {
        public object CreateInstance(Type type, object[] parameters)
        {
            throw new NotImplementedException();
        }
    }
}
