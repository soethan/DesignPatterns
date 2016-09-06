using System;
using System.Collections.Generic;

namespace DesignPatterns
{
    public interface IServiceA
    {
        void Execute();
    }

    public class ServiceA : IServiceA
    {
        public void Execute()
        {
            Console.WriteLine("A service called.");
        }
    }

    public interface IServiceB
    {
        void Execute();
    }

    public class ServiceB : IServiceB
    {
        public void Execute()
        {
            Console.WriteLine("B service called.");
        }
    }

    public interface IServiceLocator
    {
        T GetService<T>();
    }

    public class ServiceLocator : IServiceLocator
    {
        public Dictionary<object, object> serviceDict = null;
        public ServiceLocator()
        {
            serviceDict = new Dictionary<object, object>();
            serviceDict.Add(typeof(IServiceA), new ServiceA());
            serviceDict.Add(typeof(IServiceB), new ServiceB());
        }
        public T GetService<T>()
        {
            try
            {
                return (T)serviceDict[typeof(T)];
            }
            catch (Exception ex)
            {
                throw new NotImplementedException("Service not available.");
            }
        }
    }
}
