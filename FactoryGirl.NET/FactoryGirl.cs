using System;
using System.Collections.Generic;
using System.Linq;

namespace FactoryGirl.NET
{
    public static class FactoryGirl {
        private static readonly IDictionary<Type, Func<object>> builders = new Dictionary<Type, Func<object>>();
 
        public static void Define<T>(Func<T> builder) {
            if(builders.ContainsKey(typeof(T))) throw new DuplicateFactoryException(typeof(T).Name + " is already registered. You can only register one factory per type.");
            builders.Add(typeof(T), () => builder());
        }

        public static T Build<T>()
        {
            return Build<T>(x => { });
        }

        public static T Build<T>(Action<T> overrides) {
            var result = (T) builders[typeof(T)]();
            overrides(result);
            return result;
        }

        public static T Build<T>(List<Action<T>> overrides)
        {
            var result = (T)builders[typeof(T)]();
            foreach (var @override in overrides)
            {
                @override(result);
            }

            return result;
        }

        public static IEnumerable<Type> DefinedFactories {
            get { return builders.Select(x => x.Key); }
        }

        public static void ClearFactoryDefinitions() {
            builders.Clear();
        }
    }
}
