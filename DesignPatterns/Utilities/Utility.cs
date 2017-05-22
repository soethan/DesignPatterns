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
        public static bool ObjectValuesEqual(this object obj, object anotherObj)
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
                if (!objValue.ObjectValuesEqual(anotherValue)) result = false;
            }
            return result;
        }
    }
}
