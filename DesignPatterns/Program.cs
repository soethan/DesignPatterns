using DesignPatterns.BridgePattern;
using DesignPatterns.BuilderPattern;
using DesignPatterns.CommandPattern;
using DesignPatterns.Decorator;
using DesignPatterns.InterpreterPattern;
using DesignPatterns.VisitorPattern;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Creational Patterns

            #region Builder

            Console.WriteLine("*********** Builder Pattern starts ***********");

            VehicleBuilder builder;
            // Create shop with vehicle builders
            var shop = new Shop();

            // Construct and display vehicles
            builder = new ScooterBuilder();
            shop.Construct(builder);
            builder.Vehicle.Show();

            builder = new CarBuilder();
            shop.Construct(builder);
            builder.Vehicle.Show();

            builder = new MotorCycleBuilder();
            shop.Construct(builder);
            builder.Vehicle.Show();

            Console.WriteLine("*********** Builder Pattern ends ***********");

            #endregion

            #endregion

            #region Behavioral Patterns

            #region Command

            Console.WriteLine("*********** Command Pattern starts ***********");

            // Create user and let it compute
            var user = new User();

            // User presses calculator buttons
            user.Compute('+', 100);
            user.Compute('-', 50);
            user.Compute('*', 10);
            user.Compute('/', 2);

            // Undo 4 commands
            user.Undo(4);
            // Redo 3 commands
            user.Redo(3);

            Console.WriteLine("*********** Command Pattern ends ***********");
            
            #endregion

            #region Interpreter

            Console.WriteLine("*********** Interpreter Pattern starts ***********");

            string roman = "MCMXXVIII";
            var context = new Context(roman);

            // Build the 'parse tree'
            var tree = new List<Expression>();
            tree.Add(new ThousandExpression());
            tree.Add(new HundredExpression());
            tree.Add(new TenExpression());
            tree.Add(new OneExpression());

            foreach (Expression exp in tree)
            {
                exp.Interpret(context);
            }

            Console.WriteLine("{0} = {1}", roman, context.Output);

            Console.WriteLine("*********** Interpreter Pattern ends ***********");

            #endregion

            #region Visitor

            Console.WriteLine("*********** Visitor Pattern starts ***********");

            // Setup employee collection
            Employees e = new Employees();
            e.Attach(new Clerk());
            e.Attach(new Director());
            e.Attach(new President());

            // Employees are 'visited'
            e.Accept(new IncomeVisitor());
            e.Accept(new VacationVisitor());

            Console.WriteLine("*********** Visitor Pattern ends ***********");

            #endregion

            #endregion

            #region Structural Patterns

            #region Bridge

            Console.WriteLine("*********** Bridge Pattern starts ***********");

            var documents = new List<Manuscript>();
            var formatter = new FancyFormatter();

            var faq = new FAQ(formatter);
            faq.Title = "The Bridge Pattern FAQ";
            faq.Questions.Add("What is it?", "A design pattern");
            faq.Questions.Add("When do we use it?", "When you need to separate an abstraction from an implementation.");
            documents.Add(faq);

            var book = new Book(formatter)
            {
                Title = "Lots of Patterns",
                Author = "John Sonmez",
                Text = "Blah blah blah..."
            };
            documents.Add(book);

            var paper = new TermPaper(formatter)
            {
                Class = "Design Patterns",
                Student = "Joe N00b",
                Text = "Blah blah blah...",
                References = "GOF"
            };
            documents.Add(paper);

            foreach (var doc in documents)
            {
                doc.Print();
            }

            Console.WriteLine("*********** Bridge Pattern ends ***********");

            #endregion

            #region Decorator

            Console.WriteLine("*********** Decorator Pattern starts ***********");

            // create a Simple Cake Base first
            var cBase = new CakeBase();
            PrintProductDetails(cBase);

            // add cream to the cake
            var creamCake = new CreamDecorator(cBase);
            PrintProductDetails(creamCake);

            // now add a Cherry on it
            var cherryCake = new CherryDecorator(creamCake);
            PrintProductDetails(cherryCake);

            // now add Scent to it
            var scentedCake = new ArtificialScentDecorator(cherryCake);
            PrintProductDetails(scentedCake);

            // Finally add a Name card on the cake
            var nameCardOnCake = new NameCardDecorator(scentedCake);
            PrintProductDetails(nameCardOnCake);

            // now create a simple Pastry
            var pastry = new PastryBase();
            PrintProductDetails(pastry);

            // add cream and cherry only on the pastry 
            var creamPastry = new CreamDecorator(pastry);
            var cherryPastry = new CherryDecorator(creamPastry);
            PrintProductDetails(cherryPastry);

            Console.WriteLine("*********** Decorator Pattern ends ***********");

            #endregion

            #endregion

            Console.ReadKey();
        }

        private static void PrintProductDetails(BakeryComponent product)
        {
            Console.WriteLine(); // some whitespace for readability
            Console.WriteLine("Item: {0}, Price: {1}", product.GetName(), product.GetPrice());
        }
    }
}
