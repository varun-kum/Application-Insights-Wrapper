using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationInsights.Logger
{
    public interface ILoggerV2
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="OperationName"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        string StartOperation(String OperationName, Dictionary<string, string> properties = null);

        /// <summary>
        /// /
        /// </summary>
        /// <returns></returns>
        string GetOperationID();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ResponseCode"></param>
        /// 
        void MarkOperationSucess(string ResponseCode);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ResponseCode"></param>
        void MarkOperationFail(string ResponseCode);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OperationID"></param>
        /// 
        void CompleteOperation(string OperationID = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Target"></param>
        /// <param name="Type"></param>
        /// <param name="Data"></param>
        /// <returns></returns>
        string SetOperationDependency(string Target, string Type = null, string Data = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OperationName"></param>
        /// <param name="OperationID"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        string StartDependentOperation(String OperationName, string OperationID, Dictionary<string, string> properties = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ResponseCode"></param>
        /// <param name="OperationID"></param>
        void MarkDependentOperationSucess(string ResponseCode, string OperationID);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ResponseCode"></param>
        /// <param name="OperationID"></param>
        void MarkDependentOperationFail(string ResponseCode, string OperationID);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OperationID"></param>
        void CompleteDependentOperation(string OperationID = null);


        //
        // Summary:
        //     Formats and writes a log message at the specified log level.
        //
        // Parameters:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   logLevel:
        //     Entry will be written on this level.
        //
        //   message:
        //     Format string of the log message.
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        void Log(LogLevel logLevel, string message, IDictionary<string, string> properties = null);
        //
        // Summary:
        //     Formats and writes a log message at the specified log level.
        //
        // Parameters:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   logLevel:
        //     Entry will be written on this level.
        //
        //   operationID:
        //     The event id associated with the log.
        //
        //   message:
        //     Format string of the log message.
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        void Log(LogLevel logLevel, string message, string operationID, IDictionary<string, string> properties = null);
        //
        // Summary:
        //     Formats and writes a log message at the specified log level.
        //
        // Parameters:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   logLevel:
        //     Entry will be written on this level.
        //
        //   operationID:
        //     The event id associated with the log.
        //
        //   exception:
        //     The exception to log.
        //
        //   message:
        //     Format string of the log message.
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        void Log(LogLevel logLevel, Exception exception, string message, string operationID, IDictionary<string, string> properties = null);
        //
        // Summary:
        //     Formats and writes a log message at the specified log level.
        //
        // Parameters:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   logLevel:
        //     Entry will be written on this level.
        //
        //   exception:
        //     The exception to log.
        //
        //   message:
        //     Format string of the log message.
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        void Log(LogLevel logLevel, Exception exception, string message, IDictionary<string, string> properties = null);


        //
        // Summary:
        //     Formats and writes an informational log message.
        //
        // Parameters:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   message:
        //     Format string of the log message in message template format. Example: "User {User}
        //     logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        void LogInformation(string message, IDictionary<string, string> properties = null, [CallerMemberName] string callerMemberName = null,
            [CallerLineNumber] int sourceLineNumber = 0);
        //
        // Summary:
        //     Formats and writes an informational log message.
        //
        // Parameters:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   operationID:
        //     The event id associated with the log.
        //
        //   message:
        //     Format string of the log message in message template format. Example: "User {User}
        //     logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        void LogInformation(string message, string operationID, IDictionary<string, string> properties = null);
        //
        // Summary:
        //     Formats and writes an informational log message.
        //
        // Parameters:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   exception:
        //     The exception to log.
        //
        //   message:
        //     Format string of the log message in message template format. Example: "User {User}
        //     logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        void LogInformation(Exception exception, string message, IDictionary<string, string> properties = null);
        //
        // Summary:
        //     Formats and writes an informational log message.
        //
        // Parameters:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   operationID:
        //     The event id associated with the log.
        //
        //   exception:
        //     The exception to log.
        //
        //   message:
        //     Format string of the log message in message template format. Example: "User {User}
        //     logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        void LogInformation(Exception exception, string message, string operationID, IDictionary<string, string> properties = null);


        //
        // Summary:
        //     Formats and writes a critical log message.
        //
        // Parameters:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   message:
        //     Format string of the log message in message template format. Example: "User {User}
        //     logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        void LogCritical(string message, IDictionary<string, string> properties = null);
        //
        // Summary:
        //     Formats and writes a critical log message.
        //
        // Parameters:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   exception:
        //     The exception to log.
        //
        //   message:
        //     Format string of the log message in message template format. Example: "User {User}
        //     logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        void LogCritical(Exception exception, string message, IDictionary<string, string> properties = null);
        //
        // Summary:
        //     Formats and writes a critical log message.
        //
        // Parameters:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   operationID:
        //     The event id associated with the log.
        //
        //   message:
        //     Format string of the log message in message template format. Example: "User {User}
        //     logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        void LogCritical(string message, string operationID, IDictionary<string, string> properties = null);
        //
        // Summary:
        //     Formats and writes a critical log message.
        //
        // Parameters:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   operationID:
        //     The event id associated with the log.
        //
        //   exception:
        //     The exception to log.
        //
        //   message:
        //     Format string of the log message in message template format. Example: "User {User}
        //     logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        void LogCritical(Exception exception, string message, string operationID, IDictionary<string, string> properties = null);


        //
        // Summary:
        //     Formats and writes a debug log message.
        //
        // Parameters:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   operationID:
        //     The event id associated with the log.
        //
        //   exception:
        //     The exception to log.
        //
        //   message:
        //     Format string of the log message in message template format. Example: "User {User}
        //     logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        void LogDebug(Exception exception, string message, string operationID, IDictionary<string, string> properties = null);
        //
        // Summary:
        //     Formats and writes a debug log message.
        //
        // Parameters:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   operationID:
        //     The event id associated with the log.
        //
        //   message:
        //     Format string of the log message in message template format. Example: "User {User}
        //     logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        void LogDebug(string message, string operationID, IDictionary<string, string> properties = null);
        //
        // Summary:
        //     Formats and writes a debug log message.
        //
        // Parameters:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   exception:
        //     The exception to log.
        //
        //   message:
        //     Format string of the log message in message template format. Example: "User {User}
        //     logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        void LogDebug(Exception exception, string message, IDictionary<string, string> properties = null);
        //
        // Summary:
        //     Formats and writes a debug log message.
        //
        // Parameters:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   message:
        //     Format string of the log message in message template format. Example: "User {User}
        //     logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        void LogDebug(string message, IDictionary<string, string> properties = null);


        //
        // Summary:
        //     Formats and writes an error log message.
        //
        // Parameters:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   message:
        //     Format string of the log message in message template format. Example: "User {User}
        //     logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        void LogError(string message, IDictionary<string, string> properties = null);
        //
        // Summary:
        //     Formats and writes an error log message.
        //
        // Parameters:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   exception:
        //     The exception to log.
        //
        //   message:
        //     Format string of the log message in message template format. Example: "User {User}
        //     logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        void LogError(Exception exception, string message, IDictionary<string, string> properties = null);

        //
        // Summary:
        //     Formats and writes an error log message.
        //
        // Parameters:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   operationID:
        //     The event id associated with the log.
        //
        //   message:
        //     Format string of the log message in message template format. Example: "User {User}
        //     logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        void LogError(string message, string operationID, IDictionary<string, string> properties = null);
        //
        // Summary:
        //     Formats and writes an error log message.
        //
        // Parameters:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   operationID:
        //     The event id associated with the log.
        //
        //   exception:
        //     The exception to log.
        //
        //   message:
        //     Format string of the log message in message template format. Example: "User {User}
        //     logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        void LogError(Exception exception, string message, string operationID, IDictionary<string, string> properties = null);

        //void LogError(ExceptionTelemetry telemetry);


        //
        // Summary:
        //     Formats and writes a trace log message.
        //
        // Parameters:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   message:
        //     Format string of the log message in message template format. Example: "User {User}
        //     logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        void LogTrace(string message, IDictionary<string, string> properties = null);
        //
        // Summary:
        //     Formats and writes a trace log message.
        //
        // Parameters:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   operationID:
        //     The event id associated with the log.
        //
        //   message:
        //     Format string of the log message in message template format. Example: "User {User}
        //     logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        void LogTrace(string message, string operationID, IDictionary<string, string> properties = null);
        //
        // Summary:
        //     Formats and writes a trace log message.
        //
        // Parameters:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   exception:
        //     The exception to log.
        //
        //   message:
        //     Format string of the log message in message template format. Example: "User {User}
        //     logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        void LogTrace(Exception exception, string message, IDictionary<string, string> properties = null);
        //
        // Summary:
        //     Formats and writes a trace log message.
        //
        // Parameters:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   operationID:
        //     The event id associated with the log.
        //
        //   exception:
        //     The exception to log.
        //
        //   message:
        //     Format string of the log message in message template format. Example: "User {User}
        //     logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        void LogTrace(Exception exception, string message, string operationID, IDictionary<string, string> properties = null);


        //
        // Summary:
        //     Formats and writes a warning log message.
        //
        // Parameters:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   message:
        //     Format string of the log message in message template format. Example: "User {User}
        //     logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        void LogWarning(string message, IDictionary<string, string> properties = null);
        //
        // Summary:
        //     Formats and writes a warning log message.
        //
        // Parameters:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   operationID:
        //     The event id associated with the log.
        //
        //   message:
        //     Format string of the log message in message template format. Example: "User {User}
        //     logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        void LogWarning(string message, string operationID, IDictionary<string, string> properties = null);
        //
        // Summary:
        //     Formats and writes a warning log message.
        //
        // Parameters:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   operationID:
        //     The event id associated with the log.
        //
        //   exception:
        //     The exception to log.
        //
        //   message:
        //     Format string of the log message in message template format. Example: "User {User}
        //     logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        void LogWarning(Exception exception, string message, string operationID, IDictionary<string, string> properties = null);
        //
        // Summary:
        //     Formats and writes a warning log message.
        //
        // Parameters:
        //   logger:
        //     The Microsoft.Extensions.Logging.ILogger to write to.
        //
        //   exception:
        //     The exception to log.
        //
        //   message:
        //     Format string of the log message in message template format. Example: "User {User}
        //     logged in from {Address}"
        //
        //   args:
        //     An object array that contains zero or more objects to format.
        void LogWarning(Exception exception, string message, IDictionary<string, string> properties = null);
    }
}
