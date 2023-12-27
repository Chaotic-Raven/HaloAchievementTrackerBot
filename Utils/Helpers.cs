using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using System.Text;

namespace AchievementTracker.Utils
{
    /// <summary>
    /// This class contains a collection of helper functions.
    /// </summary>
    public class Helpers
    {
        /// <summary>
        /// Returns an array of class types inside a specific namespace.
        /// </summary>
        /// <param name="assembly">The assembly to look in.</param>
        /// <param name="ns">The full namespace path containing the classes.</param>
        /// <returns>An array of class types within the namespace.</returns>
        public static Type[] GetTypesInNamespace(Assembly assembly, string ns)
        {
            return assembly.GetTypes().Where(t => String.Equals(t.Namespace, ns, StringComparison.Ordinal)).ToArray();
        }
    }
}
