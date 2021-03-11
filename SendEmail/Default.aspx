<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SendEmail._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <br />

    <form runat="server">
        <asp:ScriptManager ID="ScriptManager2" runat="server" EnablePageMethods="true" />

        <script type="text/javascript">
            document.getElementById("btnAlert").click();
        </script>
        <asp:Button ID="btnAlert" CssClass="btn btn-success" ClientIDMode="Static" runat="server" OnClick="btnAlert_Click" Text="alert" />


        <br />
        <asp:TextBox ID="txtEmail" CssClass="form-control" placeholder="Send To:" runat="server" />
        <br />
        <asp:TextBox ID="txtSubject" CssClass="form-control" placeholder="Subject:" runat="server" />
        <br />
        <asp:TextBox ID="txtBody" CssClass="form-control" placeholder="Body:" runat="server" />
        <br />
        <br />
        <asp:Button ID="btnSend" CssClass="btn btn-success" runat="server" OnClick="btnSend_Click" Text="Send " />

        <br />

        <asp:TextBox ID="txtEncrypt" CssClass="form-control" runat="server" />
        <asp:Label ID="lblResult" runat="server" />

        <asp:Button ID="btnEncrypt" CssClass="btn" runat="server" OnClick="btnEncrypt_Click" Text="Encrypt" />

        <br />

        <asp:TextBox ID="txtDecrypt" CssClass="form-control" runat="server" />
        <asp:Label ID="lblResult1" runat="server" />
        <asp:Button ID="btnDecrypt" CssClass="btn" runat="server" OnClick="btnDecrypt_Click" Text="Decrypt" />



        <br />
        <asp:Label ID="lblIPAddress" runat="server" />
        <br />


        <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control" />
    </form>
    <script id="appyWidgetInit" src="https://chatbot.appypie.com/widget/loadbuild.js?cid=klirphoc-AGENT1614130641084-BOTID1614130641084&name=mixBuild"></script>
</asp:Content>
