using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;

namespace queuereader
{
    class Program
    {
        static void Main(string[] args)
        {
            CloudStorageAccount sa = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            CloudQueueClient qc = sa.CreateCloudQueueClient();
            CloudQueue q = qc.GetQueueReference("testqueue");
            CloudQueueMessage qm = null;
            Console.WriteLine("waiting to retrieve messages from event queue testqueue on vmstore (https://vwstore.queue.core.windows.net/)...");
            while (true)
            {
                while (qm == null)
                {
                    Thread.Sleep(1000);
                    qm = q.GetMessage();
                }
                Console.WriteLine(qm.AsString);
                q.DeleteMessage(qm);
                qm = q.GetMessage();               
            }
        }
    }
}
