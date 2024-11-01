using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Creational
{
    public class Prototype : IDesignPattern
    {
        public void ExecuteDesignPattern()
        {
            Eagle e1 = new Eagle("AmericanEagle", "Broad", "Eagle");
            var e2 = e1.GetClone();
            e2.Species = "GoldenEagle";
            Console.WriteLine(e1.ToString());
            Console.WriteLine(e2.ToString());
        }
    }

    class Bird
    {
        public string Name { get; set; }
        public Bird()
        {
            Name = "-";
        }
        public Bird(string Name)
        {
            this.Name = Name;
        }
        public Bird GetClone(Bird bird)
        {
            bird.Name = Name;
            return bird;
        }
    }
    class FlyingBird : Bird
    {
        public string WingType { get; set; }
        public FlyingBird()
        {
            WingType = "-";
        }
        public FlyingBird(string WingType, string Name) : base(Name)
        {
            this.WingType = WingType;
        }
        public FlyingBird GetClone(FlyingBird bird)
        {
            bird.WingType = WingType;
            base.GetClone(bird);
            return bird;
        }
    }
    class Eagle : FlyingBird
    {
        public string Species { get; set; }
        public Eagle()
        {
            Species = "-";
        }
        public Eagle(string Species, string WingType, string Name) : base(WingType, Name)
        {
            this.Species = Species;
        }
        public Eagle GetClone()
        {
            Eagle eagle = new Eagle();
            eagle.Species = Species;
            GetClone(eagle);
            return eagle;
        }
        public override string ToString()
        {
            return "Prototype Design Pattern: " + Name + ", " + WingType + ", " + Species;
        }
    }
}
