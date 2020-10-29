using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationInsights.Logger.TesterConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //V1();
            V2();
        }

        private static void V1()
        {
            ILoggerV1 logger = new TraceLoggerV1();
            var logTransactionID = logger.GetOperationID();

            logger.LogInformation("----Start 15 ----");
            logger.LogInformation("Start GUID: " + logTransactionID);
            string name = "Test Operation 15";

            string firstid = logger.StartOperation(name);
            logger.LogInformation("RequestTelemetry Name : " + name);
            logger.LogInformation("Operation1 Id : " + firstid);
            logger.LogError(new Exception("test ex"), "Exception");
            logger.MarkOperationSucess("200");
            logger.CompleteOperation();

            string DependencyID = Guid.NewGuid().ToString();
            logger.LogInformation("Operations GUID for DependencyTelemetry : " + DependencyID);
            name = "Depedency Set 15";
            var operationdep = logger.SetOperationDependency(name, "Azure Service Bus", name);
            logger.LogInformation(" DependencyTelemetry operationdep.Id : " + operationdep);


            //Operation 2
            name = "Depedency Operation 15";
            string DepID = logger.StartDependentOperation(name);
            //operationrec2.Telemetry.Properties.Add("AzureServiceRequestID", "testid");
            //operationrec2.Telemetry.Success = false;
            //operationrec2.Telemetry.ResponseCode = "500";
            logger.LogInformation("RequestTelemetry Name : " + name);
            logger.LogInformation("RequestTelemetry operationdep.Id : " + DepID);
            logger.LogError("Test fail");
            logger.LogInformation("----End 15----");
            logger.MarkDependentOperationFail("500");
            logger.CompleteDependentOperation();
        }

        private static void V2()
        {
            int i = 16;
            ILoggerV2 logger = new TraceLoggerV2();
            var logTransactionID = logger.GetOperationID();
            logger.LogError("----Test 123");

            logger.LogInformation("----Start " + i + " ----");
            logger.LogInformation("Start GUID: " + logTransactionID);
            string name = "Test Operation " + i;
            string firstid = logger.StartOperation(name);
            logger.LogInformation("RequestTelemetry Name : " + name);
            logger.LogInformation("Operation1 Id : " + firstid);
            logger.LogError(new Exception("test ex"), "Exception");
            logger.MarkOperationSucess("200");
            logger.CompleteOperation();

            string DependencyID = Guid.NewGuid().ToString();
            logger.LogInformation("Operations GUID for DependencyTelemetry : " + DependencyID);
            name = "Depedency Set " + i;
            var operationdep = logger.SetOperationDependency(name, "Azure Service Bus", name);
            logger.LogInformation(" DependencyTelemetry operationdep.Id : " + operationdep);


            //Operation 2
            name = "Depedency Operation " + i;
            string DepID = logger.StartDependentOperation(name, operationdep);
            //operationrec2.Telemetry.Properties.Add("AzureServiceRequestID", "testid");
            //operationrec2.Telemetry.Success = false;
            //operationrec2.Telemetry.ResponseCode = "500";
            logger.LogInformation("RequestTelemetry Name : " + name);
            logger.LogInformation("RequestTelemetry operationdep.Id : " + DepID);
            logger.LogError("Test fail");
            logger.LogInformation("----End " + i + "----");
             logger.MarkDependentOperationFail("500", DepID);
            logger.CompleteDependentOperation(DepID);
        }
    }
}
