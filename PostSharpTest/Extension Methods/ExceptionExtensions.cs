using System;
using System.Text;

namespace PostSharpTest.Extension_Methods
{
    public static class ExceptionExtensions
    {
        #region Exception.ToLogString

        /// <summary>
        /// <para>Creates a log-string from the Exception.</para>
        /// <para>The result includes the stacktrace, inner exception etc, separated by <seealso cref="Environment.NewLine"/>.</para>
        /// </summary>
        /// <param name="ex">The exception to create the string from.</param>
        /// <param name="additionalMessage">Additional message to place at the top of the string, maybe be empty or null.</param>
        /// <returns></returns>
        public static string ToLogString(this Exception ex, string additionalMessage = "")
        {
            var msg = new StringBuilder();

            if (!string.IsNullOrEmpty(additionalMessage))
            {
                msg.Append(additionalMessage);
                msg.Append(Environment.NewLine);
            }

            if (ex != null)
            {
                var orgEx = ex;

                msg.Append("-----------------------------------------------------------------------------");
                msg.Append(Environment.NewLine);

                var date = $"Date: {DateTime.UtcNow:G}";
                msg.Append(date);
                msg.Append(Environment.NewLine);

                msg.Append("Exception:");
                msg.Append(Environment.NewLine);

                while (orgEx != null)
                {
                    msg.Append(orgEx.Message);
                    msg.Append(Environment.NewLine);
                    orgEx = orgEx.InnerException;
                }

                foreach (var i in ex.Data)
                {
                    msg.Append("Data: ");
                    msg.Append(i);
                    msg.Append(Environment.NewLine);
                }

                if (ex.StackTrace != null)
                {
                    msg.Append("StackTrace: ");
                    msg.Append(Environment.NewLine);
                    msg.Append(ex.StackTrace);
                    msg.Append(Environment.NewLine);
                }

                if (ex.Source != null)
                {
                    msg.Append("Source: ");
                    msg.Append(Environment.NewLine);
                    msg.Append(ex.Source);
                    msg.Append(Environment.NewLine);
                }

                if (ex.TargetSite != null)
                {
                    msg.Append("TargetSite: ");
                    msg.Append(Environment.NewLine);
                    msg.Append(ex.TargetSite);
                    msg.Append(Environment.NewLine);
                }

                ex.GetBaseException();

                msg.Append("BaseException: ");
                msg.Append(Environment.NewLine);
                msg.Append(ex.GetBaseException());
            }

            return msg.ToString();
        }

        #endregion Exception.ToLogString
    }
}
