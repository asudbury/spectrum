namespace Spectrum.Core.Utilities
{
    using System;

    public abstract class Singleton<T> where T : new()
    {
        private static readonly T SingletonInstance = new T();

        protected Singleton()
        { 
            if (SingletonInstance != null) 
            { 
                throw new Exception("Only 1 instance of the class can be created."); 
            } 
        } 
 
        public static T Instance => SingletonInstance;
    }
}
