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
        <div class="col-md-4">
            <h3>Emolyess</h3>
            
            <asp:Label ID="Label4" runat="server" Text="Employee Name"></asp:Label> <br>
            <asp:TextBox ID="empname" runat="server"></asp:TextBox> <br>
            <asp:Label ID="Label7" runat="server" Text="Employee Salary"></asp:Label> <br>
            <asp:TextBox ID="empsalary" runat="server"></asp:TextBox> <br>
            <asp:Label ID="Label8" runat="server" Text="Employee Department"></asp:Label> <br>
            <asp:DropDownList ID="depddl" runat="server" AutoPostBack="True" ></asp:DropDownList> <br>
            <asp:Label ID="Label9" runat="server" Text="Employee BirthDate"></asp:Label> <br>
            <asp:TextBox ID="empbirth" runat="server" PlaceHolder="MM/DD/YYYY"></asp:TextBox> <br>
            <asp:Button ID="empbtn" runat="server" Text="Add" OnClick="empbtn_Click"  /> <br>
        </div>

    </div>

    <h2>Insert Projects Details</h2>
        <div class="row">
        
        <div class="col-md-4">
            <h3>Projects</h3>
            
            <asp:Label ID="Label5" runat="server" Text="Project Name"></asp:Label> <br>
            <asp:TextBox ID="projname" runat="server"></asp:TextBox> <br>
            <asp:Label ID="Label6" runat="server" Text="Project Start Date"></asp:Label> <br>
            <asp:TextBox ID="projst" runat="server" PlaceHolder="MM/DD/YYYY"></asp:TextBox> <br>
            <asp:Label ID="Label10" runat="server" Text="Project End Date"></asp:Label> <br>
            <asp:TextBox ID="projend" runat="server" PlaceHolder="MM/DD/YYYY"></asp:TextBox> <br>
            <asp:Button ID="projbtn" runat="server" Text="Add" OnClick="projbtn_Click" /> <br>
        </div>

        <div class="col-md-4">
            <h3>Add Projects to Department</h3>
            
            <asp:Label ID="Label11" runat="server" Text="Chosse project"></asp:Label> <br>
            <asp:DropDownList ID="projddl" runat="server" AutoPostBack="True" ></asp:DropDownList> <br>
            <asp:Label ID="Label12" runat="server" Text="Chosse Department"></asp:Label> <br>
            <asp:DropDownList ID="projdepddl" runat="server" AutoPostBack="True" ></asp:DropDownList> <br>
            <asp:Button ID="projDep" runat="server" Text="Add" OnClick="projDep_Click"  /> <br>
        </div>

        <div class="col-md-4">
            <h3>Assign Employess to Project</h3>
            
            <asp:Label ID="Label13" runat="server" Text="Chosse project"></asp:Label> <br>
            <asp:DropDownList ID="projddl2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="projddl2_SelectedIndexChanged" ></asp:DropDownList> <br>
            <asp:Label ID="Label14" runat="server" Text="Chosse Department"></asp:Label> <br>
            <asp:DropDownList ID="projdepddl2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="projdepddl2_SelectedIndexChanged" ></asp:DropDownList> <br>
            <asp:CheckBoxList ID="Empls" runat="server"></asp:CheckBoxList>

            <asp:Button ID="Button1" runat="server" Text="Add"  /> <br>

        </div>
    </div>
</asp:Content>
