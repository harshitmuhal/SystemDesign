using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Behavioral
{
    public class ChainOfResponsibility : IDesignPattern
    {
        public void ExecuteDesignPattern()
        {
            IHandler handler = Handler1.GetInstance();
            Request request = new Request();
            while (handler != null)
            {
                handler.Handle(request);
                handler = handler.Next();
            }
        }
    }

    public interface IHandler
    {
        IHandler Next();
        string Handle(Request request);
    }

    public class Request
    {
    }

    public class Handler1 : IHandler
    {
        private static IHandler _next;
        private static IHandler _instance;
        private static object InstanceLock = new object();
        private static object InstanceLockForNext = new object();
        private Handler1() { }
        public static IHandler GetInstance()
        {
            if (_instance == null)
            {
                lock (InstanceLock)
                {
                    if (_instance == null)
                    {
                        _instance = new Handler1();
                    }
                }
            }
            return _instance;
        }
        public IHandler Next()
        {
            _next = Handler2.GetInstance();
            return _next;
        }

        public string Handle(Request request)
        {
            Console.WriteLine("Handler1 is executing");
            return "Success";
        }
    }

    public class Handler2 : IHandler
    {
        private static IHandler _next;
        private static IHandler _instance;
        private static object InstanceLock = new object();
        private static object InstanceLockForNext = new object();
        private Handler2() { }
        public static IHandler GetInstance()
        {
            if (_instance == null)
            {
                lock (InstanceLock)
                {
                    if (_instance == null)
                    {
                        _instance = new Handler2();
                    }
                }
            }
            return _instance;
        }
        public IHandler Next()
        {
            _next = Handler3.GetInstance();
            return _next;
        }

        public string Handle(Request request)
        {
            Console.WriteLine("Handler2 is executing");
            return "Success";
        }
    }

    public class Handler3 : IHandler
    {
        private static IHandler _next;
        private static IHandler _instance;
        private static object InstanceLock = new object();
        private Handler3() { }
        public static IHandler GetInstance()
        {
            if (_instance == null)
            {
                lock (InstanceLock)
                {
                    if (_instance == null)
                    {
                        _instance = new Handler3();
                    }
                }
            }
            return _instance;
        }
        public IHandler Next()
        {
            return null;
        }

        public string Handle(Request request)
        {
            Console.WriteLine("Handler3 is executing");
            return "Success";
        }
    }
}
