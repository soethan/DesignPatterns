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
        /// <summary>
        /// https://stackoverflow.com/questions/31835823/parameter-count-mismatch-in-property-getvalue
        /// https://msdn.microsoft.com/en-us/library/system.object.gethashcode(v=vs.100).aspx
        /// https://en.wikipedia.org/wiki/Exclusive_or  A XOR B shows that it outputs true whenever the inputs differ
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="anotherObj"></param>
        /// <returns></returns>
        public static bool ObjectValuesEqual<T>(this T obj, T anotherObj) where T: class
        {
            if (ReferenceEquals(obj, anotherObj)) return true;
            if (obj == null || anotherObj == null) return false;    
            if (obj.GetType() != anotherObj.GetType()) return false;

            //Primitive properties: int, double, DateTime, etc
            if (!obj.GetType().IsClass) return obj.Equals(anotherObj);
            //array property
            if (obj is IEnumerable && obj.GetType().IsGenericType)
            {
                return (obj as IEnumerable<T>).SequenceEqual(anotherObj as IEnumerable<T>, new ObjectComparer<T>());
            }

            var result = true;
            foreach (var property in obj.GetType().GetProperties().Where(p => p.GetIndexParameters().Length == 0))
            {
                var objValue = property.GetValue(obj);
                var anotherValue = property.GetValue(anotherObj);
                //Recursion
                if (!objValue.ObjectValuesEqual(anotherValue)) result = false;
            }
            return result;
        }
    }

    public class ObjectComparer<T> : IEqualityComparer<T> where T: class
    {
        public bool Equals(T obj, T anotherObj)
        {
            return obj.ObjectValuesEqual(anotherObj);
        }
        
        public int GetHashCode(T obj)
        {
            return 1;
        }
    }
}
