﻿using System;
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

        private static IList<Type> supportedSequencedTypes = new List<Type> { typeof(int), typeof(uint), typeof(string) };

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

        public static T Sequence<T>(string seed = null)
        {
            if (!supportedSequencedTypes.Contains(typeof (T)))
            {
                throw new UnsequenceableTypeException(
                    string.Format("The type {0} cannot be sequenced. The types that can be sequenced are {1}.", 
                    typeof(T).Name, string.Join(",", supportedSequencedTypes)));
            }

            if (seed == null)
            {
                return (T)Convert.ChangeType(Build<int>(), typeof(T));
            }
            else
            {
                if (typeof (T) != typeof (string))
                {
                    throw new UnsequenceableTypeException("You cannot seed a sequence for any type but a string.");
                }
                return (T)Convert.ChangeType(seed + Build<int>(), typeof(T));
            }
            
        }

        public static IEnumerable<Type> DefinedFactories {
            get { return builders.Select(x => x.Key); }
        }

        public static void ClearFactoryDefinitions() {
            builders.Clear();
            m_counter = 0;
            foreach (var preRegisteredFactory in preRegisteredFactories)
            {
                builders.Add(preRegisteredFactory);
            }
        }
    }
}
