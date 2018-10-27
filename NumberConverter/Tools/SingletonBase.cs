using System;
using System.Diagnostics;
using System.Reflection;

namespace Nameless.NumberConverter.Tools
{
    // Used to create singleton classes with inheritance
    public abstract class SingletonBase<T> where T : class
    {
        private static readonly Lazy<T> sInstance = new Lazy<T>(ConstructInstance);

        public static T Instance => sInstance.Value;

        // Constructs the instance T using reflection.
        // The instance should have private empty constructor
        private static T ConstructInstance()
        {
            Type type = typeof(T);
            ConstructorInfo ci = type.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic,
                null, Type.EmptyTypes, null);

            Debug.Assert(ci != null);

            return ci.Invoke(null) as T;
        }
    }
}
