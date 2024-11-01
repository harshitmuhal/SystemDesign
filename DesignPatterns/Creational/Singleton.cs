namespace DesignPatterns.Creational
{
    internal class Singleton : IDesignPattern
    {
        public void ExecuteDesignPattern()
        {
            SingletonClass singleton1 = SingletonClass.GetInstance();
        }
    }

    internal class SingletonClass
    {
        private static SingletonClass _instance;
        private static object InstanceLock = new object();
        private SingletonClass() { }  // Private constructor : so that no one is allowed to directly instantiate the class
        public static SingletonClass GetInstance()
        {
            if (_instance == null)
            {
                lock (InstanceLock)
                {
                    if (_instance == null)
                    {
                        _instance = new SingletonClass();
                    }
                }
            }
            return _instance;
        }
    }
}
