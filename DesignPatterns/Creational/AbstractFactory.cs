using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Creational
{
    public class AbstractFactory : IDesignPattern
    {
        public void ExecuteDesignPattern()
        {
            // If we put this function in main application code it will violate OCP and SRP, hence this
            // should be in a separate function only or you can create a separate Super Factory class that will contain this logic.

            string VehicleType = "Regular"; // "Regular" or "Sports
            switch (VehicleType)
            {
                case "Regular":
                    IVehicleFactory regularVehicleFactory = new RegularVehicleFactory();
                    AbstractFactoryClient(regularVehicleFactory);
                    break;
                case "Sports":
                    IVehicleFactory sportsVehicleFactory = new SportsVehicleFactory();
                    AbstractFactoryClient(sportsVehicleFactory);
                    break;
                default:
                    break;
            }
        }

        private void AbstractFactoryClient(IVehicleFactory vehicleFactory)
        {
            /* We have a family of products but this code is not dependent on concrete classes.
             * If new types of vehicles are added, we can just add new classes and new factories.
             * This encourages the OCP, the below code is open for extension and closed for modification.
             * Even if new Car type or new vehicle type is added the below code is not going to change
            */
            IVehicle bike = vehicleFactory.GetBike("Honda", "Brown");
            Console.WriteLine(bike.GetDetails());

            IVehicle car = vehicleFactory.GetCar("Tesla", "Black");
            Console.WriteLine(car.GetDetails());
        }
    }

    public interface IVehicleFactory
    {
        IVehicle GetBike(string ModelName, string Color);
        IVehicle GetCar(string ModelName, string Color);
    }

    public class RegularVehicleFactory : IVehicleFactory
    {
        public IVehicle GetBike(string ModelName, string Color)
        {
            return new RegularBike(ModelName, Color);
        }
        public IVehicle GetCar(string ModelName, string Color)
        {
            return new RegularCar(ModelName, Color);
        }
    }

    public class SportsVehicleFactory : IVehicleFactory
    {
        public IVehicle GetBike(string ModelName, string Color)
        {
            return new SportsBike(ModelName, Color);
        }
        public IVehicle GetCar(string ModelName, string Color)
        {
            return new SportsCar(ModelName, Color);
        }
    }

    public class RegularBike : IVehicle
    {
        public string ModelName { get; set; }
        public string Color { get; set; }

        public RegularBike(string modelName, string color)
        {
            ModelName = modelName;
            Color = color;
        }

        public string GetDetails()
        {
            return $"RegularBike - Model: {ModelName}, Color: {Color}";
        }
    }

    public class SportsBike : IVehicle
    {
        public string ModelName { get; set; }
        public string Color { get; set; }
        public SportsBike(string modelName, string color)
        {
            ModelName = modelName;
            Color = color;
        }
        public string GetDetails()
        {
            return $"SportsBike - Model: {ModelName}, Color: {Color}";
        }
    }

    public class RegularCar : IVehicle
    {
        public string ModelName { get; set; }
        public string Color { get; set; }
        public RegularCar(string modelName, string color)
        {
            ModelName = modelName;
            Color = color;
        }
        public string GetDetails()
        {
            return $"RegularCar - Model: {ModelName}, Color: {Color}";
        }
    }

    public class SportsCar : IVehicle
    {
        public string ModelName { get; set; }
        public string Color { get; set; }
        public SportsCar(string modelName, string color)
        {
            ModelName = modelName;
            Color = color;
        }
        public string GetDetails()
        {
            return $"SportsCar - Model: {ModelName}, Color: {Color}";
        }
    }

    public interface IVehicle
    {
        string GetDetails();
    }
}
