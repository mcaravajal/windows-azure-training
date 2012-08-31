using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.StorageClient;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.ServiceRuntime;
using System.Collections.Specialized;
using System.Data.Services.Client;

namespace RdChat_WebRole
{
    public static class WABlobs
    {
        private static CloudBlobContainer GetContainer()
        {

            var account = CloudStorageAccount.FromConfigurationSetting("DataConnectionString");
            var client = account.CreateCloudBlobClient();

            return client.GetContainerReference(RoleEnvironment.GetConfigurationSettingValue("ContainerName"));
        }        
        public static string UploadBlob(string path, string name, string contentType, byte[] data)
        {
            if (string.IsNullOrEmpty(path))
            {
                return GetAvatarImage();
            }
            else
            {
                var blob = GetContainer().GetBlobReference(name);

                blob.Properties.ContentType = contentType;

                // Create some metadata for this image
                var metadata = new NameValueCollection();
                string RowKey = Guid.NewGuid().ToString();
                metadata["Id"] = RowKey;
                metadata["Filename"] = Guid.NewGuid().ToString();
                metadata["Autor"] = name;
                metadata["ImageName"] = string.IsNullOrEmpty(path) ? "unknown" : RowKey;

                // Add and commit metadata to blob
                blob.Metadata.Add(metadata);
                blob.UploadByteArray(data);
                return blob.Uri.ToString();
            }
        }
        public static string GetEditButton()
        {
            string uricode = "http://127.0.0.1:10000/devstoreaccount1/gallery/Edit";
            var source = GetContainer().GetBlobReference(uricode);
            return source.Uri.ToString();
        }
        public static string GetAvatarImage()
        {
            return "http://127.0.0.1:10000/devstoreaccount1/gallery/Avatar";
        }
        public static void SetAvatarByDefault(string uri)
        {
            bool flag=false;
            var account = CloudStorageAccount.FromConfigurationSetting("DataConnectionString");
            var context = new MessageDataServiceContext(account.TableEndpoint.ToString(), account.Credentials);
            //Create and execute the query
            CloudTableQuery<Message> entities =
                (from i in context.CreateQuery<Message>("Messages")
                 select i).AsTableServiceQuery<Message>();

            foreach (Message aux in entities)
            {
                if (aux.avatar == "http://127.0.0.1:10000/devstoreaccount1/gallery/Name")
                {
                    aux.avatar = "http://127.0.0.1:10000/devstoreaccount1/gallery/Avatar";
                    //Upluad the entity edited
                    context.UpdateObject(aux);
                    flag=true;
                }
            }
            //Save the changes
            if(flag)
                context.SaveChanges(SaveChangesOptions.Batch);
        }
    }
}