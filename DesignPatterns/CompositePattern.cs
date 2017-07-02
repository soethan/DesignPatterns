using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    /// <summary>
    /// Component
    /// </summary>
    public interface IWorker
    {
        void ShowHappiness();
    }
    /// <summary>
    /// Leaf
    /// </summary>
    public class Worker : IWorker
    {
        private string name;
        private int happiness;

        public Worker(string name, int happiness)
        {
            this.name = name;
            this.happiness = happiness;
        }

        public void ShowHappiness()
        {
            Console.WriteLine(name + " showed happiness level of " + happiness);
        }
    }
    /// <summary>
    /// Composite
    /// </summary>
    public class Supervisor : IWorker
    {
        private string name;
        private int happiness;

        private List<IWorker> subordinate = new List<IWorker>();

        public Supervisor(string name, int happiness)
        {
            this.name = name;
            this.happiness = happiness;
        }

        public void ShowHappiness()
        {
            Console.WriteLine(name + " showed happiness level of " + happiness);
            //show all the subordinate's happiness level
            foreach (IWorker i in subordinate)
                i.ShowHappiness();
        }

        public void AddSubordinate(IWorker employee)
        {
            subordinate.Add(employee);
        }
    }
}
