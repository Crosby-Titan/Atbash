using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Atbash.Reflection
{
    public static class ClassLoader
    {
        public static IEnumerable<Type> LoadClasses<T>()
        {
            var currentAssembly = Assembly.GetExecutingAssembly().GetTypes();

            var gotClasses = new List<Type>();

            foreach (var type in currentAssembly)
            {
                if (type.GetInterfaces().Contains(typeof(T)))
                {
                    gotClasses.Add(type);
                }
            }

            return gotClasses;
        }
    }
}
