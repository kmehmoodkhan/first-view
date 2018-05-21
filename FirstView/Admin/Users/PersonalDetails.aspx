<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PersonalDetails.aspx.cs" Inherits="FirstView.Admin.Users.PersonalDetails" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        .VeryPoor {
            background-color: red;
        }

        .Weak {
            background-color: orange;
        }

        .Average {
            background-color: #A52A2A
        }

        .Excellent {
            background-color: yellow;
        }

        .Strong {
            background-color: green;
        }

        .border {
            border: medium #000000;
            width: 200px;
        }
    </style>
    <script type="text/javascript">
        function openModal() {
            $('#myModal').modal('show');
        }
    </script>
    <div class="container">
        <div class="page-header">
            <h4>Update Personal Details</h4>
            <p class="lead">
                <asp:Button ID="butSubmit" ValidationGroup="Submit" class="btn btn-primary" runat="server" Width="80px" Text="Save" OnClick="butSubmit_Click" />
                <asp:Button ID="btnReset" class="btn btn-primary" runat="server" Width="80px" Text="Reset" OnClick="btnReset_Click" />
                <asp:Button ID="btnBack" class="btn btn-primary" runat="server" Width="80px" Text="Back" OnClick="btnBack_Click" />
            </p>
        </div>
        <div class="row">
            <div class="col-md-2 col-sm-12 col-xs-12 marginTop">
                Name
            </div>
            <div class="col-md-4 col-sm-12 col-xs-12 marginTop">
                <asp:TextBox ID="txtName" class="form-control" MaxLength="100" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ValidationGroup="Submit" ID="RequiredFieldValidator1" ControlToValidate="txtName" CssClass="text-danger" Display="Dynamic" SetFocusOnError="true" runat="server" ErrorMessage="Name is a required field"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2 col-sm-12 col-xs-12 marginTop">
                Surname
            </div>
            <div class="col-md-4 col-sm-12 col-xs-12 marginTop">
                <asp:TextBox ID="txtSurname" class="form-control" MaxLength="100" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ValidationGroup="Submit" ID="RequiredFieldValidator2" ControlToValidate="txtSurname" CssClass="text-danger" Display="Dynamic" SetFocusOnError="true" runat="server" ErrorMessage="Surname is a required field"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2 col-sm-12 col-xs-12 marginTop">
                Email Address
            </div>
            <div class="col-md-4 col-sm-12 col-xs-12 marginTop">
                <asp:TextBox ID="txtEmailAddress" class="form-control" MaxLength="100" TextMode="Email" runat="server"></asp:TextBox>
                <asp:RegularExpressionValidator ID="regexEmailValid" ValidationGroup="Submit" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" CssClass="text-danger" Display="Dynamic" ControlToValidate="txtEmailAddress" ErrorMessage="Please provide a valid email."></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ValidationGroup="Submit" ID="RequiredFieldValidator3" ControlToValidate="txtEmailAddress" CssClass="text-danger" Display="Dynamic" SetFocusOnError="true" runat="server" ErrorMessage="Email Address is a required field"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2 col-sm-12 col-xs-12 marginTop">
                Address
            </div>
            <div class="col-md-4 col-sm-12 col-xs-12 marginTop">
                <asp:TextBox ID="txtAddress" class="form-control" MaxLength="250" runat="server" TextMode="MultiLine" Rows="4"></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2 col-sm-12 col-xs-12 marginTop">
                Town
            </div>
            <div class="col-md-4 col-sm-12 col-xs-12 marginTop">
                <asp:TextBox ID="txtTown" class="form-control" MaxLength="50" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2 col-sm-12 col-xs-12 marginTop">
                Post Code
            </div>
            <div class="col-md-4 col-sm-12 col-xs-12 marginTop">
                <asp:TextBox ID="txtPostCode" class="form-control" MaxLength="50" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2 col-sm-12 col-xs-12 marginTop">
                Telephone
            </div>
            <div class="col-md-4 marginTop">
                <asp:TextBox ID="txtTelephone" class="form-control" MaxLength="50" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2 col-sm-12 col-xs-12 marginTop">
                Mobile
            </div>
            <div class="col-md-4 col-sm-12 col-xs-12 marginTop">
                <asp:TextBox ID="txtMobile" class="form-control" MaxLength="50" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2 col-sm-12 col-xs-12 marginTop">
                Bank Sort Code
            </div>
            <div class="col-md-4 marginTop">
                <asp:TextBox ID="txtBankSortCode" class="form-control" MaxLength="50" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2 col-sm-12 col-xs-12 marginTop">
                Bank Account Number
            </div>
            <div class="col-md-4 col-sm-12 col-xs-12 marginTop">
                <asp:TextBox ID="txtBankAccountNumber" class="form-control" MaxLength="50" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2 col-sm-12 col-xs-12 marginTop">
                Username
            </div>
            <div class="col-md-4 col-sm-12 col-xs-12 marginTop">
                <asp:TextBox ID="txtUsername" MaxLength="50" class="form-control" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtUsername" CssClass="text-danger" Display="Dynamic" SetFocusOnError="true" runat="server" ErrorMessage="Username is a required field"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2 col-sm-12 col-xs-12 marginTop">
                Password
            </div>
            <div class="col-md-4 col-sm-12 col-xs-12 marginTop">
                <asp:TextBox ID="txtPassword" class="form-control" TextMode="Password" MaxLength="50" Width="95%" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ValidationGroup="Submit" ID="RequiredFieldValidator5" ControlToValidate="txtPassword" CssClass="text-danger" Display="Dynamic" SetFocusOnError="true" runat="server" ErrorMessage="Password is a required field"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ValidationGroup="Submit" Display="Dynamic" ControlToValidate="txtPassword" ID="rfvPassword" ValidationExpression="^[\s\S]{6,}$" runat="server" ErrorMessage="Minimum 6 characters required."></asp:RegularExpressionValidator>
                <ajaxToolkit:PasswordStrength ID="PasswordStrength1" runat="server" DisplayPosition="RightSide" StrengthIndicatorType="BarIndicator" TargetControlID="txtPassword" PrefixText="Stength:" Enabled="true" RequiresUpperAndLowerCaseCharacters="true" MinimumLowerCaseCharacters="1" MinimumUpperCaseCharacters="1" MinimumNumericCharacters="1" PreferredPasswordLength="6" TextStrengthDescriptions="VeryPoor; Weak; Average; Strong; Excellent" StrengthStyles="VeryPoor; Weak; Average; Excellent; Strong;" CalculationWeightings="25;25;15;35" BarBorderCssClass="border" HelpStatusLabelID="lblPass" />
                <asp:Label ID="lblPass" runat="server"></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2 col-sm-12 col-xs-12 marginTop">
                Confirm Password
            </div>
            <div class="col-md-4 col-sm-12 col-xs-12 marginTop">
                <asp:TextBox ID="txtConfirmPassword" class="form-control" TextMode="Password" MaxLength="50" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ValidationGroup="Submit" ID="RequiredFieldValidator6" ControlToValidate="txtConfirmPassword" CssClass="text-danger" Display="Dynamic" SetFocusOnError="true" runat="server" ErrorMessage="Confirm Password is a required field"></asp:RequiredFieldValidator>
                <asp:CompareValidator ValidationGroup="Submit" ID="cvPass" ControlToValidate="txtConfirmPassword" ControlToCompare="txtPassword" CssClass="text-danger" SetFocusOnError="true" Display="Dynamic" runat="server" ErrorMessage="The Passwords entered do not match."></asp:CompareValidator>
            </div>
        </div>
    </div>
    <div id="myModal" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;</button>
                    <h4 class="modal-title">Confirmation</h4>
                </div>
                <div class="modal-body">
                    <p class="text-success">
                        <asp:Label ID="lblMessage" runat="server"></asp:Label>
                    </p>
                    <span class="glyphicon glyphicon-ok"></span>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        Close</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
