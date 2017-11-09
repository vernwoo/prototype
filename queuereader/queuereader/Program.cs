using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Queue;

namespace queuereader
{
    class Program
    {
        static void RetrieveQueueEvent(CloudQueue q)
        {
            CloudQueueMessage qm = null;
            
            qm = q.GetMessage();
            if (qm != null)
            {
                Console.WriteLine("Got event " + qm.AsString + " from queue");
                q.DeleteMessage(qm);
            }
            else
            {
                Console.WriteLine("Found no event in the queue");
            }
        }


        static void ReadEventFromQueue()
        {
            CloudStorageAccount sa = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            CloudQueueClient qc = sa.CreateCloudQueueClient();
            CloudQueue q = qc.GetQueueReference("testqueue");
            Console.WriteLine("waiting to retrieve messages from event queue testqueue on vmstore (https://vwstore.queue.core.windows.net/) using account key ...");
            RetrieveQueueEvent(q);
        }

        static void ReadEventFromQueueUsingSAS(string sasToken)
        {
            // Create new storage credentials using the SAS token.
            StorageCredentials scSAS = new StorageCredentials(sasToken);
            // Use these credentials and the account name to create a queue service client.
            CloudStorageAccount accountWithSAS = new CloudStorageAccount(scSAS, "vwstore", endpointSuffix: null, useHttps: true);
            CloudQueueClient qcSAS = accountWithSAS.CreateCloudQueueClient();
            CloudQueue q = qcSAS.GetQueueReference("testqueue");
            Console.WriteLine("waiting to retrieve messages from event queue testqueue on vmstore (https://vwstore.queue.core.windows.net/) using sas token ...");
            RetrieveQueueEvent(q);
        }
        static void Main(string[] args)
        {
            //this sastoken gives read, list and process permission to queue resource type with allowed resource type: service, container, object. The token expires on 2018-11-09 UTC
            string sasToken = "?sv=2017-04-17&ss=q&srt=sco&sp=rlp&se=2018-11-09T00:37:43Z&st=2017-11-08T16:37:43Z&spr=https,http&sig=kZgvCJi1E8YJQXdMHZS3LBFhYhZb7rdNR5Ggid00vrQ%3D";
           // ReadEventFromQueue();
            ReadEventFromQueueUsingSAS(sasToken);
            Console.WriteLine("done");
        }
    }
}
