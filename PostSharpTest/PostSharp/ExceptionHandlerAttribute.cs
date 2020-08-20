using PostSharp.Aspects;
using PostSharpTest.Extension_Methods;
using System;

namespace PostSharpTest.PostSharp
{
    [Serializable]
    public class ExceptionHandlerAttribute : OnMethodBoundaryAspect
    {
        /// <summary>
        /// Invoked when exception encountered in method
        /// </summary>
        /// <param name="args"></param>
        public override void OnException(MethodExecutionArgs args)
        {
            // Convert exception to log string and log it ...
            args.Exception.ToLogString().LogMsg(LogLevels.Error);

            // Swallow the exception ...
            args.FlowBehavior = FlowBehavior.Continue; 
        }
    }
}
