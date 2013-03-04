using System;
using System.Collections.Generic;
using System.Linq;

namespace FactoryGirl.NET
{
    public static class FactoryGirl
    {

        private static readonly IDictionary<String, Func<object>> namedBuilders = new Dictionary<string, Func<object>>();

        public static void Define<T>(string name, Func<T> builder)
        {
            string keyname = GetName<T>(name);
            Define<T>(builder, keyname);
        }

        public static void Define<T>(Func<T> builder)
        {
            var key = GetKeyForType<T>();
            Define<T>(builder, key);
        }

        public static T Build<T>()
        {
            return BuildObject<T>(x => { });
        }

        public static T Build<T>(string name)
        {
            return BuildObject<T>( x => { }, name);
        }

        public static T Build<T>(Action<T> overrides)
        {
            return BuildObject(overrides);
        }

        public static T Build<T>(string name, Action<T> overrides)
        {
            return BuildObject(overrides, name);
        }

        public static IEnumerable<string> DefinedFactories
        {
            get { return namedBuilders.Select(x => x.Key); }
        }

        public static string GetName<T>(string name = "")
        {
            if (string.IsNullOrEmpty(name))
            {
                return GetKeyForType<T>();
            }

            return GetKeyForType<T>() + "_" + name;
        }

        public static void ClearFactoryDefinitions()
        {
            namedBuilders.Clear();
        }

        private static T BuildObject<T>(Action<T> overrides, string key = "")
        {
            var keyName = GetName<T>(key);
            var result = (T)namedBuilders[keyName]();
            overrides(result);
            return result;
        }

        private static void Define<T>(Func<T> builder, string key = "")
        {
            if (namedBuilders.ContainsKey(key)) throw new DuplicateFactoryException(key + " is already registered. You can only register one factory per type/name.");
            namedBuilders.Add(key, () => builder());
        }

        private static string GetKeyForType<T>()
        {
            var args = typeof(T).GetGenericArguments();
            var key = typeof(T).Name;
            args.ToList().ForEach(x => key += key + "_" + x);
            return key;
        }
    }
}
