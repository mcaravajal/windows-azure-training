using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.StorageClient;
using Microsoft.WindowsAzure;

namespace RdChat_WebRole
{
    public static class WAQueues
    {
        private static CloudQueue GetCloudQueue()
        {
            var account = CloudStorageAccount.FromConfigurationSetting("DataConnectionString");            
            var queueClient= account.CreateCloudQueueClient();
            var queue = queueClient.GetQueueReference("messagequeue");
            queue.CreateIfNotExist();
            return queue;
        }
        public static void SendQueue(string msg)
        {
            GetCloudQueue().AddMessage(new CloudQueueMessage("1"));
        }

    }
}