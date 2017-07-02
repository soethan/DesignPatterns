using System;

namespace DesignPatterns
{
    /// <summary>
    /// The 'Originator' class
    /// </summary>
    public class SalesProspect
    {
        private string _name;
        private string _phone;
        private double _budget;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
            }
        }

        public string Phone
        {
            get { return _phone; }
            set
            {
                _phone = value;
            }
        }

        public double Budget
        {
            get { return _budget; }
            set
            {
                _budget = value;
            }
        }

        //Stores memento
        public Memento SaveMemento()
        {
            Console.WriteLine("\nSaving state --\n");
            return new Memento(_name, _phone, _budget);
        }

        // Restores memento
        public void RestoreMemento(Memento memento)
        {
            Console.WriteLine("\nRestoring state --\n");
            this.Name = memento.Name;
            this.Phone = memento.Phone;
            this.Budget = memento.Budget;
        }
    }

    /// <summary>
    /// The 'Memento' class
    /// </summary>
    public class Memento
    {
        private string _name;
        private string _phone;
        private double _budget;

        public Memento(string name, string phone, double budget)
        {
            this._name = name;
            this._phone = phone;
            this._budget = budget;
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }

        public double Budget
        {
            get { return _budget; }
            set { _budget = value; }
        }
    }

    /// <summary>
    /// The 'Caretaker' class
    /// </summary>
    public class ProspectMemory
    {
        private Memento _memento;

        public Memento Memento
        {
            set { _memento = value; }
            get { return _memento; }
        }
    }
}
