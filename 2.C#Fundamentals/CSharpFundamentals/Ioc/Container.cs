using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Ioc
{
    public class Container
    {
        //dictionary from type and instance of this type
        private readonly Dictionary<Type, Func<object>> _providers;
        private IInstanceCreator _instanceCreator;

        public Container(IInstanceCreator instanceCreator)
        {
            _instanceCreator = instanceCreator;
            _providers = new Dictionary<Type, Func<object>>();
        }

        public void AddAssembly(Assembly assembly)
        {
            var types = assembly.ExportedTypes;

            foreach (var type in types)
            {
                var exportAttributes = type.GetCustomAttributes<ExportAttribute>();
                if (exportAttributes.Any())
                {
                    foreach(var attribute in exportAttributes)
                    {
                        _providers.Add(attribute.Contract ?? type, () => ResolveByType(type));
                    }
                }

                var hasConstructorImportAttribute = type.GetCustomAttribute<ImportConstructorAttribute>() != null;
                var hasPropertyImportAttributes = type.GetProperties().Where(p => p.GetCustomAttribute<ImportAttribute>() != null).Any();

                if (hasConstructorImportAttribute || hasPropertyImportAttributes)
                {
                    _providers.Add(type, () => ResolveByType(type));
                }
            }
        }

        public void Bind<TKey, TConcrete>() where TConcrete : TKey
        {
            _providers.Add(typeof(TKey), () => ResolveByType(typeof(TConcrete)));
        }

        public void Bind<T>(T instance)
        {
            _providers.Add(typeof(T), () => instance);
        }

        public TKey Resolve<TKey>()
        {
            return (TKey)Resolve(typeof(TKey));
        }

        public object Resolve(Type type)
        {
            if (_providers.TryGetValue(type, out Func<object> provider))
            {
                return provider();
            }

            return ResolveByType(type);
        }

        private object ResolveByType(Type type)
        {
            var constructor = type.GetConstructors().SingleOrDefault();
            //if have not constructor but static property
            if (constructor != null)
            {
                var arguments = constructor.GetParameters()
                                           .Select(parameterInfo => Resolve(parameterInfo.ParameterType))
                                           .ToArray();

                return constructor.Invoke(arguments);
            }

            return _instanceCreator.CreateInstance(type);
        }
    }
}
