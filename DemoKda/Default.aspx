<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="DemoKda._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Bibliotheca Alexandria</h1>
        <p><a href="https://www.bibalex.org/en/default" target="_blank" class="btn btn-primary btn-lg">Learn more &raquo;</a></p>
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>Sectors</h2>
            <asp:DropDownList ID="secddl" runat="server" AutoPostBack="True" OnSelectedIndexChanged="secddl_SelectedIndexChanged"></asp:DropDownList> <br>
            <asp:GridView ID="secs" runat="server" OnRowEditing="secs_OnRowEditing" 
                OnRowCancelingEdit="secs_OnRowCancelingEdit" OnRowUpdating="secs_OnRowUpdating" EmptyDataText="No records has been added.">
                <Columns>
                    <asp:CommandField ShowEditButton="true" />
                </Columns>
            </asp:GridView>
        </div>

        <div class="col-md-4">
            <h2>Departemt</h2>

              <asp:DropDownList ID="depddl" runat="server" AutoPostBack="True" OnSelectedIndexChanged="depddl_SelectedIndexChanged" ></asp:DropDownList>
              <br><asp:Label ID="Delete" runat="server" Text=""></asp:Label>
              <asp:GridView ID="Deps" runat="server" OnRowDeleting="Deps_RowDeleting" OnRowEditing="Deps_OnRowEditing" 
                OnRowCancelingEdit="Deps_OnRowCancelingEdit" OnRowUpdating="Deps_OnRowUpdating" EmptyDataText="No records has been added.">
                  <Columns>
                    <asp:CommandField ShowEditButton="true" />
                    <asp:CommandField ShowDeleteButton="true" /> 
                </Columns>
              </asp:GridView>


        </div>
        
        <div class="col-md-4">
            <h2>Employee</h2>
            <asp:GridView ID="Emps" runat="server" OnRowDeleting="Emps_RowDeleting" OnRowEditing="Emps_OnRowEditing" RowDataBound="Emps_RowDataBound"
                OnRowCancelingEdit="Emps_OnRowCancelingEdit" OnRowUpdating="Emps_OnRowUpdating" EmptyDataText="No records has been added.">
                <Columns>
                  
                    <asp:CommandField ShowEditButton="true" />
                    <asp:CommandField ShowDeleteButton="true" /> 
                    <asp:TemplateField HeaderText="Attended At">
                                 <EditItemTemplate>
                                     <asp:DropDownList ID="ddlEditLocation" runat="server"> </asp:DropDownList>     
                                 </EditItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>

    </div>


</asp:Content>
