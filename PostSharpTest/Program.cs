using System.Drawing;
using System.Linq;
using Console = Colorful.Console;

namespace PostSharpTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var numbers = Enumerable.Range(1, 10);

            foreach (var number in numbers)
            {
                Console.WriteLine($"{number} squared is: {MathCalculations.Square(number)}", Color.GreenYellow);
                Console.WriteLine($"{number} cubed is: {MathCalculations.Cube(number)}", Color.GreenYellow);
            }

            var tmp = Console.ReadLine();
        }
    }
}