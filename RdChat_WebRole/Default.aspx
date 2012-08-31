﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="RdChat_WebRole._Default" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Windows Azure Chat</title>    
    <style type="text/css">
        body { font-family: Verdana; font-size: 9pt; }
        h1 { font-size: 12pt; color: #555555; }
        li { list-style-type: none;}
        form { background-color: #eeeeff; width: 80%; margin: 0 auto; padding: 1em; border: solid 1px #333333; }
        #entryform, #messages { margin: 1em 0 }
        #entryform li span { float: left; width: 15%; color:#333333; margin-top:0.25em; }
        #entryform input[type="text"] { border: solid 1px #999999; }
        #messages { border: solid 1px #999999; }
        #messages li { padding: 0.5em; }
        .error { color: #ff0000; }
        .even { background-color: #ccccff; margin-top: 0.25em }
        .odd { background-color: #ffffff; font-style: italic;  }
        .messageBox { width: 73%; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <h1>Windows Azure Chat</h1>        
        <ul id="entryform">
            
            <li><span>Your name</span><asp:TextBox ID="nameBox" runat="server"/></li>
            <li><span>Message</span><asp:TextBox ID="messageBox" runat="server" CssClass="messageBox" /></li>
                <li><span>Upload an avatar</span>
                <asp:FileUpload ID="FileUpload1" runat="server" />
            </li>
                <li><asp:Button ID="submitButton" runat="server" Text="Submit" OnClick="SubmitButton_Click" /></li>
        </ul>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div><asp:Label ID="status" runat="server" CssClass="error" Visible="false" /></div>                
                
                <asp:ListView ID="messageList" runat="server">
                    <LayoutTemplate>
                        <ul id="messages">
                                <%--<asp:Timer ID="Timer1" runat="server" Interval="1000" ontick="Timer1_Tick1" />--%>
                            <li ID="itemPlaceholder" runat="server" />
                        </ul>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <li class="even">
                            <asp:LinkButton ID="Edit_link" CommandArgument='<%# Eval("RowKey")%>' runat="server" oncommand="OnEdit_link">
                            <img src="<%# Eval("Url") %>" alt="<%# Eval("Url") %>" style="float:left"/>
                            </asp:LinkButton>
                            <img src="<%# Eval("Avatar") %>" height="30" width="30" alt="<%# Eval("Avatar") %>"/>
                            <%# Eval("Name") %> said: <%# Eval("Body") %>
                            <span style="float:right"><%# Eval("Date") %></span>
                        </li>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <li class="odd">
                            <asp:LinkButton ID="Edit_link" CommandArgument='<%# Eval("RowKey")%>' runat="server" oncommand="OnEdit_link" >
                            <img src="<%# Eval("Url") %>" alt="<%# Eval("Url") %>" style="float:left"/>
                            </asp:LinkButton>
                            <img src="<%# Eval("Avatar") %>" height="30" width="30" alt="<%# Eval("Avatar") %>"/>
                            <%# Eval("Name") %> said: <%# Eval("Body") %>
                            <span style="float:right"><%# Eval("Date") %></span>
                        </li>
                    </AlternatingItemTemplate>
                </asp:ListView>
                <asp:Label runat="server" Text="Total:" />
                <asp:Label ID="Total" runat="server" Text="" />                
                <asp:Label id="Rowkey" runat="server" Visible="false" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
