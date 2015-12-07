using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.VisitorPattern
{
    /// <summary>
    /// The 'Visitor' interface
    /// </summary>
    public interface IVisitor
    {
        void Visit(Element element);
    }

    /// <summary>
    /// A 'ConcreteVisitor' class
    /// </summary>
    public class IncomeVisitor : IVisitor
    {
        public void Visit(Element element)
        {
            Employee employee = element as Employee;

            // Provide 10% pay raise
            employee.Income *= 1.10;
            Console.WriteLine("{0} {1}'s new income: {2:C}",
              employee.GetType().Name, employee.Name, employee.Income);
        }
    }

    /// <summary>
    /// A 'ConcreteVisitor' class
    /// </summary>
    public class IncomeVisitor2 : IVisitor
    {
        public void Visit(Element element)
        {
            Employee employee = element as Employee;

            // Make 10% pay cut
            employee.Income *= 0.9;
            Console.WriteLine("{0} {1}'s new income: {2:C}",
              employee.GetType().Name, employee.Name, employee.Income);
        }
    }

    /// <summary>
    /// A 'ConcreteVisitor' class
    /// </summary>
    public class VacationVisitor : IVisitor
    {
        public void Visit(Element element)
        {
            var employee = element as Employee;

            employee.VacationDays++;

            // Provide 3 extra vacation days
            Console.WriteLine("{0} {1}'s new vacation days: {2}",
              employee.GetType().Name, employee.Name,
              employee.VacationDays);
        }
    }

    /// <summary>
    /// The 'Element' abstract class
    /// </summary>
    public abstract class Element
    {
        public abstract void Accept(IVisitor visitor);
    }

    /// <summary>
    /// The 'ConcreteElement' class
    /// </summary>
    public class Employee : Element
    {
        private string _name;
        private double _income;
        private int _vacationDays;

        // Constructor
        public Employee(string name, double income,
          int vacationDays)
        {
            this._name = name;
            this._income = income;
            this._vacationDays = vacationDays;
        }

        // Gets or sets the name
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        // Gets or sets income
        public double Income
        {
            get { return _income; }
            set { _income = value; }
        }

        // Gets or sets number of vacation days
        public int VacationDays
        {
            get { return _vacationDays; }
            set { _vacationDays = value; }
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    /// <summary>
    /// The 'ObjectStructure' class
    /// </summary>
    public class Employees
    {
        private List<Employee> _employees = new List<Employee>();

        public void Attach(Employee employee)
        {
            _employees.Add(employee);
        }

        public void Detach(Employee employee)
        {
            _employees.Remove(employee);
        }

        public void Accept(IVisitor visitor)
        {
            foreach (Employee e in _employees)
            {
                e.Accept(visitor);
            }
            Console.WriteLine();
        }
    }

    public class Clerk : Employee
    {
        public Clerk()
            : base("Hank", 25000.0, 14)
        {
        }
    }

    public class Director : Employee
    {
        public Director()
            : base("Elly", 35000.0, 16)
        {
        }
    }

    public class President : Employee
    {
        public President()
            : base("Dick", 45000.0, 21)
        {
        }
    }
}
