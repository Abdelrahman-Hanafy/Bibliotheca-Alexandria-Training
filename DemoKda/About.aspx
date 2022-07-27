<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="DemoKda.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
     <div class="jumbotron">
        <h1>Projects</h1>
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>List</h2>
            <asp:GridView ID="projs" runat="server"></asp:GridView>
        </div>
        <div class="col-md-4">
            <h2>Details</h2>
            <asp:DropDownList ID="projsddl" runat="server" AutoPostBack="True" OnSelectedIndexChanged="Projsddl_SelectedIndexChanged" ></asp:DropDownList>
            <asp:GridView ID="Dets" runat="server"></asp:GridView>
        </div>

        <div class="col-md-4">
            <h2>Employees</h2>
            <asp:DropDownList ID="projdepsddl" runat="server" AutoPostBack="True" OnSelectedIndexChanged="projdepsddl_SelectedIndexChanged" ></asp:DropDownList>
            <asp:GridView ID="emps" runat="server"></asp:GridView>
        </div>

    </div>
    
</asp:Content>
