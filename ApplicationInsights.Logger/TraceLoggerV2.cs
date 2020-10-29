using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationInsights.Logger
{
    public class TraceLoggerV2 : ILoggerV2
    {
        #region Private Section
        private string _operationId = string.Empty;
        private string _dependencyOperationID = string.Empty;

        private IOperationHolder<DependencyTelemetry> _dependencyOperation;
        private RequestOperation _requestOperation;

        /// <summary>
        /// Creating singleton TelemetryClient wrapper to be used across the application
        /// </summary>
        private static readonly TelemetryClient telemetryClient = new TelemetryClient(
            new Microsoft.ApplicationInsights.Extensibility.TelemetryConfiguration()
            {
                InstrumentationKey = ConfigurationManager.AppSettings["APPINSIGHTS_INSTRUMENTATIONKEY"],
                ConnectionString = "InstrumentationKey=07369c9f-e0a2-437f-9666-ca52b0413835"

            });

        private Dictionary<string, string> exceptionProperties;
        #endregion

        #region Public Properties and Constructors
        public Dictionary<string, RequestOperation> DependencyOperations;

        public TraceLoggerV2()
        {
            _ = TelemetryConfiguration.Active;
            telemetryClient.InstrumentationKey = ConfigurationManager.AppSettings["APPINSIGHTS_INSTRUMENTATIONKEY"];
        }
        #endregion

        #region Public Operation Methods
        public string StartOperation(String OperationName, Dictionary<string, string> properties = null)
        {
            if (_requestOperation != null)
                throw new Exception(string.Format("Only one Instance of Parent Operation can run. Operation ID {0} already created.", _operationId));
            else
            {
                _requestOperation = new RequestOperation(telemetryClient);
                _operationId = _requestOperation.StartOperation(OperationName, properties);
            }
            telemetryClient.Flush();
            return _operationId;
        }

        public string GetOperationID()
        {
            return _operationId;
        }

        public void MarkOperationSucess(string ResponseCode)
        {
            _requestOperation.MarkOperationSucess(ResponseCode, true);
        }

        public void MarkOperationFail(string ResponseCode)
        {
            _requestOperation.MarkOperationSucess(ResponseCode, false);
        }

        public void CompleteOperation(string OperationID = null)
        {
            if (string.IsNullOrEmpty(OperationID) || _requestOperation.GetOperationID() == OperationID)
                _requestOperation.CompleteOperation();
            telemetryClient.Flush();
        }

        public string SetOperationDependency(string Target, string Type = null, string Data = null)
        {
            string DependencyID = Guid.NewGuid().ToString();
            _dependencyOperation = telemetryClient.StartOperation<DependencyTelemetry>(Target, DependencyID, _operationId);
            _dependencyOperation.Telemetry.Type = Type;
            _dependencyOperation.Telemetry.Data = Data;
            _dependencyOperation.Telemetry.Success = true;

            _dependencyOperationID = _dependencyOperation.Telemetry.Context.Operation.Id;

            telemetryClient.StopOperation(_dependencyOperation);
            telemetryClient.Flush();
            return _dependencyOperationID;
        }
        #endregion

        public string StartDependentOperation(String OperationName, string OperationID, Dictionary<string, string> properties = null)
        {
            RequestOperation _requestDependencyOperation;
            _requestDependencyOperation = new RequestOperation(telemetryClient, OperationID, _operationId);
            if (DependencyOperations == null)
                DependencyOperations = new Dictionary<string, RequestOperation>();
            string dependencyOperationID = _requestDependencyOperation.StartOperation(OperationName, properties);
            DependencyOperations.Add(dependencyOperationID, _requestDependencyOperation);
            telemetryClient.Flush();
            return dependencyOperationID;
        }

        public void MarkDependentOperationSucess(string ResponseCode, string OperationID)
        {
            if (DependencyOperations != null &&
                DependencyOperations.Count > 0 &&
                DependencyOperations[OperationID] != null)
            {
                DependencyOperations[OperationID].MarkOperationSucess(ResponseCode, true);
            }
        }

        public void MarkDependentOperationFail(string ResponseCode, string OperationID)
        {
            if (DependencyOperations != null &&
               DependencyOperations.Count > 0 &&
               DependencyOperations[OperationID] != null)
            {
                DependencyOperations[OperationID].MarkOperationSucess(ResponseCode, false);
            }
        }

        public void CompleteDependentOperation(string OperationID = null)
        {
            if (DependencyOperations != null &&
                DependencyOperations.Count > 0 &&
                DependencyOperations[OperationID] != null)
            {
                DependencyOperations[OperationID].MarkOperationSucess("400", false);
                DependencyOperations[OperationID].CompleteOperation();
            }
            DependencyOperations.Remove(OperationID);
            telemetryClient.Flush();
        }


        //Logging Methods
        #region Log
        public void Log(LogLevel logLevel, string message, IDictionary<string, string> properties = null)
        {
            throw new NotImplementedException();
        }

        public void Log(LogLevel logLevel, string message, string operationID, IDictionary<string, string> properties = null)
        {
            throw new NotImplementedException();
        }

        public void Log(LogLevel logLevel, Exception exception, string message, string operationID, IDictionary<string, string> properties = null)
        {
            throw new NotImplementedException();
        }

        public void Log(LogLevel logLevel, Exception exception, string message, IDictionary<string, string> properties = null)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region LogCritical
        public void LogCritical(string message, IDictionary<string, string> properties = null)
        {
            throw new NotImplementedException();
        }

        public void LogCritical(Exception exception, string message, IDictionary<string, string> properties = null)
        {
            throw new NotImplementedException();
        }

        public void LogCritical(string message, string operationID, IDictionary<string, string> properties = null)
        {
            throw new NotImplementedException();
        }

        public void LogCritical(Exception exception, string message, string operationID, IDictionary<string, string> properties = null)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region LogDebug
        public void LogDebug(Exception exception, string message, string operationID, IDictionary<string, string> properties = null)
        {
            throw new NotImplementedException();
        }

        public void LogDebug(string message, string operationID, IDictionary<string, string> properties = null)
        {
            throw new NotImplementedException();
        }

        public void LogDebug(Exception exception, string message, IDictionary<string, string> properties = null)
        {
            throw new NotImplementedException();
        }

        public void LogDebug(string message, IDictionary<string, string> properties = null)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region LogError
        public void LogError(string message, IDictionary<string, string> properties = null)
        {
            LogError(message, null, properties);
        }
        public void LogError(string message, string operationID, IDictionary<string, string> properties = null)
        {
            LogError(null, message, operationID, properties);
        }
        public void LogError(Exception exception, string message, IDictionary<string, string> properties = null)
        {
            LogError(exception, message, null, properties);
        }
        public void LogError(Exception exception, string message, string operationID, IDictionary<string, string> properties = null)
        {
            ExceptionTelemetry ex = new ExceptionTelemetry();
            ex.Exception = exception;
            ex.Message = message;
            ex.SeverityLevel = SeverityLevel.Error;
            ex.Context.Operation.ParentId = "";
            if (exception == null)
                exception = new Exception(message);

            operationID = string.IsNullOrEmpty(operationID) ? _operationId : operationID;
            AddDefaultProperties(operationID);
            if (properties == null || properties.Count <= 0)
                properties = exceptionProperties;
            else
                properties.Concat(exceptionProperties).ToDictionary(x => x.Key, x => x.Value);

            telemetryClient.TrackException(exception, properties);
            telemetryClient.Flush();
        }
        //public void LogError(ExceptionTelemetry telemetry)
        //{
        //    telemetryClient.TrackException(telemetry);
        //    telemetryClient.Flush();
        //}

        #endregion

        #region LogInformation
        public void LogInformation(string message, IDictionary<string, string> properties = null, [CallerMemberName] string callerMemberName = null,
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            telemetryClient.TrackTrace(message, SeverityLevel.Information);
            telemetryClient.Flush();
        }

        public void LogInformation(string message, string operationID, IDictionary<string, string> properties = null)
        {
            throw new NotImplementedException();
        }

        public void LogInformation(Exception exception, string message, IDictionary<string, string> properties = null)
        {
            throw new NotImplementedException();
        }

        public void LogInformation(Exception exception, string message, string operationID, IDictionary<string, string> properties = null)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region LogTrace
        public void LogTrace(string message, IDictionary<string, string> properties = null)
        {
            throw new NotImplementedException();
        }

        public void LogTrace(string message, string operationID, IDictionary<string, string> properties = null)
        {
            throw new NotImplementedException();
        }

        public void LogTrace(Exception exception, string message, IDictionary<string, string> properties = null)
        {
            throw new NotImplementedException();
        }

        public void LogTrace(Exception exception, string message, string operationID, IDictionary<string, string> properties = null)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region LogWarning
        public void LogWarning(string message, IDictionary<string, string> properties = null)
        {
            throw new NotImplementedException();
        }

        public void LogWarning(string message, string operationID, IDictionary<string, string> properties = null)
        {
            throw new NotImplementedException();
        }

        public void LogWarning(Exception exception, string message, string operationID, IDictionary<string, string> properties = null)
        {
            throw new NotImplementedException();
        }

        public void LogWarning(Exception exception, string message, IDictionary<string, string> properties = null)
        {
            throw new NotImplementedException();
        }
        #endregion

        private void AddDefaultProperties(string operationID)
        {
            exceptionProperties = new Dictionary<string, string>();
            if (string.IsNullOrEmpty(operationID))
                exceptionProperties.Add("ParentID", _operationId);
            else
                exceptionProperties.Add("ParentID", operationID);
        }

        /// <summary>
        /// Used to merge two dictionary
        /// </summary>
        /// <param name="property"></param>
        /// <param name="customValue"></param>
        /// <return> a dictionary merged or the property</returns>
        private static IDictionary<string, string> MergeDictionary(IDictionary<string, string> property, IDictionary<string, string> customValue)
        {
            try
            {
                if (customValue.Count > 0)
                {
                    var mergedDictionary = property.Concat(customValue).ToDictionary(x => x.Key, x => x.Value);
                    return mergedDictionary;
                }
                else
                {
                    return property;
                }
            }
            // If an exception occurs, return without merging
            catch (Exception exception)
            {
                return property;
            }
        }
    }
}
