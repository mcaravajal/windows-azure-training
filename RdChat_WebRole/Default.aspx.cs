// ----------------------------------------------------------------------------------
// Microsoft Developer & Platform Evangelism
// 
// Copyright (c) Microsoft Corporation. All rights reserved.
// 
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES 
// OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
// ----------------------------------------------------------------------------------
// The example companies, organizations, products, domain names,
// e-mail addresses, logos, people, places, and events depicted
// herein are fictitious.  No association with any real company,
// organization, product, domain name, email address, logo, person,
// places, or events is intended or should be inferred.
// ----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.WindowsAzure;
using System.Data.Services.Client;
using System.Data;
using System.Collections;
using Microsoft.WindowsAzure.StorageClient;
using WorkerRole1;
using Microsoft.WindowsAzure.ServiceRuntime;
using System.Collections.Specialized;

namespace RdChat_WebRole
{
    public partial class _Default : System.Web.UI.Page
    {
        delegate void Mydelegate();
        WorkerRole work = new WorkerRole();
        private Mydelegate checking;
        protected void refresh()
        {
            try
            {
                DataTable DT = WATables.GetTable();
                this.Total.Text = DT.Rows.Count.ToString();
                this.messageList.DataSource = DT;
                this.messageList.DataBind();
                this.status.Visible = false;
            }
            catch (Exception ex)
            {
                this.status.Visible = true;
                this.status.Text = ex.Message;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                checking = new Mydelegate(Check);
                checking.BeginInvoke(null, null);
            }
            //System.Threading.Thread newthread = new System.Threading.Thread(Check);
            //newthread.Start();
            //WABlobs.SetAvatarByDefault("http://127.0.0.1:10000/devstoreaccount1/gallery/Avatar");
            refresh();
        }

        /// <summary>
        /// Save the text message to the message data context, and bind it to the list control.
        /// </summary>
        /// <param name="sender">Submit button</param>
        /// <param name="e">Button click event</param>

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            var statusMessage = string.Empty;

            try
            {
                
                    WATables.NewRow(HttpUtility.HtmlEncode(this.nameBox.Text), HttpUtility.HtmlEncode(this.messageBox.Text), this.FileUpload1);
                    refresh();
             //           WATables.EditRow(Rowkey.Text, nameBox.Text, messageBox.Text);
             //           refresh();
            }
            catch (DataServiceRequestException ex)
            {
                statusMessage = ex.Message;
                this.status.Visible = true;
                this.status.Text = statusMessage;
            }
        }

        protected void Timer1_Tick1(object sender, EventArgs e)
        {
            //Check();
        }

        protected void OnEdit_link(object sender, CommandEventArgs e)
        {
            try
            {
                Message entity_toEdit = WATables.GetRowByRowkey(e.CommandArgument.ToString());
                this.Rowkey.Text = entity_toEdit.RowKey;
                this.nameBox.Text = entity_toEdit.Name;
                this.messageBox.Text = entity_toEdit.Body;
                this.submitButton.CommandName = "Edit";
            }
            catch (Exception ex)
            {
                this.status.Text = ex.Message;
                this.status.Visible = true;
            }
        }
        private void Check()
        {
            try
            {
                WorkerRole work = new WorkerRole();
                if (work.check())
                {
                    refresh();
                    Timer1_Tick1(null, null);
                }
            }
            catch (Exception ex)
            {
                status.Visible = true;
                status.Text = ex.Message;
            }
        }
    }
}