<%@ Page Title="Contact Us" Language="C#" MasterPageFile="~/PublicMaster.Master" AutoEventWireup="true" CodeBehind="ContactUs.aspx.cs" Inherits="FirstView.ContactUs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="page-header">
            <h4>Contact Us</h4>
            <p class="lead">
                <asp:Button ID="butSubmit" class="btn btn-primary" runat="server" Width="80px" Text="Submit" OnClick="butSubmit_Click" />        
            </p>
        </div>
        <br />
        <div class="row">
            <div class="col-md-12">
                For further information
                <br />
                Please complete the form and we will be in touch as soon as possible 
                <br />                
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-md-2">
                <asp:Label ID="Label1" runat="server" Text="Your Name"></asp:Label>
            </div>
            <div class="col-md-4">
                <asp:TextBox ID="txtName" class="form-control" Width="420px" runat="server"  MaxLength="100"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvName" runat="server" ErrorMessage="Name is a required field" CssClass="text-danger" ControlToValidate="txtName" Display="Dynamic"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2">
                <asp:Label ID="Label8" runat="server" Text="Address"></asp:Label>
            </div>
            <div class="col-md-4">
                <asp:TextBox ID="txtAddress" runat="server" class="form-control" Width="400px" TextMode="MultiLine" Rows="4" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvAddress"  ControlToValidate="txtAddress" runat="server" CssClass="text-danger" ErrorMessage="Address is a required field" Display="Dynamic"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2">
                <asp:Label ID="Label2" runat="server" Text="Town"></asp:Label>
            </div>
            <div class="col-md-4">
                <asp:TextBox ID="txtTown" class="form-control" Width="420px" runat="server"  MaxLength="100" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvTown"  ControlToValidate="txtTown" runat="server" ErrorMessage="Town is a required field" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2">
                <asp:Label ID="Label3" runat="server" Text="Post Code"></asp:Label>
            </div>
            <div class="col-md-4">
                <asp:TextBox ID="txtPostCode" class="form-control" Width="420px" runat="server"  MaxLength="100"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvPostCode"  ControlToValidate="txtPostCode" runat="server" ErrorMessage="Post Code is a required field" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2">
                <asp:Label ID="Label4" runat="server" Text="Telephone"></asp:Label>
            </div>
            <div class="col-md-4">
                <asp:TextBox ID="txtTel" class="form-control" Width="420px" runat="server"  MaxLength="100" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvTel"  ControlToValidate="txtTel" runat="server" CssClass="text-danger" ErrorMessage="Telephone is a required field" Display="Dynamic"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2">
                <asp:Label ID="Label5" runat="server" Text="Email Address"></asp:Label>
            </div>
            <div class="col-md-4">
                <asp:TextBox ID="txtEmailAddress" class="form-control" Width="420px" runat="server" TextMode="Email"  MaxLength="100"></asp:TextBox>
                <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" CssClass="text-danger" Display="Dynamic" ControlToValidate="txtEmailAddress" ErrorMessage="Please provide a valid email."></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="rfvEmailAddress"  ControlToValidate="txtEmailAddress" runat="server" CssClass="text-danger" ErrorMessage="Email Address is a required field" Display="Dynamic"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2">
                <asp:Label ID="Label6" runat="server" Text="Message"></asp:Label>
            </div>
            <div class="col-md-4">
                <asp:TextBox ID="txtMessage" runat="server" class="form-control" Width="400px" TextMode="MultiLine" Rows="4"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvMessage"  ControlToValidate="txtMessage" runat="server" CssClass="text-danger" ErrorMessage="Message is a required field" Display="Dynamic"></asp:RequiredFieldValidator>
            </div>
        </div>
    </div>
</asp:Content>
