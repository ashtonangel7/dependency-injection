using System.Configuration;

namespace Deps
{
    public interface IWheel
    {
        bool isAlloy();
    }

    public class MagWheel : IWheel
    {
        public bool isAlloy()
        {
            return true;
        }
    }

    public class SteelWheel : IWheel
    {
        public bool isAlloy()
        {
            return false;
        }
    }

    public class Car
    {
        public IWheel leftWheel { get; set; }
        public bool HasAlloyWheels()
        {
            return leftWheel.isAlloy();
        }
    }


    public class Program
    {
        public static void Main()
        {
            string? fqn = ConfigurationManager.AppSettings.Get("Type");

            Console.WriteLine($"Config currently set to {fqn}");

            if (string.IsNullOrWhiteSpace(fqn))
            {
                Console.WriteLine("Please provide an App setting called Type for the Wheel Type");
                return;
            }

            Type? type = Type.GetType(fqn);
            if (type == null)
            {
                Console.WriteLine($"Could not find a type for the app setting {fqn} provided");
                return;
            }

            object? instance = Activator.CreateInstance(type);
            if (instance == null)
            {
                Console.WriteLine($"Could not create an instance of the {type.Name} Class");
                return;
            }

            Car car = new Car
            {
                leftWheel = (IWheel)instance
            };

            Console.WriteLine($"My car has an alloy left wheel : {car.HasAlloyWheels()}");
        }
    }
}
