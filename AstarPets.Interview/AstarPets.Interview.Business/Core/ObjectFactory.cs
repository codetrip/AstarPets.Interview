using System;
using Castle.Windsor;

namespace AstarPets.Interview.Business.Core
{
    public static class ObjectFactory
    {
        private static WindsorContainer _container = new WindsorContainer();

        public static T GetObject<T>(Type t)
        {
            return (T)_container.Resolve(t);
        }

        public static void Release(object o)
        {
            _container.Release(o);
        }

        public static WindsorContainer WindsorContainer { get { return _container; } }
    }
}