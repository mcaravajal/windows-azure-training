using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Collections;
using Microsoft.WindowsAzure;
using System.Web.UI.WebControls;

namespace RdChat_WebRole
{
    public static class WATables
    {
        public static MessageDataServiceContext Getcontainer()
        {
            var account = CloudStorageAccount.FromConfigurationSetting("DataConnectionString");
            return new MessageDataServiceContext(account.TableEndpoint.ToString(), account.Credentials);
        }
        public static DataTable GetTable()
        {            
            IQueryable query = Getcontainer().Messages;
            int count = 0;
            IEnumerator enumerator = query.GetEnumerator();
            enumerator.MoveNext();
            DataTable DT = new DataTable();
            DT.Columns.Add("Name");
            DT.Columns.Add("Body");
            DT.Columns.Add("Date");
            DT.Columns.Add("Rowkey");
            DT.Columns.Add("Url");
            DT.Columns.Add("Avatar");
            while (enumerator.Current != null)
            {
                Message msg = (Message)enumerator.Current;
                DataRow dr = DT.NewRow();
                dr["Rowkey"] = msg.RowKey;
                dr["Name"] = msg.Name;
                dr["Body"] = msg.Body;
                dr["Url"] = WABlobs.GetEditButton();
                dr["Avatar"] = msg.avatar;
                dr["Date"] = SetTimeLapsedByDate(Convert.ToDateTime(msg.Date));
                count++;
                DT.Rows.Add(dr);
                if (!enumerator.MoveNext())
                {
                    break;
                }
            }
            return DT;            
        }
        public static string SetTimeLapsedByDate(DateTime date)
        {
            string tempo = string.Empty;
            TimeSpan time = DateTime.Now - Convert.ToDateTime(date);
            if (time.Hours == 0 && time.Minutes == 0)
            {
                tempo = "Hace instantes";
            }
            if (time.Days >= 1)
            {
                tempo = time.Days + " days and " + time.Hours + " hours ago";
            }
            else if (time.Hours >= 0)
            {
                tempo = time.Hours + " hours and " + time.Minutes + " minutes ago";
            }
            else
            {
                tempo = time.Minutes + " minutes and " + time.Seconds + " seconds ago";
            }
            return tempo;
        }
        public static void NewRow(string name,string body,FileUpload file)
        {
            string uri = WABlobs.UploadBlob(file.FileName, name, file.PostedFile.ContentType, file.FileBytes);
            //Add the row to the Table
            Getcontainer().AddMessage(name, body, DateTime.Now, uri);
            //Sending the query telling that the row was inserted
            WAQueues.SendQueue("Name: " + name + "| Message:" + body + "| Date: " + DateTime.Now + "| Avatar:" + uri);
        }
        public static void EditRow(string RowKey,string name,string body)
        {
            //Create and execute the query
            Message entity =
                (from i in Getcontainer().CreateQuery<Message>("Messages")
                 where i.RowKey == RowKey
                 select i).FirstOrDefault();
            if (entity == null)
                throw new Exception("No se encontro el mensaje seleccionado");
            //Change the fields to update
            entity.Name = name;
            entity.Body = body;
            //Upluad the entity edited
            Getcontainer().UpdateObject(entity);
            //Save the changes
            Getcontainer().SaveChangesWithRetries();
        }
    }
}