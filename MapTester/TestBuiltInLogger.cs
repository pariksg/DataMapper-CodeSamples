// ----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ----------------------------------------------------------------------------

namespace Microsoft.Azure.Tests.BuiltIn.Xslt.Netfx.Utils
{
    using System;
    using System.Runtime.CompilerServices;
    using Microsoft.Azure.Workflows.BuiltIn.Abstractions.Xslt.Logging;

    /// <summary>
    /// Console logger for built-in tests
    /// </summary>
    /// <remarks>Log events can simply be read from the test console output.</remarks>
    public class TestBuiltInLogger : IBuiltInLogger
    {
        /// <summary>
        /// Gets the calling method name.
        /// </summary>
        /// <param name="caller">The caller (do not provide this input, let the compiler do it).</param>
        private static string GetCallerName([CallerMemberName] string caller = null) => caller;

        void IBuiltInLogger.Debug(string message, Exception exception)
            => Console.WriteLine($"{TestBuiltInLogger.GetCallerName()} - {message} - {exception}");

        void IBuiltInLogger.Error(string message, Exception exception)
            => Console.WriteLine($"{TestBuiltInLogger.GetCallerName()} - {message} - {exception}");

        void IBuiltInLogger.Critical(string message, Exception exception)
            => Console.WriteLine($"{TestBuiltInLogger.GetCallerName()} - {message} - {exception}");

        void IBuiltInLogger.Warning(string message, Exception exception)
            => Console.WriteLine($"{TestBuiltInLogger.GetCallerName()} - {message} - {exception}");
    }
}
