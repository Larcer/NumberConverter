using System;
using System.Diagnostics;
using System.Reflection;

namespace Nameless.NumberConverter.Tools
{
    public abstract class SingletonBase<T> where T : class
    {
        private static readonly Lazy<T> sInstance = new Lazy<T>(ConstructInstance);

        public static T Instance => sInstance.Value;

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
