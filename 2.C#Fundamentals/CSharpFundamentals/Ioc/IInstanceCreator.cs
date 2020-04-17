using System;

namespace Ioc
{
    public interface IInstanceCreator
    {
        object CreateInstance(Type type, params object[] parameters);
    }
}
