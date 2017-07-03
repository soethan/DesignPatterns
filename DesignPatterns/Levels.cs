using System.Collections.Generic;

namespace DesignPatterns
{
    public class Level1
    {
        public string Name { get; set; }
        public Level2 Level2 { get; set; }
    }
    public class Level2
    {
        public string Name { get; set; }
        public List<string> strArray { get; set; }
        public List<Person> Persons { get; set; }
        public Level3 Level3 { get; set; }
    }
    public class Level3
    {
        public string Name { get; set; }
    }
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
