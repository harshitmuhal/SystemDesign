using DesignPatterns;
using DesignPatterns.Behavioral;
using DesignPatterns.Creational;

class DesignPatternsInCSharp
{
    static void Main()
    {
        Console.WriteLine("Hello World!");

        IDesignPattern designPattern;

        //designPattern = new AbstractFactory();
        designPattern = new Prototype();
        designPattern = new ChainOfResponsibility();
        designPattern = new Builder();

        designPattern.ExecuteDesignPattern();
    }
}