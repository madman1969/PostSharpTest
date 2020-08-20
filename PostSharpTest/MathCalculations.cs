using PostSharpTest.PostSharp;
using System;

namespace PostSharpTest
{
    [Boundary, ExceptionHandler]
    public class MathCalculations
    {
        #region Fields and properties

        private static readonly Random rnd = new Random();

        #endregion

        #region Public Methods

        /// <summary>
        /// Calculate the square of the supplied value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Calculate the cube of the supplied value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
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

        #endregion
    }
}
