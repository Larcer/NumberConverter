using System;
using System.Reflection;

namespace Nameless.NumberConverter.Tools
{
    // Used to create singleton classes through inheritance
    public abstract class SingletonBase<T> where T : class
    {
        private static readonly Lazy<T> SInstance = new Lazy<T>(ConstructInstance);

        public static T Instance => SInstance.Value;

        // Constructs the instance T using reflection.
        // The instance should have private empty constructor
        private static T ConstructInstance()
        {
            var type = typeof(T);
            var constructorInfo = type.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic,
                null, Type.EmptyTypes, null);
            
            return constructorInfo.Invoke(null) as T;
        }
    }
}
