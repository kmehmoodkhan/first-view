<%@ Page Title="Artist Add" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Add.aspx.cs" Inherits="FirstView.Admin.Artists.Add" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
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
        function openModalUsernameExists() {
            $('#myModalUsernameExists').modal('show');
        }
    </script>
    <div class="container">
        <div class="page-header">
            <h4>Administration - Artist - Add</h4>
            <p class="lead">
                <a href="../Menu.aspx" class="btn btn-default" style="width: 80px" role="button">Back</a>
                <asp:Button ID="butRegister" class="btn btn-primary" runat="server" Width="80px" Text="Add" OnClick="butRegister_Click" />

            </p>
        </div>
        <div class="row">
            <div class="col-md-2 col-sm-12 col-xs-12 marginTop">
                <asp:Label ID="Label5" runat="server" Text="Name"></asp:Label>
            </div>
            <div class="col-md-4 col-sm-12 col-xs-12 marginTop">
                <asp:TextBox ID="txtName" class="form-control" MaxLength="100" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtName" CssClass="text-danger" Display="Dynamic" SetFocusOnError="true" runat="server" ErrorMessage="Name is a required field"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2 col-sm-12 col-xs-12 marginTop">
                <asp:Label ID="Label6" runat="server" Text="Surname"></asp:Label>
            </div>
            <div class="col-md-4 col-sm-12 col-xs-12 marginTop">
                <asp:TextBox ID="txtSurname" class="form-control" MaxLength="100" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtSurname" CssClass="text-danger" Display="Dynamic" SetFocusOnError="true" runat="server" ErrorMessage="Surname is a required field"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2 col-sm-12 col-xs-12 marginTop">
                <asp:Label ID="Label7" runat="server" Text="Email Address"></asp:Label>
            </div>
            <div class="col-md-4 col-sm-12 col-xs-12 marginTop">
                <asp:TextBox ID="txtEmailAddress" class="form-control" MaxLength="100" TextMode="Email" runat="server"></asp:TextBox>
                <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" CssClass="text-danger" Display="Dynamic" ControlToValidate="txtEmailAddress" ErrorMessage="Please provide a valid email."></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtEmailAddress" CssClass="text-danger" Display="Dynamic" SetFocusOnError="true" runat="server" ErrorMessage="Email Address is a required field"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2 col-sm-12 col-xs-12 marginTop">
                <asp:Label ID="Label9" runat="server" Text="Address"></asp:Label>
            </div>
            <div class="col-md-4 col-sm-12 col-xs-12 marginTop">
                <asp:TextBox ID="txtAddress" class="form-control" MaxLength="250" runat="server" TextMode="MultiLine" Rows="4"></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2 col-sm-12 col-xs-12 marginTop">
                <asp:Label ID="Label10" runat="server" Text="Town"></asp:Label>
            </div>
            <div class="col-md-4 col-sm-12 col-xs-12 marginTop">
                <asp:TextBox ID="txtTown" class="form-control" MaxLength="50" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2 col-sm-12 col-xs-12 marginTop">
                <asp:Label ID="Label11" runat="server" Text="Post Code"></asp:Label>
            </div>
            <div class="col-md-4 col-sm-12 col-xs-12 marginTop">
                <asp:TextBox ID="txtPostCode" class="form-control" MaxLength="50" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2 col-sm-12 col-xs-12 marginTop">
                <asp:Label ID="Label12" runat="server" Text="Telephone"></asp:Label>
            </div>
            <div class="col-md-4 marginTop">
                <asp:TextBox ID="txtTelephone" class="form-control" MaxLength="50" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2 col-sm-12 col-xs-12 marginTop">
                <asp:Label ID="Label13" runat="server" Text="Mobile"></asp:Label>
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
                <asp:Label ID="Label1" runat="server" Text="Username"></asp:Label>
            </div>
            <div class="col-md-4 col-sm-12 col-xs-12 marginTop">
                <asp:TextBox ID="txtUsername" MaxLength="50" class="form-control" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtUsername" CssClass="text-danger" Display="Dynamic" SetFocusOnError="true" runat="server" ErrorMessage="Username is a required field"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2 col-sm-12 col-xs-12 marginTop">
                <asp:Label ID="Label2" runat="server" Text="Password"></asp:Label>
            </div>
            <div class="col-md-4 col-sm-12 col-xs-12 marginTop">
                <asp:TextBox ID="txtPassword" class="form-control" TextMode="Password" MaxLength="50" Width="95%" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtPassword" CssClass="text-danger" Display="Dynamic" SetFocusOnError="true" runat="server" ErrorMessage="Password is a required field"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtPassword" ID="rfvPassword" ValidationExpression="^[\s\S]{6,}$" runat="server" ErrorMessage="Minimum 6 characters required."></asp:RegularExpressionValidator>
                <ajaxToolkit:PasswordStrength ID="PasswordStrength1" runat="server" DisplayPosition="RightSide" StrengthIndicatorType="BarIndicator" TargetControlID="txtPassword" PrefixText="Stength:" Enabled="true" RequiresUpperAndLowerCaseCharacters="true" MinimumLowerCaseCharacters="1" MinimumUpperCaseCharacters="1" MinimumNumericCharacters="1" PreferredPasswordLength="6" TextStrengthDescriptions="VeryPoor; Weak; Average; Strong; Excellent" StrengthStyles="VeryPoor; Weak; Average; Excellent; Strong;" CalculationWeightings="25;25;15;35" BarBorderCssClass="border" HelpStatusLabelID="lblPass" />
                <asp:Label ID="lblPass" runat="server"></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2 col-sm-12 col-xs-12 marginTop">
                <asp:Label ID="Label8" runat="server" Text="Confirm Password"></asp:Label>
            </div>
            <div class="col-md-4 col-sm-12 col-xs-12 marginTop">
                <asp:TextBox ID="txtConfirmPassword" class="form-control" TextMode="Password" MaxLength="50" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="txtConfirmPassword" CssClass="text-danger" Display="Dynamic" SetFocusOnError="true" runat="server" ErrorMessage="Confirm Password is a required field"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="cvPass" ControlToValidate="txtConfirmPassword" ControlToCompare="txtPassword" CssClass="text-danger" SetFocusOnError="true" Display="Dynamic" runat="server" ErrorMessage="The Passwords entered do not match."></asp:CompareValidator>
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
                    <p class="text-success">The data has been saved successfully.</p>
                    <span class="glyphicon glyphicon-ok"></span>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        Close</button>
                </div>
            </div>
        </div>
    </div>
    <div id="myModalUsernameExists" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;</button>
                    <h4 class="modal-title">Error</h4>
                </div>
                <div class="modal-body">
                    <p class="text-danger"><span class="glyphicon glyphicon-remove"></span>&nbsp;The Username entered already exists, please enter another.</p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        Close</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
