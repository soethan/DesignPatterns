using System;

namespace DesignPatterns
{
    public class Singleton<T> where T : class, new()
    {
        private static readonly Lazy<T> _instance = new Lazy<T>(() => new T());

        private Singleton()
        {

        }

        public static T Instance
        {
            get
            {
                return _instance.Value;
            }
        }
    }

    public class DbConnection
    {
        public DbConnection()
        {
            Console.WriteLine("DbConnection constructor is called...");
        }

        public void Connect()
        {
            Console.WriteLine("Connect to DB");
        }
    }
}
