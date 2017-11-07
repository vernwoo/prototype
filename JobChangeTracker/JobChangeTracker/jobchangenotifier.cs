using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;

namespace JobChangeTracker
{
    public class JobChangeEvent
    {
        public string upn { get; set; }
        public string changeType { get; set; }

        public DateTime changeTime { get; set;}
    }

    public static class jobchangenotifier
    {
        [FunctionName("jobchangenotifier")]
        public static void Run([QueueTrigger("jobchangequeue", Connection = "AzureWebJobsStorage")]string myQueueItem, TraceWriter log)
        {
            //myQueueItem would be in format of 
            //string myQueueItem = "{'upn':'foo@microsoft.com','changeType':'leaveCompany','changeTime':'2017-11-07T00:00:00Z'}";
            log.Info($"C# Queue trigger function processed: {myQueueItem}");
            JobChangeEvent jce = JsonConvert.DeserializeObject<JobChangeEvent>(myQueueItem);
            if (jce != null)
            {
                log.Info($"{jce.upn} {jce.changeType} at {jce.changeTime}");
            }
            else
            {
                log.Info($"{myQueueItem} can't be deserialized");
            }
        }
    }
}
