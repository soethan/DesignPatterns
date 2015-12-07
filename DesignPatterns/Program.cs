using DesignPatterns.BridgePattern;
using DesignPatterns.CommandPattern;
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



            #endregion

            #region Behavioral Patterns

            #region Command

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

            #endregion

            #region Interpreter

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

            #endregion

            #region Visitor

            // Setup employee collection
            Employees e = new Employees();
            e.Attach(new Clerk());
            e.Attach(new Director());
            e.Attach(new President());

            // Employees are 'visited'
            e.Accept(new IncomeVisitor());
            e.Accept(new VacationVisitor());

            #endregion

            #endregion

            #region Structural Patterns

            #region Bridge

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

            #endregion

            #endregion

            Console.ReadKey();
        }
    }
}
