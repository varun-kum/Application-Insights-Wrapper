using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationInsights.Logger
{
    public class TraceLoggerV1 : ILoggerV1
    {
        private string _operationId = string.Empty;
        private string _dependencyOperationID = string.Empty;

        private RequestTelemetry _requestTelemetry;
        private RequestTelemetry _dependentRequestTelemetry;

        private IOperationHolder<RequestTelemetry> _requestOperation;
        private IOperationHolder<RequestTelemetry> _dependentRequestOperation;

        private IOperationHolder<DependencyTelemetry> _dependencyOperation;


        /// <summary>
        /// Creating singleton TelemetryClient wrapper to be used across the application
        /// </summary>
        private static readonly TelemetryClient telemetryClient = new TelemetryClient(
            new Microsoft.ApplicationInsights.Extensibility.TelemetryConfiguration()
            {
                InstrumentationKey = ConfigurationManager.AppSettings["APPINSIGHTS_INSTRUMENTATIONKEY"],
                ConnectionString = "InstrumentationKey=07369c9f-e0a2-437f-9666-ca52b0413835"

            });

        public TraceLoggerV1()
        {
            _ = TelemetryConfiguration.Active;
            telemetryClient.InstrumentationKey = ConfigurationManager.AppSettings["APPINSIGHTS_INSTRUMENTATIONKEY"];
        }

        public string StartOperation(String OperationName, Dictionary<string, string> properties = null)
        {
            GetOperationID();
            _requestTelemetry = new RequestTelemetry { Name = OperationName, Id = _operationId };
            _requestTelemetry.Context.Operation.ParentId = GetOperationID();
            _requestOperation = telemetryClient.StartOperation(_requestTelemetry);
            if (properties != null && properties.Count > 0)
            {
                var mergedDictionary = _requestOperation.Telemetry.Properties.Concat(properties).ToDictionary(x => x.Key, x => x.Value);
            }
            return _operationId;
        }

        public string GetOperationID()
        {
            if (string.IsNullOrEmpty(_operationId))
                _operationId = Guid.NewGuid().ToString();
            return _operationId;

        }
        public void MarkOperationSucess(string ResponseCode)
        {
            _requestOperation.Telemetry.Success = true;
            _requestOperation.Telemetry.ResponseCode = ResponseCode;
        }

        public void MarkOperationFail(string ResponseCode)
        {
            _requestOperation.Telemetry.Success = false;
            _requestOperation.Telemetry.ResponseCode = ResponseCode;
        }

        public void CompleteOperation()
        {
            telemetryClient.StopOperation(_requestOperation);
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
            return _dependencyOperation.Telemetry.Context.Operation.Id;
        }

        public string StartDependentOperation(String OperationName, string RootID = null, Dictionary<string, string> properties = null)
        {
            string DependencyRequestID = Guid.NewGuid().ToString();

            _dependentRequestTelemetry = new RequestTelemetry { Name = OperationName, Id = DependencyRequestID };
            _dependentRequestTelemetry.Context.Operation.Id = _dependencyOperationID;
            _dependentRequestTelemetry.Context.Operation.ParentId = _operationId;
            _dependentRequestOperation = telemetryClient.StartOperation(_dependentRequestTelemetry);

            if (properties != null && properties.Count > 0)
            {
                var mergedDictionary = _requestOperation.Telemetry.Properties.Concat(properties).ToDictionary(x => x.Key, x => x.Value);
            }
            return _dependencyOperationID;
        }

        public void CompleteDependentOperation()
        {
            telemetryClient.StopOperation(_dependentRequestOperation);
            telemetryClient.Flush();
        }

        public void MarkDependentOperationSucess(string ResponseCode)
        {
            _dependentRequestOperation.Telemetry.Success = true;
            _dependentRequestOperation.Telemetry.ResponseCode = ResponseCode;
        }

        public void MarkDependentOperationFail(string ResponseCode)
        {
            _dependentRequestOperation.Telemetry.Success = false;
            _dependentRequestOperation.Telemetry.ResponseCode = ResponseCode;
        }

        public string NewLogTransaction(string logTransactionID = null)
        {
            if (string.IsNullOrWhiteSpace(logTransactionID))
            {

            }
            return logTransactionID;
        }

        //public IInformation Information { get => new TraceInformation(); set => throw new NotImplementedException(); }
        //public IInformation Exception { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IDisposable BeginScope(string messageFormat, IDictionary<string, string> properties)
        {
            throw new NotImplementedException();
        }


        public void Log(LogLevel logLevel, string message, IDictionary<string, string> properties = null)
        {
            throw new NotImplementedException();
        }

        public void Log(LogLevel logLevel, string message, string eventId, IDictionary<string, string> properties = null)
        {
            throw new NotImplementedException();
        }

        public void Log(LogLevel logLevel, Exception exception, string message, string eventId, IDictionary<string, string> properties = null)
        {
            throw new NotImplementedException();
        }

        public void Log(LogLevel logLevel, Exception exception, string message, IDictionary<string, string> properties = null)
        {
            throw new NotImplementedException();
        }

        public void LogCritical(string message, IDictionary<string, string> properties = null)
        {
            throw new NotImplementedException();
        }

        public void LogCritical(Exception exception, string message, IDictionary<string, string> properties = null)
        {
            throw new NotImplementedException();
        }

        public void LogCritical(string message, string eventId, IDictionary<string, string> properties = null)
        {
            throw new NotImplementedException();
        }

        public void LogCritical(Exception exception, string message, string eventId, IDictionary<string, string> properties = null)
        {
            throw new NotImplementedException();
        }

        public void LogDebug(Exception exception, string message, string eventId, IDictionary<string, string> properties = null)
        {
            throw new NotImplementedException();
        }

        public void LogDebug(string message, string eventId, IDictionary<string, string> properties = null)
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

        public void LogError(string message, IDictionary<string, string> properties = null)
        {
            telemetryClient.TrackException(new Exception("test ex"));
            telemetryClient.Flush();
        }

        public void LogError(Exception exception, string message, IDictionary<string, string> properties = null)
        {
            telemetryClient.TrackException(exception);
            telemetryClient.Flush();
        }

        public void LogError(Exception exception, string message, string eventId, IDictionary<string, string> properties = null)
        {
            throw new NotImplementedException();
        }

        public void LogError(string message, string eventId, IDictionary<string, string> properties = null)
        {
            throw new NotImplementedException();
        }

        public void LogInformation(string message, IDictionary<string, string> properties = null)
        {
            telemetryClient.TrackTrace(message);
            telemetryClient.Flush();
        }

        public void LogInformation(string message, string eventId, IDictionary<string, string> properties = null)
        {
            throw new NotImplementedException();
        }

        public void LogInformation(Exception exception, string message, IDictionary<string, string> properties = null)
        {
            throw new NotImplementedException();
        }

        public void LogInformation(Exception exception, string message, string eventId, IDictionary<string, string> properties = null)
        {
            throw new NotImplementedException();
        }

        public void LogTrace(string message, IDictionary<string, string> properties = null)
        {
            throw new NotImplementedException();
        }

        public void LogTrace(string message, string eventId, IDictionary<string, string> properties = null)
        {
            throw new NotImplementedException();
        }

        public void LogTrace(Exception exception, string message, IDictionary<string, string> properties = null)
        {
            throw new NotImplementedException();
        }

        public void LogTrace(Exception exception, string message, string eventId, IDictionary<string, string> properties = null)
        {
            throw new NotImplementedException();
        }

        public void LogWarning(string message, IDictionary<string, string> properties = null)
        {
            throw new NotImplementedException();
        }

        public void LogWarning(string message, string eventId, IDictionary<string, string> properties = null)
        {
            throw new NotImplementedException();
        }

        public void LogWarning(Exception exception, string message, string eventId, IDictionary<string, string> properties = null)
        {
            throw new NotImplementedException();
        }

        public void LogWarning(Exception exception, string message, IDictionary<string, string> properties = null)
        {
            throw new NotImplementedException();
        }
    }
}
