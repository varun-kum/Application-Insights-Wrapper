using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationInsights.Logger
{
    public class RequestOperation
    {
        /// <summary>
        /// Creating singleton TelemetryClient wrapper to be used across the application
        /// </summary>
        private readonly TelemetryClient _telemetryClient;
        private bool _isDependencyOperation;
        private RequestTelemetry _requestTelemetry;
        private IOperationHolder<RequestTelemetry> _requestOperation;
        private string _operationId = string.Empty;
        private string _parentId = string.Empty;
        private string _dependencyID = string.Empty;


        public RequestOperation(TelemetryClient TelemetryClient)
        {
            _telemetryClient = TelemetryClient;
            _isDependencyOperation = false;
        }

        public RequestOperation(TelemetryClient TelemetryClient, string DependencyID, string ParentID)
        {
            _telemetryClient = TelemetryClient;
            _dependencyID = DependencyID;
            _parentId = ParentID;
            _isDependencyOperation = true;
        }

        public string StartOperation(String OperationName, Dictionary<string, string> properties = null)
        {
            _operationId = GetOperationID();
            string DependencyRequestID = Guid.NewGuid().ToString();
            _requestTelemetry = new RequestTelemetry { Name = OperationName, Id = _operationId };

            if (_isDependencyOperation)
            {
                _requestTelemetry.Context.Operation.Id = _dependencyID;
                _requestTelemetry.Context.Operation.ParentId = _parentId;
            }
            else
            {
                _requestTelemetry.Context.Operation.Id = _operationId;
                _requestTelemetry.Context.Operation.ParentId = _operationId;
            }

            _requestOperation = _telemetryClient.StartOperation(_requestTelemetry);
            if (properties != null && properties.Count > 0)
            {
                var mergedDictionary = _requestOperation.Telemetry.Properties.Concat(properties).ToDictionary(x => x.Key, x => x.Value);
            }
            return _operationId;
        }

        public void MarkOperationSucess(string ResponseCode, bool Status = true)
        {
            _requestOperation.Telemetry.Success = Status;
            _requestOperation.Telemetry.ResponseCode = ResponseCode;
        }

        public void CompleteOperation()
        {
            _telemetryClient.StopOperation(_requestOperation);
            _telemetryClient.Flush();
        }

        public string GetOperationID()
        {
            if (string.IsNullOrEmpty(_operationId))
                _operationId = Guid.NewGuid().ToString();
            return _operationId;
        }
    }
}
