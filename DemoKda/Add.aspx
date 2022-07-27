<%@ Page Title="Add" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Add.aspx.cs" Inherits="DemoKda.Add" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Insert New Records</h2>
    <div class="row">
        <div class="col-md-4">
            <h3>Sectors</h3>
            
            <asp:Label ID="Label1" runat="server" Text="Sector Name"></asp:Label> <br>
            <asp:TextBox ID="secname" runat="server"></asp:TextBox> <br>
            <asp:Button ID="addbtn" runat="server" Text="Add" OnClick="addbtn_Click" /> <br>
        </div>
        <div class="col-md-4">
            <h3>Departments</h3>
            
            <asp:Label ID="Label2" runat="server" Text="Department Name"></asp:Label> <br>
            <asp:TextBox ID="depname" runat="server"></asp:TextBox> <br>
            <asp:Label ID="Label3" runat="server" Text="Section Name"></asp:Label> <br>
            <asp:DropDownList ID="secddl" runat="server" AutoPostBack="True" ></asp:DropDownList> <br>
            <asp:Button ID="depbtn" runat="server" Text="Add" OnClick="depbtn_Click"/> <br>
        </div>

    </div>
</asp:Content>
