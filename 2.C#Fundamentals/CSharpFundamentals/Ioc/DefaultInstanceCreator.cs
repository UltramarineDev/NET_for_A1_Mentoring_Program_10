using System;

namespace Ioc
{
    public class DefaultInstanceCreator : IInstanceCreator
    {
        public object CreateInstance(Type type, params object[] parameters)
        {
           return Activator.CreateInstance(type, parameters);
        }
    }
}
