using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace FactoryGirl.NET
{
    public static class FactoryGirl {
        private static int m_counter;

        private static readonly IDictionary<Type, Func<object>> preRegisteredFactories = new Dictionary<Type, Func<object>>
        {
            {typeof(int), () => Interlocked.Increment(ref m_counter)},
            {typeof(uint), () => Interlocked.Increment(ref m_counter)}
        };

        private static readonly IDictionary<Type, Func<object>> builders = new Dictionary<Type, Func<object>>(preRegisteredFactories);

        public static void Define<T>(Func<T> builder) {
            if(builders.ContainsKey(typeof(T))) throw new DuplicateFactoryException(typeof(T).Name + " is already registered. You can only register one factory per type.");
            builders.Add(typeof(T), () => builder());
        }

        public static T Build<T>() {
            return Build<T>(x => { });
        }

        public static T Build<T>(Action<T> overrides) {
            var result = (T) builders[typeof(T)]();
            overrides(result);
            return result;
        }

        public static string Sequence(string seed = null)
        {
            return seed == null ? Build<int>().ToString() : seed + Build<int>();
        }

        public static IEnumerable<Type> DefinedFactories {
            get { return builders.Select(x => x.Key); }
        }

        public static void ClearFactoryDefinitions() {
            builders.Clear();

            foreach (var preRegisteredFactory in preRegisteredFactories)
            {
                builders.Add(preRegisteredFactory);
            }
        }
    }
}
