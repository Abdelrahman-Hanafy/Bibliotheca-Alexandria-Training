<%@ Page Title="Projects" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Projects.aspx.cs" Inherits="DemoKda.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
     <div class="jumbotron">
        <h1>Projects</h1>
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>List</h2>
            <br><asp:Label ID="Delete1" runat="server" Text=""></asp:Label>
            <asp:GridView ID="projs" runat="server" OnRowDeleting="Projs_RowDeleting" OnRowEditing="Projs_OnRowEditing" 
                OnRowCancelingEdit="Projs_OnRowCancelingEdit" OnRowUpdating="Projs_OnRowUpdating">
                <Columns>
                    <asp:CommandField ShowEditButton="true" />
                    <asp:CommandField ShowDeleteButton="true" /> 
                </Columns>
            </asp:GridView>
        </div> 
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>Details</h2>
            <asp:DropDownList ID="projsddl" runat="server" AutoPostBack="True" OnSelectedIndexChanged="Projsddl_SelectedIndexChanged" ></asp:DropDownList>
            <br><asp:Label ID="Delete" runat="server" Text=""></asp:Label>
            <asp:GridView ID="Dets" runat="server" OnRowDeleting="Deps_RowDeleting">
                <Columns>
                    <asp:CommandField ShowDeleteButton="true" /> 
                </Columns>
            </asp:GridView>
        </div>

        <div class="col-md-4">
            <h2>Employees</h2>
            <asp:DropDownList ID="projdepsddl" runat="server" AutoPostBack="True" OnSelectedIndexChanged="projdepsddl_SelectedIndexChanged" ></asp:DropDownList>
            <asp:GridView ID="emps" runat="server" OnRowDeleting="Emps_RowDeleting">
                <Columns>
                    <asp:CommandField ShowDeleteButton="true" /> 
                </Columns>
            </asp:GridView>
        </div>

    </div>
    
</asp:Content>
