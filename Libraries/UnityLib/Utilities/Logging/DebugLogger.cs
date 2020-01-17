using System;
using UnityEngine;

namespace UnityPureMVC.Core.Libraries.UnityLib.Utilities.Logging
{
    /// <summary>
    /// Logs messages and possibly throws errors in 'logError()'.
    /// Note: the current implementation logs messages by using 'trace()'.
    /// Later this can be changed to a different implementation, while maintaining the same interface.
    /// </summary>
    internal class DebugLogger
    {
        /// <summary>
        /// Tell whether to log messages. Default is <code>true</code>.
        /// </summary>
        internal static bool LogTexts { get; set; }

        /// <summary>
        /// Tell whether to throw exceptions while calling <code>LogError</code>. Default is <code>true</code>.
        /// </summary>
        internal static bool ThrowErrors { get; set; }

        /// <summary>
        /// Tell whether to throw exceptions while calling <code>LogWarning</code>. Default is <code>true</code>.
        /// </summary>
        internal static bool ThrowWarnings { get; set; }

        internal static string LastMessage { get; private set; }

        /// <summary>
        /// Callback used to log messages that are neither warnings nor errors. System.Console.WriteLine by default.
        /// </summary>
        internal static Action<string> LogCallback;

        /// <summary>
        /// Callback used to log warnings. System.Console.WriteLine by default.
        /// </summary>
        internal static Action<string> LogWarningCallback;

        /// <summary>
        /// Callback used to log errors. System.Console.WriteLine by default.
        /// </summary>
        internal static Action<string> LogErrorCallback;

        //-----------------------------------------------------------------------------------------
        static DebugLogger()
        {
            LogTexts = true;
            ThrowErrors = true;
            ThrowWarnings = true;
            LogCallback = Debug.Log;
            LogWarningCallback = Debug.LogWarning;
            LogErrorCallback = Debug.LogError;
        }

        /// <summary>
        /// Logs the error text and throws an <code>Exception</code> if <code>ThrowErrors</code> is true.
        /// </summary>
        /// <param name="aText">The error message.</param>
        internal static void LogError(string aText)
        {
            LastMessage = aText;
            if (LogTexts && LogErrorCallback != null)
            {
                LogErrorCallback(aText);
            }
            if (ThrowErrors)
            {
                throw new Exception(aText);
            }
            else if (LogErrorCallback != null)
            {
                LogErrorCallback(Environment.StackTrace);
            }
        }

        /// <summary>
        /// Logs the error text and throws an <code>Exception</code> if <code>ThrowErrors</code> is true.
        /// </summary>
        /// <param name="aText">The error message.</param>
        /// <param name="aParameters">The message parameters.</param>
        internal static void LogError(string aText, params object[] aParameters)
        {
            LogError(String.Format(aText, aParameters));
        }

        /// <summary>
        /// Logs the warning text and throws an <code>Exception</code> if <code>ThrowWarnings</code> is true.
        /// </summary>
        /// <param name="aText">The warning message.</param>
        internal static void LogWarning(string aText)
        {
            LastMessage = aText;
            if (LogTexts && LogWarningCallback != null)
            {
                LogWarningCallback(aText);
            }
            if (ThrowWarnings)
            {
                //throw new Exception(aText);
            }
        }

        /// <summary>
        /// Logs the warning text and throws an <code>Exception</code> if <code>ThrowWarnings</code> is true.
        /// </summary>
        /// <param name="aText">The warning message.</param>
        /// <param name="aParameters">The message parameters.</param>
        internal static void LogWarning(string aText, params object[] aParameters)
        {
            LogWarning(String.Format(aText, aParameters));
        }

        /// <summary>
        /// Logs the specified text.
        /// </summary>
        /// <param name="aText">The message.</param>
        internal static void Log(string aText)
        {
            if (LogTexts && LogCallback != null)
            {
                LogCallback(aText);
            }
        }

        /// <summary>
        /// Logs the specified text.
        /// </summary>
        /// <param name="aText">The message.</param>
        /// <param name="aParameters">The message parameters.</param>
        internal static void Log(string aText, params object[] aParameters)
        {
            Log(String.Format(aText, aParameters));
        }
    }
}