using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.StorageClient;

namespace WorkerRole1
{
    public class WorkerRole : RoleEntryPoint
    {
        public override void Run()            
        {
            // This is a sample worker implementation. Replace with your logic.
            Trace.WriteLine("WorkerRole1 entry point called", "Information");

            //while (true)
            //{
            //    Thread.Sleep(10000);
            //    Trace.WriteLine("Working", "Information");
            //}
            // initialize the account information
            var storageAccount = CloudStorageAccount.FromConfigurationSetting("DataConnectionString");

            // retrieve a reference to the messages queue
            var queueClient = storageAccount.CreateCloudQueueClient();
            var queue = queueClient.GetQueueReference("messagequeue");
            // retrieve messages and write them to the compute emulator log
            while (true)
            {
                Thread.Sleep(1000);
                if (queue.Exists())
                {
                    var msg = queue.GetMessage();
                    if (msg != null && msg.AsString=="1")
                    {
                        Trace.TraceInformation(string.Format("Message '{0}' processed.", msg.AsString));
                        //queue.Delete();                       
                        
                    }
                }
            }
        }        
        public override bool OnStart()
        {
            // Set the maximum number of concurrent connections 
            ServicePointManager.DefaultConnectionLimit = 12;
            
            CloudStorageAccount.SetConfigurationSettingPublisher((configName, configSetter) =>
            {
                configSetter(RoleEnvironment.GetConfigurationSettingValue(configName));
            });

            return base.OnStart();
        }
        public bool check()
        {
            var storageAccount = CloudStorageAccount.FromConfigurationSetting("DataConnectionString");

            // retrieve a reference to the messages queue
            var queueClient = storageAccount.CreateCloudQueueClient();
            var queue = queueClient.GetQueueReference("messagequeue");
            // retrieve messages and write them to the compute emulator log            
            if (queue.Exists())
            {
                if (queue.GetMessage().AsString == "1")
                    return true;
                else
                    return false;

            }
            else
                return false;
        }
    }
}
