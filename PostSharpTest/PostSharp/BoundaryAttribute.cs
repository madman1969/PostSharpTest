using System;
using System.Collections.Generic;
using PostSharp.Aspects;
using PostSharpTest.Extension_Methods;

namespace PostSharpTest
{
    [Serializable]
    class BoundaryAttribute : OnMethodBoundaryAspect
    {
        public override void OnEntry(MethodExecutionArgs args)
        {
            $"Entering [{args.Method.DeclaringType.FullName}.{args.Method.Name}]: [{ParseArguments(args.Arguments)}]".LogMsg();
        }

        public override void OnExit(MethodExecutionArgs args)
        {
            $"Exiting [{args.Method.DeclaringType.FullName}.{args.Method.Name}]".LogMsg();
        }

        private string ParseArguments(Arguments args)
        {
            var result = new List<string>();

            foreach (var argument in args)
            {
                result.Add(argument.ToString());
            }

            return string.Join(", ", result);
        }
    }
}
