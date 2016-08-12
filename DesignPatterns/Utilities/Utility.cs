using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Utilities
{
    public static class Utility
    {
        public static bool ObjectValuesCompare(this object obj, object anotherObj)
        {
            if (ReferenceEquals(obj, anotherObj)) return true;
            if (obj == null || anotherObj == null) return false;
            if (obj.GetType() != anotherObj.GetType()) return false;

            //Primitive properties: int, double, DateTime, etc
            if (!obj.GetType().IsClass) return obj.Equals(anotherObj);
            //array property
            if (obj is IEnumerable && obj.GetType().IsGenericType)
            {
                return (obj as IEnumerable<string>).SequenceEqual(anotherObj as IEnumerable<string>);
            }

            var result = true;
            foreach (var property in obj.GetType().GetProperties())
            {
                var objValue = property.GetValue(obj);
                var anotherValue = property.GetValue(anotherObj);
                //Recursion
                if (!objValue.ObjectValuesCompare(anotherValue)) result = false;
            }
            return result;
        }
    }

    public class Level1
    {
        public string Name { get; set; }
        public Level2 Level2 { get; set; }
    }
    public class Level2
    {
        public string Name { get; set; }
        public List<string> List { get; set; }
        public Level3 Level3 { get; set; }
    }
    public class Level3
    {
        public string Name { get; set; }
    }
}
