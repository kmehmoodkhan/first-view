<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="FirstView.Users.ResetPassword" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
    .VeryPoor
    {
        background-color:red;
        }

        .Weak
    {
        background-color:orange;
        }

        .Average
        {
        background-color:  #A52A2A
        }
        .Excellent
        {
        background-color:yellow;
        }
        .Strong
        {
        background-color:green;
        }
        .border
        {
        border:medium #000000;
        width:200px;                
        }
    </style>
 <script type="text/javascript">
    function openModalRecovery() {
        $('#myModalRecovery').modal('show');
    }
</script>
    <div class="container">
        <div class="page-header">
            <h4>Reset Password</h4>
            <p>Password Recovery. Please complete your details below.</p>
            <p class="lead"> 
                <a href="Login.aspx" class="btn btn-default" style="width:80px" role="button">Back</a>    
                <asp:Button ID="butSubmit" class="btn btn-primary" runat="server" Width="80px" Text="Submit" OnClick="butSubmit_Click" />                
            </p>
        </div>
        <br />

        <div class="row">
            <div class="col-md-2">
                <asp:Label ID="Label1" runat="server" Text="Temporary Password"></asp:Label>
            </div>
            <div class="col-md-4">
                <asp:TextBox ID="txtTempPassword" class="form-control" TextMode="Password"  MaxLength="50"  Width="420px" runat="server" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtTempPassword" CssClass="text-danger" SetFocusOnError="true" Display="Dynamic" ErrorMessage="Temp Password is a required field"></asp:RequiredFieldValidator>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-md-2">
                <asp:Label ID="Label2" runat="server" Text="New Password"></asp:Label>
            </div>
            <div class="col-md-4">
                <asp:TextBox ID="txtNewPassword" class="form-control" TextMode="Password"  MaxLength="50"  Width="420px" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="text-danger" ControlToValidate="txtNewPassword" SetFocusOnError="true" Display="Dynamic" ErrorMessage="New Password is a required field"></asp:RequiredFieldValidator>
                <ajaxToolkit:PasswordStrength ID="PasswordStrength1" runat="server" DisplayPosition="RightSide" StrengthIndicatorType="BarIndicator" TargetControlID="txtNewPassword" PrefixText="Stength:" Enabled="true"  RequiresUpperAndLowerCaseCharacters="true" MinimumLowerCaseCharacters="1" MinimumUpperCaseCharacters="1" MinimumNumericCharacters="1" PreferredPasswordLength="6" TextStrengthDescriptions="VeryPoor; Weak; Average; Strong; Excellent"  StrengthStyles="VeryPoor; Weak; Average; Excellent; Strong;"  CalculationWeightings="25;25;15;35" BarBorderCssClass="border" HelpStatusLabelID="lblPass" />
                <asp:Label ID="lblPass" runat="server" ></asp:Label>
            </div>
        </div> 
        <br />
        <div class="row">
            <div class="col-md-2">
                <asp:Label ID="Label3" runat="server" Text="Confirm New Password"></asp:Label>
            </div>
            <div class="col-md-4">
                <asp:TextBox ID="txtConfirmNewPassword" class="form-control" TextMode="Password"  MaxLength="50"  Width="420px" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="text-danger" runat="server" ControlToValidate="txtConfirmNewPassword" SetFocusOnError="true" Display="Dynamic" ErrorMessage="Confirm New Password is a required field"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="cvPass" ControlToValidate="txtNewPassword" ControlToCompare="txtConfirmNewPassword" CssClass="text-danger" SetFocusOnError="true" Display="Dynamic" runat="server" ErrorMessage="The Passwords entered do not match."></asp:CompareValidator>
            </div>
        </div> 
        <br />
    </div> 
    <div id="myModalRecovery" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;</button>
                    <h4 class="modal-title">
                        Error</h4>
                </div>
                <div class="modal-body">
                    <p class="text-danger"><span class="glyphicon glyphicon-remove"></span>&nbsp;The Password Recovery information that you have entered is incorrect or the Temporary password entered has expired (Remember that your Temporary password is valid for 15 minutes only). Please try again.</p>                    
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        Close</button>
                </div>
            </div>
        </div>
    </div>      
</asp:Content>
