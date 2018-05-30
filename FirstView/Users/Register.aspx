<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="FirstView.Users.Register" %>

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
            width: 500px;
        }
    </style>
    <script type="text/javascript">
        function openModal() {
            $('#myModal').modal('show');
        }
        function openModalUsernameExists() {
            $('#myModalUsernameExists').modal('show');
        }
        $(function () {
            var bootstrapTooltip = $.fn.tooltip.noConflict();

            // give $().bootstrapTooltip the Bootstrap functionality
            $.fn.bootstrapTooltip = bootstrapTooltip

            // now activate tooltip plugin from jQuery ui

            $("input").tooltip({
                disabled: true,
                position: {
                    my: 'right bottom-5',
                    at: 'right top',
                    using: function (position, feedback) {
                        $(this).css(position);
                        $(this).addClass(feedback.vertical);
                    }
                }
            }).on("focusin", function () {
                $(this)
                    .tooltip("enable")
                    .tooltip("open");
            }).on("focusout", function () {
                $(this)
                    .tooltip("close")
                    .tooltip("disable");
            });
        });
    </script>
    <div class="container">
        <div class="page-header">
            <h4>Registration</h4>
            <p>Please complete the registration details below:</p>
            <p class="lead">
                <asp:Button ID="butRegister" ValidationGroup="reg" class="btn btn-primary" runat="server" Width="80px" Text="Save" OnClick="butRegister_Click" />
            </p>
        </div>
        <div class="row">
            <div class="col-md-2 col-sm-12 col-xs-12 marginTop">
                <asp:Label ID="Label5" runat="server" Text="Name"></asp:Label>
            </div>
            <div class="col-md-4 col-sm-12 col-xs-12 marginTop">
                <asp:TextBox ID="txtName" class="form-control" MaxLength="100" ToolTip="Name is a required field." runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvtxtName" ValidationGroup="reg" ControlToValidate="txtName" CssClass="text-danger" Display="Dynamic" SetFocusOnError="true" runat="server" ErrorMessage="Name is a required field"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2 col-sm-12 col-xs-12 marginTop">
                <asp:Label ID="Label6" runat="server" Text="Surname"></asp:Label>
            </div>
            <div class="col-md-4 col-sm-12 col-xs-12 marginTop">
                <asp:TextBox ID="txtSurname" class="form-control" MaxLength="100" ToolTip="Surname is a required field." runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvtxtSurname" ValidationGroup="reg" ControlToValidate="txtSurname" CssClass="text-danger" Display="Dynamic" SetFocusOnError="true" runat="server" ErrorMessage="Surname is a required field"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2 col-sm-12 col-xs-12 marginTop">
                <asp:Label ID="Label7" runat="server" Text="Email Address"></asp:Label>
            </div>
            <div class="col-md-4 col-sm-12 col-xs-12 marginTop">
                <asp:TextBox ID="txtEmailAddress" class="form-control" MaxLength="100" TextMode="Email" runat="server" ToolTip="Email Address is a required field."></asp:TextBox>
                <asp:RegularExpressionValidator ID="regexEmailValid" ValidationGroup="reg" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" CssClass="text-danger" Display="Dynamic" ControlToValidate="txtEmailAddress" ErrorMessage="Please provide a valid email."></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="rfvtxtEmailAddress" ValidationGroup="reg" ControlToValidate="txtEmailAddress" CssClass="text-danger" Display="Dynamic" SetFocusOnError="true" runat="server" ErrorMessage="Email Address is a required field"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2 col-sm-12 col-xs-12 marginTop">
                <asp:Label ID="Label9" runat="server" Text="Address"></asp:Label>
            </div>
            <div class="col-md-4 col-sm-12 col-xs-12 marginTop">
                <asp:TextBox ID="txtAddress" class="form-control" MaxLength="250" runat="server" TextMode="MultiLine" ToolTip="Address is a required field." Rows="4"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvtxtAddress" ValidationGroup="reg" ControlToValidate="txtAddress" CssClass="text-danger" Display="Dynamic" SetFocusOnError="true" runat="server" ErrorMessage="Address is a required field"></asp:RequiredFieldValidator>
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
                <asp:TextBox ID="txtPostCode" class="form-control" MaxLength="50" runat="server" ToolTip="Postal Code is a required field."></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvtxtPostCode" ValidationGroup="reg" ControlToValidate="txtPostCode" CssClass="text-danger" Display="Dynamic" SetFocusOnError="true" runat="server" ErrorMessage="Postal Code is a required field"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2 col-sm-12 col-xs-12 marginTop">
                <asp:Label ID="Label12" runat="server" Text="Telephone"></asp:Label>
            </div>
            <div class="col-md-4 marginTop">
                <asp:TextBox ID="txtTelephone" class="form-control" MaxLength="50" runat="server" ToolTip="Between Telephone and Mobile number  at least one should be specified."></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2 col-sm-12 col-xs-12 marginTop">
                <asp:Label ID="Label13" runat="server" Text="Mobile"></asp:Label>
            </div>
            <div class="col-md-4 col-sm-12 col-xs-12 marginTop">
                <asp:TextBox ID="txtMobile" class="form-control" MaxLength="50" runat="server" ToolTip="Between Telephone and Mobile number  at least one should be specified."></asp:TextBox>
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
                <asp:TextBox ID="txtUsername" MaxLength="50" class="form-control" runat="server" ToolTip="Username is a required field."></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvtxtUsername" ValidationGroup="reg" ControlToValidate="txtUsername" CssClass="text-danger" Display="Dynamic" SetFocusOnError="true" runat="server" ErrorMessage="Username is a required field"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2 col-sm-12 col-xs-12 marginTop">
                <asp:Label ID="Label2" runat="server" Text="Password"></asp:Label>
            </div>
            <div class="col-md-4 col-sm-12 col-xs-12 marginTop">
                <asp:TextBox ID="txtPassword" class="form-control" TextMode="Password" MaxLength="50" Width="95%" runat="server" ToolTip="Password is a required field."></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvtxtPassword" ValidationGroup="reg" ControlToValidate="txtPassword" CssClass="text-danger" Display="Dynamic" SetFocusOnError="true" runat="server" ErrorMessage="Password is a required field"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator Display="Dynamic" ValidationGroup="reg" ControlToValidate="txtPassword" ID="revPassword" ValidationExpression="^[\s\S]{6,}$" runat="server" ErrorMessage="Minimum 6 characters required."></asp:RegularExpressionValidator>
                <ajaxToolkit:PasswordStrength ID="PasswordStrength1" runat="server" DisplayPosition="RightSide" StrengthIndicatorType="BarIndicator" TargetControlID="txtPassword" PrefixText="Stength:" Enabled="true" RequiresUpperAndLowerCaseCharacters="true" MinimumLowerCaseCharacters="1" MinimumUpperCaseCharacters="1" MinimumNumericCharacters="1" PreferredPasswordLength="6" TextStrengthDescriptions="VeryPoor; Weak; Average; Strong; Excellent" StrengthStyles="VeryPoor; Weak; Average; Excellent; Strong;" CalculationWeightings="25;25;15;35" BarBorderCssClass="border" HelpStatusLabelID="lblPass" />
                <asp:Label ID="lblPass" runat="server"></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2 col-sm-12 col-xs-12 marginTop">
                <asp:Label ID="Label8" runat="server" Text="Confirm Password"></asp:Label>
            </div>
            <div class="col-md-4 col-sm-12 col-xs-12 marginTop">
                <asp:TextBox ID="txtConfirmPassword" class="form-control" TextMode="Password" MaxLength="50" runat="server" ToolTip="Confirm Password is a required field and should be same as password."></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvtxtConfirmPassword" ValidationGroup="reg" ControlToValidate="txtConfirmPassword" CssClass="text-danger" Display="Dynamic" SetFocusOnError="true" runat="server" ErrorMessage="Confirm Password is a required field"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="cvPass" ControlToValidate="txtConfirmPassword" ControlToCompare="txtPassword" CssClass="text-danger" SetFocusOnError="true" Display="Dynamic" runat="server" ErrorMessage="The Passwords entered do not match."></asp:CompareValidator>
            </div>
        </div>

        <div class="row">
            <div class="col-md-2 col-sm-12 col-xs-12 marginTop">
                <asp:Button ID="Button1" ValidationGroup="reg" class="btn btn-primary" runat="server" Width="80px" Text="Save" OnClick="butRegister_Click" />
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
