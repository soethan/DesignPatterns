using DesignPatterns.BridgePattern;
using DesignPatterns.BuilderPattern;
using DesignPatterns.ChainOfResponsibility;
using DesignPatterns.CommandPattern;
using DesignPatterns.Decorator;
using DesignPatterns.InterpreterPattern;
using DesignPatterns.Mediator;
using DesignPatterns.Observer;
using DesignPatterns.VisitorPattern;
using System;
using System.Collections.Generic;
using DesignPatterns.Utilities;

namespace DesignPatterns
{
    class Program
    {
        static void Main(string[] args)
        {
            var level1 = new Level1 {
                Name = "Level1",
                Level2 = new Level2 {
                    Name = "Level2",
                    List = new List<string> { "aaa", "bbb", "ccc" },
                    Level3 = new Level3 {
                        Name = "Level3"
                    }
                }
            };

            var level1_samevalue = new Level1
            {
                Name = "Level1",
                Level2 = new Level2
                {
                    Name = "Level2",
                    List = new List<string> { "aaa", "bbb", "ccc" },
                    Level3 = new Level3
                    {
                        Name = "Level3"
                    }
                }
            };

            var level1_diffvalue = new Level1
            {
                Name = "Level1",
                Level2 = new Level2
                {
                    Name = "Level2",
                    List = new List<string> { "aaaA", "bbb", "ccc" },
                    Level3 = new Level3
                    {
                        Name = "Level3"
                    }
                }
            };

            if (level1.ObjectValuesEqual(level1_samevalue))
            {
                Console.WriteLine("Same Values");
            }
            else
            {
                Console.WriteLine("Different Values");
            }

            if (level1.ObjectValuesEqual(level1_diffvalue))
            {
                Console.WriteLine("Same Values");
            }
            else
            {
                Console.WriteLine("Different Values");
            }
            #region Creational Patterns

            #region Abstract Factory

            Console.WriteLine("*********** Abstract Factory Pattern starts ***********");

            var phoneClient1 = new PhoneClient(MANUFACTURER.NOKIA);
            var normalPhone1 = phoneClient1.GetPhone(PHONETYPE.NORMAL) as INormalPhone;
            var normalPhone2 = phoneClient1.GetPhone(PHONETYPE.SMART) as ISmartPhone;
            Console.WriteLine(normalPhone1.Name());
            Console.WriteLine(normalPhone2.Name());

            var phoneClient2 = new PhoneClient(MANUFACTURER.SONYERICSSON);
            var normalPhone3 = phoneClient2.GetPhone(PHONETYPE.NORMAL) as INormalPhone;
            var normalPhone4 = phoneClient2.GetPhone(PHONETYPE.SMART) as ISmartPhone;
            Console.WriteLine(normalPhone3.Name());
            Console.WriteLine(normalPhone4.Name());

            Console.WriteLine("*********** Abstract Factory Pattern ends ***********");

            #endregion

            #region Factory Method

            Console.WriteLine("*********** Factory Method Pattern starts ***********");

            // Note: constructors call Factory Method
            var documentList = new List<Document>();
            documentList.Add(new Resume());
            documentList.Add(new Report());

            // Display document pages
            foreach (Document document in documentList)
            {
                Console.WriteLine("\n" + document.GetType().Name + "--");
                foreach (Page page in document.Pages)
                {
                    Console.WriteLine(" " + page.GetType().Name);
                }
            }

            Console.WriteLine("*********** Factory Method Pattern ends ***********");

            #endregion

            #region Singleton

            Console.WriteLine("*********** Singleton Pattern starts ***********");

            Singleton<DbConnection>.Instance.Connect();
            Console.WriteLine(string.Format("IsSameInstance = {0}", Singleton<DbConnection>.Instance == Singleton<DbConnection>.Instance));

            Console.WriteLine("*********** Singleton Pattern ends ***********");

            #endregion

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

            #region ChainOfResponsibility

            Console.WriteLine("*********** Chain of Responsibility Pattern starts ***********");

            // Setup Chain of Responsibility
            Approver directorLarry = new CompanyDirector();
            Approver vicePresidentSam = new CompanyVicePresident();
            Approver presidentTammy = new CompanyPresident();

            directorLarry.SetSuccessor(vicePresidentSam);
            vicePresidentSam.SetSuccessor(presidentTammy);

            // Generate and process purchase requests
            Purchase p = new Purchase(2034, 350.00, "Assets");
            directorLarry.ProcessRequest(p);

            p = new Purchase(2035, 32590.10, "Project X");
            directorLarry.ProcessRequest(p);

            p = new Purchase(2036, 122100.00, "Project Y");
            directorLarry.ProcessRequest(p);

            Console.WriteLine("*********** Chain of Responsibility Pattern ends ***********");

            #endregion

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

            #region Mediator

            Console.WriteLine("*********** Mediator Pattern starts ***********");

            // Create chatroom
            IChatroom chatroom = new Chatroom();

            // Create participants and register them
            Participant George = new GraduateStudentParticipant("George");
            Participant Paul = new GraduateStudentParticipant("Paul");
            Participant Ringo = new GraduateStudentParticipant("Ringo");
            Participant John = new GraduateStudentParticipant("John");
            Participant Yoko = new UnderGraduateStudentParticipant("Yoko");

            chatroom.Register(George);
            chatroom.Register(Paul);
            chatroom.Register(Ringo);
            chatroom.Register(John);
            chatroom.Register(Yoko);

            // Chatting participants
            Yoko.Send("John", "Hi John!");
            Paul.Send("Ringo", "You need to study");
            Ringo.Send("George", "Hello");
            Paul.Send("John", "How are you");
            John.Send("Yoko", "I can explain you Maths");

            Console.WriteLine("*********** Mediator Pattern ends ***********");

            #endregion

            #region Observer

            Console.WriteLine("*********** Observer Pattern starts ***********");

            // Create IBM stock and attach investors
            Stock ibm = new IBM("IBM", 120.00);
            ibm.Attach(new Investor("Sorros"));
            ibm.Attach(new Investor("Berkshire"));

            // Fluctuating prices will notify investors
            ibm.Price = 120.10;
            ibm.Price = 121.00;
            ibm.Price = 120.50;
            ibm.Price = 120.75;

            Console.WriteLine("*********** Observer Pattern ends ***********");

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

            #region Service Locator Pattern

            //http://www.c-sharpcorner.com/UploadFile/dacca2/service-locator-design-pattern/
            Console.WriteLine("*********** Service Locator Pattern starts ***********");

            ServiceLocator serviceLocator = new ServiceLocator();
            IServiceA serviceA = serviceLocator.GetService<IServiceA>();
            serviceA.Execute();

            IServiceB serviceB = serviceLocator.GetService<IServiceB>();
            serviceB.Execute();

            Console.WriteLine("*********** Service Locator Pattern ends ***********");
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
