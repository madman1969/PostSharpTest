using System;
using PostSharpTest.PostSharp;

namespace PostSharpTest
{
    [Boundary, ExceptionHandler]
    public class MathCalculations
    {
        private static readonly Random rnd = new Random();

        public static int Square(int value)
        {
            var tmp = rnd.Next(11);

            if (tmp == 5)
            {
                throw new ApplicationException("Exception case");
            }
            else
            {
                return value * value;    
            }
        }

        public static int Cube(int value)
        {
            var tmp = rnd.Next(6);

            if (tmp == 5)
            {
                throw new ApplicationException("Exception case");
            }
            else
            {
                return value * value * value;
            }
            
        }
    }
}
