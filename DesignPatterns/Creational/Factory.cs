using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Creational
{
    public class Factory: IDesignPattern
    {
        public void ExecuteDesignPattern()
        {
            var animalFactory = new AnimalFactory();
            IAnimal dog = animalFactory.GetAnimal("Dog");
            IAnimal cat = animalFactory.GetAnimal("Cat");
            Console.WriteLine(dog.Speak());
            Console.WriteLine(cat.Speak());
        }
    }

    public interface IAnimal
    {
        string Speak();
    }

    public class Dog : IAnimal
    {
        public string Speak()
        {
            return "Woof";
        }
    }

    public class Cat : IAnimal
    {
        public string Speak()
        {
            return "Meow";
        }
    }

    public class AnimalFactory
    {
        public IAnimal GetAnimal(string animalType)
        {
            if (animalType == null)
            {
                return null;
            }
            if (animalType.Equals("Dog", StringComparison.OrdinalIgnoreCase))
            {
                return new Dog();
            }
            if (animalType.Equals("Cat", StringComparison.OrdinalIgnoreCase))
            {
                return new Cat();
            }
            return null;
        }
    }
}
