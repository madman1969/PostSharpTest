using PostSharpTest.PostSharp;
using System;

namespace PostSharpTest
{
    [ExceptionHandler(AspectPriority = 1), Boundary(AspectPriority = 2)]
    public class MathCalculations
    {
        #region Fields and properties

        private static readonly Random Rnd = new Random();

        #endregion

        #region Public Methods

        /// <summary>
        /// Calculate the square of the supplied value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int Square(int value)
        {
            var tmp = Rnd.Next(11);

            if (tmp == 5)
            {
                throw new ApplicationException("Exception case");
            }
            else
            {
                return value * value;
            }
        }

        /// <summary>
        /// Calculate the cube of the supplied value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int Cube(int value)
        {
            var tmp = Rnd.Next(6);

            if (tmp == 5)
            {
                throw new ApplicationException("Exception case");
            }
            else
            {
                return value * value * value;
            }

        }

        #endregion
    }
}
