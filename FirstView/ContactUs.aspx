<%@ Page Title="Contact Us" Language="C#" MasterPageFile="~/PublicMaster.Master" AutoEventWireup="true" CodeBehind="ContactUs.aspx.cs" Inherits="FirstView.ContactUs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        .padding-bottom {
            margin-bottom: 4px;
            margin-top: 4px;
        }

        .info {
            color: red;
            padding-right: 3px;
            font-size: 18px;
            vertical-align: middle
        }

        .padding-8-top {
            padding-top: 8px;
        }

        .form-control {
            display: inline !important;
            margin-right: 20px !important;
        }

        .container .row div input[type="text"],
        input[type="password"],
        input[type="email"],
        input[type="tel"],
        input[type="select"],
        span, select {
            margin-bottom: 4px;
            margin-top: 4px;
        }

        @media (min-width: 992px) {
            .col-md-2 {
                width: 10.666667%;
            }
        }
    </style>
    <div class="container">
        <div class="page-header">
            <h4 style="font-size: 25px; margin-bottom: 0px; margin-top: 0px;">Contact Us</h4>
        </div>
        <div class="row">
            <div class="col-md-12">
                <span class="info">*</span>Please complete the form and we will be in touch as soon as possible               
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-md-2 padding-8-top">
                <asp:Label ID="Label1" CssClass="padding-bottom" runat="server" Text="Your Name"></asp:Label>
            </div>
            <div class="col-md-8">
                <asp:TextBox ID="txtName" class="form-control padding-bottom" Width="420px" runat="server" MaxLength="100"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvName" runat="server" ErrorMessage="Name is a required field" CssClass="text-danger" ControlToValidate="txtName" Display="Dynamic"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2 padding-8-top">
                <asp:Label ID="Label8" CssClass="padding-bottom" runat="server" Text="Address"></asp:Label>
            </div>
            <div class="col-md-8">
                <asp:TextBox ID="txtAddress" runat="server" class="form-control padding-bottom" Width="400px" TextMode="MultiLine" Rows="3"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvAddress" ControlToValidate="txtAddress" runat="server" CssClass="text-danger" ErrorMessage="Address is a required field" Display="Dynamic"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2 padding-8-top">
                <asp:Label ID="Label2" CssClass="padding-bottom" runat="server" Text="Town"></asp:Label>
            </div>
            <div class="col-md-8">
                <asp:TextBox ID="txtTown" class="form-control padding-bottom" Width="420px" runat="server" MaxLength="100"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvTown" ControlToValidate="txtTown" runat="server" ErrorMessage="Town is a required field" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2 padding-8-top">
                <asp:Label ID="Label3" runat="server" Text="Post Code"></asp:Label>
            </div>
            <div class="col-md-8">
                <asp:TextBox ID="txtPostCode" class="form-control padding-bottom" Width="420px" runat="server" MaxLength="100"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvPostCode" ControlToValidate="txtPostCode" runat="server" ErrorMessage="Post Code is a required field" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2 padding-8-top">
                <asp:Label ID="Label4" runat="server" Text="Telephone"></asp:Label>
            </div>
            <div class="col-md-8">
                <asp:TextBox ID="txtTel" class="form-control padding-bottom" Width="420px" runat="server" MaxLength="100"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvTel" ControlToValidate="txtTel" runat="server" CssClass="text-danger" ErrorMessage="Telephone is a required field" Display="Dynamic"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2 padding-8-top">
                <asp:Label ID="Label5" runat="server" Text="Email Address"></asp:Label>
            </div>
            <div class="col-md-8">
                <asp:TextBox ID="txtEmailAddress" class="form-control padding-bottom" Width="420px" runat="server" TextMode="Email" MaxLength="100"></asp:TextBox>
                <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" CssClass="text-danger" Display="Dynamic" ControlToValidate="txtEmailAddress" ErrorMessage="Please provide a valid email."></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="rfvEmailAddress" ControlToValidate="txtEmailAddress" runat="server" CssClass="text-danger" ErrorMessage="Email Address is a required field" Display="Dynamic"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2 padding-8-top">
                <asp:Label ID="Label6" runat="server" Text="Message"></asp:Label>
            </div>
            <div class="col-md-8">
                <asp:TextBox ID="txtMessage" runat="server" class="form-control padding-bottom" Width="400px" TextMode="MultiLine" Rows="3"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvMessage" ControlToValidate="txtMessage" runat="server" CssClass="text-danger" ErrorMessage="Message is a required field" Display="Dynamic"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2">
            </div>
            <div class="col-md-4">
                <p class="lead">
                    <asp:Button ID="butSubmit" class="btn btn-primary padding-bottom" runat="server" Width="80px" Text="Submit" OnClick="butSubmit_Click" />&nbsp;
                    <asp:Button ID="ButtonCancel" class="btn btn-primary padding-bottom" runat="server" Width="80px" Text="Cancel" CausesValidation="false" OnClick="ButtonCancel_Click" />
                </p>
            </div>
        </div>
    </div>
</asp:Content>
