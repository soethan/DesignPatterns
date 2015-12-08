using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Observer
{
    /// <summary>
    /// The 'Subject' abstract class
    /// </summary>
    public abstract class Stock
    {
        private string _symbol;
        private double _price;
        private List<IInvestor> _investors = new List<IInvestor>();

        public Stock(string symbol, double price)
        {
            this._symbol = symbol;
            this._price = price;
        }

        public void Attach(IInvestor investor)
        {
            _investors.Add(investor);
        }

        public void Detach(IInvestor investor)
        {
            _investors.Remove(investor);
        }

        public void Notify()
        {
            foreach (var investor in _investors)
            {
                investor.Update(this);
            }
            Console.WriteLine("");
        }

        // Gets or sets the price
        public double Price
        {
            get { return _price; }
            set
            {
                if (_price != value)
                {
                    _price = value;
                    Notify();
                }
            }
        }

        // Gets the symbol
        public string Symbol
        {
            get { return _symbol; }
        }
    }

    /// <summary>
    /// The 'ConcreteSubject' class
    /// </summary>
    public class IBM : Stock
    {
        // Constructor
        public IBM(string symbol, double price)
            : base(symbol, price)
        {
        }
    }

    /// <summary>
    /// The 'Observer' interface
    /// </summary>
    public interface IInvestor
    {
        void Update(Stock stock);
    }

    /// <summary>
    /// The 'ConcreteObserver' class
    /// </summary>
    public class Investor : IInvestor
    {
        private string _name;

        public Investor(string name)
        {
            this._name = name;
        }
        public void Update(Stock stock)
        {
            Stock = stock;
            Console.WriteLine("Notified {0} of {1}'s change to {2:C}", _name, stock.Symbol, stock.Price);
        }

        public Stock Stock { get; set; }
    }
}