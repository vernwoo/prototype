using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Blobreader
{
    class Program
    {
        static void RetrieveBlob(CloudBlockBlob b)
        {
            using (MemoryStream ms = new MemoryStream(1000))
            {
                String result = b.DownloadText(Encoding.UTF8);
                if (!String.IsNullOrEmpty(result))
                {
                    Console.WriteLine("Got blob: " + result );
                }
                else
                {
                    Console.WriteLine("Found no data in Blob");
                }
            }
        }


        static void ReadBlob()
        {
            CloudStorageAccount sa = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            CloudBlobClient qc = sa.CreateCloudBlobClient();
            CloudBlobContainer cbc = qc.GetContainerReference("testcontainer"); //https://vwstore.blob.core.windows.net/testcontainer
            CloudBlockBlob b = cbc.GetBlockBlobReference("testblob");//https://vwstore.blob.core.windows.net/testcontainer/testblob
            Console.WriteLine("waiting to retrieve data from testblob on vmstore (https://vwstore.blob.core.windows.net/testcontainer/testblob) using account key ...");
            RetrieveBlob(b);
        }

        static void ReadBlobUsingSAS(string sasToken)
        {
            // Create new storage credentials using the SAS token.
            StorageCredentials scSAS = new StorageCredentials(sasToken);
            // Use these credentials and the account name to create a Blob service client.
            CloudStorageAccount accountWithSAS = new CloudStorageAccount(scSAS, "vwstore", endpointSuffix: null, useHttps: true);
            CloudBlobClient qcSAS = accountWithSAS.CreateCloudBlobClient();
            CloudBlobContainer cbc = qcSAS.GetContainerReference("testcontainer"); //https://vwstore.blob.core.windows.net/testcontainer
            CloudBlockBlob b = cbc.GetBlockBlobReference("testblob");//https://vwstore.blob.core.windows.net/testcontainer/testblob
            Console.WriteLine("waiting to retrieve testblob from testcontainer on vmstore (https://vwstore.blob.core.windows.net/testcontainer/testblob) using sas token ...");
            RetrieveBlob(b);
        }
        static void Main(string[] args)
        {
            //this sastoken gives read, list and process permission to blob resource type with allowed resource type: service, container, object. The token expires on 2018-11-13 UTC
            string sasToken = "?sv=2017-04-17&ss=b&srt=sco&sp=rl&se=2018-11-13T22:24:47Z&st=2017-11-11T14:24:47Z&spr=https&sig=NthqlSfeOAZHYPB5gQjC8EGflhDv%2BXMDfbvHyh7ImE8%3D";
            ReadBlob();
            ReadBlobUsingSAS(sasToken);
            Console.WriteLine("done");
        }
    }
}
