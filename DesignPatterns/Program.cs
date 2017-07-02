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
        private static object _ConsoleLock = new object();

        static void Main(string[] args)
        {
            #region Object Values Equal

            var level1 = new Level1 {
                Name = "Level1",
                Level2 = new Level2 {
                    Name = "Level2",
                    strArray = new List<string> { "aaa", "bbb", "ccc" },
                    Persons = new List<Person> { new Person { Id = 1, Name = "aaa" }, new Person { Id = 2, Name = "bb" } },
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
                    strArray = new List<string> { "ccc", "aaa", "bbb" },
                    Persons = new List<Person> { new Person{ Id = 1, Name = "aaa"}, new Person { Id = 2, Name = "bb" } },
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
                    strArray = new List<string>{"aaa", "bbbB", "ccc"},
                    Persons = new List<Person> { new Person { Id = 1, Name = "aaa" } },
                    Level3 = new Level3
                    {
                        Name = "Level3"
                    }
                }
            };

            if (level1.ObjectValuesEqual(level1_samevalue))
            {
                ConsoleWriteWithColor("Same Values");
            }
            else
            {
                ConsoleWriteWithColor("Different Values");
            }

            if (level1.ObjectValuesEqual(level1_diffvalue))
            {
                ConsoleWriteWithColor("Same Values");
            }
            else
            {
                ConsoleWriteWithColor("Different Values");
            }

            #endregion

            #region Creational Patterns

            #region Abstract Factory

            ConsoleWriteWithColor("*********** Abstract Factory Pattern starts ***********", ConsoleColor.Green);

            var phoneClient1 = new PhoneClient(MANUFACTURER.NOKIA);
            var normalPhone1 = phoneClient1.GetPhone(PHONETYPE.NORMAL) as INormalPhone;
            var normalPhone2 = phoneClient1.GetPhone(PHONETYPE.SMART) as ISmartPhone;
            ConsoleWriteWithColor(normalPhone1.Name(), ConsoleColor.Green);
            ConsoleWriteWithColor(normalPhone2.Name(), ConsoleColor.Green);

            var phoneClient2 = new PhoneClient(MANUFACTURER.SONYERICSSON);
            var normalPhone3 = phoneClient2.GetPhone(PHONETYPE.NORMAL) as INormalPhone;
            var normalPhone4 = phoneClient2.GetPhone(PHONETYPE.SMART) as ISmartPhone;
            ConsoleWriteWithColor(normalPhone3.Name(), ConsoleColor.Green);
            ConsoleWriteWithColor(normalPhone4.Name(), ConsoleColor.Green);

            ConsoleWriteWithColor("*********** Abstract Factory Pattern ends ***********", ConsoleColor.Green);

            #endregion

            #region Factory Method

            ConsoleWriteWithColor("*********** Factory Method Pattern starts ***********", ConsoleColor.Green);

            // Note: constructors call Factory Method
            var documentList = new List<Document>();
            documentList.Add(new Resume());
            documentList.Add(new Report());

            // Display document pages
            foreach (Document doc in documentList)
            {
                ConsoleWriteWithColor("\n" + doc.GetType().Name + "--", ConsoleColor.Green);
                foreach (Page page in doc.Pages)
                {
                    ConsoleWriteWithColor(" " + page.GetType().Name, ConsoleColor.Green);
                }
            }

            ConsoleWriteWithColor("*********** Factory Method Pattern ends ***********", ConsoleColor.Green);

            #endregion

            #region Singleton

            ConsoleWriteWithColor("*********** Singleton Pattern starts ***********", ConsoleColor.Green);

            Singleton<DbConnection>.Instance.Connect();
            ConsoleWriteWithColor(string.Format("IsSameInstance = {0}", Singleton<DbConnection>.Instance == Singleton<DbConnection>.Instance), ConsoleColor.Green);

            ConsoleWriteWithColor("*********** Singleton Pattern ends ***********", ConsoleColor.Green);

            #endregion

            #region Builder

            ConsoleWriteWithColor("*********** Builder Pattern starts ***********", ConsoleColor.Green);

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

            ConsoleWriteWithColor("*********** Builder Pattern ends ***********", ConsoleColor.Green);

            #endregion

            #region Prototype

            ConsoleWriteWithColor("*********** Prototype Pattern starts ***********", ConsoleColor.Green);

            var dev = new Developer(100, "Michael", "Developer", "C#");
            var devCopy = (Developer)dev.Clone();
            devCopy.Name = "Steven"; //Not mention Role and PreferredLanguage, it will copy above

            ConsoleWriteWithColor(dev.GetDetails(), ConsoleColor.Green);
            ConsoleWriteWithColor(devCopy.GetDetails(), ConsoleColor.Green);

            ConsoleWriteWithColor("*********** Prototype Pattern ends ***********", ConsoleColor.Green);

            #endregion

            #endregion

            #region Behavioral Patterns

            #region ChainOfResponsibility

            ConsoleWriteWithColor("*********** Chain of Responsibility Pattern starts ***********");

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

            ConsoleWriteWithColor("*********** Chain of Responsibility Pattern ends ***********");

            #endregion

            #region Command

            ConsoleWriteWithColor("*********** Command Pattern starts ***********");

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

            ConsoleWriteWithColor("*********** Command Pattern ends ***********");
            
            #endregion

            #region Interpreter

            ConsoleWriteWithColor("*********** Interpreter Pattern starts ***********");

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

            ConsoleWriteWithColor(string.Format("{0} = {1}", roman, context.Output), ConsoleColor.Blue);

            ConsoleWriteWithColor("*********** Interpreter Pattern ends ***********");

            #endregion

            #region Mediator

            ConsoleWriteWithColor("*********** Mediator Pattern starts ***********");

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

            ConsoleWriteWithColor("*********** Mediator Pattern ends ***********");

            #endregion

            #region Observer

            ConsoleWriteWithColor("*********** Observer Pattern starts ***********");

            // Create IBM stock and attach investors
            Stock ibm = new IBM("IBM", 120.00);
            ibm.Attach(new Investor("Sorros"));
            ibm.Attach(new Investor("Berkshire"));

            // Fluctuating prices will notify investors
            ibm.Price = 120.10;
            ibm.Price = 121.00;
            ibm.Price = 120.50;
            ibm.Price = 120.75;

            ConsoleWriteWithColor("*********** Observer Pattern ends ***********");

            #endregion

            #region Visitor

            ConsoleWriteWithColor("*********** Visitor Pattern starts ***********");

            // Setup employee collection
            Employees e = new Employees();
            e.Attach(new Clerk());
            e.Attach(new Director());
            e.Attach(new President());

            // Employees are 'visited'
            e.Accept(new IncomeVisitor());
            e.Accept(new VacationVisitor());

            ConsoleWriteWithColor("*********** Visitor Pattern ends ***********");

            #endregion

            #endregion

            #region Structural Patterns

            #region Adapter

            ConsoleWriteWithColor("*********** Adapter Pattern starts ***********", ConsoleColor.Yellow);

            ITarget adapter = new VendorAdapter();
            foreach (string product in adapter.GetProducts())
            {
                ConsoleWriteWithColor(product, ConsoleColor.Yellow);
            }

            ConsoleWriteWithColor("*********** Adapter Pattern ends ***********", ConsoleColor.Yellow);

            #endregion

            #region Bridge

            ConsoleWriteWithColor("*********** Bridge Pattern starts ***********", ConsoleColor.Yellow);

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

            ConsoleWriteWithColor("*********** Bridge Pattern ends ***********", ConsoleColor.Yellow);

            #endregion

            #region Composite

            ConsoleWriteWithColor("*********** Composite Pattern starts ***********", ConsoleColor.Yellow);

            Worker workerA = new Worker("Worker A", 5);
            Supervisor supervisorB = new Supervisor("Supervisor B", 6);
            Supervisor supervisorC = new Supervisor("Supervisor C", 7);
            Supervisor supervisorD = new Supervisor("Supervisor D", 9);
            Worker workerE = new Worker("Worker E", 8);

            //set up the relationships
            //C ==> B, D
            //B ==> A
            //D ==> E
            supervisorB.AddSubordinate(workerA);
            supervisorC.AddSubordinate(supervisorB);
            supervisorC.AddSubordinate(supervisorD);
            supervisorD.AddSubordinate(workerE);

            //supervisorC shows his happiness and asks everyone else to do the same
            if (supervisorC is IWorker)
                (supervisorC as IWorker).ShowHappiness();

            ConsoleWriteWithColor("*********** Composite Pattern ends ***********", ConsoleColor.Yellow);

            #endregion

            #region Decorator

            ConsoleWriteWithColor("*********** Decorator Pattern starts ***********", ConsoleColor.Yellow);

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

            ConsoleWriteWithColor("*********** Decorator Pattern ends ***********");

            #endregion

            #region Facade

            ConsoleWriteWithColor("*********** Facade Pattern starts ***********", ConsoleColor.Yellow);
            //Creating the Order/Product details
            OrderDetails orderDetails = new OrderDetails("C# Design Pattern Book",
                                                         "Simplified book on design patterns in C#",
                                                         500,
                                                         10,
                                                         "Street No 1",
                                                         "Educational Area",
                                                         1212,
                                                         "4156213754"
                                                         );

            // Using Facade
            var facade = new OnlineShoppingFacade();
            facade.FinalizeOrder(orderDetails);

            ConsoleWriteWithColor("*********** Facade Pattern ends ***********", ConsoleColor.Yellow);

            #endregion

            #region Flyweight

            ConsoleWriteWithColor("*********** Flyweight Pattern starts ***********", ConsoleColor.Yellow);

            // Build a document with text
            string document = "AAZZBBZB";
            char[] chars = document.ToCharArray();

            CharacterFactory factory = new CharacterFactory();

            // extrinsic state
            int pointSize = 10;

            // For each character use a flyweight object
            foreach (char c in chars)
            {
                pointSize++;
                Character character = factory.GetCharacter(c);
                character.Display(pointSize);
            }

            ConsoleWriteWithColor("*********** Flyweight Pattern ends ***********", ConsoleColor.Yellow);

            #endregion

            #region Proxy

            ConsoleWriteWithColor("*********** Proxy Pattern starts ***********", ConsoleColor.Yellow);

            // Create math proxy
            var proxy = new MathProxy();

            // Do the math
            Console.WriteLine("4 + 2 = " + proxy.Add(4, 2));
            Console.WriteLine("4 - 2 = " + proxy.Sub(4, 2));
            Console.WriteLine("4 * 2 = " + proxy.Mul(4, 2));
            Console.WriteLine("4 / 2 = " + proxy.Div(4, 2));

            ConsoleWriteWithColor("*********** Proxy Pattern ends ***********", ConsoleColor.Yellow);

            #endregion

            #endregion

            #region Service Locator Pattern

            //http://www.c-sharpcorner.com/UploadFile/dacca2/service-locator-design-pattern/
            ConsoleWriteWithColor("*********** Service Locator Pattern starts ***********");

            ServiceLocator serviceLocator = new ServiceLocator();
            IServiceA serviceA = serviceLocator.GetService<IServiceA>();
            serviceA.Execute();

            IServiceB serviceB = serviceLocator.GetService<IServiceB>();
            serviceB.Execute();

            ConsoleWriteWithColor("*********** Service Locator Pattern ends ***********");
            #endregion

            Console.ReadKey();
        }

        private static void PrintProductDetails(BakeryComponent product)
        {
            ConsoleWriteWithColor(""); // some whitespace for readability
            ConsoleWriteWithColor(string.Format("Item: {0}, Price: {1}", product.GetName(), product.GetPrice()), ConsoleColor.Yellow);
        }

        private static void ConsoleWriteWithColor(string text, ConsoleColor textColor = ConsoleColor.White)
        {
            lock (_ConsoleLock)
            {
                Console.ForegroundColor = textColor;
                Console.WriteLine(text);
                Console.ResetColor();
            }
        }
    }
}
