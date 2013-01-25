using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.StorageClient;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.ServiceRuntime;
using System.Collections.Specialized;
using System.Data.Services.Client;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Drawing.Imaging;

namespace RdChat_WebRole
{
    public static class WABlobs
    {
        private static CloudBlobContainer GetContainer()
        {

            var account = CloudStorageAccount.FromConfigurationSetting("DataConnectionString");
            CloudBlobClient client = account.CreateCloudBlobClient();
            BlobContainerPermissions permission = new Microsoft.WindowsAzure.StorageClient.BlobContainerPermissions();
            permission.PublicAccess = BlobContainerPublicAccessType.Container;
            CloudBlobContainer container= client.GetContainerReference(RoleEnvironment.GetConfigurationSettingValue("ContainerName"));
            container.CreateIfNotExist();
            container.SetPermissions(permission);
            return container;
        }
        public static string UploadBlob(string path, string name, string contentType, Stream data)
        {
            if (string.IsNullOrEmpty(path))
            {
                return GetAvatarImage();
            }
            else
            {
                string RowKey = Guid.NewGuid().ToString();
                var blob = GetContainer().GetBlobReference(RowKey);

                blob.Properties.ContentType = contentType;

                // Create some metadata for this image
                var metadata = new NameValueCollection();
                metadata["Filename"] = Guid.NewGuid().ToString();
                metadata["Autor"] = name;
                metadata["ImageName"] = string.IsNullOrEmpty(path) ? "unknown" : path;

                // Add and commit metadata to blob
                blob.Metadata.Add(metadata);
                //blob.UploadByteArray(data);
                /*Image img = Image.FromStream(data);
                Bitmap b = new Bitmap(50, 50);
                Graphics g = Graphics.FromImage((Image)b);
                g.DrawImage(img, 0, 0, 50, 50);
                g.Dispose();
                Image thumbnail= img.GetThumbnailImage(50,50,null,IntPtr.Zero);
                thumbnail.Save(data, ImageFormat.Jpeg);
                blob.UploadFromStream(data);*/
                Bitmap orig = new Bitmap(data);

                int width=50;
                int height=50;

                Bitmap thumb = new Bitmap(width, height);
                using (Graphics graphic = Graphics.FromImage(thumb))
                {
                    graphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    graphic.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    graphic.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

                    graphic.DrawImage(orig, 0, 0, width, height);
                    MemoryStream ms = new MemoryStream();
                    thumb.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

                    ms.Seek(0, SeekOrigin.Begin);
                    blob.UploadFromStream(ms);
                    return blob.Uri.ToString();
                }
            }
        }
        public static string GetEditButton()
        {
            string uricode = "http://chatraining.blob.core.windows.net/public/Edit";
            var source = GetContainer().GetBlobReference(uricode);
            return source.Uri.ToString();
        }
        public static string GetAvatarImage()
        {
            return "http://chatraining.blob.core.windows.net/public/Avatar";
        }
        //public static void SetAvatarByDefault(string uri)
        //{
        //    bool flag=false;
        //    var account = CloudStorageAccount.FromConfigurationSetting("DataConnectionString");
        //    var context = new MessageDataServiceContext(account.TableEndpoint.ToString(), account.Credentials);
        //    //Create and execute the query
        //    CloudTableQuery<Message> entities =
        //        (from i in context.CreateQuery<Message>("Messages")
        //         select i).AsTableServiceQuery<Message>();

        //    foreach (Message aux in entities)
        //    {
        //        if (aux.avatar == "http://127.0.0.1:10000/devstoreaccount1/gallery/Name")
        //        {
        //            aux.avatar = "http://chatraining.blob.core.windows.net/public/Avatar";
        //            //Upluad the entity edited
        //            context.UpdateObject(aux);
        //            flag=true;
        //        }
        //    }
        //    //Save the changes
        //    if(flag)
        //        context.SaveChanges(SaveChangesOptions.Batch);
        //}
    }
}