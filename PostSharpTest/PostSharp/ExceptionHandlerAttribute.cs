using System;
using PostSharp.Aspects;
using PostSharpTest.Extension_Methods;

namespace PostSharpTest.PostSharp
{
    [Serializable]
    class ExceptionHandlerAttribute : OnMethodBoundaryAspect
    {
        public override void OnException(MethodExecutionArgs args)
        {
            args.Exception.ToLogString().LogMsg(LogLevels.Error);
            args.FlowBehavior = FlowBehavior.Continue; // swallow
        }
    }
}
