using System;
using System.Collections.Generic;
using System.Reflection;

namespace Reflection.DIContainer
{
    public class DiContainer
    {
        private List<Assembly> _assemblies;
        private Dictionary<Type, object> _types;

        public DiContainer()
        {
            _assemblies = new List<Assembly>();
            _types = new Dictionary<Type, object>();
        }

        public void AddAssembly(Assembly assembly)
        {
            if (assembly != null)
            {
                _assemblies.Add(assembly);
            }
            //TODO: get all types from addition assembly
        }

        public void AddType(Type typeImpl, Type typeAbstract = null)
        {
            if (typeAbstract == null)
            {
                typeAbstract = typeImpl;
            }
            _types.Add(typeAbstract, typeImpl);
        }
    }
}
