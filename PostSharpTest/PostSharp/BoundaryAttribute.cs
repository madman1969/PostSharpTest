using PostSharp.Aspects;
using PostSharpTest.Extension_Methods;
using System;
using System.Collections.Generic;

namespace PostSharpTest.PostSharp
{
    [Serializable]
    public class BoundaryAttribute : OnMethodBoundaryAspect
    {
        #region Public Methods

        /// <summary>
        /// Invoke on method entry
        /// </summary>
        /// <param name="args"></param>
        public override void OnEntry(MethodExecutionArgs args)
        {
            $"Entering [{args.Method.DeclaringType?.FullName}.{args.Method.Name}]: [{ParseArguments(args.Arguments)}]".LogMsg();
        }

        /// <summary>
        /// Invoked on method exit
        /// </summary>
        /// <param name="args"></param>
        public override void OnExit(MethodExecutionArgs args)
        {
            $"Exiting [{args.Method.DeclaringType?.FullName}.{args.Method.Name}]".LogMsg();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Parses method arguments into comma separated string
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private string ParseArguments(Arguments args)
        {
            var result = new List<string>();

            foreach (var argument in args)
            {
                result.Add(argument.ToString());
            }

            return string.Join(", ", result);
        }

        #endregion
    }
}
