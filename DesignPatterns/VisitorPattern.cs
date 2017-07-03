using System;
using System.Collections.Generic;

namespace DesignPatterns.VisitorPattern
{
    /// <summary>
    /// Represent an operation to be performed on the elements of an object structure. 
    /// Visitor lets you define a new operation without changing the classes of the elements on which it operates.
    /// http://www.dofactory.com/net/visitor-design-pattern
    /// </summary>

    //The 'Visitor' interface
    public interface IVisitor
    {
        void Visit(AbstractEmployee element);
    }

    //ConcreteVisitor
    public class IncomeVisitor : IVisitor
    {
        public void Visit(AbstractEmployee element)
        {
            Employee employee = element as Employee;

            // Provide 10% pay raise
            employee.Income *= 1.10;
            Console.WriteLine("{0} {1}'s new income: {2:C}",
              employee.GetType().Name, employee.Name, employee.Income);
        }
    }

    //ConcreteVisitor
    public class IncomeVisitor2 : IVisitor
    {
        public void Visit(AbstractEmployee element)
        {
            Employee employee = element as Employee;

            // Make 10% pay cut
            employee.Income *= 0.9;
            Console.WriteLine("{0} {1}'s new income: {2:C}",
              employee.GetType().Name, employee.Name, employee.Income);
        }
    }

    //ConcreteVisitor
    public class VacationVisitor : IVisitor
    {
        public void Visit(AbstractEmployee element)
        {
            var employee = element as Employee;

            employee.VacationDays++;

            // Provide 3 extra vacation days
            Console.WriteLine("{0} {1}'s new vacation days: {2}",
              employee.GetType().Name, employee.Name,
              employee.VacationDays);
        }
    }

    //'Element' abstract class
    public abstract class AbstractEmployee
    {
        public abstract void Accept(IVisitor visitor);
    }

    //ConcreteElement
    public class Employee : AbstractEmployee
    {
        private string _name;
        private double _income;
        private int _vacationDays;

        public Employee(string name, double income,
          int vacationDays)
        {
            this._name = name;
            this._income = income;
            this._vacationDays = vacationDays;
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

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

    //ObjectStructure
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
